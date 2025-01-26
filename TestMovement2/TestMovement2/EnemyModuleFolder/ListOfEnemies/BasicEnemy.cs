using Jypeli;

namespace TestMovement2.ListOfEnemies;

/// <summary>
/// Represents the basic enemy with behavior and stats.
/// </summary>
public class BasicEnemy : PhysicsObject
{
    public string EnemyName { get; private set; }
    public double Speed { get; private set; }
    public int HP { get; private set; }
    public int Damage { get; private set; }
    public double PatrolRange { get; private set; }
    
    private double patrolStartX;
    private double patrolEndX;

    public BasicEnemy(string name, double speed, int hp, int damage, double patrolRange, double x, double y)
        : base(50, 50) // Default size for the enemy
    {
        EnemyName = name;
        Speed = speed;
        HP = hp;
        Damage = damage;
        PatrolRange = patrolRange;
        
        patrolStartX = x - patrolRange;
        patrolEndX = x + patrolRange;

        Position = new Vector(x, y);
        Image = Game.LoadImage("EnemyImages/Red.png"); // Placeholder image
    }

    public void Patrol()
    {
        if (X <= patrolStartX || X >= patrolEndX)
        {
            Speed = -Speed; // Reverse direction
        }
        
        Move(new Vector(Speed, 0));
    }
}