using Jypeli;
using TestMovement3.PlayerSetup;

namespace TestMovement3.MapLayoutFolder.BlockSystem;

public class WaterModule
{
    private static Timer waterExitCheckTimer;
    private static bool isInWater;
    public static void ApplyWaterEffects(PhysicsObject player, MovementMain playerMovement)
    {
        player.Acceleration = new Vector(0, -100); // Reduce downward acceleration (simulating lower gravity)
        player.Velocity = new Vector(player.Velocity.X, player.Velocity.Y * 0.75); // Reduce speed smoothly
        player.LinearDamping = 2.0; // Make movement feel heavier in water
        playerMovement.EnableUnlimitedJumps(); // Enable unlimited jumps in water

        if (waterExitCheckTimer == null)
        {
            waterExitCheckTimer = new Timer { Interval = 0.1 };
            waterExitCheckTimer.Timeout += () =>
            {
                if (isInWater && !IsPlayerTouchingWater(player))
                {
                    isInWater = false;
                    waterExitCheckTimer.Stop();
                    RemoveWaterEffects(player, playerMovement);
                }
            };
        }
        isInWater = true;
        waterExitCheckTimer.Start();
    }

    public static void RemoveWaterEffects(PhysicsObject player, MovementMain playerMovement)
    {
        player.Acceleration = new Vector(0, -1000);// Reset normal gravity
        player.LinearDamping = 0; // Remove water resistance
        playerMovement.DisableUnlimitedJumps(); // Disable unlimited jumps outside water
    }
    private static bool IsPlayerTouchingWater(PhysicsObject playerObject)
    {
        foreach (GameObject obj in Game.Instance.GetObjects(obj => obj is PhysicsObject))
        {
            if (obj.Tag != null && obj.Tag.ToString() == "Water")
            {
                if (((PhysicsObject)obj).IsInside(playerObject.Position))
                {
                    return true;
                }
            }
        }
        return false;
    }
}