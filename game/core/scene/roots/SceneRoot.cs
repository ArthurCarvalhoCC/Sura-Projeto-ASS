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
		_doorsContainer.SceneChangeInfos += OnSceneChangeInfos;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	public void OnSceneChangeInfos(Vector2I coords, PackedScene targetScene)
	{
		Managers.Instance.sceneManager.LoadScene(targetScene, coords);
	}
}
