using Jypeli;

namespace TestMovement3;

/// <summary>
/// Manages the game's environment, including gravity and static objects like the floor.
/// </summary>
public class Environment
{
    private PhysicsObject floor; // The main floor object where the player stands.
    private PhysicsObject player; // The player object, used to track movement.
    private PhysicsGame game; // The main game instance, used to access game functionality.
    private PhysicsObject backgroundObject; // The background image object.
    private Timer backgroundUpdateTimer; // A timer for updating the background position.

    /// <summary>
    /// Sets up the game environment, including gravity, background, and floor.
    /// </summary>
    /// <param name="gameInstance">The current game instance.</param>
    public void Setup(PhysicsGame gameInstance)
    {
        // Store reference to the game instance for later use.
        game = gameInstance;

        // Set the gravity for the game world. Negative Y indicates gravity pulls downwards.
        game.Gravity = new Vector(0, -1000);

        // Set up the background image and its properties.
        SetupBackground();

        // Create the floor object and add it to the game.
        floor = CreateFloor(game);

        // Start the timer that updates the background position based on the player's movement.
        StartBackgroundUpdateTimer();
    }

    /// <summary>
    /// Sets the player object to track its position for background movement.
    /// </summary>
    /// <param name="playerObject">The player object.</param>
    public void SetPlayer(PhysicsObject playerObject)
    {
        // Store the reference to the player object for later use in background movement.
        player = playerObject;
    }

    /// <summary>
    /// Creates the background object and sets its properties.
    /// </summary>
    private void SetupBackground()
    {
        Image backgroundImage = Game.LoadImage("BackgroundImages/Background.png");

        // Create a physics object for the background and set its size.
        backgroundObject = new PhysicsObject(2000, 1000);
        backgroundObject.Image = backgroundImage;
        
        // Ensure the background does not interact with other objects or respond to gravity.
        backgroundObject.IgnoresCollisionResponse = true;
        backgroundObject.IgnoresGravity = true;

        // Add the background to the game at layer -1, ensuring it appears behind everything else.
        game.Add(backgroundObject, -1);
    }

    /// <summary>
    /// Creates the floor object and adds it to the game.
    /// </summary>
    /// <param name="gameInstance">The current game instance.</param>
    /// <returns>The floor object.</returns>
    private PhysicsObject CreateFloor(Game gameInstance)
    {
        // Create a large static floor object.
        PhysicsObject newFloor = PhysicsObject.CreateStaticObject(10000, 20);
        newFloor.X = 0;
        newFloor.Y = -200;
        newFloor.Shape = Shape.Rectangle;
        newFloor.Color = Color.DarkGray; // Make the floor visible
        newFloor.Tag = "Floor"; // Tag the object for identification.
        gameInstance.Add(newFloor);

        // Create a test object (e.g., a vertical wall or obstacle).
        PhysicsObject testObject = PhysicsObject.CreateStaticObject(20, 100);
        testObject.X = 100;
        testObject.Y = -150;
        testObject.Shape = Shape.Rectangle;
        testObject.Color = Color.DarkGray; // Make the floor visible
        testObject.Tag = "Floor"; // Tag the object for identification.
        gameInstance.Add(testObject);

        return newFloor; // Return the main floor object.
    }

    /// <summary>
    /// Retrieves the floor object.
    /// </summary>
    /// <returns>The floor object.</returns>
    public PhysicsObject GetFloor()
    {
        return floor; // Provide access to the floor object if needed elsewhere.
    }

    /// <summary>
    /// Updates the background position to follow the player's movement.
    /// </summary>
    private void UpdateBackground()
    {
        // If the player is not set, skip the update.
        if (player == null) return;

        // Apply a parallax effect by scaling the player's movement.
        double parallaxFactor = 1.0; // Controls how much slower the background moves compared to the player.
        double offsetX = player.X * parallaxFactor; // Calculate the X position for the background.
        double offsetY = player.Y * parallaxFactor; // Calculate the Y position for the background.

        // Smoothly update the background position to prevent jittery movement.
        double smoothingFactor = 0.1; // Adjust this for smoother or faster updates.
        backgroundObject.X += (offsetX - backgroundObject.X) * smoothingFactor; // Smooth the X position.
        backgroundObject.Y += (offsetY - backgroundObject.Y) * smoothingFactor; // Smooth the Y position.
    }

    /// <summary>
    /// Starts the timer that updates the background position.
    /// </summary>
    private void StartBackgroundUpdateTimer()
    {
        // Create a timer with a short interval to frequently update the background.
        backgroundUpdateTimer = new Timer
        {
            Interval = 0.001 // Update every 1ms for smooth background movement.
        };
        // Attach the UpdateBackground method to the timer's Timeout event.
        backgroundUpdateTimer.Timeout += UpdateBackground;
        backgroundUpdateTimer.Start(); // Start the timer.
    }
}