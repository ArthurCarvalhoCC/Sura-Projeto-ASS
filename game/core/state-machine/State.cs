using Godot;
using System;

public interface IState
{	
	protected StateMachine _stateMachine { get; set; }
	void Enter() { }
	void Update(double delta) { }
	void PhysicsUpdate(double delta) { }
	void Exit() { }
	void SetStateMachine(StateMachine _stateMachine) { }
}
