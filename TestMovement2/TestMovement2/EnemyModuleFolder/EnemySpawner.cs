using System;
using System.Linq;
using System.Collections.Generic;
using Jypeli;
using TestMovement2.MapLayoutFolder;

namespace TestMovement2.EnemyModuleFolder;

/// <summary>
/// Handles spawning of enemies based on stored enemy data.
/// </summary>
public class EnemySpawner
{
    private Game game;
    private PhysicsObject player;

    public EnemySpawner(Game game, PhysicsObject player)
    {
        this.game = game;
        this.player = player;
    }

    /// <summary>
    /// Spawns an enemy of the given type at the specified position.
    /// </summary>
    public static void SpawnEnemy(double x, double y)
    {
        EnemyData enemyData = EnemyManager.GetEnemyData("BasicEnemy"); // Fetch enemy stats
        if (enemyData != null)
        {
            // Get the first object with the "Player" tag
            PhysicsObject player = Game.Instance.GetObjectsWithTag("Player").FirstOrDefault() as PhysicsObject;
            if (player != null)
            {
                BasicEnemy enemy = new BasicEnemy(x, y, enemyData, player);
                Game.Instance.Add(enemy); // Add enemy to game world
            }
            else
            {
                Console.WriteLine("Player not found! Enemy not spawned.");
            }
        }
    }
    
    /// <summary>
    /// Spawns all enemies after the player is created.
    /// </summary>
    public static void SpawnEnemiesAfterPlayer(MapModule mapModule)
    {
        List<Vector> enemyPositions = mapModule.GetEnemyPositions();
        foreach (Vector pos in enemyPositions)
        {
            SpawnEnemy(pos.X, pos.Y);
        }
    }
}