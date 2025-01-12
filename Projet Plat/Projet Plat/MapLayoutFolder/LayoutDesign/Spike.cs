using Jypeli;

namespace Projet_Plat.MapLayoutFolder.LayoutDesign;

/// <summary>
/// Handles the creation of spike blocks in the game world.
/// </summary>
public class Spike
{
    private PhysicsGame game;

    public Spike(PhysicsGame gameInstance)
    {
        game = gameInstance;
    }

    /// <summary>
    /// Creates and adds a spike object to the game.
    /// </summary>
    public void CreateSpike(double x, double y, double width, double height)
    {
        PhysicsObject spike = PhysicsObject.CreateStaticObject(width, height);
        spike.Shape = Shape.Triangle;
        spike.Color = Color.White;
        spike.X = x;
        spike.Y = y;
        spike.Tag = "Spike";

        game.Add(spike);
    }
}