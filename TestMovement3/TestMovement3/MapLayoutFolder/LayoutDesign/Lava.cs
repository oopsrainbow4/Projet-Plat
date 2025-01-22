using Jypeli;

namespace TestMovement3.MapLayoutFolder.LayoutDesign;

/// <summary>
/// Handles the creation of lava blocks in the game world.
/// </summary>
public class Lava
{
    private PhysicsGame game;

    public Lava(PhysicsGame gameInstance)
    {
        game = gameInstance;
    }

    /// <summary>
    /// Creates and adds a lava object to the game.
    /// </summary>
    public void CreateLava(double x, double y, double width, double height)
    {
        PhysicsObject lava = PhysicsObject.CreateStaticObject(width, height);
        lava.Shape = Shape.Rectangle;
        lava.Color = Color.Red;
        lava.X = x;
        lava.Y = y;
        lava.Tag = "Lava";

        game.Add(lava);
    }
}