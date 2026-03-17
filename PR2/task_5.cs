using System;

public interface IReport
{
    void Export(String data);
}

public class PdfReport : IReport
{
    public void Export(String data) => Console.WriteLine($"Exporting '{data}' to PDF file...");
}

public class ExcelReport : IReport
{
    public void Export(String data) => Console.WriteLine($"Exporting '{data}' to Excel spreadsheet...");
}

public class HtmlReport : IReport
{
    public void Export(String data) => Console.WriteLine($"Exporting '{data}' to HTML page...");
}

public abstract class ReportGenerator
{
    public abstract IReport CreateReport();

    public void Generate(String content)
    {
        IReport report = CreateReport();
        report.Export(content);
    }
}

public class PdfGenerator : ReportGenerator
{
    public override IReport CreateReport() => new PdfReport();
}

public class ExcelGenerator : ReportGenerator
{
    public override IReport CreateReport() => new ExcelReport();
}

public class HtmlGenerator : ReportGenerator
{
    public override IReport CreateReport() => new HtmlReport();
}

class Program
{
    static void Main()
    {
        ReportGenerator generator;
        
        String configSetting = "Html"; 

        if (configSetting == "PDF")
        {
            generator = new PdfGenerator();
        }
        else if (configSetting == "Excel")
        {
            generator = new ExcelGenerator();
        }
        else
        {
            generator = new HtmlGenerator();
        }

        Console.WriteLine($"Current config: {configSetting}");
        generator.Generate("Monthly Sales Report 2026");
    }
}