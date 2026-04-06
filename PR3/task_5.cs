using System;
using System.Collections.Generic;

public interface IDocument
{
    string GetData();
    string GetDescription();
}

public class BaseDocument : IDocument
{
    private readonly string _content;

    public BaseDocument(string content)
    {
        _content = content;
        Console.WriteLine("Base document created.");
    }

    public string GetData() => _content;

    public string GetDescription() => "Simple Text";
}

public abstract class DocumentDecorator : IDocument
{
    protected readonly IDocument _innerDocument;

    protected DocumentDecorator(IDocument document)
    {
        _innerDocument = document;
    }

    public virtual string GetData() => _innerDocument.GetData();
    public virtual string GetDescription() => _innerDocument.GetDescription();
}

public class EncryptionDecorator : DocumentDecorator
{
    public EncryptionDecorator(IDocument document) : base(document) { }

    public override string GetData()
    {
        return $"Encrypted({base.GetData()})";
    }

    public override string GetDescription() => base.GetDescription() + " + Encryption";
}

public class CompressionDecorator : DocumentDecorator
{
    public CompressionDecorator(IDocument document) : base(document) { }

    public override string GetData()
    {
        return $"Compressed({base.GetData()})";
    }

    public override string GetDescription() => base.GetDescription() + " + Compression";
}

public class WatermarkDecorator : DocumentDecorator
{
    private readonly string _stamp;
    public WatermarkDecorator(IDocument document, string stamp) : base(document) 
    {
        _stamp = stamp;
    }

    public override string GetData()
    {
        return $"{base.GetData()} [STAMP: {_stamp}]";
    }

    public override string GetDescription() => base.GetDescription() + " + Watermark";
}

public class LoggerDecorator : DocumentDecorator
{
    public LoggerDecorator(IDocument document) : base(document) { }

    public override string GetData()
    {
        Console.WriteLine($"Logging: Accessing document data at {DateTime.Now:HH:mm:ss}");
        return base.GetData();
    }

    public override string GetDescription() => base.GetDescription() + " + Logged";
}
class Program
{
    static void Main()
    {
        IDocument myDoc = new BaseDocument("Data about parkomat");

        myDoc = new LoggerDecorator(myDoc);
        myDoc = new WatermarkDecorator(myDoc, "KPI MARK");

        myDoc = new CompressionDecorator(myDoc);
        myDoc = new EncryptionDecorator(myDoc);

        Console.WriteLine("\nFinal Doc:");
        Console.WriteLine($"Description: {myDoc.GetDescription()}");
        Console.WriteLine($"Final Data: {myDoc.GetData()}");
    }
}