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
    public void CreateBlocks(double x, double y, BlockModule.BlockType blockType, Image cachedImage)
    {
        PhysicsObject block = CreateBaseBlock(x, y, blockType, cachedImage);

        // Special cases for certain blocks
        if (blockType == BlockModule.BlockType.HealingBox)
        { 
            // Removes bump effect so player get stop like hit a wall
            block.IgnoresCollisionResponse = true;
        }
        
        game.Add(block);
    }

    /// <summary>
    /// Creates a block object without adding it to the game (for batching).
    /// </summary>
    public PhysicsObject CreateBlockObject(double x, double y, BlockModule.BlockType blockType, Image cachedImage)
    {
        PhysicsObject block = CreateBaseBlock(x, y, blockType, cachedImage);

        // Special cases for certain blocks
        if (blockType == BlockModule.BlockType.Water)
        {
            block.IgnoresCollisionResponse = true;
        }
        return block;
    }
    
    private PhysicsObject CreateBaseBlock(double x, double y, BlockModule.BlockType blockType, Image cachedImage)
    {
        var (tag, _, shape, width, height) = BlockModule.BlockInfo[blockType];

        PhysicsObject block = PhysicsObject.CreateStaticObject(width, height);
        block.Shape = shape;
        block.X = x;
        block.Y = y;
        block.Tag = tag;
        block.Image = cachedImage;

        return block;
    }
}