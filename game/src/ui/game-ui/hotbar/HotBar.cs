using Godot;
using System;

public partial class HotBar : Control
{
	public void SetHotbarVisible(bool turn)
	{
		Visible = turn;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
