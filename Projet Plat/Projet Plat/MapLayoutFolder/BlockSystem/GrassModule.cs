using Jypeli;
using Projet_Plat.Image_Sound_Storage;

namespace Projet_Plat.MapLayoutFolder.BlockSystem;

/// <summary>
/// Determines if a Land block should be grass or dirt based on the block above.
/// </summary>
public class GrassModule
{
    /// <summary>
    /// Returns the correct image (Grass or Dirt) based on the tile above.
    /// If it's something on top then it would be a dirt.
    /// Or if it's nothing or HealingBox, SpeedBoost and others then it would be a grass.
    /// </summary>
    public static Image SetLandBlockImage(char [,] layout, int x, int y)
    {
        bool isGrass = true;

        if (y > 0)
        {
            char aboveTile = layout[y - 1, x];
            if (BlockModule.SignToBlockType.TryGetValue(aboveTile, out var aboveType))
            {
                if (aboveType != BlockModule.BlockType.HealingBox &&
                    aboveType != BlockModule.BlockType.SpeedBoost &&
                    aboveType != BlockModule.BlockType.JumpPad &&
                    aboveType != BlockModule.BlockType.Checkpoint &&
                    aboveType != BlockModule.BlockType.FinnishFlag)
                {
                    isGrass = false;
                }
            }
        }
        
        return isGrass ? ImageModule.GrassImage : ImageModule.DirtImage;
    }
}