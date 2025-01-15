using Jypeli;

namespace TestMovement3.MapLayoutFolder.LayoutDesign;

/// <summary>
/// Handles the creation of healing boxes in the game world.
/// </summary>
public class HealingBox
{
    private PhysicsGame game;

    public HealingBox(PhysicsGame gameInstance)
    {
        game = gameInstance;
    }

    /// <summary>
    /// Creates and adds a healing box object to the game.
    /// </summary>
    public void CreateHealingBox(double x, double y, double width, double height)
    {
        PhysicsObject healingBox = PhysicsObject.CreateStaticObject(width / 2, height / 2);
        healingBox.Shape = Shape.Rectangle;
        healingBox.Color = Color.LightGreen;
        healingBox.X = x;
        healingBox.Y = y;
        healingBox.Tag = "HealingBox";
        
        game.Add(healingBox);
    }
}