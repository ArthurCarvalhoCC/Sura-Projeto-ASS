using Godot;
using System;

public partial class PauseMenu : Control
{
	[Export] private PackedScene _mainMenuScene;
	public void ShowPauseMenu()
	{
		Managers.Instance._sceneManager.FreezeSceneProcess(true);
		Show();
	}
	public void OnResumePressed()
	{
		HidePauseMenu();
	}
	public void OnMainMenuPressed()
	{
		Managers.Instance._sceneManager.StartSceneChange(_mainMenuScene);
		HidePauseMenu();
	}
	public void HidePauseMenu()
	{
		Managers.Instance._sceneManager.FreezeSceneProcess(false);
		Hide();
	}
}
