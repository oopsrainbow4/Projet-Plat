using Jypeli;
using TestMovement2.PlayerSetup;

namespace TestMovement2.MapLayoutFolder.BlockSystem;

public class WaterModule
{
    public static void ApplyWaterEffects(PhysicsObject player, MovementMain playerMovement)
    {
        player.Acceleration = new Vector(0, -200); // Reduce downward acceleration (simulating lower gravity)
        player.Velocity = new Vector(player.Velocity.X, player.Velocity.Y * 0.5); // Reduce speed smoothly
        player.LinearDamping = 2.0; // Make movement feel heavier in water
        playerMovement.EnableUnlimitedJumps(); // Enable unlimited jumps in water
    }

    public static void RemoveWaterEffects(PhysicsObject player, MovementMain playerMovement)
    {
        player.Acceleration = new Vector(0, -1000);// Reset normal gravity
        player.LinearDamping = 0; // Remove water resistance
        playerMovement.DisableUnlimitedJumps(); // Disable unlimited jumps outside water
    }
}