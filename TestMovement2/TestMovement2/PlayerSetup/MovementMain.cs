using Jypeli;

namespace TestMovement2.PlayerSetup;

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
    private readonly double MAX_SPEED = 1000;       // Maximum horizontal speed
    private readonly double ACCELERATION_RATE = 20; // Speed increment when moving
    private readonly double JUMP_HEIGHT = 500.0;    // Vertical jump height
    
    // States to track player movement and jumping
    private bool isDoubleJumpingAllowed; // Whether the player can perform a double jump
    private bool isJumpKeyReleased;      // To ensure the jump key isn't held down
    private bool isOnGround;             // Tracks whether the player is grounded
    
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
}