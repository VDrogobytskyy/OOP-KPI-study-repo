using System;


public interface IButton
{
    void Info();
}

public interface IText
{
    void Info();
}

public interface IDropList
{
    void Info();
}

public class LightButton : IButton
{
    public void Info()
    {
        Console.WriteLine("Ligth Button created!");
    }
}

public class LightText : IText
{
    public void Info()
    {
        Console.WriteLine("Ligth Text created!");
    }
}

public class LightDropList : IDropList
{
    public void Info()
    {
        Console.WriteLine("Ligth droplist created!");
    }
}

public class DarkButton : IButton
{
    public void Info()
    {
        Console.WriteLine("Dark Button created!");
    }
}

public class DarkText : IText
{
    public void Info()
    {
        Console.WriteLine("Dark Text created!");
    }
}

public class DarkDropList : IDropList
{
    public void Info()
    {
        Console.WriteLine("Dark droplist created!");
    }
}

public abstract class UI_F
{
    public abstract IButton Create_Button();
    public abstract IText Create_Text();
    public abstract IDropList Create_DropList();
}

public class LightUI_F : UI_F
{
    public override IButton Create_Button()
    {
        return new LightButton();
    }
    public override IText Create_Text()
    {
        return new LightText();
    }
    public override IDropList Create_DropList()
    {
        return new LightDropList();
    }
}

public class DarkUI_F : UI_F
{
    public override IButton Create_Button()
    {
        return new DarkButton();
    }
    public override IText Create_Text()
    {
        return new DarkText();
    }
    public override IDropList Create_DropList()
    {
        return new DarkDropList();
    }
}

public class Mobile_App
{
    private IButton button;
    private IText text;
    private IDropList dropList;

    public Mobile_App(UI_F uI_F)
    {
        button = uI_F.Create_Button();
        text = uI_F.Create_Text();
        dropList = uI_F.Create_DropList();
    }

    public void All_Info()
    {
        button.Info();
        text.Info();
        dropList.Info();
    }
}

class Program
{
    static void Main()
    {
        UI_F general_fabrick;

        Console.WriteLine("Let`s create Mobile app with Light theme");
        general_fabrick = new LightUI_F();
        Mobile_App light_mob_App = new Mobile_App(general_fabrick);
        light_mob_App.All_Info();

        Console.WriteLine();

        Console.WriteLine("Let`s create Mobile app with Dark theme");
        general_fabrick = new DarkUI_F();
        Mobile_App dark_mob_App = new Mobile_App(general_fabrick);
        dark_mob_App.All_Info();
    }
}