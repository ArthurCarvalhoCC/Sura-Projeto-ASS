using Godot;
using System;

public partial class Door : Node2D
{
	[Export]
	public string _targetScenePath {get; private set;}
	[Export]
	public Vector2I _targetCoords {get; private set;}
	public PackedScene targetScene {get; private set;}

	private bool canTeleport = false;
	private bool buttonPressed = true;
	[Signal]
	public delegate void SceneChangeRequestEventHandler(Door door);

	public override void _Process(double delta)
	{
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Player player)
		{
			canTeleport = true;
			if (buttonPressed)
			{
				targetScene = ResourceLoader.Load<PackedScene>(_targetScenePath);
				EmitSignal(SignalName.SceneChangeRequest, this);
				GD.Print(_targetScenePath);
				GD.Print(_targetCoords);
			}
		}


	}

	private void OnBodyExited(Node2D body)
	{
		if (!(body is Player player)) return;
		canTeleport = false;
	}
}
