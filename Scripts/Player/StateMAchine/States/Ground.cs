using Godot;
using System;

public partial class Ground : State
{
    [Export]
    public const float JumpVelocity = -400.0f;

    [Export]
    public State AirState;

    public void StateInput(InputEvent @event){ 
        if (@event.IsActionPressed("jump")) {
            Jump();
        }
    }

    public void Jump()
    {
        Character.velocity.Y = JumpVelocity;
        NextState = AirState;
    }
        

}