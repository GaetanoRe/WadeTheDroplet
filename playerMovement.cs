using Godot;
using System;

public partial class playerMovement : CharacterBody2D
{
	public float Health = 100.0f;
	public float Speed = 200.0f;
	public float gainedSpeed = 0f;
	public const float JumpVelocity = -400.0f;
	public bool dashPressed = false;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("move_left", "move_right", "look_up", "crouch");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			if (Speed <= 1000f && Input.GetActionStrength("dash") >= 0.5f)
			{
				Speed += 5.5f;
				dashPressed = true;
			}

		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			if (Speed >= 100.0f)
				Speed = 200f;
            if (dashPressed)
            {
				//Skid a little bit (will add tomorrow)
				dashPressed = false;
            }
		}
		Velocity = velocity;
		MoveAndSlide();
	}


	public void _OnHazardDetectorAreaEntered(Area2D area)
    {
		QueueFree();
    }
}
