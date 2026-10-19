using Godot;
using System;
using System.Collections.Generic;

public abstract partial class StateMachine : Node
{
	private readonly Dictionary<string,IState> _statesList = new();
	[Export] public Node BeginState { get; private set; }
	private IState ActualState;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() 
    {
		foreach (Node child in GetChildren())
		{
			if (child is not IState state)
				continue;
			
			string key = child.Name.ToString().ToLowerInvariant();
			
			_statesList[key] = state;
			state.SetStateMachine(this);
		}
		
		if (BeginState is not IState initialState)
        {
            GD.PushError($"{Name}: BeginState não foi definido ou não implementa IState.");
			return;
		}
		ActualState?.Enter();
	}
	public override void _Process(double delta)
    {
        ActualState?.Update(delta);
    }
    public override void _PhysicsProcess(double delta)
	{
		ActualState?.PhysicsUpdate(delta);
	}
	public virtual void TransitionTo(string newState)
	{
		if (string.IsNullOrWhiteSpace(newState))
			return;
		
		string key = newState.ToLowerInvariant();
		
		if (!_statesList.TryGetValue(key, out IState nextState))
        {
            GD.PushWarning($"Estado '{newState}' não encontrado.");
            return;
        }
		if (ReferenceEquals(ActualState, nextState))
			return;
		ActualState?.Exit();
		ActualState = nextState;
		ActualState.Enter();
	}
}
