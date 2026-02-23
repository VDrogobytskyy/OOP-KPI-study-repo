
public interface ICarRental
{
    public decimal Rent(/*Car car - якщо машина впливає на вартість*/ decimal baseValue, int amount);
}

public class DailyCarRental : ICarRental
{
    public decimal Rent(/*Car car - якщо машина впливає на вартість*/ decimal baseValue, int amount)
    {
        return baseValue * amount;
    }
}

public class WeeklyCarRental : ICarRental
{
    public decimal Rent(/*Car car - якщо машина впливає на вартість*/ decimal baseValue, int amount)
    {
        return baseValue * (7 * amount);
    }
}