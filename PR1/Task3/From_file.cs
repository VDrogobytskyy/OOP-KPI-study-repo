using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
public enum RentType
{
    Daily,
    Weekly
}
public class Car
{
    public string Maker { get; set; }
    public Color Color { get; set; }
}
public class CarRental
{
    public decimal Rent(Car car, decimal baseValue, int amount, RentType rentType)
    {
        if (rentType == RentType.Daily)
        {
        // Daily Logic
            return baseValue * amount;
        }
        if (rentType == RentType.Weekly)
        {
        // Weekly Logic
            return baseValue * (7 * amount);
        }
        return 0;
    }
}