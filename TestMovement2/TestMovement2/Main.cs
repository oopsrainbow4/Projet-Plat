using Jypeli;

using TestMovement2.PlayerSetup;
using TestMovement2.MapLayoutFolder;
using TestMovement2.MapLayoutFolder.LayoutDesign;

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
    
    private MapModule mapModule;
    private MapLayout mapLayout;
    
    public override void Begin()
    {
        // Initialize the player
        player = new Player();
        player.Setup(this); // Creates the player's PhysicsObject and adds it to the game

        // Initialize movement system with the player's object
        movement = new Movement(player.GetPlayerObject(), this);

        // Initialize environment and set up controls
        environment = new Environment();
        environment.Setup(this); // Adds gravity and background 
        environment.SetPlayer(player.GetPlayerObject());
            
        // Set up collision events from the player
        movement.SetupCollisionEvents(player.GetPlayerObject());

        // Start deceleration logic and controls
        movement.SetupControls();
        movement.DecelerationTimer();
        
        // Initialize the camera setup
        cameraSetup = new CameraSetup(player.GetPlayerObject(), this);
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