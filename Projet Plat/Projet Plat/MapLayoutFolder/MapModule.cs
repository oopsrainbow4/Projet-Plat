using Jypeli;
using System.Collections.Generic;

namespace Projet_Plat.MapLayoutFolder;

/// <summary>
/// Contains important map-related logic like generating the map from a layout.
/// </summary>
public class MapModule
{
    private readonly PhysicsGame game;
    private readonly CreateBlock createBlock;
    private Vector spawnPoint; // Store the spawn point coordinates
    private readonly Dictionary<BlockModule.BlockType, Image> cachedImages; // Cache images

    public MapModule(PhysicsGame gameInstance)
    {
        game = gameInstance;
        createBlock = new CreateBlock(gameInstance);
        cachedImages = new Dictionary<BlockModule.BlockType, Image>();

        // Preload images to avoid repeated loading
        foreach (var blockType in BlockModule.BlockInfo.Keys)
        {
            var imagePath = BlockModule.BlockInfo[blockType].ImagePath;
            if (!string.IsNullOrEmpty(imagePath))
            {
                cachedImages[blockType] = Game.LoadImage(imagePath);
            }
        }
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
        double blockWidth = 50;
        double blockHeight = 50;

        List<PhysicsObject> staticBlocks = new List<PhysicsObject>(); // Batch static blocks

        for (int y = 0; y < layout.Length; y++)
        {
            string line = layout[y];
            for (int x = 0; x < line.Length; x++)
            {
                char tile = line[x];
                double posX = x * blockWidth - (layout[0].Length / 1.99) * blockWidth;
                double posY = -(y * blockHeight - (layout.Length / 1.99) * blockHeight);

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
                else if (tile == 's')
                {
                    spawnPoint = new Vector(posX, posY);
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
