using Jypeli;
using Vector = Jypeli.Vector;

namespace TestMovement2.PlayerSetup;

/// <summary>
/// Handles player respawn logic when they lose all HP or fall off the map.
/// Restores player HP, subtracts a life, and moves them back to the latest checkpoint or spawn.
/// Shows Game Over and disables player movement if lives reach zero.
/// </summary>
public class Respawn
{
    private readonly PhysicsObject player;
    private readonly IntMeter playerHP;
    private readonly IntMeter playerLives;
    private Vector spawnPoint;
    private Timer respawnTimer;

    /// <summary>
    /// Constructor for Respawn class.
    /// </summary>
    /// <param name="player">The player's PhysicsObject</param>
    /// <param name="playerHP">The player's HP meter</param>
    /// <param name="playerLives">The player's lives meter</param>
    /// <param name="spawnPoint">Current active respawn point</param>
    public Respawn(PhysicsObject player, IntMeter playerHP, IntMeter playerLives,Vector spawnPoint)
    {
        this.player = player;
        this.playerHP = playerHP;
        this.playerLives = playerLives;
        this.spawnPoint = spawnPoint;
    }

    /// <summary>
    /// Starts a repeating timer to check if the player should be respawned.
    /// Called in Main.cs when the game starts.
    /// </summary>
    /// <param name="interval">How often to check (default 0.1 seconds)</param>
    public void StartRespawnTimer(double interval = 0.1)
    {
        respawnTimer = new Timer{ Interval = interval }; // Set timer interval
        respawnTimer.Timeout += CheckRespawnConditions;
        respawnTimer.Start();
    }

    /// <summary>
    /// Checks if player is dead (HP = 0) or has fallen below the map.
    /// If so, calls the RespawnPlayer() function.
    /// </summary>
    private void CheckRespawnConditions()
    {
        // Check if the player needs to be respawned
        if (playerHP.Value == 0 || player.Y < -1000) // HP is 0 or player fell off the map
        {
            RespawnPlayer();
        }
    }

    /// <summary>
    /// Handles the logic for respawning the player.
    /// If the player has lives left, restore HP and move them to the spawn.
    /// If no lives left, stop movement and show Game Over.
    /// </summary>
    private void RespawnPlayer()
    {
        if (playerLives.Value > 0)
        {
            player.Position = spawnPoint + new Vector(0, 10); // Reset position to spawn point
            player.Velocity = Vector.Zero; // Stop movement
            playerHP.Value = CreatePlayer.MAX_HP; // Restore HP
            playerLives.Value--; // Lose one life
        }
        else
        {
            GameOver();
        }
    }

    /// <summary>
    /// Disables player movement and shows Game Over text.
    /// Called when the player has no more lives left.
    /// </summary>
    private void GameOver()
    {
        player.Velocity = Vector.Zero;
        player.Stop();
        player.IgnoresPhysicsLogics = true; // Stops gravity, movement, etc.
        Game.Instance.MessageDisplay.Add("Game Over :)"); // Show Game Over message
        StopRespawnTimer();
    }

    /// <summary>
    /// Stops the timer that checks for respawn.
    /// Useful if you want to pause it during cutscenes, game pause, or after Game Over.
    /// </summary>
    public void StopRespawnTimer()
    {
        if (respawnTimer != null)
        {
            respawnTimer.Stop();
            respawnTimer = null;
        }
    }
    
    // Allows checkpoint to change spawn position
    public void SetSpawnPoint(Vector newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }
}