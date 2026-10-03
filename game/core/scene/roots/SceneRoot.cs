using Godot;
using System;

public partial class SceneRoot : Node2D
{
	[Export]
	public DoorsContainer _doorsContainer;
	[Export]
	public Player _player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player.SetNewPosition(Managers.Instance.sceneManager.playerInitialNewSceneCoords);
		Managers.Instance.sceneManager.FinishSceneChange();
		_doorsContainer.SceneChangeInfos += OnSceneChangeRequest;
	}

	public void OnSceneChangeRequest(Vector2I targetCoords, PackedScene targetScene)
	{
		Managers.Instance.sceneManager.SetPlayerInitialNewSceneCoords(targetCoords);
		Managers.Instance.sceneManager.StartSceneChange(targetScene);
	}
}
