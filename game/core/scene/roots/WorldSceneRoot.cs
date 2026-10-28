using Godot;
using System;

public partial class WorldSceneRoot : Node2D, ISceneRoot
{
	[Export] public AudioStream InitialMusic {get; private set;}
	[Export] public AudioStream AmbienceMusic {get; private set;}
	[Export] public DoorsContainer _doorsContainer;
	[Export] public Player _player;
	public bool hideMobileControls { get; private set; } = true;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Gui.Instance._mobileControls.SetMobileControlsVisible(hideMobileControls);
		Gui.Instance._hotbar.SetHotbarVisible(hideMobileControls);
		_player.SetNewPosition(Managers.Instance._sceneManager.playerInitialNewSceneCoords);
		
		Managers.Instance._sceneManager.FinishSceneChange();
		Managers.Instance._audioManager.PlayMusic(InitialMusic);
		Managers.Instance._audioManager.PlaySFX(AmbienceMusic, "Ambience", 1f, -10f);
		_doorsContainer.SceneChangeRequest += OnSceneChangeRequest;
	}

	public void OnSceneChangeRequest(PackedScene targetScene, Vector2I targetCoords)
	{
		Managers.Instance._sceneManager.SetPlayerInitialNewSceneCoords(targetCoords);
		Managers.Instance._sceneManager.StartSceneChange(targetScene);
	}
}
