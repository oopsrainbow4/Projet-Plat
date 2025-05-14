using Jypeli;
using Projet_Plat.Image_Sound_Storage;
using Projet_Plat.PlayerSetup;

namespace Projet_Plat.MapLayoutFolder.BlockSystem;

public static class SpikeModule
{
    /// <summary>
    /// Handles the player's interaction with a spike.
    /// </summary>
    public static void HandleSpikeCollision(MovementMain movement, PhysicsObject player, IntMeter playerHP, IPhysicsObject spike)
    {
        if (!movement.isInvincible)
        {
            SoundModule.PlaySoundEffect(SoundData.Ouch);
            playerHP.Value -= 1;
            movement.ActivateInvincibility();
            ApplyKnockback(player, spike);
        }
    }

    /// <summary>
    /// Applies knockback when the player collides with a spike.
    /// </summary>
    private static void ApplyKnockback(PhysicsObject player, IPhysicsObject spike)
    {
        Vector knockbackDirection = (player.Position - spike.Position).Normalize();
        player.Velocity = knockbackDirection * 600;
    }
}

