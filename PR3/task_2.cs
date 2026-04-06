using System;
using System.Collections.Generic;

public class InventorySystem
{
    public bool CheckStock(string productName) {
        Console.WriteLine($"[Inventory] Is it available {productName}");
        return true;
    }
    public void ReserveItem(string productName) => 
        Console.WriteLine($"[Inventory] Product {productName} booked.");
}

public class DiscountService
{
    public decimal GetDiscount(string discountCode) {
        Console.WriteLine($"[Discount] Discount application {discountCode}");
        return discountCode == "KPI-ROZPRODASH" ? 0.3m : 0.0m;
    }
}

public class PaymentGateway
{
    public bool Charge(decimal amount) {
        Console.WriteLine($"[Payment] Charging: {amount} USD");
        return true;
    }
}

public class ShippingProvider
{
    public void ArrangeDelivery(string address) => 
        Console.WriteLine($"[Shipping] Shipping to: {address}");
}

public class OrderHistory
{
    public void LogOrder(string details) => 
        Console.WriteLine($"[History] Order saved in DB: {details}");
}

public class ShopFacade
{
    private readonly InventorySystem Inv_Sys = new();
    private readonly DiscountService Disc_Sys = new();
    private readonly PaymentGateway Paym_Sys = new();
    private readonly ShippingProvider Ship_Sys = new();
    private readonly OrderHistory Ord_Sys = new();

    public void DoOrder(
        string productName,
        string discountCode,
        string userAddress,
        decimal price
        )
    {
        Console.WriteLine("Start creating an order...");
        Inv_Sys.CheckStock(productName);
        Inv_Sys.ReserveItem(productName);
        decimal disc = Disc_Sys.GetDiscount(discountCode);
        if(disc != 0.0m)
        {
            price = price * (1 - disc);
        }
        bool is_Payed = Paym_Sys.Charge(price);

        if (is_Payed)
        {
            Ship_Sys.ArrangeDelivery(userAddress);

            string currentTime = DateTime.Now.ToString();
            Ord_Sys.LogOrder($"On time {currentTime}");
        }

        Console.WriteLine("Order placed.");
    }
}
class Program
{
    static void Main()
    {
        var shop = new ShopFacade();

        shop.DoOrder("Parkomat", "KPI-ROZPRODASH", "Kyiv, KPI block 5", 10000m);
        
    }
}