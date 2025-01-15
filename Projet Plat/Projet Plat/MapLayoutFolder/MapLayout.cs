namespace Projet_Plat.MapLayoutFolder;

/// <summary>
/// Stores the map design as a layout for easy modifications.
/// </summary>
public class MapLayout
{
    /// "#" Land
    /// "^" Spike
    /// "+" Healing Box
    public string[] GetLayout()
    {
        return new[]
        {
            "                 ",
            "                 ",
            "                 ",
            "  ##             ",
            " ##  #^#   +      ",
            "#####  #######^^ "
        };
    }
}