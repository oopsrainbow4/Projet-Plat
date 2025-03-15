using System;
using Jypeli;
using System.Collections.Generic;

namespace Projet_Plat.MapLayoutFolder.BlockSystem;


public static class SpeedBoostModule
{
    private static readonly double SpeedBoostAmount = 500;  // Adjust as needed
    private static readonly double BoostDuration = 0.5; // Boost lasts for 2 seconds
    private static readonly double AccelerationRate = 50.0; // How much speed increases per step
    private static readonly double DecelerationRate = 30.0; // Slow down speed per step

    private static readonly Dictionary<PhysicsObject, Timer> activeBoosts = new();

    public static void ApplySpeedBoost(PhysicsObject player, double maxSpeed)
    {
        if (player == null || activeBoosts.ContainsKey(player)) return; // Prevent multiple boost stacks

        double boostDirection = player.Velocity.X >= 0 ? 1 : -1; // Determine movement direction
        double newMaxSpeed = maxSpeed + SpeedBoostAmount;

        // **Gradual Speed Increase Timer**
        Timer speedUpTimer = new Timer { Interval = 0.05 }; // Increase speed smoothly
        speedUpTimer.Timeout += () =>
        {
            if (Math.Abs(player.Velocity.X) < newMaxSpeed)
            {
                player.Velocity += new Vector(AccelerationRate * boostDirection, 0);
            }
            else
            {
                speedUpTimer.Stop(); // Stop increasing when max speed is reached
            }
        };
        speedUpTimer.Start();

        // If player already has an active boost, reset the timer
        if (activeBoosts.ContainsKey(player))
        {
            activeBoosts[player].Reset();
            return;
        }

        // Create a new timer to end the boost after BoostDuration
        Timer boostTimer = new Timer { Interval = BoostDuration };
        boostTimer.Timeout += () =>
        {
            activeBoosts.Remove(player);
            speedUpTimer.Stop(); // Stop boosting if it's still running

            // **Gradual Deceleration Timer**
            Timer decelerationTimer = new Timer { Interval = 0.05 }; // Smooth reduction
            decelerationTimer.Timeout += () =>
            {
                // **Check if player changed direction**
                if (HasPlayerChangedDirection(player, boostDirection))
                {
                    decelerationTimer.Stop(); // Stop the deceleration completely
                    return;
                }

                // **Only slow down if player is NOT pressing movement keys**
                if (!IsPlayerMovingManually(player))
                {
                    if (Math.Abs(player.Velocity.X) > maxSpeed)
                    {
                        player.Velocity -= new Vector(DecelerationRate * boostDirection, 0);
                        if (Math.Abs(player.Velocity.X) <= maxSpeed)
                        {
                            player.Velocity = new Vector(maxSpeed * boostDirection, player.Velocity.Y);
                            decelerationTimer.Stop();
                        }
                    }
                }
                else
                {
                    decelerationTimer.Stop(); // Stop decelerating if player moves manually
                }
            };
            decelerationTimer.Start();
        };
        boostTimer.Start();

        activeBoosts[player] = boostTimer;
    }

    /// <summary>
    /// Checks if the player is actively moving (pressing left or right).
    /// </summary>
    private static bool IsPlayerMovingManually(PhysicsObject player)
    {
        return Math.Abs(player.Acceleration.X) > 0; // If acceleration is applied, player is controlling movement
    }

    /// <summary>
    /// Checks if the player has changed direction (moving opposite to the original boost direction).
    /// </summary>
    private static bool HasPlayerChangedDirection(PhysicsObject player, double boostDirection)
    {
        return (boostDirection > 0 && player.Velocity.X < 0) || (boostDirection < 0 && player.Velocity.X > 0);
    }
}
