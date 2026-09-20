using Godot;
using System;

public partial class Debug : PlayerState
{

    public override void Update(double delta)
    {
        if (_player.debugMode) Machine.TransitionTo("idle");
        if (_player.back) {
            onCollision(_player.collisionActive);      
            _player.collisionActive = !_player.collisionActive;
            GD.Print("Player Collision: "+_player.collisionActive);
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