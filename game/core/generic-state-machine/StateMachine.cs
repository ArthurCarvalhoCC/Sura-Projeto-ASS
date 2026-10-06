using Godot;
using System;

public abstract partial class StateMachine : Node
{
	private Godot.Collections.Dictionary<string,State> statesList = new();
	[Export] public State BeginState;
	public State ActualState;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() 
    {
		foreach (State state in GetChildren())
		{
			statesList[state.Name.ToString().ToLower()] = state;
			state.Machine = this;
		}
		ActualState = BeginState;
		
		ActualState?.Enter();
	}
	public override void _Process(double delta)
    {
        ActualState?.Update(delta);
    }
    public override void _PhysicsProcess(double delta){ActualState?.PhysicsUpdate(delta);}
	public virtual void TransitionTo(string newState)
	{
		if (newState == null ) return;
		string key = newState.ToLower();

		ActualState?.Exit();
		ActualState = statesList[key];
		statesList[key]?.Enter();
	}
}
