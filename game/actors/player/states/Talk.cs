using Godot;


public partial class Talk : PlayerState
{
	public override void Enter()
	{
        _player._animStateMachine.Travel("Idle");
	}
    public override void Update(double delta)
    {
        if (_player == null) return;
        if (_player.DebugMode) Machine.TransitionTo("debug"); 
    }
    public override void PhysicsUpdate(double delta)
    {
        _player.MoveAndSlide();
    }
    public override void Exit()
    {
        
    }
}
