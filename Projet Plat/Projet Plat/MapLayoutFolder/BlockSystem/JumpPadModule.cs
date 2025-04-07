using Projet_Plat.Image_Sound_Storage;

namespace Projet_Plat.MapLayoutFolder.BlockSystem;
public static class JumpPadModule
{
    private static readonly double JumpMultiplier = 2.0;  // Jump height increase factor
    public static double GetJumpPadBoost(double baseJumpHeight, bool isOnJumpPad)
    {
        if (isOnJumpPad)
        {
            SoundModule.PlaySoundEffect(SoundData.JumpPad); // Play Jump Pad sound
            return JumpMultiplier * baseJumpHeight;
        }
        return baseJumpHeight;
    }
}