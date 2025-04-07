using System;
using System.Linq;
using Jypeli;
using TestMovement3.MapLayoutFolder.BlockSystem;
using TestMovement3.EnemyModuleFolder;
using TestMovement3.Image_Sound_Storage;
using TestMovement3.MapLayoutFolder;

namespace TestMovement3.PlayerSetup;

public partial class MovementMain
{
    // Tracks whether the player is temporarily invincible after taking damage
    public bool isInvincible;
    private Timer invincibilityTimer;  // Timer to handle invincibility duration
    private bool isInWater;     // Tracks if the player is inside water
    private Timer waterEffectTimer;
    
    /// <summary>
    /// Sets up collision events for the player and the floor.
    /// </summary>
    /// <param name="playerObject">The player's PhysicsObject.</param>
    /// <param name="playerHP"></param>
    public void SetupCollisionEvents(PhysicsObject playerObject, IntMeter playerHP, Respawn respawnSystem)
    {
        // Centralized tags for collision objects
        string[] layoutTags = BlockModule.BlockInfo.Values.Select(info => info.Tag).ToArray();

        // Initialize the invincibility timer
        invincibilityTimer = new Timer
        {
            Interval = 2.0 // 2 seconds of invincibility
        };
        invincibilityTimer.Timeout += () => {isInvincible = false;}; // Turn off invincibility
        
        // Initialize the water effect timer
        waterEffectTimer = new Timer { Interval = 0.5}; // Apply water effects every 0.5 seconds
        waterEffectTimer.Timeout += () =>
        {
            if (isInWater)
                WaterModule.ApplyWaterEffects(playerObject, this);
        };
        
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
                            //HandleSpikeCollision(playerObject, playerHP, target);
                            SpikeModule.HandleSpikeCollision(this, playerObject, playerHP, target);
                            break;
                        case "Lava":
                            if (!isInvincible) playerHP.Value -= 10;
                            break;
                        case "Water":
                            if (!isInWater) // Avoid applying effects multiple times
                            {
                                isInWater = true;
                                WaterModule.ApplyWaterEffects(playerObject, this);
                            }
                            break;
                        case "HealingBox":
                            if (target is PhysicsObject healingBox)
                            {
                                HealingBoxModule.HandleHealingBoxCollision(playerHP, healingBox);
                            }
                            break;
                        case "SpeedBoost":
                            SpeedBoostModule.ApplySpeedBoost(playerObject, MAX_SPEED);
                            break;
                        case "JumpPad":
                            isOnJumpPad = true;
                            break;
                        case "Checkpoint":
                            if (target is PhysicsObject checkpointBlock)
                                CheckpointModule.HandleCheckpointCollision(this, checkpointBlock, respawnSystem);
                            break;
                    } 
                }
                else if (targetTag == "Enemy") // Handle enemy collision
                {
                    HandleEnemyCollision(playerObject, playerHP, target as BasicEnemy);
                }
            }
        };

        // Timer to check if the player is no longer colliding with the block
        Timer groundCheckTimer = new Timer { Interval = 0.1};
        groundCheckTimer.Timeout += () =>
        {
            // If the player is not colliding for a while, assume they are in the air
            if (!isOnGround) isOnGround = false;
        };
        groundCheckTimer.Start(); // Start the timer
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
                SoundModule.PlaySoundEffect(SoundData.Ouch);
                playerHP.Value -= enemy.Damage; // Reduce player HP
                ActivateInvincibility();
                ApplyKnockback(playerObject, enemy);
            }
        }
    }
    
    /// <summary>
    /// Activates temporary invincibility after taking damage.
    /// </summary>
    public void ActivateInvincibility()
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