using Jypeli;
using System.Collections.Generic;

namespace TestMovement2.MapLayoutFolder;

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
        Spike
    }

    public static readonly Dictionary<BlockType, (string Tag, string ImagePath, Shape Shape,
        double Width, double Height)> BlockInfo = new()
    {
        { BlockType.Land, ("Block", "Images/MapLayoutImages/Block.png", Shape.Rectangle, blockWidth, blockHeight)},
        { BlockType.Lava, ("Lava", "Images/MapLayoutImages/Lava.png", Shape.Rectangle, blockWidth, blockHeight)},
        { BlockType.HealingBox, ("HealingBox", "Images/MapLayoutImages/HealingBox.png", 
            Shape.Rectangle, blockWidth/2, blockHeight/2)},
        { BlockType.Spike, ("Spike", "Images/MapLayoutImages/Spike.png", Shape.Triangle, blockWidth, blockHeight)}
    };
}