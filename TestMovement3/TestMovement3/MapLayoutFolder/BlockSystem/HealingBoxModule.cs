using Jypeli;
using TestMovement3.Image_Sound_Storage;
using TestMovement3.PlayerSetup;

namespace TestMovement3.MapLayoutFolder.BlockSystem;

public static class HealingBoxModule
{
    /// <summary>
    /// Handles the player's interaction with a HealingBox.
    /// </summary>
    public static void HandleHealingBoxCollision(PhysicsObject player, IntMeter playerHP, PhysicsObject healingBox)
    {
        if (playerHP.Value >= CreatePlayer.MAX_HP) return; // Prevent overhealing

        playerHP.Value += 1;
        SoundModule.PlaySoundEffect(SoundData.HealingBox);
        
        Vector originalPosition = healingBox.Position; // Save original position
        healingBox.Position = new Vector(-9999, -9999); // Move off-screen

        // Restore the HealingBox after 3 seconds
        Timer.SingleShot(3.0, () => healingBox.Position = originalPosition);
    }
}