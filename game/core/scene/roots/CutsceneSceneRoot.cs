using Godot;
using System;

public partial class CutsceneSceneRoot : Godot.Node, ISceneRoot
{
	[Export] public AudioStream InitialMusic {get; private set;}
	public bool hideMobileControls { get; private set; } = true;

	public override void _Ready()
	{
		Gui.Instance._hotbar.SetHotbarVisible(hideMobileControls);
		Gui.Instance._mobileControls.SetMobileControlsVisible(hideMobileControls);
		Managers.Instance._sceneManager.FinishSceneChange();
		Managers.Instance._audioManager.PlayMusic(InitialMusic);
		
	}
	public void OnSceneChangeRequest(PackedScene targetScene, Vector2I targetCoords)
	{
		Managers.Instance._sceneManager.SetPlayerInitialNewSceneCoords(targetCoords);
		Managers.Instance._sceneManager.StartSceneChange(targetScene);
	}
}
