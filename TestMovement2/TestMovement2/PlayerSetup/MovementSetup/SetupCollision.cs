using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;

namespace TestMovement2.PlayerSetup;

public partial class Movement
{
    /// <summary>
    /// Sets up collision events for the player and the floor.
    /// </summary>
    /// <param name="playerObject">The player's PhysicsObject.</param>
    public void SetupCollisionEvents(PhysicsObject playerObject)
    {
        // Tag the floor object for easy identification in collision events
        
        string[] layoutTags = { "Block", "Spike", "HealingBox"};

        // Detect when the player collides with the floor
        playerObject.Collided += (_, target) =>
        {
            // Check if the target of the collision is the floor
            if (target.Tag != null && Array.Exists(layoutTags, tag => tag.Equals(target.Tag.ToString())))
            {
                if (target.Tag.ToString() == "Block")
                {
                    isOnGround = true; // Player is on the ground
                    isDoubleJumpingAllowed = true; // Reset double jump capability
                }
                else if (target.Tag.ToString() == "Spike")
                {
                    playerHP.Value -= 1; // Reduce player's HP by 1
                    // Optional: Add feedback (e.g., flash player, play sound, etc.)
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
}
