using Godot;
using System;

public partial class MobileControls : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void OnPausePressed()
	{
		Gui.Instance._pauseMenu.ShowPauseMenu();
	}
	public void SetMobileControlsVisible(bool turn)
	{
		Visible = turn;
	}
}
