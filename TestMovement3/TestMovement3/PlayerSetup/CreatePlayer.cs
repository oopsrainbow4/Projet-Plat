using Jypeli;

namespace TestMovement3.PlayerSetup;

/// <summary>
/// Represents the player in the game, encapsulating its properties and logic.
/// </summary>
public class CreatePlayer
{
    private PhysicsObject player;
    public IntMeter playerHP;

    public const int MAX_HP = 5;

    public void Setup(Game game, Vector spawnPosition)
    {
        // Create the player (a block with width and height)
        player = new PhysicsObject(64, 64); // Size of the block
        
        Image playerImage = Game.LoadImage("Images/PlayerImages/Yellow.png");
        player.Image = playerImage;
        
        // Set initial position to the spawn point
        player.X = spawnPosition.X;
        player.Y = spawnPosition.Y;
        player.Mass = 1;
        player.Restitution = 0.2; // Slight bounce
        player.Tag = "Player";
        game.Add(player);

        // Initialize player's HP
        playerHP = new IntMeter(MAX_HP, 0, MAX_HP); // 5 max HP, minimum 0

        // Add HP display (GUI)
        Label hpLabel = new Label
        {
            TextColor = Color.Black,
            Position = new Vector(300, 300),
            Text = "HP: " + playerHP.Value
        };
        game.Add(hpLabel);

        // Update the GUI whenever HP changes
        playerHP.Changed += delegate
        {
            hpLabel.Text = "HP: " + playerHP.Value;
        };
    }

    public PhysicsObject GetPlayerObject()
    {
        return player;
    }
}