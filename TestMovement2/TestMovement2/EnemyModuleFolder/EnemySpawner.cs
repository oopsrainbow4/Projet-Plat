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
    /// <summary>
    /// Spawns an enemy of the given type at the specified position.
    /// </summary>
    public static void SpawnEnemy(double x, double y)
    {
        EnemyData enemyData = EnemyManager.GetEnemyData("BasicEnemy");
        if (enemyData == null) return;

        // Wait for player to exist before spawning
        PhysicsObject player = Game.Instance.GetObjectsWithTag("Player").FirstOrDefault() as PhysicsObject;
        if (player == null)
        {
            Timer.SingleShot(0.5, () => SpawnEnemy(x, y)); // Retry after 0.5s
            return;
        }

        BasicEnemy enemy = new BasicEnemy(x, y, enemyData, player);
        Game.Instance.Add(enemy);
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