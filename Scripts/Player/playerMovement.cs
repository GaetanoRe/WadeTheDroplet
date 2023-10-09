using Godot;
using System;

public partial class playerMovement : CharacterBody2D
{
	public float Health = 100.0f;
	public float Speed = 200.0f;
	public float gainedSpeed = 0f;
	public Vector2 velocity;
	public Vector2 direction;
	public bool dashPressed = false;
	public AnimationTree animationTree;
	public Sprite2D sprite;
	public CharacterStateMachine stateMachine;

	public override void _Ready()
    {
		animationTree = GetNode<AnimationTree>("AnimationTree");
		sprite = GetNode<Sprite2D>("Sprite2D");
		animationTree.Active = true;
		stateMachine = GetNode<CharacterStateMachine>("CharacterStateMachine");

    }

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		direction = Input.GetVector("move_left", "move_right", "look_up", "crouch");
		if (direction != Vector2.Zero && stateMachine.CheckIfMove())
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
		UpdateAnimation();
		UpdateFacingDirection();
		MoveAndSlide();
	}

	public void UpdateAnimation()
    {
		animationTree.Set("parameters/Move1/blend_position", direction.X);
    }

	public void UpdateFacingDirection()
    {
		if(direction.X > 0)
        {
			sprite.FlipH = false;
        }
		else if(direction.X < 0)
        {
			sprite.FlipH = true;
        }
    }

	public void _OnHazardDetectorAreaEntered(Area2D area)
    {
		QueueFree();
    }
}
