using Jypeli;

namespace Projet_Plat.Image_Sound_Storage;

/// <summary>
/// Handles centralized image loading for easy access throughout the game.
/// </summary>
public static class ImageModule
{
    public static readonly Image PlayerImage = Game.LoadImage("Images/PlayerImages/Bunny.png");
    public static readonly Image EnemyImage = Game.LoadImage("Images/EnemyImages/Feral_Bunny.png");
    public static readonly Image BackgroundImage = Game.LoadImage("Images/BackgroundImages/Background.png");
    
    public static readonly Image FinnishFlagImage = Game.LoadImage("Images/MapLayoutImages/FinnishFlag.png");
    
    // Load Block Images
    public static readonly Image LavaImage = Game.LoadImage("Images/MapLayoutImages/Lava.png");
    public static readonly Image HealingBoxImage = Game.LoadImage("Images/MapLayoutImages/HealingBox2.png");
    public static readonly Image SpikeImage = Game.LoadImage("Images/MapLayoutImages/Spike3.png");
    public static readonly Image WaterImage = Game.LoadImage("Images/MapLayoutImages/Water.png");
    public static readonly Image SpeedBoostImage = Game.LoadImage("Images/MapLayoutImages/SpeedBoost.png");
    public static readonly Image JumpPadImage = Game.LoadImage("Images/MapLayoutImages/JumpPad.png");
    public static readonly Image StoneImage = Game.LoadImage("Images/MapLayoutImages/Stone.png");
    public static readonly Image BoardImage = Game.LoadImage("Images/MapLayoutImages/Board.png");
    
    // Load Land Images
    public static readonly Image GrassImage = Game.LoadImage("Images/MapLayoutImages/Grass.png");
    public static readonly Image DirtImage = Game.LoadImage("Images/MapLayoutImages/Dirt.png");
    
    // Load Checkpoint's block Images
    public static readonly Image RedFlagImage = Game.LoadImage("Images/CheckpointImages/Red_Flag2.png");
    public static readonly Image GreenFlagImage = Game.LoadImage("Images/CheckpointImages/Green_Flag2.png");
}

