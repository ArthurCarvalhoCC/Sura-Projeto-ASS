using Godot;
using System;
using System.Collections.Generic;

public partial class DoorsContainer : Node2D
{
	[Signal]
	public delegate void doorsSearchFinishedEventHandler();
	private List<Door> doorsList = new();

	public override void _Ready() 
    {
		foreach (Node child in GetChildren())
		{
			if (child is Door door) {
				doorsList.Add(door);
				GD.Print(door.Name);
			}
			EmitSignal(SignalName.doorsSearchFinished);
		}
	
	}

	public override void _Process(double delta)
	{
	}

	public IReadOnlyList<Door> GetDoorsList()
	{
		return doorsList;
	}
}
