using Jypeli;
using Vector = Jypeli.Vector;

namespace Projet_Plat.PlayerSetup;

/// <summary>
/// Handles the camera logic to follow the player smoothly.
/// </summary>
public class CameraSetup
{
    private readonly PhysicsObject player; // Reference to the player object
    private readonly Game game; // Reference to the game instance
    private readonly Timer cameraUpdateTimer; // Timer to update the camera position

    public CameraSetup(PhysicsObject playerObject, Game gameInstance)
    {
        player = playerObject;
        game = gameInstance;

        // Initialize a timer for smooth camera updates
        cameraUpdateTimer = new Timer
        {
            Interval = 0.02 // Update every 20ms
        };
        cameraUpdateTimer.Timeout += UpdateCameraPosition;
    }

    /// <summary>
    /// Sets up the camera to follow the player.
    /// </summary>
    public void SetupCamera()
    {
        game.Camera.Follow(player); // Make the camera follow the player
        cameraUpdateTimer.Start(); // Start the timer for smooth updates
    }

    /// <summary>
    /// Manually adjusts the zoom level of the camera.
    /// </summary>
    /// <param name="zoomLevel">The desired zoom level.</param>
    public void SetZoom(double zoomLevel)
    {
        game.Camera.ZoomFactor = zoomLevel; // Set the zoom level of the camera
    }

    /// <summary>
    /// Smoothly updates the camera position to follow the player.
    /// </summary>
    private void UpdateCameraPosition()
    {
        Vector targetPosition = player.Position;
        Vector currentPosition = game.Camera.Position;

        // Interpolate between current position and target position
        if ((targetPosition - currentPosition).Magnitude > 0.1)
        { 
            double smoothingFactor = 0.1; // Adjust this value for smoother or quicker movement
            Vector newPosition = currentPosition + (targetPosition - currentPosition) * smoothingFactor;
            game.Camera.Position = newPosition;   
        }
    }
}

