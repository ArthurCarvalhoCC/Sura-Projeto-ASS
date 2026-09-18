using Godot;
using System;

public partial class Pause : PlayerState
{
	public override void Enter()
	{

	}
    public override void Update(double delta)
    {
        if (_player == null) return;

    }
    public override void PhysicsUpdate(double delta)
    {

    }
    public override void Exit()
    {
        _player._animStateMachine.Travel("Idle");
    }
}
