using Jypeli;

namespace Projet_Plat.PlayerSetup;

/// <summary>
/// Handles the player's movement, including walking, jumping, and smooth deceleration.
/// </summary>
public partial class MovementMain
{
    // Reference to the player's PhysicsObject
    private PhysicsObject player;
    
    // Reference to the main game instance for accessing game-wide functionality
    private Game game; 
    
    // Constants to control movement behavior
    private readonly double MAX_SPEED = 900;       // Maximum horizontal speed
    private readonly double ACCELERATION_RATE = 20; // Speed increment when moving
    private double JUMP_HEIGHT = 500.0;    // Vertical jump height
    private const double DECELERATION_RATE = 10;    // Decelerate speed when stop moving
    
    // States to track player movement and jumping
    private bool isDoubleJumpingAllowed; // Whether the player can perform a double jump
    private bool isJumpKeyReleased;      // To ensure the jump key isn't held down
    private bool isOnGround;             // Tracks whether the player is grounded
    private bool isOnJumpPad;

    /// <summary>
    /// Initializes the Movement class with references to the player object and the game instance.
    /// </summary>
    /// <param name="playerObject">The PhysicsObject representing the player.</param>
    /// <param name="gameInstance">The main game instance.</param>
    public MovementMain(PhysicsObject playerObject, Game gameInstance)
    {
        // Assign the player object and game instance
        player = playerObject;
        game = gameInstance;
    }

    /// <summary>
    /// Enables unlimited jumps when the player is in water.
    /// </summary>
    public void EnableUnlimitedJumps()
    {
        isInWater = true;
        isDoubleJumpingAllowed = true; // Allow continuous jumping in water
        JUMP_HEIGHT = 500.0; // Reduce jump height in water for a floaty effect
    }

    /// <summary>
    /// Disables unlimited jumps when the player leaves water.
    /// </summary>
    public void DisableUnlimitedJumps()
    {
        isInWater = false;
        isDoubleJumpingAllowed = false;
        JUMP_HEIGHT = 500.0; // Reset jump height to normal
    }
}