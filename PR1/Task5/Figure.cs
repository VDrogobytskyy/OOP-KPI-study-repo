
public abstract class Figure
{
    public abstract double GetArea{ get; }
}

public class Rectangle : Figure
{
    public double Height { get; set; }
    public double Width { get; set; }
    public override double GetArea => Width * Height;
}

public class Square : Figure
{
    public double SideLen { get; set; }
    public override double GetArea => SideLen * SideLen;
}

public class Execution
{
    public Execution()
    {
        var r = new Rectangle()
        {
            Height = 10,
            Width = 5
        };
        
        var s = new Square()
        {
            SideLen = 5
        };
        GetFigureArea(r);
        GetFigureArea(s);
    }

    public double GetFigureArea(Figure figure)
    {
        Console.WriteLine($"Area: {figure.GetArea}");
        return figure.GetArea;
    }
}