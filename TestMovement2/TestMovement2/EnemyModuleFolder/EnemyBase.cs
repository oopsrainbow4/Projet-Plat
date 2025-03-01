using Jypeli;

namespace TestMovement2.EnemyModuleFolder;

/// <summary>
/// Base class for all enemies, containing shared properties and behavior.
/// </summary>
public abstract class EnemyBase : PhysicsObject
{
    public int HP { get; protected set; }
    public int Damage { get; protected set; }
    public double Speed { get; protected set; }

    public EnemyBase(double width, double height) : base(width, height)
    {
        Tag = "Enemy"; 
        IgnoresGravity = false;
    }

    /// <summary>
    /// Reduces enemy HP and destroys the enemy if HP reaches 0.
    /// </summary>
    public void TakeDamage(int amount)
    {
        HP -= amount;
        if (HP <= 0) Destroy();
    }
}