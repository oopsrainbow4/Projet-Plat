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
        Water
    }

    public static readonly Dictionary<BlockType, (string Tag, Image Image, Shape Shape,
        double Width, double Height)> BlockInfo = new()
    {
        { BlockType.Land, ("Block", ImageModule.BlockImage, Shape.Rectangle, blockWidth, blockHeight) },
        { BlockType.Lava, ("Lava", ImageModule.LavaImage, Shape.Rectangle, blockWidth, blockHeight) },
        { BlockType.HealingBox, ("HealingBox", ImageModule.HealingBoxImage, Shape.Rectangle, blockWidth, blockHeight) },
        { BlockType.Spike, ("Spike", ImageModule.SpikeImage, Shape.Triangle, blockWidth, blockHeight) },
        { BlockType.Water, ("Water", ImageModule.WaterImage, Shape.Rectangle, blockWidth, blockHeight) }
        
    };
}