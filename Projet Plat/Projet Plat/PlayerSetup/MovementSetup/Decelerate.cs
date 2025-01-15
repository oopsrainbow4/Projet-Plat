using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;

namespace Projet_Plat.PlayerSetup;

/// <summary>
/// Handles smooth deceleration of the player's movement.
/// </summary>
public partial class MovementMain
{
    /// <summary>
    /// Gradually reduces the player's horizontal velocity to simulate friction.
    /// </summary>
    public void Decelerate()
    {
        if (player == null) return; // Exit if player is not initialized

        // Decelerate when moving to the right
        if (player.Velocity.X > 0)
        {
            // Decrease speed to the right
            player.Velocity -= new Vector(ACCELERATION_RATE / 2, 0);
            
            // If velocity goes negative, set it to zero
            if (player.Velocity.X < 0) 
                player.Velocity = new Vector(0, player.Velocity.Y);
        }
        // Decelerate when moving to the left
        else if (player.Velocity.X < 0)
        {
            // Decrease speed to the left
            player.Velocity += new Vector(ACCELERATION_RATE / 2, 0);
            
            // If velocity goes positive, set it to zero
            if (player.Velocity.X > 0) 
                player.Velocity = new Vector(0, player.Velocity.Y);
        }
    }

    /// <summary>
    /// Sets up a timer that repeatedly calls Decelerate() for smooth movement.
    /// </summary>
    public void DecelerationTimer()
    {
        Timer decelerationTimer = new Timer
        {
            Interval = 0.02 // Call Decelerate every 0.02 seconds (50 FPS)
        };
        
        // Attach the Decelerate method to the timer's Timeout event
        decelerationTimer.Timeout += Decelerate;
        decelerationTimer.Start(); // Start the timer
    }
}