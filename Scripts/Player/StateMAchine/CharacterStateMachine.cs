using Godot;
using System;

public partial class CharacterStateMachine : Node
{
	[Export]
	public Godot.Collections.Array<State> states { get; set; }

    [Export]
    public State CurrentState;

    [Export]
    public CharacterBody2D Character;

    public override void _Ready()
    {
		foreach(State child in GetChildren())
        {
            if(child is State)
            {
                states.Add(child);
                child.Character = Character;

            }
            else
            {
                GD.Print("Child " + child.Name + " is not a State");
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if(CurrentState.NextState != null)
        {
            SwitchStates(CurrentState.NextState);
        }
    }

    public void SwitchStates(State NewState)
    {
        if(CurrentState != null)
        {
            CurrentState.OnExit();
            CurrentState.NextState = null;
        }
        CurrentState = NewState;

        CurrentState.OnEnter();
    }

    public bool CheckIfMove()
    {
        return CurrentState.CanMove;
    }

    public override void _Input(InputEvent @event)
    {
        CurrentState.StateInput(@event);
    }

}