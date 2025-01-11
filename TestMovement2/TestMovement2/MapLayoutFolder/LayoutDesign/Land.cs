using Jypeli;

namespace TestMovement2.MapLayoutFolder.LayoutDesign;

/// <summary>
/// Handles the creation of land blocks in the game world.
/// </summary>
public class Land
{
    private PhysicsGame game;

    public Land(PhysicsGame gameInstance)
    {
        game = gameInstance;
    }
    
    /// <summary>
    /// Creates and adds a land block to the game.
    /// </summary>
    public void CreateLandBlock(double x, double y, double width, double height)
    {
        PhysicsObject landBlock = PhysicsObject.CreateStaticObject(width, height);
        landBlock.Shape = Shape.Rectangle;
        landBlock.Color = Color.Gray;
        landBlock.X = x;
        landBlock.Y = y;
        landBlock.Tag = "Land";
        
        game.Add(landBlock);
    }
}