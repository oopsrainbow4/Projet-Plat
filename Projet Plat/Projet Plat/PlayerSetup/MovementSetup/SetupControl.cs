using Jypeli;

namespace Projet_Plat.PlayerSetup;

/// <summary>
/// Handles setting up player controls for movement and jumping.
/// </summary>
public partial class MovementMain
{
    /// <summary>
    /// Sets up keyboard controls for the player's movement and actions.
    /// </summary>
    public void SetupControls()
    {
        game.Keyboard.Listen(Key.D, ButtonState.Down, MoveRight,null);
        game.Keyboard.Listen(Key.A, ButtonState.Down, MoveLeft, null);
        game.Keyboard.Listen(Key.Space, ButtonState.Pressed, Jump, null);
        
        // Track when the jump key is released (to prevent holding the jump key)
        game.Keyboard.Listen(Key.Space, ButtonState.Released, OnJumpKeyRelease, null);
    }
    
    /// <summary>
    /// Moves the player to the right by increasing their horizontal velocity.
    /// </summary>
    private void MoveRight()
    {
        if (player == null) return;
        
        // Only increase speed if below the maximum speed
        if (player.Velocity.X < MAX_SPEED)
            player.Velocity += new Vector(ACCELERATION_RATE, 0); // Apply acceleration
    }
    
    /// <summary>
    /// Moves the player to the left by decreasing their horizontal velocity.
    /// </summary>
    private void MoveLeft()
    {
        if (player == null) return;
        
        // Only decrease speed if above the negative maximum speed
        if (player.Velocity.X > -MAX_SPEED)
            player.Velocity += new Vector(-ACCELERATION_RATE, 0); // Apply negative acceleration
    }
    
    /// <summary>
    /// Makes the player jump, allowing for a double jump if conditions are met.
    /// </summary>
    private void Jump()
    {
        if (!isJumpKeyReleased) return; // Prevent holding the jump key

        if (isOnGround)
        {
            // Perform the first jump
            player.Velocity = new Vector(player.Velocity.X, JUMP_HEIGHT); 
            isDoubleJumpingAllowed = true; // Allow a double jump after the first jump
            isOnGround = false;            // The player is now in the air
        }
        else if (isDoubleJumpingAllowed)
        {
            // Perform the double jump
            player.Velocity = new Vector(player.Velocity.X, JUMP_HEIGHT); 
            isDoubleJumpingAllowed = false; // Disable further jumps until grounded
        }
        isJumpKeyReleased = false; // Prevent holding the jump key
    }
    
    /// <summary>
    /// Tracks when the jump key is released, allowing the player to jump again.
    /// </summary>
    private void OnJumpKeyRelease()
    {
        isJumpKeyReleased = true; // Allow jumping when the key is released
    }
}