namespace TestMovement2.MapLayoutFolder;

/// <summary>
/// Stores the map design as a layout for easy modifications.
/// </summary>
public class MapLayout
{
    /// "#" Land
    /// "^" Spike
    /// "+" Healing Box
    /// "s" Player's Spawn Point
    /// "L" Lava
    /// "E" Basic Enemy
    /// "w" Water
    public string[] GetLayout()
    {
        return
        [
            "                          s ",
            "                 ",
            "                    +        ",
            "                   ##              E       E",
            "        +     L   ##  #^#        ",
            "####wwwwwww####################^^####################################### ",
            "####wwwwwwww#########",
            "####################"
        ];
    }
}