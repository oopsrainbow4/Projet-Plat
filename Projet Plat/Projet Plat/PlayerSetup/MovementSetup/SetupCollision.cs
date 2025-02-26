using System;
using System.Linq;
using Jypeli;
using Projet_Plat.EnemyModuleFolder;
using Projet_Plat.Image_Sound_Storage;
using Projet_Plat.MapLayoutFolder;

namespace Projet_Plat.PlayerSetup;

public partial class MovementMain
{
    // Tracks whether the player is temporarily invincible after taking damage
    private bool isInvincible;
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
            if (target.Tag != null) 
            {
                string targetTag = target.Tag.ToString();

                if (Array.Exists(layoutTags, tag => tag.Equals(targetTag)))
                { 
                    isOnGround = true;
                    isDoubleJumpingAllowed = true;
                   
                    switch (target.Tag.ToString())
                    {
                        case "Spike":
                            HandleSpikeCollision(playerObject, playerHP, target);
                            break;
                        case "Lava":
                            if (!isInvincible) playerHP.Value -= 10;
                            break;
                        case "HealingBox":
                            if (target is PhysicsObject healingBox)
                            {
                                // Check if the player's HP is already full
                                if (playerHP.Value >= CreatePlayer.MAX_HP) break;
                                
                                playerHP.Value += 1;
                                
                                SoundModule.PlaySoundEffect("HealingBox");
                                
                                // Save original position
                                Vector originalPosition = healingBox.Position;
                                
                                // Move it off-screen
                                healingBox.Position = new Vector(-9999, -9999);

                                // Restore after 3 seconds
                                Timer.SingleShot(3.0, () =>
                                {
                                    healingBox.Position = originalPosition; // Bring it back
                                });
                            }
                            break;
                    } 
                }
                else if (targetTag == "Enemy") // Handle enemy collision
                { 
                    HandleEnemyCollision(playerObject, playerHP, target as BasicEnemy);
                    SoundModule.PlaySoundEffect("Ouch");
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

    /// <summary>
    /// Handles collision with spikes by reducing health and applying knockback.
    /// </summary>
    private void HandleSpikeCollision(PhysicsObject playerObject, IntMeter playerHP, IPhysicsObject spike)
    {
        if (!isInvincible)
        {
            SoundModule.PlaySoundEffect("Ouch");
            playerHP.Value -= 1; // Reduce HP by 1
            ActivateInvincibility(); // Start invincibility timer to prevent instant damage
            ApplyKnockback(playerObject, spike); // Push the player away from the spike
        }
    }

    private void HandleEnemyCollision(PhysicsObject playerObject, IntMeter playerHP, BasicEnemy enemy)
    {
        if (enemy != null)
        {
            if (playerObject.Bottom >= enemy.Top - 5) // Check if player is landing on enemy
            {
                enemy.TakeDamage(1); 
                playerObject.Velocity = new Vector(playerObject.Velocity.X, 500); // Bounce player upwards
            }
            else if (!isInvincible)
            {
                playerHP.Value -= enemy.Damage; // Reduce player HP
                ActivateInvincibility();
                ApplyKnockback(playerObject, enemy);
            }
        }
    }
    
    /// <summary>
    /// Activates temporary invincibility after taking damage.
    /// </summary>
    private void ActivateInvincibility()
    {
        isInvincible = true;  // Player becomes invincible
        invincibilityTimer.Start(); // Start the timer to track duration
    }
    
    /// <summary>
    /// Applies a knockback effect to the player when colliding with damaging objects.
    /// </summary>
    private void ApplyKnockback(PhysicsObject playerCharacter, IPhysicsObject spike)
    {
        // Determine the direction of knockback based on relative positions
        Vector knockbackDirection = (playerCharacter.Position - spike.Position).Normalize();
        player.Velocity = knockbackDirection * 600; // Apply knockback velocity (adjust the multiplier as needed)
    }
}
