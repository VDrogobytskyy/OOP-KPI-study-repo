using System;
using System.Collections.Generic;
using System.Linq;

public interface ISystemItem
{
    string Title { get; }
    decimal GetTotalPrice();
    void PrintDetails(int indentation);
    void UpdateStatus(bool active);
}

public class SoftwareModule : ISystemItem
{
    public string Title { get; }
    private decimal _price;
    private bool _isActive = true;

    public SoftwareModule(string title, decimal price)
    {
        Title = title;
        _price = price;
    }

    public decimal GetTotalPrice() => _isActive ? _price : 0;

    public void UpdateStatus(bool active)
    {
        _isActive = active;
        Console.WriteLine($"[Module] {Title} status changed to: {(active ? "ON" : "OFF")}");
    }

    public void PrintDetails(int indentation)
    {
        string offset = new string(' ', indentation);
        Console.WriteLine($"{offset}* {Title} (Cost: ${_price})");
    }
}

public class ModuleBundle : ISystemItem
{
    public string Title { get; }
    private readonly List<ISystemItem> _elements = new();

    public ModuleBundle(string title) => Title = title;

    public void AddElement(ISystemItem item) => _elements.Add(item);
    
    public void RemoveElement(ISystemItem item) => _elements.Remove(item);

    public decimal GetTotalPrice()
    {
        decimal total = 0;
        foreach (var el in _elements) 
        {
            total += el.GetTotalPrice();
        }
        return total;
    }

    public void UpdateStatus(bool active)
    {
        Console.WriteLine($"\nUpdating bundle: {Title} to {(active ? "ACTIVE" : "INACTIVE")}");
        foreach (var item in _elements)
        {
            item.UpdateStatus(active);
        }
    }

    public void PrintDetails(int indentation)
    {
        string offset = new string(' ', indentation);
        Console.WriteLine($"{offset}+ BUNDLE: {Title}");
        foreach (var item in _elements)
        {
            item.PrintDetails(indentation + 4);
        }
    }
}

class Program
{
    static void Main()
    {
        var mainCore = new SoftwareModule("Core System", 5500);
        var authUnit = new SoftwareModule("Auth Gateway", 1250);
        var dwh = new SoftwareModule("Data Warehouse", 2100);
        var reports = new SoftwareModule("Visual Reports", 950);

        var analyticsSuite = new ModuleBundle("Analytics Suite");
        analyticsSuite.AddElement(dwh);
        analyticsSuite.AddElement(reports);

        var corporateCrm = new ModuleBundle("Global Enterprise CRM");
        corporateCrm.AddElement(mainCore);
        corporateCrm.AddElement(authUnit);
        corporateCrm.AddElement(analyticsSuite);

        Console.WriteLine("System Configuration Map");
        corporateCrm.PrintDetails(0);

        Console.WriteLine($"\n[Summary] Total license cost: ${corporateCrm.GetTotalPrice()}");

        analyticsSuite.UpdateStatus(false);

        Console.WriteLine($"\n[Summary] Revised cost after changes: ${corporateCrm.GetTotalPrice()}");
        Console.WriteLine("Process finished.");
    }
}