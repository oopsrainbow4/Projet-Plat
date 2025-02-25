using System;
using Jypeli;

using Projet_Plat.Image_Sound_Storage;

namespace Projet_Plat.EnemyModuleFolder;

/// <summary>
/// Represents a basic enemy that moves left and right, detects the player, and attacks.
/// </summary>
public class BasicEnemy : PhysicsObject
{
    private readonly double speed; // The movement speed of the enemy
    private readonly double patrolRange; // The distance the enemy patrols before turning
    private readonly double startX; // The starting X position of the enemy
    private bool movingRight = true; // Determines the enemy's movement direction
    private readonly PhysicsObject player; // Reference to the player object
    
    // The amount of damage the enemy deals to the player
    public int Damage {get; private set;}
    public int HP { get; private set; } // The enemy's health points

    /// <summary>
    /// Constructor for the BasicEnemy.
    /// </summary>
    /// <param name="x">Initial X position</param>
    /// <param name="y">Initial Y position</param>
    /// <param name="data">Enemy data containing HP, speed, and patrol range</param>
    /// <param name="player">Reference to the player object</param>
    public BasicEnemy(double x, double y, EnemyData data, PhysicsObject player) : base(64, 64)
    {
        X = x;
        Y = y;
        HP = data.HP;
        speed = data.Speed;
        patrolRange = data.PatrolRange;
        startX = x;
        this.player = player;
        Damage = data.Damage;

        Image = ImageModule.EnemyImage;
        Tag = "Enemy"; // Assign enemy tag
        IgnoresGravity = false; // Enemy is affected by gravity

        Timer.SingleShot(0.1, Patrol); // Start patrol behavior
    }
    
    /// <summary>
    /// Prevents the enemy from rotating by forcing Angle to stay at 0.
    /// </summary>
    public override void Update(Time time)
    {
        base.Update(time);
        Angle = Angle.Zero; // Keep enemy upright
    }

    /// <summary>
    /// Controls the enemy's behavior based on the player's position.
    /// </summary>
    private void Patrol()
    {
        if (Math.Abs(player.X - X) <= patrolRange)
        {
            ChasePlayer(); // If the player is within range, chase them
        }
        else
        {
            MovePatrol(); // Otherwise, continue patrolling
        }
        Timer.SingleShot(0.1, Patrol); // Repeat patrol logic periodically
    }

    /// <summary>
    /// Moves the enemy within its patrol range.
    /// </summary>
    private void MovePatrol()
    {
        if (movingRight)
        {
            Velocity = new Vector(speed, 0); // Move right
            if (X >= startX + patrolRange) movingRight = false; // Change direction if patrol limit is reached
        }
        else
        {
            Velocity = new Vector(-speed, 0); // Move left
            if (X <= startX - patrolRange) movingRight = true; // Change direction if patrol limit is reached
        }
    }

    /// <summary>
    /// Makes the enemy chase the player when in range.
    /// </summary>
    private void ChasePlayer()
    {
        double direction = player.X > X ? 1 : -1; // Determine direction towards the player
        Velocity = new Vector(speed * direction, 0); // Move towards the player
    }

    /// <summary>
    /// Reduces the enemy's HP when damaged and destroys it if HP reaches zero.
    /// </summary>
    /// <param name="amount">Amount of damage taken</param>
    public void TakeDamage(int amount)
    {
        HP -= amount;
        if (HP <= 0)
        {
            Destroy(); // Destroy the enemy if HP is zero
        }
    }
}