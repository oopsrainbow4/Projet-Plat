using Jypeli;
using Projet_Plat.EnemyModuleFolder;
using Projet_Plat.Image_Sound_Storage;
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
    private MovementMain movementMain;
    private CameraSetup cameraSetup;
    
    private Environment environment;
    
    private MapModule mapModule;
    private MapLayout mapLayout;
    
    public override void Begin()
    {
        // Initialize the map layout system
        mapLayout = new MapLayout();
        mapModule = new MapModule(this);
                
        // Generate the map from the layout
        string[] layout = mapLayout.GetLayout();
        mapModule.GenerateMap(layout);
        
        // Get the spawn point from the map
        Vector spawnPoint = mapModule.GetSpawnPoint();
        
        // Initialize the player
        createPlayer = new CreatePlayer();
        createPlayer.Setup(this, spawnPoint); // Creates the player's PhysicsObject and adds it to the game
        
        // Spawn enemies after the player exists
        EnemySpawner.SpawnEnemiesAfterPlayer(mapModule);
        
        // Initialize movement system with the player's object
        movementMain = new MovementMain(createPlayer.GetPlayerObject(), this);
        
        SoundModule.LoadSounds();

        // Initialize environment and set up controls
        environment = new Environment();
        environment.Setup(this); // Adds gravity and background 
        environment.SetPlayer(createPlayer.GetPlayerObject());
            
        // Set up collision events from the player
        movementMain.SetupCollisionEvents(createPlayer.GetPlayerObject(), createPlayer.playerHP);

        // Start deceleration logic and controls
        movementMain.SetupControls();
        movementMain.DecelerationTimer();
        
        // Initialize the camera setup
        cameraSetup = new CameraSetup(createPlayer.GetPlayerObject(), this);
        cameraSetup.SetupCamera(); // Attach the camera to the player
        cameraSetup.SetZoom(0.5);  // Optional: Adjust the zoom level
        
        // Initialize respawn logic
        Respawn respawn = new Respawn(createPlayer.GetPlayerObject(), createPlayer.playerHP, spawnPoint);
        respawn.StartRespawnTimer();
    }
}