#nullable enable
using Jypeli;
using Projet_Plat.Image_Sound_Storage;
using Projet_Plat.PlayerSetup;

namespace Projet_Plat.MapLayoutFolder.BlockSystem;

/// <summary>
/// Handles checkpoint behavior when the player touches a checkpoint block.
/// Only one checkpoint can be active at a time.
/// </summary>
public static class CheckpointModule
{
    // Stores a reference to the currently active checkpoint (green flag)
    private static PhysicsObject? activeCheckpoint;

    /// <summary>
    /// Called when the player touches a checkpoint.
    /// Changes the flag color, plays a sound, and updates the respawn location.
    /// </summary>
    /// <param name="playerMain">Reference to the MovementMain class that holds the respawn position.</param>
    /// <param name="checkpointBlock">The checkpoint block the player touched.</param>
    /// <param name="respawnSystem">The respawn system to update spawn point</param>
    public static void HandleCheckpointCollision(MovementMain playerMain, PhysicsObject checkpointBlock, Respawn respawnSystem)
    {
        // Do nothing if this checkpoint is already active
        if (checkpointBlock == activeCheckpoint)
            return;

        // If there’s an old checkpoint, revert its image back to the red flag
        if (activeCheckpoint != null)
            activeCheckpoint.Image = ImageModule.RedFlagImage;
        

        // Set the new checkpoint image to green and mark it as active
        checkpointBlock.Image = ImageModule.GreenFlagImage;
        activeCheckpoint = checkpointBlock;

        // Update the player's respawn point to this checkpoint's position
        respawnSystem.SetSpawnPoint(checkpointBlock.Position);

        // Play checkpoint activation sound
        SoundModule.PlaySoundEffect(SoundData.Checkpoint);
    }
}

