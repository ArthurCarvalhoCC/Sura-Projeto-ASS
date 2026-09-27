using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Signal]
	public delegate void StateChangedEventHandler(string stateName);
	[Export] public float MoveSpd = 40f;
	[Export] public AnimationTree _animTree;
	[Export] public CollisionShape2D _collision;
	[Export] public Camera2D _camera;
	[Export] public float acceleration = 3000f;
	[Export] public float fricttion = 5000f;
	public AnimationNodeStateMachinePlayback _animStateMachine;
	public Vector2 velocity = Vector2.Zero;
	public Vector2 direction = Vector2.Zero;
	public bool debugMode = false;
	public bool collisionActive = true;
	public bool back = false;

	public override void _Ready()
	{
		_camera.ProcessCallback = Camera2D.Camera2DProcessCallback.Physics;
		_animStateMachine = (AnimationNodeStateMachinePlayback)_animTree.Get("parameters/playback");
	}

	public override void _PhysicsProcess(double delta)
	{
		direction = Input.GetVector("game_left", "game_right", "game_up", "game_down");
		debugMode = Input.IsActionJustPressed("debug_mode");
		back = Input.IsActionJustPressed("game_back");
	}

	// SIGNALS
	public void NotifyStateChanged(string newState)
	{
		GD.Print("New State: " + newState);
		EmitSignal(SignalName.StateChanged, newState);
	}
}
