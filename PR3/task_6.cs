using System;

public interface IDisplayPlatform
{
    void RenderHeader(string title);
    void RenderDataRow(string label, string value);
    void RenderFooter();
}

public class WebDashboard : IDisplayPlatform
{
    public void RenderHeader(string title) => Console.WriteLine($"<html><body><h1>{title}</h1>");
    public void RenderDataRow(string label, string value) => Console.WriteLine($"<p><b>{label}:</b> {value}</p>");
    public void RenderFooter() => Console.WriteLine("</body></html>");
}

public class PdfExporter : IDisplayPlatform
{
    public void RenderHeader(string title) => Console.WriteLine($"[PDF] --- {title.ToUpper()} ---");
    public void RenderDataRow(string label, string value) => Console.WriteLine($"[PDF] {label}: {value}");
    public void RenderFooter() => Console.WriteLine("[PDF] End of Document");
}

public abstract class AnalyticsReport
{
    protected IDisplayPlatform _platform; 

    protected AnalyticsReport(IDisplayPlatform platform) => _platform = platform;

    public abstract void Display();
}

public class FinancialReport : AnalyticsReport
{
    public FinancialReport(IDisplayPlatform platform) : base(platform) { }

    public override void Display()
    {
        _platform.RenderHeader("Quarterly Financial Report");
        _platform.RenderDataRow("Revenue", "$500,000");
        _platform.RenderDataRow("Expenses", "$320,000");
        _platform.RenderFooter();
    }
}

public class UserMetricsReport : AnalyticsReport
{
    public UserMetricsReport(IDisplayPlatform platform) : base(platform) { }

    public override void Display()
    {
        _platform.RenderHeader("User Growth Stats");
        _platform.RenderDataRow("Active Users", "15,400");
        _platform.RenderDataRow("Churn Rate", "2.5%");
        _platform.RenderFooter();
    }
}

class Program
{
    static void Main()
    {
        IDisplayPlatform web = new WebDashboard();
        IDisplayPlatform pdf = new PdfExporter();

        AnalyticsReport report1 = new FinancialReport(web);
        AnalyticsReport report2 = new UserMetricsReport(pdf);

        Console.WriteLine("Executing Web Report");
        report1.Display();

        Console.WriteLine("\nExecuting PDF Export");
        report2.Display();
        
        AnalyticsReport report3 = new FinancialReport(pdf);
        Console.WriteLine("\nExecuting Financial Report in PDF");
        report3.Display();
    }
}