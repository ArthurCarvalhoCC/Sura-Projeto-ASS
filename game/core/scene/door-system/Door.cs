using Godot;
using System;

public partial class Door : Node2D
{
	[Export]
	public PackedScene _targetScene {get; private set;}
	[Export]
	public Vector2I _targetCoords {get; private set;}
	private bool canTeleport = false;
	private bool buttonPressed = true;
	[Signal]
	public delegate void SceneChangeResquestEventHandler(Door door);
	public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{
	}

	private void OnBodyEntered(Node2D node)
	{
		if (!(node is Player player)) return;

		canTeleport = true;
		if (buttonPressed)
		{
			EmitSignal(SignalName.SceneChangeResquest, this);
			GD.Print(_targetScene);
			GD.Print(_targetCoords);
		}
	}

	private void OnBodyExited(Node2D node)
	{
		if (!(node is Player player)) return;
		canTeleport = false;
	}
}
