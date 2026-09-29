using Godot;
using System;
using System.Collections.Generic;

public partial class DoorsContainer : Node2D
{
	private List<Door> doorsList = new();

	public override void _Ready()
	{
		foreach (Node child in GetChildren())
		{
			if (child is Door door)
			{
				doorsList.Add(door);
				door.SceneChangeResquest += OnSceneChangeResquest;
				GD.Print(door.Name);
			}
		}
	}

	public override void _Process(double delta)
	{
	}
	public void OnSceneChangeResquest(Door door)
	{

	}
}
