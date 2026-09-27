using Godot;
using System;

public partial class PlayerStateMachine : StateMachine
{
    [Export]
    private Player _player;
    public override void TransitionTo(string newState)
    {
        base.TransitionTo(newState);
       _player.NotifyStateChanged(newState);
    }
}
