using Jypeli;

using TestMovement2.PlayerSetup;

namespace TestMovement2;

/// @author gr301847
/// @version 17.11.2024
/// <summary>
/// The main game script that initializes the game and manages the core components like the player,
/// movement, and environment.
/// </summary>
public class Main : PhysicsGame
{
    private Player player;
    private Movement movement;
    private CameraSetup cameraSetup;
    
    private Environment environment;
    
    public override void Begin()
    {
        // Initialize the player
        player = new Player();
        player.Setup(this); // Creates the player's PhysicsObject and adds it to the game

        // Initialize movement system with the player's object
        movement = new Movement(player.GetPlayerObject(), this);

        // Initialize environment and set up controls
        environment = new Environment();
        environment.Setup(this); // Adds gravity, background and creates the floor
        environment.SetPlayer(player.GetPlayerObject());
            
        // Set up collision events between the player and the floor
        movement.SetupCollisionEvents(player.GetPlayerObject(), environment.GetFloor());

        // Start deceleration logic and controls
        movement.SetupControls();
        movement.DecelerationTimer();
        
        // Initialize the camera setup
        cameraSetup = new CameraSetup(player.GetPlayerObject(), this);
        cameraSetup.SetupCamera(); // Attach the camera to the player
        cameraSetup.SetZoom(0.8);  // Optional: Adjust the zoom level
    }
}