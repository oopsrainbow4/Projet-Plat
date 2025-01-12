using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;

namespace TestMovement3.PlayerSetup;

/// <summary>
/// Represents the player in the game, encapsulating its properties and logic.
/// </summary>
public class CreatePlayer
{
    private PhysicsObject player;

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
    }

    public PhysicsObject GetPlayerObject()
    {
        return player;
    }
}