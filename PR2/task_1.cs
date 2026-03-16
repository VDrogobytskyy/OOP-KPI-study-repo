using System;

public class Character
{
    public String Name, Class, EyeColor, HairStyle, Weapon;
    public String Gender;
    public int Strength, Intelect, Speed;
    
    public void CharacterStats()
    {
        Console.WriteLine($"Character name: {Name}");
        Console.WriteLine($"Character Class: {Class}");
        Console.WriteLine($"Character EyeColor: {EyeColor}");
        Console.WriteLine($"Character HairStyle: {HairStyle}");
        Console.WriteLine($"Character Weapon: {Weapon}");
        Console.WriteLine($"Character Gender: {Gender}");
        Console.WriteLine($"Character Strength: {Strength}");
        Console.WriteLine($"Character Intelect: {Intelect}");
        Console.WriteLine($"Character Speed: {Speed}\n");
    }
}

public abstract class CharacterBuilder
{
    protected Character character;

    public void Create_New_Character()
    {
        character = new Character();
        Apply_Defaults();
    }

    public abstract CharacterBuilder SetInitials(String base_name, String base_Class);
    public abstract CharacterBuilder SetLook(String base_EyeColor, String base_HairStyle);
    public abstract CharacterBuilder SetWeapon(String base_Weapon);
    public abstract CharacterBuilder SetGender(String base_Gender);
    public abstract CharacterBuilder SetStats(int base_Strength, int base_Intelect, int base_Speed);

    private void Apply_Defaults()
    {
        character.Name = "Soldat";
        character.Class = "Striker";
        character.EyeColor = "Green eyes";
        character.HairStyle = "Bold";
        character.Weapon = "M4A4-silencer";
        character.Gender = "Man";
        character.Strength = 40;
        character.Intelect = 35;
        character.Speed = 30;
    }

    public Character GetCharacter()
    {
        return character;
    }
}

public class HeroBuilder : CharacterBuilder
{
    public override CharacterBuilder SetInitials(String _Name, String _Class) 
    { 
        character.Name = _Name; 
        character.Class = _Class; 
        return this; 
    }

    public override CharacterBuilder SetLook(String _EyeColor, String _HairStyle) 
    { 
        character.EyeColor = _EyeColor; 
        character.HairStyle = _HairStyle; 
        return this; 
    }

    public override CharacterBuilder SetWeapon(String _Weapon) 
    { 
        character.Weapon = _Weapon; 
        return this; 
    }

    public override CharacterBuilder SetGender(String _Gender) 
    { 
        character.Gender = _Gender; 
        return this; 
    }

    public override CharacterBuilder SetStats(int _Strength, int _Intelect, int _Speed) 
    { 
        character.Strength = _Strength; 
        character.Intelect = _Intelect; 
        character.Speed = _Speed; 
        return this; 
    }
}

public class Standart_Character
{
    public void Create_Standart_Character(CharacterBuilder builder)
    {
        builder.Create_New_Character(); 
        builder.SetInitials("Soldat", "Striker")
               .SetLook("Green eyes", "Bold")
               .SetWeapon("M4A4-silencer")
               .SetGender("Man")
               .SetStats(40, 35, 30);
    }
}

class Program
{
    static void Main()
    {
        HeroBuilder my_builder = new HeroBuilder();
        Standart_Character std = new Standart_Character();

        Console.WriteLine("Standart Character");
        std.Create_Standart_Character(my_builder);
        Character soldier = my_builder.GetCharacter();
        soldier.CharacterStats();

        Console.WriteLine("Custom Character");
        my_builder.Create_New_Character(); 
        Character custom = my_builder
            .SetInitials("Makar", "Sniper")
            .SetWeapon("AWM")
            .SetStats(10, 80, 100)
            .GetCharacter(); 
        
        custom.CharacterStats();
    }
}