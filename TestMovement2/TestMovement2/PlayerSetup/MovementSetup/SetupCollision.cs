using System;
using System.Linq;
using Jypeli;
using TestMovement2.MapLayoutFolder;

namespace TestMovement2.PlayerSetup;

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
        // Centralized tags for collision objects
        string[] layoutTags = BlockModule.BlockInfo.Values.Select(info => info.Tag).ToArray();

        // Initialize the invincibility timer
        invincibilityTimer = new Timer
        {
            Interval = 2.0 // 2 seconds of invincibility
        };
        invincibilityTimer.Timeout += () => {isInvincible = false;}; // Turn off invincibility

        // Detect when the player collides with objects.
        playerObject.Collided += (_, target) =>
        {
            if (target.Tag != null && Array.Exists(layoutTags, tag => tag.Equals(target.Tag.ToString())))
            {
                isOnGround = true;
                isDoubleJumpingAllowed = true;

                switch (target.Tag.ToString())
                {
                    case "Spike":
                        HandleSpikeCollision(playerObject, playerHP, target);
                        break;
                    case "Lava":
                        if (!isInvincible) playerHP.Value -= 5;
                        break;
                    case "HealingBox":
                        playerHP.Value += 1;
                        break;
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

    // Handles collision with spikes
    private void HandleSpikeCollision(PhysicsObject playerObject, IntMeter playerHP, IPhysicsObject spike)
    {
        if (!isInvincible)
        {
            playerHP.Value -= 1; // Reduce HP
            ActivateInvincibility(); // Start invincibility
            ApplyKnockback(playerObject, spike); // Apply knockback effect
        }
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
