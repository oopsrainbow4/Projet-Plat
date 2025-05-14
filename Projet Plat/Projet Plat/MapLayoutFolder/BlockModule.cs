using Jypeli;
using System.Collections.Generic;
using Projet_Plat.Image_Sound_Storage;

namespace Projet_Plat.MapLayoutFolder;

/// <summary>
/// Centralized block metadata and types.
/// </summary>
public class BlockModule
{
    private static readonly double blockWidth = 64;
    private static readonly double blockHeight = 64;
    public enum BlockType
    {
        Land,
        Lava,
        HealingBox,
        Spike,
        Water,
        SpeedBoost,
        JumpPad,
        Checkpoint,
        Stone,
        FinnishFlag,
        Board
    }
    
    public static readonly Dictionary<BlockType, BlockData> BlockInfo = new()
    {
        { BlockType.Land, new('#',"Block", ImageModule.DirtImage, Shape.Rectangle,
            blockWidth, blockHeight, false, true) },
        
        { BlockType.Lava, new('L',"Lava", ImageModule.LavaImage, Shape.Rectangle, 
            blockWidth, blockHeight, false, true) },
        
        { BlockType.HealingBox, new('+',"HealingBox", ImageModule.HealingBoxImage, 
            Shape.Rectangle, blockWidth, blockHeight, true, false) },
        
        { BlockType.Spike, new('^',"Spike", ImageModule.SpikeImage, Shape.Triangle, 
            blockWidth, blockHeight, false, true) },
        
        { BlockType.Water, new('w',"Water", ImageModule.WaterImage, Shape.Rectangle, 
            blockWidth, blockHeight, true, true) },
        
        { BlockType.SpeedBoost, new('s',"SpeedBoost", ImageModule.SpeedBoostImage, 
            Shape.Rectangle, blockWidth, blockHeight, true, false) },
        
        { BlockType.JumpPad, new('j',"JumpPad", ImageModule.JumpPadImage, Shape.Rectangle, 
            blockWidth * 1.5, blockHeight * 1.5, true, false) },
        
        { BlockType.Checkpoint, new('c',"Checkpoint", ImageModule.RedFlagImage, 
            Shape.Rectangle, blockWidth, blockHeight, true, false) },
        
        { BlockType.Stone, new('r',"Stone", ImageModule.StoneImage, 
            Shape.Rectangle, blockWidth, blockHeight, false, true) },
        
        { BlockType.FinnishFlag, new('F',"FinnishFlag", ImageModule.FinnishFlagImage, 
            Shape.Rectangle, blockWidth * 1.5, blockHeight * 1.5, true, false) },
        
        { BlockType.Board, new('B',"Board", ImageModule.BoardImage, 
            Shape.Rectangle, blockWidth * 5, blockHeight * 2, true, false) }
    };
    
    public static readonly Dictionary<char, BlockType> SignToBlockType = new();
    static BlockModule()
    {
        foreach (var pair in BlockInfo)
        {
            SignToBlockType[pair.Value.Sign] = pair.Key;
        }
    }
}

