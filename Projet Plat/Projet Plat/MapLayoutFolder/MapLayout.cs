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
            "                                                                                                                                     ##                                                                                                                                                                                                                                                                                      ",
            "                                                                                                                                                                                                             w                                                                                                                                                                                                            ",
            "                                                                                                                                         ##                    ##                                            w                   w                    w                                                                                                                                                                      ",
            "                                                                                                                                                               ##                                                                w                    w                 LLLLLLLLL                        ",
            "                                                                                                                                                                                                 w                                                                                              ",
            "                                                                                                                                                                                                 w                                                                          rrr                 ",
            "                                                                                                                                                                        S                    c                                                                              r rr                ",
            "                                                                                                                                                                      ########################                                                                            F rrr                 ",
            "                                                                                                                                                                      #                                                                                                 rrrrrr   ",
            "                                                                                                                                                                      #                                                                                                rrrrrrr     ",
            "                                                                                                                                                             j        #                                                                                                 rrrrr     ",
            "                                              #                                                                                                              #        #                                                                                                 r   r",
            "                                              #                                                                                                              #        #                                                                                                 r   r",
            "                                              #                                                                                                              #^^^^^^^^#                                                                                                 rr  rr                 ",   
            "                      ##                      #                                                                                                              ##########                                                                       ",
            "                      ##                      #                                                                                                              #                                                                                      ",
            "        B      ##     ##       ^^   +    j    #         c     LLL      c      s                  s                              E             +     c      j #                                                                                                                 ", 
            "#######################################################################################################wwwwwwwwwwwwww#########################################                                                                                                                                         ",
            "                                                                                                       wwwwwwwwwwwwww",
            "                                                                                                                                         ",
            "                                                                                                                                         ",
            "                                                                                                                                         ",
            "                                                                                                                                         ",
            "                                                                                                                                         ",
            "                                                                                                                                        ",
            "                                                                                                                           L              ",
            "                                                                                                                           L              ",
            "                                                                                                                           #           ",
            "                                                                                                                           #            ",
            "                                                                                                                           #                                            ",
            "                                                                                                                           #                                  ^^^^             F",
            "                                                                                                                           #                            ##############        ###    ",
            "                                                                   ^  E                     j                c             #                    ##    ",
            "                                                                ############LLLLLLLLLLLLLL################################## ",
            "                                                                                                                                                                                                                                                                                                                                           ",
            "                                                                                                                                         ",
            "                                                                                                                                         ",
            "                                                                                                                                                       ##  ",
            "                                                                                                                                  c                           ",
            "                                                          ^^^^           E        E        +     c        ##                     ##           ^^                 ",
            "                                                          ############################################                                        ##      ##        ",
            "                                                                                                             ##          ##                                 ",
            "                                                                                                                                                           ",
            "                                                                                                                                                           ## ",
            "                                                                                                                                       ##                  ",
            "                                                                                                                                                  ##",
            "                                                                                                                                         ",
        ]; 
    }
}

