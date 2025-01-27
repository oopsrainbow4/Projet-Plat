using Jypeli;

namespace TestMovement2.MapLayoutFolder;

/// <summary>
/// Contains important map-related logic like generating the map from a layout.
/// </summary>
public class MapModule
{
    private readonly PhysicsGame game;
    private readonly CreateBlock createBlock;
    private Vector spawnPoint; // Store the spawn point coordinates

    public MapModule(PhysicsGame gameInstance)
    {
        game = gameInstance;
        createBlock = new CreateBlock(gameInstance); // Use CreateBlock for all object types
    }

    /// <summary>
    /// Get the player spawn point.
    /// </summary>
    public Vector GetSpawnPoint()
    {
        return spawnPoint;
    }
    
    /// <summary>
    /// Parses the layout and places objects in the game world.
    /// </summary>
    /// <param name="layout">A string array representing the map layout.</param>
    public void GenerateMap(string[] layout)
    {
        double blockWidth = 50; // Width of one block
        double blockHeight = 50; // Height of one block

        for (int y = 0; y < layout.Length; y++)
        {
            string line = layout[y];
            for (int x = 0; x < line.Length; x++)
            {
                char tile = line[x];
                
                // Calculate block position
                double posX = x * blockWidth - (layout[0].Length / 1.99) * blockWidth;
                double posY = -(y * blockHeight - (layout.Length / 1.99) * blockHeight);

                switch (tile)
                {
                    case '#': // Land
                        createBlock.CreateBlocks(posX, posY, BlockModule.BlockType.Land);
                        break;
                    case '^': // Spike
                        createBlock.CreateBlocks(posX, posY, BlockModule.BlockType.Spike);
                        break;
                    case '+': // Healing Box
                        createBlock.CreateBlocks(posX, posY, BlockModule.BlockType.HealingBox);
                        break;
                    case 'L': // Lava
                        createBlock.CreateBlocks(posX, posY, BlockModule.BlockType.Lava);
                        break;
                    case 's': // Spawn point
                        spawnPoint = new Vector(posX, posY);
                        break;
                }
            }
        }
    }
}