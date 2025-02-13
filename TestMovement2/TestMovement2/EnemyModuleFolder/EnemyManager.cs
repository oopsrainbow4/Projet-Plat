using System.Collections.Generic;

namespace TestMovement2.EnemyModuleFolder;

/// <summary>
/// Manages enemy data, including stats like HP, Speed, Damage, and Patrol Range.
/// </summary>
public static class EnemyManager
{
    // Dictionary to store enemy data by name/type
    public static readonly Dictionary<string, EnemyData> EnemyStats = new()
    {
        {"BasicEnemy", new EnemyData{ HP = 2, Speed = 100, Damage = 1, PatrolRange = 300}},
        {"FastEnemy", new EnemyData { HP = 1, Speed = 200, Damage = 1, PatrolRange = 400 }},
        {"TankEnemy", new EnemyData { HP = 5, Speed = 50, Damage = 2, PatrolRange = 250 }}
    };
    
    /// <summary>
    /// Retrieves enemy data based on enemy type.
    /// </summary>
    public static EnemyData GetEnemyData(string enemyType)
    {
        return EnemyStats.ContainsKey(enemyType) ? EnemyStats[enemyType] : null;
    }
}