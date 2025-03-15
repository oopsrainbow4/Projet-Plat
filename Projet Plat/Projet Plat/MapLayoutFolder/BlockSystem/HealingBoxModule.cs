using Jypeli;
using Projet_Plat.Image_Sound_Storage;
using Projet_Plat.PlayerSetup;

namespace Projet_Plat.MapLayoutFolder.BlockSystem;

public static class HealingBoxModule
{
    /// <summary>
    /// Handles the player's interaction with a HealingBox.
    /// </summary>
    public static void HandleHealingBoxCollision(IntMeter playerHP, PhysicsObject healingBox)
    {
        if (playerHP.Value >= CreatePlayer.MAX_HP) return; // Prevent overhealing

        playerHP.Value += 5;
        SoundModule.PlaySoundEffect(SoundData.HealingBox);
        
        Vector originalPosition = healingBox.Position; // Save original position
        healingBox.Position = new Vector(-9999, -9999); // Move off-screen

        // Restore the HealingBox after 3 seconds
        Timer.SingleShot(3.0, () => healingBox.Position = originalPosition);
    }
}