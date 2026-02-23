using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
public class Rectangle
{
    public virtual double Height { get; set; }

    public virtual double Width { get; set; }
    public double Area { get { return this.Height * this.Width; } }
}
class Square : Rectangle
{
    public override double Height { set => base.Width = base.Height = value; }
    public override double Width { set => base.Width = base.Height = value; }
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
        GetRectArea(r);
        var s = new Square()
        {
            Height = 10, // Squares cannot have diff height and

            //width event thought Square ARE Rectangles in real life

            Width = 5 
        };
        GetRectArea(s);
    }
    public double GetRectArea(Rectangle rect)
    {
        Debug.WriteLine($"{rect.Height.ToString()} * {rect.Width.ToString()}");

        Debug.WriteLine($"{rect.Area.ToString()}");
        return rect.Area;
    }
}