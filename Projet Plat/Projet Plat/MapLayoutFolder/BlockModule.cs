using Jypeli;
using System.Collections.Generic;

namespace Projet_Plat.MapLayoutFolder;

/// <summary>
/// Centralized block metadata and types.
/// </summary>
public class BlockModule
{
    private static readonly double blockWidth = 50;
    private static readonly double blockHeight = 50;
    public enum BlockType
    {
        Land,
        Lava,
        HealingBox,
        Spike
    }

    public static readonly Dictionary<BlockType, (string Tag, string ImagePath, Shape Shape,
        double Width, double Height)> BlockInfo = new()
    {
        { BlockType.Land, ("Block", "MapLayoutImages/Block.png", Shape.Rectangle, blockWidth, blockHeight)},
        { BlockType.Lava, ("Lava", "MapLayoutImages/Lava.png", Shape.Rectangle, blockWidth, blockHeight)},
        { BlockType.HealingBox, ("HealingBox", "MapLayoutImages/HealingBox.png", 
            Shape.Rectangle, blockWidth/2, blockHeight/2)},
        { BlockType.Spike, ("Spike", "MapLayoutImages/Spike.png", Shape.Triangle, blockWidth, blockHeight)}
    };
}