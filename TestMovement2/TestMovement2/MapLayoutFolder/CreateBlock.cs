using Jypeli;

namespace TestMovement2.MapLayoutFolder;

/// <summary>
/// Handles creating blocks with predefined metadata.
/// </summary>
public class CreateBlock
{
    private readonly PhysicsGame game;
    public CreateBlock(PhysicsGame gameInstance)
    {
        game = gameInstance;
    }

    /// <summary>
    /// Creates a block of the specified type at the given position.
    /// </summary>
    /// <param name="x">The x-coordinate.</param>
    /// <param name="y">The y-coordinate.</param>
    /// <param name="blockType">The type of block to create.</param>
    public void CreateBlocks(double x, double y, BlockModule.BlockType blockType)
    {
        var (tag, imagePath, shape, width, height) = BlockModule.BlockInfo[blockType];

        // Create and configure the block
        PhysicsObject block = PhysicsObject.CreateStaticObject(width, height);
        block.Shape = shape;
        block.X = x;
        block.Y = y;
        block.Tag = tag;

        if (!string.IsNullOrEmpty(imagePath))
        {
            block.Image = Game.LoadImage(imagePath);
        }

        game.Add(block);
    }
}