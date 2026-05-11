using System;

// Chain of Responsibility

abstract class SupportHandler
{
    private SupportHandler? _next;

    public SupportHandler SetNext(SupportHandler next)
    {
        _next = next;
        return next;
    }

    public void Handle(string request)
    {
        if (CanHandle(request))
        {
            Process(request);
        }
        else if (_next != null)
        {
            Console.WriteLine($"{GetType().Name} Next..");
            _next.Handle(request);
        }
        else
        {
            Console.WriteLine("Noone helped.");
        }
    }

    protected abstract bool CanHandle(string request);
    protected abstract void Process(string request);
}

class ChatBotHandler : SupportHandler
{
    protected override bool CanHandle(string request)
    {
        return request.Contains("Password", StringComparison.OrdinalIgnoreCase);
    }

    protected override void Process(string request)
    {
        Console.WriteLine("Chat-bot: instruction to recover password is sent.");
    }
}

class FirstLevelOperatorHandler : SupportHandler
{
    protected override bool CanHandle(string request)
    {
        return request.Contains("Order", StringComparison.OrdinalIgnoreCase);
    }

    protected override void Process(string request)
    {
        Console.WriteLine("First operator: checked order status.");
    }
}

class TechnicalSpecialistHandler : SupportHandler
{
    protected override bool CanHandle(string request)
    {
        return request.Contains("server", StringComparison.OrdinalIgnoreCase)
            || request.Contains("error", StringComparison.OrdinalIgnoreCase);
    }

    protected override void Process(string request)
    {
        Console.WriteLine("Tech worker: analysing tech probleam.");
    }
}

class Program
{
    static void Main()
    {
        var bot = new ChatBotHandler();
        var operatorLevel1 = new FirstLevelOperatorHandler();
        var tech = new TechnicalSpecialistHandler();

        bot.SetNext(operatorLevel1).SetNext(tech);

        bot.Handle("I have an error on a server after update");
    }
}

