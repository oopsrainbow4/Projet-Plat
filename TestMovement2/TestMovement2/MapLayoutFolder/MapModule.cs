using Jypeli;
using TestMovement2.MapLayoutFolder.LayoutDesign;

namespace TestMovement2.MapLayoutFolder;

/// <summary>
/// Contains important map-related logic like generating the map from a layout.
/// </summary>
public class MapModule
{
    private Block blockCreator;
    private Spike spikeCreator;

    public MapModule(PhysicsGame gameInstance)
    {
        blockCreator = new Block(gameInstance); // Initialize the Land module
        spikeCreator = new Spike(gameInstance);
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
                double posX = x * blockWidth - (layout[0].Length / 2.0) * blockWidth;
                double posY = -(y * blockHeight - (layout.Length / 2.0) * blockHeight);
                
                if (tile == '#') //Land
                {
                    blockCreator.CreateLandBlock(posX, posY, blockWidth, blockHeight);
                }
                else if (tile == '^') // Spike
                {
                    spikeCreator.CreateSpike(posX, posY, blockWidth, blockHeight);
                }
            }
        }
    }
}