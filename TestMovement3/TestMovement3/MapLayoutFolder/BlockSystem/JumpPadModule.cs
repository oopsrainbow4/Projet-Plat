using TestMovement3.Image_Sound_Storage;

namespace TestMovement3.MapLayoutFolder.BlockSystem;
public static class JumpPadModule
{
    private static readonly double JumpMultiplier = 1.8;  // Jump height increase factor
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