using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;

namespace TestMovement2.PlayerSetup;

/// <summary>
/// Represents the player in the game, encapsulating its properties and logic.
/// </summary>
public class CreatePlayer
{
    private PhysicsObject player;
    public IntMeter playerHP;

    public void Setup(Game game)
    {
        // Create the player (a block with width and height)
        player = new PhysicsObject(50, 50); // Size of the block
        
        Image playerimage = Game.LoadImage("PlayerImages/Yellow.png");
        player.Image = playerimage;
        
        player.X = 0;
        player.Y = 0;
        player.Mass = 1;
        player.Restitution = 0.2; // Slight bounce
        game.Add(player);

        // Initialize player's HP
        playerHP = new IntMeter(3, 0, 3); // 3 max HP, minimum 0

        // Add HP display (GUI)
        Label hpLabel = new Label
        {
            TextColor = Color.Black,
            Position = new Vector(300, 300),
            Text = "HP: " + playerHP.Value
        };
        game.Add(hpLabel);

        // Update the GUI whenever HP changes
        playerHP.Changed += () =>
        {
            hpLabel.Text = "HP: " + playerHP.Value;
        };
    }

    public PhysicsObject GetPlayerObject()
    {
        return player;
    }
}