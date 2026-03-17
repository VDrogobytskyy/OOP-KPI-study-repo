using System;

public abstract class EnemyPrototype
{
    public String Textures, Effects, MovementStyle;
    
    public int X, Y;
    public String Weapon;
    public int Level, Strength;

    public abstract EnemyPrototype Clone();
    
    public abstract void ShowInfo();
}

public class Orc : EnemyPrototype
{
    public Orc(String textures, String effects, String move)
    {
        this.Textures = textures;
        this.Effects = effects;
        this.MovementStyle = move;
        Console.WriteLine("Standard Orc template loaded");
    }

    private Orc(Orc source)
    {
        this.Textures = source.Textures;
        this.Effects = source.Effects;
        this.MovementStyle = source.MovementStyle;
    }

    public override EnemyPrototype Clone()
    {
        return new Orc(this);
    }

    public override void ShowInfo()
    {
        Console.WriteLine($"Orc [Pos: {X},{Y} | Weapon: {Weapon} | Lvl: {Level}] Textures: {Textures}, Effects: {Effects}, MovementStyle: {MovementStyle}");
    }
}

class Program
{
    static void Main()
    {
        Orc orcTemplate = new Orc("Maximum Rank", "Magic", "Teleporting");

        Console.WriteLine("\nSpawning an army by cloning");

        Orc orc1 = (Orc)orcTemplate.Clone();
        orc1.X = 10; orc1.Y = 20; orc1.Weapon = "Axe"; orc1.Level = 5;

        Orc orc2 = (Orc)orcTemplate.Clone();
        orc2.X = 55; orc2.Y = 80; orc2.Weapon = "Pistol"; orc2.Level = 3;

        orc1.ShowInfo();
        orc2.ShowInfo();
    }
}