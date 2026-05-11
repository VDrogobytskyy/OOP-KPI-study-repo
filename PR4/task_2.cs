using System;

// Strategy

interface IPaymentStrategy
{
    void Pay(decimal amount);
}

class PayPalPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paied {amount} USD by PayPal.");
    }
}

class CreditCardPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paied {amount} USD by credit card.");
    }
}

class CryptoPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paied {amount} USD by crypto.");
    }
}

class ShoppingCart
{
    private IPaymentStrategy _paymentStrategy;

    public ShoppingCart(IPaymentStrategy paymentStrategy)
    {
        _paymentStrategy = paymentStrategy;
    }

    public void SetPaymentStrategy(IPaymentStrategy paymentStrategy)
    {
        _paymentStrategy = paymentStrategy;
    }

    public void Checkout(decimal amount)
    {
        _paymentStrategy.Pay(amount);
    }
}

class Program
{
    static void Main()
    {
        var cart = new ShoppingCart(new PayPalPayment());
        cart.Checkout(1200);

        cart.SetPaymentStrategy(new CreditCardPayment());
        cart.Checkout(850);

        cart.SetPaymentStrategy(new CryptoPayment());
        cart.Checkout(3000);
    }
}

