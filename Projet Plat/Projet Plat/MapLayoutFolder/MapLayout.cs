namespace Projet_Plat.MapLayoutFolder;

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
    /// "c" Checkpoint
    public string[] GetLayout()
    {
        return
        [
            "                            S         c     L      c  ",
            "                                 #####################    ",
            "                           +     ",
            "   E                       ##                     ",
            "       s  j     +     L   ##  #^#              s            c ",
            "############wwwwwww####################^^####################################### ",
            "        ####wwwwwwww#########",
            "            wwwwwww"
        ];
    }
}