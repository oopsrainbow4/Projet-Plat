using Jypeli;
using System.Collections.Generic;

namespace TestMovement2.MapLayoutFolder;

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
    /// Parses the layout and places objects in the game world.
    /// </summary>
    /// <param name="layout">A string array representing the map layout.</param>
    public void GenerateMap(string[] layout)
    {
        double blockWidth = 64;
        double blockHeight = 64;

        List<PhysicsObject> staticBlocks = new List<PhysicsObject>(); // Batch static blocks

        for (int y = 0; y < layout.Length; y++)
        {
            string line = layout[y];
            for (int x = 0; x < line.Length; x++)
            {
                char tile = line[x];
                double posX = x * blockWidth - layout[0].Length * blockWidth / 2;
                double posY = -(y * blockHeight - layout.Length * blockHeight / 2);
                
                if (tile == 'S')
                {
                    spawnPoint = new Vector(posX, posY);
                }
                else if (tile == 'E') 
                {
                    enemyPositions.Add(new Vector(posX, posY)); // Store instead of spawning immediately
                }
                else if (BlockModule.SignToBlockType.TryGetValue(tile, out var blockType))
                {
                   var blockData = BlockModule.BlockInfo[blockType];
                   var block = createBlock.CreateBlocks(posX, posY, blockType);
                   
                   if (blockData.UsesBatching)
                       staticBlocks.Add(block);
                   else
                       game.Add(block);
                }
                else if (!char.IsWhiteSpace(tile))
                {
                    game.MessageDisplay.Add($"Unknown symbol in map: '{tile}' at ({x},{y})");
                }
            }
        }
        
        // Loop through all pre-created static blocks and add them to the game world in bulk.
        // This improves performance by reducing individual object additions, which can cause lag.
        foreach (var block in staticBlocks)
            game.Add(block);
        
    }
}
