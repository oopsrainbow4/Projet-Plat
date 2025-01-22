using Jypeli;
using TestMovement2.MapLayoutFolder.LayoutDesign;

namespace TestMovement2.MapLayoutFolder;

/// <summary>
/// Contains important map-related logic like generating the map from a layout.
/// </summary>
public class MapModule
{
    private Vector spawnPoint; // Store the spawn point coordinates
    
    private Block blockCreator;
    private Spike spikeCreator;
    private HealingBox healingBoxCreator;

    public MapModule(PhysicsGame gameInstance)
    {
        blockCreator = new Block(gameInstance); // Initialize the Land module
        spikeCreator = new Spike(gameInstance);
        healingBoxCreator = new HealingBox(gameInstance);
    }

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
                
                if (tile == '#') //Land
                {
                    blockCreator.CreateLandBlock(posX, posY, blockWidth, blockHeight);
                }
                else if (tile == '^') // Spike
                {
                    spikeCreator.CreateSpike(posX, posY, blockWidth, blockHeight);
                }
                else if (tile == '+') // Healing Box
                {
                    healingBoxCreator.CreateHealingBox(posX, posY, blockWidth, blockHeight);
                }
                else if (tile == 's') // Player spawn point
                {
                    spawnPoint = new Vector(posX, posY); // Save the spawn point
                }
            }
        }
    }
}