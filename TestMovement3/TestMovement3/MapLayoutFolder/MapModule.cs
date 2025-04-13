using Jypeli;
using System.Collections.Generic;
using System.Linq;
using TestMovement3.MapLayoutFolder.BlockSystem;

namespace TestMovement3.MapLayoutFolder;

/// <summary>
/// Contains important map-related logic like generating the map from a layout.
/// </summary>
public class MapModule
{
    private readonly PhysicsGame game;
    private readonly CreateBlock createBlock;
    private Vector spawnPoint; // Store the spawn point coordinates
    private readonly List<Vector> enemyPositions = []; // Store enemy positions
    
    public MapModule(PhysicsGame gameInstance)
    {   
        game = gameInstance;
        createBlock = new CreateBlock();
    }

    /// <summary>
    /// Returns a list of all enemy spawn positions found in the map.
    /// </summary>
    public List<Vector> GetEnemyPositions()
    {
        return enemyPositions;
    }

    /// <summary>
    /// Get the player spawn point.
    /// </summary>
    public Vector GetSpawnPoint()
    {
        return spawnPoint;
    }
    
    /// <summary>
    /// Parses the string-based map layout and creates all blocks, enemies, and spawn points in the game.
    /// </summary>
    /// <param name="layout">A string array representing each row of the map layout.</param>
    public void GenerateMap(string[] layout)
    {
        double blockWidth = 64;
        double blockHeight = 64;

        // Stores all static blocks for bulk insertion at the end
        List<PhysicsObject> staticBlocks = new List<PhysicsObject>(); 
        
        // Reason: The MapLayout's string length are different in x-axis that cause the confusion for programming.
        
        // Find the maximum line length to prevent index errors
        int maxWidth = layout.Max(line => line.Length);
        
        // 🔧 Auto-pad all rows to match the longest one
        for (int i = 0; i < layout.Length; i++)
            layout[i] = layout[i].PadRight(maxWidth, ' '); // Add spaces to short rows

        // Convert string[] layout to char[,] for easier 2D indexing (e.g., check tiles above (GrassModule.cs))
        char[,] layoutGrid = new char[layout.Length, maxWidth];
        for (int y = 0; y < layout.Length; y++)
        {
            for (int x = 0; x < layout[y].Length; x++)
            {
                layoutGrid[y, x] = layout[y][x];
            }
        }
        // Solution: By add more space ' ' until it match same length as max length per row. 
        
        // Loop through the entire map layout grid
        for (int y = 0; y < layout.Length; y++)
        {
            string line = layout[y];
            for (int x = 0; x < line.Length; x++)
            {
                char tile = line[x];
                
                // Convert tile coordinates to game world coordinates
                double posX = x * blockWidth - maxWidth * blockWidth / 2;
                double posY = -(y * blockHeight - layout.Length * blockHeight / 2);
                
                // Handle special symbols in the layout
                if (tile == 'S') // Player spawn point
                {
                    spawnPoint = new Vector(posX, posY);
                }
                else if (tile == 'E') // Enemy spawn point
                {
                    enemyPositions.Add(new Vector(posX, posY)); // Store instead of spawning immediately
                }
                else if (BlockModule.SignToBlockType.TryGetValue(tile, out var blockType))
                {
                    // Special logic for Land blocks (dirt or grass)
                    if (blockType == BlockModule.BlockType.Land)
                    {
                        // Determine whether to use grass or dirt image
                        var image = GrassModule.SetLandBlockImage(layoutGrid, x, y);

                        // Temporarily override the image for the Land block
                        var originalImage = BlockModule.BlockInfo[blockType].Image;
                        BlockModule.BlockInfo[blockType].Image = image;

                        var block = createBlock.CreateBlocks(posX, posY, blockType);
                        BlockModule.BlockInfo[blockType].Image = originalImage; // Restore image for future blocks

                        if (BlockModule.BlockInfo[blockType].UsesBatching)
                            staticBlocks.Add(block);
                        else
                            game.Add(block);
                    }
                    else
                    {
                        // Normal block creation for all other block types
                        var block = createBlock.CreateBlocks(posX, posY, blockType);
                                            
                        if (BlockModule.BlockInfo[blockType].UsesBatching) 
                            staticBlocks.Add(block);
                        else
                            game.Add(block);
                    }
                }
                else if (!char.IsWhiteSpace(tile))
                {
                    // Warn about unknown characters in the layout
                    game.MessageDisplay.Add($"Unknown symbol in map: '{tile}' at ({x},{y})");
                }
            }
        }
        
        // Add all batched static blocks to the game at once for better performance
        // This improves performance by reducing individual object additions, which can cause lag.
        foreach (var block in staticBlocks)
            game.Add(block);
        
    }
}
