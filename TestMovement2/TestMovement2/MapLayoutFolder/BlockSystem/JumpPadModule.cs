namespace TestMovement2.MapLayoutFolder.BlockSystem;
public static class JumpPadModule
{
    private static readonly double JumpMultiplier = 1.8;  // Jump height increase factor

    public static double GetJumpPadBoost(double baseJumpHeight, bool isOnJumpPad)
    {
        return isOnJumpPad ? baseJumpHeight * JumpMultiplier : baseJumpHeight;
    }
}