using System;
using Jypeli;

namespace Projet_Plat.PlayerSetup;

public partial class MovementMain
{
    // Add to class-level variables
    private bool isInvincible; // Tracks if the player is invincible
    private Timer invincibilityTimer;  // Timer to handle invincibility duration
    
    /// <summary>
    /// Sets up collision events for the player and the floor.
    /// </summary>
    /// <param name="playerObject">The player's PhysicsObject.</param>
    /// <param name="playerHP"></param>
    public void SetupCollisionEvents(PhysicsObject playerObject, IntMeter playerHP)
    {
        // Tag the floor object for easy identification in collision events
        
        string[] layoutTags = { "Block", "Spike", "HealingBox", "Lava"};

        // Initialize the invincibility timer
        invincibilityTimer = new Timer
        {
            Interval = 2.0 // 2 seconds of invincibility
        };
        invincibilityTimer.Timeout += () => { isInvincible = false; }; // Turn off invincibility

        // Detect when the player collides with the floor
        playerObject.Collided += (_, target) =>
        {
            if (target.Tag != null && Array.Exists(layoutTags, tag => tag.Equals(target.Tag.ToString())))
            {
                isOnGround = true;
                isDoubleJumpingAllowed = true;

                if (target.Tag.ToString() == "Spike")
                {
                    if (!isInvincible) // Only lose HP if not invincible
                    {
                        playerHP.Value -= 1; // Reduce HP by 1
                        ActivateInvincibility(); // Start invincibility
                        ApplyKnockback(playerObject, target); // Apply knockback effect
                    }
                }
                else if (target.Tag.ToString() == "Lava")
                {
                    if (!isInvincible) // Only lose HP if not invincible
                    {
                        playerHP.Value -= 5; // Reduce HP
                    }
                }
                else if (target.Tag.ToString() == "HealingBox")
                {
                    playerHP.Value += 1; // Heal the player by 1 HP
                    // Optional: Add visual or sound feedback
                }
            }
        };

        // Timer to check if the player is no longer colliding with the block
        Timer groundCheckTimer = new Timer
        {
            Interval = 0.1 // Check every 0.1 seconds
        };

        groundCheckTimer.Timeout += () =>
        {
            // If the player is not colliding for a while, assume they are in the air
            if (!isOnGround) isOnGround = false;
        };

        groundCheckTimer.Start(); // Start the timer
    }

    // Activates invincibility for the player
    private void ActivateInvincibility()
    {
        isInvincible = true;  // Set invincible status
        invincibilityTimer.Start(); // Start the timer
    }
    
    // Applies knockback to the player when they touch a spike
    private void ApplyKnockback(PhysicsObject playerCharacter, IPhysicsObject spike)
    {
        // Determine the direction of knockback based on relative positions
        Vector knockbackDirection = (playerCharacter.Position - spike.Position).Normalize();
        player.Velocity = knockbackDirection * 600; // Apply knockback velocity (adjust the multiplier as needed)
    }
}
