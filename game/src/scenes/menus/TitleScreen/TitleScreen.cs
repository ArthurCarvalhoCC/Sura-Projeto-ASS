using Godot;
using System;

public partial class TitleScreen : UISceneRoot
{
	[Export] private PackedScene targetScene;
    // Called when the node enters the scene tree for the first time.
	[Export] private Control Main;
	[Export] private Control Settings;
	[Export] private Control Credits;
    public override void _Ready()
    {
        base._Ready();
    }
	public void OnPlayPressed()
	{
		Managers.Instance._sceneManager.SetPlayerInitialNewSceneCoords(new Vector2I(0,0));
		Managers.Instance._sceneManager.StartSceneChange(targetScene);
	}
	public void OnReturnPressed()
	{
		Settings.Hide();
		Credits.Hide();
		Main.Show();
	}
	public void OnCreditsPressed()
	{
		Settings.Hide();
		Credits.Show();
		Main.Hide();
	}
	public void OnSettingsPressed()
	{
		Settings.Show();
		Credits.Hide();
		Main.Hide();
	}
	public void OnQuitPressed()
	{
		Managers.Instance._sceneManager.QuitGame();
	}
}
