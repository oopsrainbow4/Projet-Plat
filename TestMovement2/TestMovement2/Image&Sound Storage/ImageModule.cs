using Jypeli;

namespace TestMovement2.Image_Sound_Storage;

/// <summary>
/// Handles centralized image loading for easy access throughout the game.
/// </summary>
public static class ImageModule
{
    public static readonly Image PlayerImage = Game.LoadImage("Images/PlayerImages/Yellow.png");
    public static readonly Image EnemyImage = Game.LoadImage("Images/EnemyImages/BasicEnemy.png");
    public static readonly Image BackgroundImage = Game.LoadImage("Images/BackgroundImages/Background.png");
}