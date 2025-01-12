using Jypeli;

using TestMovement3.PlayerSetup;
using TestMovement3.MapLayoutFolder;

namespace TestMovement3;

/// @author gr301847
/// @version 17.11.2024
/// <summary>
/// The main game script that initializes the game and manages the core components like the player,
/// movement, and environment.
/// </summary>
public class Main : PhysicsGame
{
    private CreatePlayer createPlayer;
    private Movement movement;
    private CameraSetup cameraSetup;
    
    private Environment environment;
    
    private MapModule mapModule;
    private MapLayout mapLayout;
    
    public override void Begin()
    {
        // Initialize the player
        createPlayer = new CreatePlayer();
        createPlayer.Setup(this); // Creates the player's PhysicsObject and adds it to the game

        // Initialize movement system with the player's object
        movement = new Movement(createPlayer.GetPlayerObject(), this);

        // Initialize environment and set up controls
        environment = new Environment();
        environment.Setup(this); // Adds gravity, background and creates the floor
        environment.SetPlayer(createPlayer.GetPlayerObject());
            
        // Set up collision events between the player and the floor
        movement.SetupCollisionEvents(createPlayer.GetPlayerObject());

        // Start deceleration logic and controls
        movement.SetupControls();
        movement.DecelerationTimer();
        
        // Initialize the camera setup
        cameraSetup = new CameraSetup(createPlayer.GetPlayerObject(), this);
        cameraSetup.SetupCamera(); // Attach the camera to the player
        cameraSetup.SetZoom(0.8);  // Optional: Adjust the zoom level
        
        // Initialize the map layout system
        mapLayout = new MapLayout();
        mapModule = new MapModule(this);
        
        // Generate the map from the layout
        string[] layout = mapLayout.GetLayout();
        mapModule.GenerateMap(layout);
    }
}