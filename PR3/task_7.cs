using System;
using System.Collections.Generic;

// Внутрішній стан
public class BlockType
{
    public string Name { get; }
    public string TextureData { get; } 
    public int BaseHardness { get; } 

    public BlockType(string name, string texture, int hardness)
    {
        Name = name;
        TextureData = texture;
        BaseHardness = hardness;

        Console.WriteLine($"Loading resourses for: {name}");
        Console.WriteLine($"[MEMORY] Allocation: {texture} loaded into memory.");
    }

    public void Render(int x, int y, int z, int currentHealth)
    {
        Console.WriteLine($"rendering on {x}, {y}, {z}, with heals: {currentHealth}");
    }
}

public static class BlockFactory
{
    private static Dictionary<string, BlockType> _types = new Dictionary<string, BlockType>();

    public static BlockType GetBlockType(string name, string texture, int hardness)
    {
        if (!_types.ContainsKey(name))
        {
            _types[name] = new BlockType(name, texture, hardness);
        }
        return _types[name];
    }
}

// Зовнішній стан
public class Block
{
    private int _x, _y, _z;      
    private int _currentHealth;
    private BlockType _type;

    public Block(int x, int y, int z, BlockType type)
    {
        _x = x;
        _y = y;
        _z = z;
        _type = type;
        _currentHealth = type.BaseHardness; 
    }

    public void Mine()
    {
        _currentHealth -= 20;
        Console.WriteLine($"{_type.Name} у [{_x},{_y},{_z}] HIT! HP left: {_currentHealth}");
    }
}

class MinecraftWorld
{
    private List<Block> _blocks = new List<Block>();

    public void AddBlock(int x, int y, int z, string name, string texture, int hardness)
    {
        BlockType type = BlockFactory.GetBlockType(name, texture, hardness);
        
        Block block = new Block(x, y, z, type);
        _blocks.Add(block);
    }

    public int GetTotalBlocks() => _blocks.Count;
}

class Program
{
    static void Main()
    {
        MinecraftWorld world = new MinecraftWorld();

        Console.WriteLine("Minecraft world generating");

        for (int i = 0; i < 1000000; i++)
        {
            world.AddBlock(i, 0, 0, "Oak", "Heavy_Oak_Texture_10MB", 100);
        }

        world.AddBlock(1, 1, 1, "Stone", "Heavy_Stone_Texture_15MB", 500);
        world.AddBlock(2, 1, 1, "Stone", "Heavy_Stone_Texture_15MB", 500);

        Console.WriteLine($"\nAmount of blocks: {world.GetTotalBlocks()}");        
    }
}