namespace TestMovement2.MapLayoutFolder;

/// <summary>
/// Stores the map design as a layout for easy modifications.
/// </summary>
public class MapLayout
{
    /// <summary>
    /// Returns the map layout as a string array.
    /// </summary>
    public string[] GetLayout()
    {
        return new[]
        {
            "               ",
            "               ",
            "               ",
            "  ##           ",
            " ##  #^#       ",
            "#####  #######^^ "
        };
    }
}