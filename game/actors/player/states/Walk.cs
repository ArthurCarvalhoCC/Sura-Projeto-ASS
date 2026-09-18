using Godot;


public partial class Walk : PlayerState
{
	public override void Enter()
	{
        _player._animStateMachine.Travel("Walk");
	}
    public override void Update(double delta)
    {
        if (_player == null) return;
        if (_player.DebugMode) Machine.TransitionTo("debug");

        if (_player.direction == Vector2.Zero) Machine.TransitionTo("idle");
        else {
            _player._animTree.Set("parameters/Walk/blend_position", _player.direction);
            _player._animTree.Set("parameters/Idle/blend_position", _player.direction);   
        }
    }
    public override void PhysicsUpdate(double delta)
    {
        Vector2 targetVelocity = _player.direction * _player.MoveSpd;
        _player.velocity = _player.Velocity; 
        
        // se tiver input, acelera. se não tiver, freia (fricção)
        float accelRate = (_player.direction != Vector2.Zero) ? _player.acceleration : _player.fricttion;

        // movimentação suave
        _player.velocity = _player.velocity.MoveToward(targetVelocity, accelRate * (float)delta);

        
        _player.Velocity = _player.velocity;
        _player.MoveAndSlide();
    }
    public override void Exit()
    {
        
    }
}
