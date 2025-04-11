using Jypeli;

namespace Projet_Plat.MapLayoutFolder;

/// <summary>
/// Contains metadata for a specific block type.
/// </summary>
public class BlockData
{
    public readonly char Sign; // The char in the map layout
    public readonly string Tag;
    public readonly Image Image;
    public readonly Shape Shape;
    public readonly double Width;
    public readonly double Height;
    public readonly bool IgnoresCollision;
    public readonly bool UsesBatching;

    public BlockData(char sign, string tag, Image image, Shape shape,
        double width, double height, bool ignoresCollision, bool usesBatching)
    {
        Sign = sign;
        Tag = tag;
        Image = image;
        Shape = shape;
        Width = width;
        Height = height;
        IgnoresCollision = ignoresCollision;
        UsesBatching = usesBatching;
    }
}