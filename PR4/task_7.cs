using System;
using System.Collections.Generic;

// Command

class Document
{
    public string Content { get; private set; } = "";
    public bool IsDeleted { get; private set; }
    public bool IsApproved { get; private set; }

    public void Create(string content)
    {
        Content = content;
        IsDeleted = false;
        Console.WriteLine($"Document created: {Content}");
    }

    public void Edit(string content)
    {
        Content = content;
        Console.WriteLine($"Document changed: {Content}");
    }

    public void Delete()
    {
        IsDeleted = true;
        Console.WriteLine("Document deleted.");
    }

    public void Approve()
    {
        IsApproved = true;
        Console.WriteLine("Document approved.");
    }

    public void Restore(string content, bool isDeleted, bool isApproved)
    {
        Content = content;
        IsDeleted = isDeleted;
        IsApproved = isApproved;
        Console.WriteLine("Canceled action. Document recovered.");
    }

    public void Show()
    {
        Console.WriteLine($"State: content=\"{Content}\", deleted={IsDeleted}, approved={IsApproved}");
    }
}

interface ICommand
{
    void Execute();
    void Undo();
}

abstract class DocumentCommand : ICommand
{
    protected readonly Document Document;
    private string _backupContent = "";
    private bool _backupDeleted;
    private bool _backupApproved;

    protected DocumentCommand(Document document)
    {
        Document = document;
    }

    public void Execute()
    {
        SaveBackup();
        DoExecute();
    }

    public void Undo()
    {
        Document.Restore(_backupContent, _backupDeleted, _backupApproved);
    }

    private void SaveBackup()
    {
        _backupContent = Document.Content;
        _backupDeleted = Document.IsDeleted;
        _backupApproved = Document.IsApproved;
    }

    protected abstract void DoExecute();
}

class CreateDocumentCommand : DocumentCommand
{
    private readonly string _content;

    public CreateDocumentCommand(Document document, string content) : base(document)
    {
        _content = content;
    }

    protected override void DoExecute()
    {
        Document.Create(_content);
    }
}

class EditDocumentCommand : DocumentCommand
{
    private readonly string _content;

    public EditDocumentCommand(Document document, string content) : base(document)
    {
        _content = content;
    }

    protected override void DoExecute()
    {
        Document.Edit(_content);
    }
}

class DeleteDocumentCommand : DocumentCommand
{
    public DeleteDocumentCommand(Document document) : base(document)
    {
    }

    protected override void DoExecute()
    {
        Document.Delete();
    }
}

class ApproveDocumentCommand : DocumentCommand
{
    public ApproveDocumentCommand(Document document) : base(document)
    {
    }

    protected override void DoExecute()
    {
        Document.Approve();
    }
}

class CommandManager
{
    private readonly Stack<ICommand> _history = new();

    public void Run(ICommand command)
    {
        Console.WriteLine($"Log: command doing {command.GetType().Name}");
        command.Execute();
        _history.Push(command);
    }

    public void UndoLast()
    {
        if (_history.Count > 0)
        {
            _history.Pop().Undo();
        }
    }
}

class Program
{
    static void Main()
    {
        var document = new Document();
        var manager = new CommandManager();

        manager.Run(new CreateDocumentCommand(document, "Rule №1"));
        manager.Run(new EditDocumentCommand(document, "Updated rule №1"));
        manager.Run(new ApproveDocumentCommand(document));
        manager.Run(new DeleteDocumentCommand(document));

        document.Show();

        Console.WriteLine("\nCanceling:");
        manager.UndoLast();
        document.Show();
    }
}

