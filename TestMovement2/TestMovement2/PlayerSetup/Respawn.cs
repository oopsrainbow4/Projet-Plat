using Jypeli;
using Vector = Jypeli.Vector;

namespace TestMovement2.PlayerSetup;

/// <summary>
/// Handles respawn logic, including checking player HP and position.
/// </summary>
public class Respawn
{
    private readonly PhysicsObject player;
    private readonly IntMeter playerHP;
    private readonly Vector spawnPoint;
    private Timer respawnTimer;

    private const int MAX_HP = 5;

    public Respawn(PhysicsObject player, IntMeter playerHP, Vector spawnPoint)
    {
        this.player = player;
        this.playerHP = playerHP;
        this.spawnPoint = spawnPoint;
    }

    public void StartRespawnTimer(double interval = 0.1)
    {
        respawnTimer = new Timer{ Interval = interval }; // Set timer interval
        respawnTimer.Timeout += CheckRespawnConditions;
        respawnTimer.Start();
    }

    private void CheckRespawnConditions()
    {
        // Check if the player needs to be respawned
        if (playerHP.Value == 0 || player.Y < -1000) // HP is 0 or player fell off the map
        {
            RespawnPlayer();
        }
    }

    private void RespawnPlayer()
    {
        player.Position = spawnPoint; // Reset position to spawn point
        player.Velocity = Vector.Zero; // Stop movement
        playerHP.Value = MAX_HP; // Restore HP
    }

    /// <summary>
    /// No longer need to monitor respawn conditions, such as during game over, level transitions, or cutscenes.
    /// Temporarily pause respawn monitoring, such as during invincibility or when the game is paused.
    ///
    /// I leave there just in case if I need it or not.
    /// </summary>
    public void StopRespawnTimer()
    {
        if (respawnTimer != null)
        {
            respawnTimer.Stop();
            respawnTimer = null;
        }
    }
}