using Godot;


public partial class Talk : PlayerState
{
	public override void Enter()
	{
        if (_player._animStateMachine == null) return;
        _player._animStateMachine.Travel("Idle");
	}
    public override void Update(double delta)
    {
        if (_player == null) return;
        if (_player.debugMode) Machine.TransitionTo("debug"); 
    }
    public override void PhysicsUpdate(double delta)
    {
        _player.MoveAndSlide();
    }
    public override void Exit()
    {
        
    }
}
