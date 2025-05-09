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
    /// "r" Stone
    /// "F" End game's goal (Finnish Flag)
    public string[] GetLayout()
    {
        return
        [
            "                                                                                                                 ",
            "                                              #                                                                                         ",
            "                                              #                                                                                          ",
            "                                              #                                                                                            ",
            "                      ##                      #                                            ",
            "                      ##                      #                                                           ",
            "  S            ##     ##       ^^   +    j    #         c     LLL      c      s                 s                              E             +", 
            "#######################################################################################################wwwwwwwwwwwwww############################# ",
            "                                                                                                       wwwwwwwwwwwwww",
            ""
        ];
    }
}