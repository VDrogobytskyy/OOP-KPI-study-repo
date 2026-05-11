using System;
using System.Collections.Generic;

// Visitor

interface IFinancialDocument
{
    void Accept(IDocumentVisitor visitor);
}

class Invoice : IFinancialDocument
{
    public decimal Amount { get; }

    public Invoice(decimal amount)
    {
        Amount = amount;
    }

    public void Accept(IDocumentVisitor visitor)
    {
        visitor.VisitInvoice(this);
    }
}

class TaxReport : IFinancialDocument
{
    public decimal TaxAmount { get; }

    public TaxReport(decimal taxAmount)
    {
        TaxAmount = taxAmount;
    }

    public void Accept(IDocumentVisitor visitor)
    {
        visitor.VisitTaxReport(this);
    }
}

class BankStatement : IFinancialDocument
{
    public decimal SuspiciousTransactionAmount { get; }

    public BankStatement(decimal suspiciousTransactionAmount)
    {
        SuspiciousTransactionAmount = suspiciousTransactionAmount;
    }

    public void Accept(IDocumentVisitor visitor)
    {
        visitor.VisitBankStatement(this);
    }
}

interface IDocumentVisitor
{
    void VisitInvoice(Invoice invoice);
    void VisitTaxReport(TaxReport taxReport);
    void VisitBankStatement(BankStatement bankStatement);
}

class TotalAmountVisitor : IDocumentVisitor
{
    public decimal Total { get; private set; }

    public void VisitInvoice(Invoice invoice)
    {
        Total += invoice.Amount;
    }

    public void VisitTaxReport(TaxReport taxReport)
    {
        Total += taxReport.TaxAmount;
    }

    public void VisitBankStatement(BankStatement bankStatement)
    {
        Total += bankStatement.SuspiciousTransactionAmount;
    }
}

class RiskSearchVisitor : IDocumentVisitor
{
    public void VisitInvoice(Invoice invoice)
    {
        if (invoice.Amount > 10000)
        {
            Console.WriteLine($"Risk: large invoice amount {invoice.Amount}");
        }
    }

    public void VisitTaxReport(TaxReport taxReport)
    {
        if (taxReport.TaxAmount < 1000)
        {
            Console.WriteLine($"Risk: suspiciously low tax report amount {taxReport.TaxAmount}");
        }
    }

    public void VisitBankStatement(BankStatement bankStatement)
    {
        if (bankStatement.SuspiciousTransactionAmount > 0)
        {
            Console.WriteLine($"Risk: suspicious transaction {bankStatement.SuspiciousTransactionAmount}");
        }
    }
}

class Program
{
    static void Main()
    {
        var documents = new List<IFinancialDocument>
        {
            new Invoice(15000),
            new TaxReport(800),
            new BankStatement(5000)
        };

        var totalVisitor = new TotalAmountVisitor();
        foreach (var document in documents)
        {
            document.Accept(totalVisitor);
        }

        Console.WriteLine($"Total amount of documents: {totalVisitor.Total}");

        Console.WriteLine("\nRisk search:");
        var riskVisitor = new RiskSearchVisitor();
        foreach (var document in documents)
        {
            document.Accept(riskVisitor);
        }
    }
}
