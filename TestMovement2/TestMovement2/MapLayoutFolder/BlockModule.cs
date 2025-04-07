using Jypeli;
using System.Collections.Generic;
using TestMovement2.Image_Sound_Storage;

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
        Spike,
        Water,
        SpeedBoost,
        JumpPad,
        Checkpoint
    }

    public static readonly Dictionary<BlockType, (string Tag, Image Image, Shape Shape,
        double Width, double Height)> BlockInfo = new()
    {
        { BlockType.Land, ("Block", ImageModule.BlockImage, Shape.Rectangle, blockWidth, blockHeight) },
        { BlockType.Lava, ("Lava", ImageModule.LavaImage, Shape.Rectangle, blockWidth, blockHeight) },
        { BlockType.HealingBox, ("HealingBox", ImageModule.HealingBoxImage, Shape.Rectangle, blockWidth, blockHeight) },
        { BlockType.Spike, ("Spike", ImageModule.SpikeImage, Shape.Triangle, blockWidth, blockHeight) },
        { BlockType.Water, ("Water", ImageModule.WaterImage, Shape.Rectangle, blockWidth, blockHeight) },
        { BlockType.SpeedBoost, ("SpeedBoost", ImageModule.SpeedBoostImage, Shape.Rectangle, blockWidth, blockHeight) },
        { BlockType.JumpPad, ("JumpPad", ImageModule.JumpPadImage, Shape.Rectangle, blockWidth * 1.5, blockHeight * 1.5) },
        { BlockType.Checkpoint, ("Checkpoint", ImageModule.RedFlagImage, Shape.Rectangle, blockWidth, blockHeight) }
    };
}