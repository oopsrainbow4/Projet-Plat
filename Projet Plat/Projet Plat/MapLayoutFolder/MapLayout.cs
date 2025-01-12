namespace Projet_Plat.MapLayoutFolder;

/// <summary>
/// Stores the map design as a layout for easy modifications.
/// </summary>
public class MapLayout
{
    /// <summary>
    /// Returns the map layout as a string array.
    /// </summary>
    
    /// "#" Block
    /// "^" Spike
     
    public string[] GetLayout()
    {
        return new[]
        {
            "                 ",
            "                 ",
            "                 ",
            "  ##             ",
            " ##  #^#         ",
            "#####  #######^^ "
        };
    }
}