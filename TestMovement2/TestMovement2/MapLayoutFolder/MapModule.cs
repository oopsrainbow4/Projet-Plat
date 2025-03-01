using System;
using Jypeli;
using System.Collections.Generic;
using TestMovement2.EnemyModuleFolder;

namespace TestMovement2.MapLayoutFolder;

/// <summary>
/// Contains important map-related logic like generating the map from a layout.
/// </summary>
public class MapModule
{
    private readonly PhysicsGame game;
    private readonly CreateBlock createBlock;
    private Vector spawnPoint; // Store the spawn point coordinates
    private readonly Dictionary<BlockModule.BlockType, Image> cachedImages; // Cache images
    private List<Vector> enemyPositions = new List<Vector>(); // Store enemy positions
    
    public MapModule(PhysicsGame gameInstance)
    {
        game = gameInstance;
        createBlock = new CreateBlock(gameInstance);
        cachedImages = new Dictionary<BlockModule.BlockType, Image>();

        // Preload images to avoid repeated loading
        foreach (var blockType in BlockModule.BlockInfo.Keys)
        {
            cachedImages[blockType] = BlockModule.BlockInfo[blockType].Image; // Use preloaded images
        }
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
                double posX = x * blockWidth - (layout[0].Length * blockWidth / 2);
                double posY = -(y * blockHeight - (layout.Length * blockHeight / 2));
                
                if (tile == 's')
                {
                    spawnPoint = new Vector(posX, posY);
                }
                else if (tile == 'E') 
                {
                    enemyPositions.Add(new Vector(posX, posY)); // Store instead of spawning immediately
                }
                else
                {
                   BlockModule.BlockType? blockType = tile switch
                   {
                       '#' => BlockModule.BlockType.Land,
                       '^' => BlockModule.BlockType.Spike,
                       '+' => BlockModule.BlockType.HealingBox,
                       'L' => BlockModule.BlockType.Lava,
                       's' => null, // Player spawn
                       _ => null 
                   }; 
                   
                   if (blockType.HasValue)
                   { 
                       if (blockType == BlockModule.BlockType.Land || 
                         blockType == BlockModule.BlockType.Spike || 
                         blockType == BlockModule.BlockType.Lava) 
                        {
                            // Optimize by batching static blocks
                            var block = createBlock.CreateBlockObject(posX, posY, blockType.Value, 
                                cachedImages[blockType.Value]);
                            staticBlocks.Add(block);
                        }
                        else
                        {
                            createBlock.CreateBlocks(posX, posY, blockType.Value, cachedImages[blockType.Value]); 
                        }
                   } 
                }
            }
        }
        // Loop through all pre-created static blocks and add them to the game world in bulk.
        // This improves performance by reducing individual object additions, which can cause lag.
        foreach (var block in staticBlocks)
        {
            game.Add(block);
        }
    }
}
