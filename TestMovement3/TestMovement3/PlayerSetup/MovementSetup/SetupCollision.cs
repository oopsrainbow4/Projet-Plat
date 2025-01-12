using System;
using Jypeli;

namespace TestMovement3.PlayerSetup;

public partial class Movement
{
    /// <summary>
    /// Sets up collision events for the player and the floor.
    /// </summary>
    /// <param name="playerObject">The player's PhysicsObject.</param>
    public void SetupCollisionEvents(PhysicsObject playerObject)
    {
        // Tag the floor object for easy identification in collision events
        
        string[] layoutTags = { "Block", "Spike"};

        // Detect when the player collides with the floor
        player.Collided += (_, target) =>
        {
            // Check if the target of the collision is the floor
            if (target.Tag != null && Array.Exists(layoutTags, tag => tag.Equals(target.Tag.ToString())))
            {
                isOnGround = true; // Player is on the ground
                isDoubleJumpingAllowed = true; // Reset double jump capability
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