using System;

public class Logger
{
    private static Logger _instance;

    private Logger()
    {
        Console.WriteLine("Connection to Log Server established!");
    }

    public static Logger GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Logger();
        }
        return _instance;
    }

    public void Log(String message)
    {
        Console.WriteLine($"[LOG]: {message} at {DateTime.Now}");
    }
}

public class RegModule
{
    public void Register(String username)
    {
        Logger log = Logger.GetInstance();
        log.Log($"User {username} registered.");
    }
}

public class PaymentModule
{
    public void ProcessPayment(int amount)
    {
        Logger log = Logger.GetInstance();
        log.Log($"Payment of {amount} processed.");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Program started...");

        RegModule reg = new RegModule();
        reg.Register("Vova");

        PaymentModule payment = new PaymentModule();
        payment.ProcessPayment(100000);

        Logger l1 = Logger.GetInstance();
        Logger l2 = Logger.GetInstance();

        if (l1 == l2)
        {
            Console.WriteLine("\nBoth loggers are the SAME instance.");
        }
    }
}