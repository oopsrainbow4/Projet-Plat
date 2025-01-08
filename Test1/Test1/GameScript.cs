using Jypeli;

/// @author Jean Joensuu
/// @version 16.11.2024
/// <summary>
/// 
/// </summary>

public class GameScript : PhysicsGame
{
    private PhysicsObject player; // The player object represented as a physics-enabled block
    private bool isDoubleJumpingAllowed; // Flag to allow or disallow double jumping
    private bool isJumpKeyReleased; // Track if jump key is released
    
    private readonly double maxSpeed = 1000; // Maximum speed the player can reach
    private readonly double acceleration  = 20; // Acceleration rate

    public override void Begin()
    {
        isDoubleJumpingAllowed = false; // Initially, double jumping is not allowed
        isJumpKeyReleased = true; // Player starts with the ability to jump
        
        SetupPlayer(); // Create the player
        SetupControl(); // Set up controls
        SetupEnvironment(); // Create the game environment, including gravity and the floor
        DecelerationTimer(); // Set up a timer to handle gradual deceleration of the player
    }

    private void SetupPlayer()
    {
        // Create the player (a block with width and height)
        player = new PhysicsObject(50, 50); // Size of the block
        player.X = 0;
        player.Y = 0;
        player.Shape = Shape.Rectangle;
        player.Mass = 1;
        player.Restitution = 0.5; // Slight bounce
        Add(player);
    }

    private void SetupControl()
    {
        // Set up controls
        Keyboard.Listen(Key.D, ButtonState.Down, MoveRight, null); // Move right when "D" is pressed
        Keyboard.Listen(Key.A, ButtonState.Down, MoveLeft, null);  // Move left when "A" is pressed
        Keyboard.Listen(Key.Space, ButtonState.Pressed, Jump, null); // Call Jump on press
        Keyboard.Listen(Key.Space, ButtonState.Released, OnJumpKeyRelease, null); // Reset flag on release
    }

    private void SetupEnvironment()
    {
        // Set gravity
        Gravity = new Vector(0, -1000); // Gravity pulling downwards
        CreateFloor();
    }

    private void CreateFloor()
    {
        // Create a floor
        PhysicsObject floor = PhysicsObject.CreateStaticObject(10000, 20); // Width and height of the floor
        floor.X = 0; // Center it horizontally (adjust as needed)
        floor.Y = -200; // Position below the player (adjust the Y value as needed)
        floor.Shape = Shape.Rectangle;
        Add(floor);
    }
    
    // Move right
    void MoveRight() // Moving to the right
    {
        if (player.Velocity.X < maxSpeed) // Limit the maximum speed
        {
            player.Velocity += new Vector(acceleration, 0); // Gradually increase the speed to the right
        }
    }
    
    // Move left
    void MoveLeft() // Moving to the left
    {
        if (player.Velocity.X > -maxSpeed) // Limit the maximum speed in the negative direction
        {
            player.Velocity += new Vector(-acceleration, 0); // Gradually increase the speed to the left
        }
    }
    
    // Jump (with double jump)
    void Jump()
    {
        if (isJumpKeyReleased) // Only allow jumping when the space key was released
        {
           if (IsOnGround())
           {
               // Normal jump when on the ground
               player.Velocity = new Vector(player.Velocity.X, 500);  // Set vertical velocity for jump
               isDoubleJumpingAllowed = true;  // Allow for double jump
           }
           else if (isDoubleJumpingAllowed)
           {
               // Double jump when in the air
               player.Velocity = new Vector(player.Velocity.X, 500);  // Set vertical velocity for double jump
               isDoubleJumpingAllowed = false;  // Disable double jump once used
           } 
           isJumpKeyReleased = false; // Set to false until the key is released
        }
    }

    // Add a method to track key release
    void OnJumpKeyRelease()
    {
        isJumpKeyReleased = true;
    }

    // Custom method to check if the player is on the ground based on position
    bool IsOnGround()
    {
        // We check if the player's vertical velocity is near zero, indicating they're not falling
        return player.Y <= 0;  // Adjust condition based on where the ground is located in your world
    }

    // Gradually slow down the player's horizontal movement
    public void Decelerate()
    {
        // Check if the player is on the ground to reset double jump
        if (player.Velocity.X > 0) // Moving right
        {
            player.Velocity -= new Vector(acceleration / 2, 0); // Gradual deceleration
            // Stop completely if close to zero
            if (player.Velocity.X < 0) player.Velocity = new Vector(0, player.Velocity.Y);
        }
        else if (player.Velocity.X < 0)
        {
            player.Velocity += new Vector(acceleration / 2, 0); // Gradual deceleration
            // Stop completely if close to zero
            if (player.Velocity.X > 0) player.Velocity = new Vector(0, player.Velocity.Y); 
        }
    }

    // Set up a timer to call the Decelerate method at regular intervals
    private void DecelerationTimer()
    {
        Timer decelerationTimer = new Timer(); // Create a new timer
        decelerationTimer.Interval = 0.02; // 20 milliseconds or adjust as needed
        decelerationTimer.Timeout += Decelerate; // Bind the Decelerate method to the timer's tick event
        decelerationTimer.Start(); // Start the timer
    }
}