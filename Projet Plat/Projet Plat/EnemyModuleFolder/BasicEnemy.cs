using Jypeli;
using Projet_Plat.Image_Sound_Storage;

namespace Projet_Plat.EnemyModuleFolder;

/// <summary>
/// Represents a basic enemy that moves left and right, detects the player, and attacks.
/// </summary>
public class BasicEnemy : EnemyBase
{
    private readonly double patrolRange; // The maximum distance the enemy moves before turning
    private readonly double startX; // The initial X position of the enemy (used for patrol range calculation)
    private bool movingRight = true; // Keeps track of the enemy's current movement direction
    private readonly PhysicsObject player; // Reference to the player object

    /// <summary>
    /// Constructor for the BasicEnemy. Sets initial position, movement, and properties.
    /// </summary>
    /// <param name="x">Initial X position</param>
    /// <param name="y">Initial Y position</param>
    /// <param name="data">Enemy data containing HP, speed, patrol range, and damage</param>
    /// <param name="player">Reference to the player object</param>
    public BasicEnemy(double x, double y, EnemyData data, PhysicsObject player) : base(64, 64)
    {
        // Initialize position
        X = x;
        Y = y;
        
        // Assign stats from EnemyData
        HP = data.HP;
        Speed = data.Speed;
        patrolRange = data.PatrolRange;
        Damage = data.Damage;

        startX = x; // Store the starting X position
        this.player = player; // Store reference to the player

        // Set enemy appearance and behavior
        Image = ImageModule.EnemyImage;
        Tag = "Enemy"; // Assign the "Enemy" tag for collision detection
        IgnoresGravity = false; // The enemy is affected by gravity

        // Start enemy movement logic (patrol and chasing)
        Timer.SingleShot(0.1, Patrol);
    }

    /// <summary>
    /// Prevents the enemy from rotating due to physics interactions.
    /// Ensures the enemy always remains upright.
    /// </summary>
    public override void Update(Time time)
    {
        base.Update(time);
        Angle = Angle.Zero; // Force the enemy's rotation to stay at 0 degrees
    }

    /// <summary>
    /// Determines whether the enemy should patrol or chase the player.
    /// </summary>
    private void Patrol()
    {
        if (Position.Distance(player.Position) <= patrolRange) // Check if the player is within the patrol range
        {
            ChasePlayer(); // If the player is close enough, chase them
        }
        else
        {
            MovePatrol(); // Otherwise, continue normal patrolling
        }

        // Repeat this patrol logic every 0.1 seconds
        Timer.SingleShot(0.1, Patrol);
    }

    /// <summary>
    /// Moves the enemy back and forth within the patrol range.
    /// </summary>
    private void MovePatrol()
    {
        if (movingRight)
        {
            Velocity = new Vector(Speed, 0); // Move right
            if (X >= startX + patrolRange) movingRight = false; // If reached patrol limit, turn left
        }
        else
        {
            Velocity = new Vector(-Speed, 0); // Move left
            if (X <= startX - patrolRange) movingRight = true; // If reached patrol limit, turn right
        }
    }

    /// <summary>
    /// Makes the enemy chase the player if they are within range.
    /// </summary>
    private void ChasePlayer()
    {
        double direction = player.X > X ? 1 : -1; // Determine direction towards the player
        Velocity = new Vector(Speed * direction, 0); // Move in the player's direction
    }
}