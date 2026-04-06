// тип даних Task для асинхронного програмування, типу як обіцянка
using System.Text.RegularExpressions;
public interface IDomainPayment
{
    decimal GetAmount();
    string GetCurrency();
    Task<string> GetStatusAsync();
}

public class PaymentProcessor
{
    public async Task Process(IDomainPayment payment)
    {
        Console.WriteLine("Transaction starting...");
        Console.WriteLine($"Total: {payment.GetAmount()} {payment.GetCurrency()}");
        Console.WriteLine($"Status: {await payment.GetStatusAsync()}");
        Console.WriteLine("Finished\n");
    }
}

public class JsonBankApi
{
    public string amount_value { get; set; } = "100.50";
    public string currency_code { get; set; } = "USD";
    public string GetTransactionState() => "Success";
}

public class JsonBankAdapter : IDomainPayment {
    private readonly JsonBankApi _JsonApi;

    public JsonBankAdapter(JsonBankApi JsonApi)
    {
        _JsonApi = JsonApi;
    }

    public decimal GetAmount()
    {
        return Convert.ToDecimal(_JsonApi.amount_value);
    }

    public string GetCurrency()
    {
        return _JsonApi.currency_code;
    }

    public Task<string> GetStatusAsync()
    {
        string res = _JsonApi.GetTransactionState();

        return Task.FromResult(res);
    }
}
public class XmlBankApi
{
    public string GetXmlResponse() => "<payment><val>2500</val><cur>UAH</cur></payment>";
    public string CheckStatus() => "Processed";
}

public class XmlBankAdapter : IDomainPayment
{
    private readonly XmlBankApi _xmlApi;
    public XmlBankAdapter(XmlBankApi xmlApi)
    {
        _xmlApi = xmlApi;
    }

    public decimal GetAmount()
    {
        string xml = _xmlApi.GetXmlResponse();

        //Regex.Match - стандарт який шукає послідовність цифер
        string numberStr = Regex.Match(xml, @"\d+").Value;

        decimal amount = Convert.ToDecimal(numberStr);

        return amount;
    }
    public string GetCurrency()
    {
        string xml = _xmlApi.GetXmlResponse();

        string currency = xml.Split("<cur>")[1].Split("</cur>")[0];

        return currency;
    }
    public Task<string> GetStatusAsync()
    {
        string res = _xmlApi.CheckStatus();

        return Task.FromResult(res);
    }
}
public class ModernAsyncBankApi
{
    public decimal RawValue => 50.0m;
    public string ISO_Currency => "EUR";
    
    public async Task<int> FetchStatusCodeAsync() 
    {
        await Task.Delay(100); 
        return 1; 
    }
}

public class ModernBankAdapter : IDomainPayment
{
    private readonly ModernAsyncBankApi _modernApi;

    public ModernBankAdapter(ModernAsyncBankApi modernApi)
    {
        _modernApi = modernApi;
    }

    public decimal GetAmount()
    {
        return _modernApi.RawValue;
    }
    public string GetCurrency()
    {
        return _modernApi.ISO_Currency;
    }
    public Task<string> GetStatusAsync()
    {
        int res = _modernApi.FetchStatusCodeAsync();

        if(res == 1)
        {
            return "Finished";
        }
        else
        {
            return "In process";
        }
    }
}

class Program
{
    static async Task Main()
    {
        var processor = new PaymentProcessor();

        var bankJson = new JsonBankApi();
        var bankXml = new XmlBankApi();
        var bankAsync = new ModernAsyncBankApi();

        IDomainPayment bankJsonA = new JsonBankAdapter(bankJson);
        IDomainPayment bankXmlA = new XmlBankAdapter(bankXml);
        IDomainPayment bankAsyncA = new ModernBankAdapter(bankAsync);
        
        await processor.Process(bankJsonA);
        await processor.Process(bankXmlA);
        await processor.Process(bankAsyncA);

    }
}