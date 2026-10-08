using Godot;
using System;

public partial class WorldSceneRoot : Node2D, ISceneRoot
{
	[Export] public AudioStream InitialMusic {get; private set;}
	[Export] public DoorsContainer _doorsContainer;
	[Export] public Player _player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player.SetNewPosition(Managers.Instance.sceneManager.playerInitialNewSceneCoords);
		Managers.Instance.sceneManager.FinishSceneChange();
		Managers.Instance.audioManager.PlayMusic(InitialMusic);
		_doorsContainer.SceneChangeRequest += OnSceneChangeRequest;
	}

	public void OnSceneChangeRequest(PackedScene targetScene, Vector2I targetCoords)
	{
		Managers.Instance.sceneManager.SetPlayerInitialNewSceneCoords(targetCoords);
		Managers.Instance.sceneManager.StartSceneChange(targetScene);
	}
}
