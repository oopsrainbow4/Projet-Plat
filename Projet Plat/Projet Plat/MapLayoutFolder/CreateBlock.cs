using Jypeli;

namespace Projet_Plat.MapLayoutFolder;

/// <summary>
/// Handles creating blocks with predefined metadata.
/// </summary>
public class CreateBlock
{
    public PhysicsObject CreateBlocks(double x, double y, BlockModule.BlockType blockType)
    {
        var data = BlockModule.BlockInfo[blockType];
        PhysicsObject block = PhysicsObject.CreateStaticObject(data.Width, data.Height);
        block.Shape = data.Shape;
        block.X = x;
        block.Y = y;
        block.Tag = data.Tag;
        block.Image = data.Image;

        if (data.IgnoresCollision)
            block.IgnoresCollisionResponse = true;
        
        return block;
    }
}