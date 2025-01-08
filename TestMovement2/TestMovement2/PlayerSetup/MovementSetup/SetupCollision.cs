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
    /// <param name="floor">The floor PhysicsObject.</param>
    public void SetupCollisionEvents(PhysicsObject playerObject, PhysicsObject floor)
    {
        // Tag the floor object for easy identification in collision events
        floor.Tag = "Floor";

        // Detect when the player collides with the floor
        player.Collided += (_, target) =>
        {
            // Check if the target of the collision is the floor
            if (target.Tag != null && target.Tag.ToString() == "Floor")
            {
                isOnGround = true; // Player is on the ground
                isDoubleJumpingAllowed = true; // Reset double jump capability
            }
        };

        // Timer to check if the player is no longer colliding with the floor
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
