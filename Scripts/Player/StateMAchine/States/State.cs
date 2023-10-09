using Godot;
using System;

public partial class State : Node
{
	[Export]
	public bool CanMove = true;

	public CharacterBody2D Character;

	public State NextState;

	public void StateInput(InputEvent @event){

    }

	public void OnEnter()
    {

    }

	public void OnExit()
    {

    }
}
