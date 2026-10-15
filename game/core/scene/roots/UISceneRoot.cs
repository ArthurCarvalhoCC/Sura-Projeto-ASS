using Godot;
using System;

public partial class UISceneRoot : Control, ISceneRoot
{

	[Export] public AudioStream InitialMusic { get; private set; }
	public bool hideMobileControls { get; private set; } = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Gui.Instance._mobileControls.SetMobileControlsVisible(hideMobileControls);
		Gui.Instance._hotBar.Hide();
		Managers.Instance._sceneManager.FinishSceneChange();
		Managers.Instance._audioManager.PlayMusic(InitialMusic);
	}
	public void OnSceneChangeRequest(PackedScene targetScene, Vector2I targetCoords)
	{
		Managers.Instance._sceneManager.SetPlayerInitialNewSceneCoords(targetCoords);
		Managers.Instance._sceneManager.StartSceneChange(targetScene);
	}
}
