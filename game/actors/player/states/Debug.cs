using Godot;
using System;

public partial class Debug : PlayerState
{
    bool collisionSwitch;

    public override void Update(double delta)
    {
        if (_player.DebugMode) Machine.TransitionTo("idle");
        if (_player.back) {
            onCollision(collisionSwitch);      
            collisionSwitch = !collisionSwitch;
            GD.Print(collisionSwitch);
        }
    }   
    public override void PhysicsUpdate(double delta)
    {
        _player.velocity = _player.Velocity;

        _player.velocity = _player.direction * _player.MoveSpd * 3;
        
        _player.Velocity = _player.velocity;
        _player.MoveAndSlide();
    }
    public override void Exit()
    {
        onCollision(true);
    }
    private void onCollision(bool button)
    {
        _player._collision.SetDeferred(CollisionShape2D.PropertyName.Disabled, !button);
    }
    private void RestartScene()
	{
		GetTree().ReloadCurrentScene();
	}
}