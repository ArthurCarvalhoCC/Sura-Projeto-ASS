using Godot;
using System;

public partial class WorldSceneRoot : Node2D, ISceneRoot
{
	[Export] public AudioStream InitialMusic {get; private set;}
	[Export] public DoorsContainer _doorsContainer;
	[Export] public Player _player;
	public bool hideMobileControls { get; private set; } = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Gui.Instance.SetMobileControlsVisible(hideMobileControls);
		_player.SetNewPosition(Managers.Instance.sceneManager.playerInitialNewSceneCoords);
		Managers.Instance.sceneManager.FinishSceneChange();
		_doorsContainer.SceneChangeRequest += OnSceneChangeRequest;
	}

	public void OnSceneChangeRequest(PackedScene targetScene, Vector2I targetCoords)
	{
		Managers.Instance.sceneManager.SetPlayerInitialNewSceneCoords(targetCoords);
		Managers.Instance.sceneManager.StartSceneChange(targetScene);
	}
}
