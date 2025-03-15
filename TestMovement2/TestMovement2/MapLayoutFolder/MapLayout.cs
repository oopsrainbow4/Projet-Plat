namespace TestMovement2.MapLayoutFolder;

/// <summary>
/// Stores the map design as a layout for easy modifications.
/// </summary>
public class MapLayout
{
    /// "#" Land
    /// "^" Spike
    /// "+" Healing Box
    /// "S" Player's Spawn Point
    /// "L" Lava
    /// "E" Basic Enemy
    /// "w" Water
    /// "s" SpeedBoost
    /// "j" JumpPad
    public string[] GetLayout()
    {
        return
        [
            "                            S ",
            "                                                  ",
            "                           +        ",
            "   E                       ##                     ",
            "       s  j     +     L   ##  #^#              s   ",
            "############wwwwwww####################^^####################################### ",
            "        ####wwwwwwww#########",
            "            wwwwwww"
        ];
    }
}