using Godot;
using System;
using System.Collections.Generic;

public partial class DoorsContainer : Node2D
{
	[Signal]
	public delegate void SceneChangeRequestEventHandler(PackedScene scene, Vector2I coords);
	private List<Door> doorsList = new();

	public override void _Ready()
	{
		foreach (Node child in GetChildren())
		{
			if (child is Door door)
			{
				doorsList.Add(door);
				door.NodeEntered += OnSceneChangeResquest;
				GD.Print(door.Name+": "+door);
			}
		}
	}

	public override void _Process(double delta)
	{
	}
	public void OnSceneChangeResquest(Door door)
	{
		
		GD.Print(door.targetCoords);
		GD.Print(door.targetScene);
		EmitSignal(SignalName.SceneChangeRequest, door.targetScene, door.targetCoords);
	}
}