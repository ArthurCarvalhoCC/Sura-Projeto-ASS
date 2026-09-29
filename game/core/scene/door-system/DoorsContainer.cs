using Godot;
using System;
using System.Collections.Generic;

public partial class DoorsContainer : Node2D
{
	[Signal]
	public delegate void SceneChangeInfosEventHandler(Vector2I coords, PackedScene targetScene);
	private List<Door> doorsList = new();

	public override void _Ready()
	{
		foreach (Node child in GetChildren())
		{
			if (child is Door door)
			{
				doorsList.Add(door);
				door.SceneChangeRequest += OnSceneChangeResquest;
				GD.Print(door.Name);
			}
		}
	}

	public override void _Process(double delta)
	{
	}
	public void OnSceneChangeResquest(Door door)
	{
		
		GD.Print(door._targetCoords);
		GD.Print(door.targetScene);
		EmitSignal(SignalName.SceneChangeInfos, door._targetCoords, door.targetScene);
	}
}
