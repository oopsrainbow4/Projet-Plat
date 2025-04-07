using Jypeli;

namespace TestMovement3.Image_Sound_Storage;

/// <summary>
/// Handles centralized image loading for easy access throughout the game.
/// </summary>
public static class ImageModule
{
    public static readonly Image PlayerImage = Game.LoadImage("Images/PlayerImages/Yellow.png");
    public static readonly Image EnemyImage = Game.LoadImage("Images/EnemyImages/BasicEnemy.png");
    public static readonly Image BackgroundImage = Game.LoadImage("Images/BackgroundImages/Background.png");
    
    // Load Block Images
    public static readonly Image BlockImage = Game.LoadImage("Images/MapLayoutImages/Grass.png");
    public static readonly Image LavaImage = Game.LoadImage("Images/MapLayoutImages/Lava.png");
    public static readonly Image HealingBoxImage = Game.LoadImage("Images/MapLayoutImages/HealingBox2.png");
    public static readonly Image SpikeImage = Game.LoadImage("Images/MapLayoutImages/Spike3.png");
    public static readonly Image WaterImage = Game.LoadImage("Images/MapLayoutImages/Water.png");
    
    public static readonly Image SpeedBoostImage = Game.LoadImage("Images/MapLayoutImages/SpeedBoost.png");
    public static readonly Image JumpPadImage = Game.LoadImage("Images/MapLayoutImages/JumpPad.png");
    
    // Load Checkpoint's block Images
    public static readonly Image RedFlagImage = Game.LoadImage("Images/CheckpointImages/Red_Flag.png");
    public static readonly Image GreenFlagImage = Game.LoadImage("Images/CheckpointImages/Green_Flag.png");
}