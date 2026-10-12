using Godot;
using System;

public partial class PauseMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hide();
	}
	public void OnReturnPressed()
	{
		Managers.Instance._sceneManager.FreezeSceneProcess(false);
		Hide();
	}
}
