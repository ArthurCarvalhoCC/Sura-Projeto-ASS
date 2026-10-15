using Godot;
using System;

public partial class WorldSceneRoot : Node2D, ISceneRoot
{
	[Export] public AudioStream InitialMusic { get; set; }
	[Export] public DoorsContainer _doorsContainer;
	[Export] public Player _player;
	public bool hideMobileControls { get; private set; } = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Gui.Instance._mobileControls.SetMobileControlsVisible(hideMobileControls);
		_player.SetNewPosition(Managers.Instance._sceneManager.playerInitialNewSceneCoords);
		Managers.Instance._sceneManager.FinishSceneChange();
		Managers.Instance._audioManager.PlayMusic(InitialMusic);
		_doorsContainer.SceneChangeRequest += OnSceneChangeRequest;
	}

	public void OnSceneChangeRequest(PackedScene targetScene, Vector2I targetCoords)
	{
		Managers.Instance._sceneManager.SetPlayerInitialNewSceneCoords(targetCoords);
		Managers.Instance._sceneManager.StartSceneChange(targetScene);
	}
}
