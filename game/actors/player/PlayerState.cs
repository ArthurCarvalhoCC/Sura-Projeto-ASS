using Godot;
using System;

public abstract partial class PlayerState : State
{
    [Export]
    protected Player _player; 
}