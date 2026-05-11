using System;
using System.Collections.Generic;

// Memento(Знімок)

class EditorMemento
{
    public string Text { get; }
    public string Color { get; }

    public EditorMemento(string text, string color)
    {
        Text = text;
        Color = color;
    }
}

class TextEditor
{
    private string _text = "";
    private string _color = "Black";

    public void TypeText(string text)
    {
        _text += text;
    }

    public void DeleteText()
    {
        _text = "";
    }

    public void ChangeColor(string color)
    {
        _color = color;
    }

    public EditorMemento Save()
    {
        return new EditorMemento(_text, _color);
    }

    public void Restore(EditorMemento memento)
    {
        _text = memento.Text;
        _color = memento.Color;
    }

    public void Show()
    {
        Console.WriteLine($"Text: \"{_text}\", color: {_color}");
    }
}

class EditorHistory
{
    private readonly Stack<EditorMemento> _history = new();

    public void Backup(TextEditor editor)
    {
        _history.Push(editor.Save());
    }

    public void Undo(TextEditor editor)
    {
        if (_history.Count > 0)
        {
            editor.Restore(_history.Pop());
        }
    }
}

class Program
{
    static void Main()
    {
        var editor = new TextEditor();
        var history = new EditorHistory();

        history.Backup(editor);
        editor.TypeText("Hello KPI!");
        editor.Show();

        history.Backup(editor);
        editor.ChangeColor("Blue");
        editor.Show();

        history.Backup(editor);
        editor.DeleteText();
        editor.Show();

        Console.WriteLine("\nCtrl+Z:");
        history.Undo(editor);
        editor.Show();

        Console.WriteLine("Ctrl+Z:");
        history.Undo(editor);
        editor.Show();
    }
}

