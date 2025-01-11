using Jypeli;

namespace TestMovement2.MapLayoutFolder.LayoutDesign;

/// <summary>
/// Handles the creation of land blocks in the game world.
/// </summary>
public class Block
{
    private PhysicsGame game;

    public Block(PhysicsGame gameInstance)
    {
        game = gameInstance;
    }
    
    /// <summary>
    /// Creates and adds a land block to the game.
    /// </summary>
    public void CreateLandBlock(double x, double y, double width, double height)
    {
        PhysicsObject Block = PhysicsObject.CreateStaticObject(width, height);
        Block.Shape = Shape.Rectangle;
        Block.Color = Color.Gray;
        Block.X = x;
        Block.Y = y;
        Block.Tag = "Block";
        
        game.Add(Block);
    }
}