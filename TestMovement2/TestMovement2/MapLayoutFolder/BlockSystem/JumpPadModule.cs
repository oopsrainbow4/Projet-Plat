using TestMovement2.Image_Sound_Storage;

namespace TestMovement2.MapLayoutFolder.BlockSystem;
public static class JumpPadModule
{
    private static readonly double JumpMultiplier = 1.5;  // Jump height increase factor
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