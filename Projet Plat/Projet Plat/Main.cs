using Jypeli;

using Projet_Plat.PlayerSetup;
using Projet_Plat.MapLayoutFolder;

namespace Projet_Plat;

/// @author gr301847
/// @version 17.11.2024
/// <summary>
/// The main game script that initializes the game and manages the core components like the player,
/// movement, and environment.
/// </summary>
public class Main : PhysicsGame
{
    private CreatePlayer createPlayer;
    private MovementMain _movementMain;
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
        _movementMain = new MovementMain(createPlayer.GetPlayerObject(), this);

        // Initialize environment and set up controls
        environment = new Environment();
        environment.Setup(this); // Adds gravity, background and creates the floor
        environment.SetPlayer(createPlayer.GetPlayerObject());
            
        // Set up collision events between the player and the floor
        _movementMain.SetupCollisionEvents(createPlayer.GetPlayerObject(), createPlayer.playerHP);

        // Start deceleration logic and controls
        _movementMain.SetupControls();
        _movementMain.DecelerationTimer();
        
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