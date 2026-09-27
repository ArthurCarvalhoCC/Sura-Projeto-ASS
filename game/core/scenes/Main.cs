using Godot;
using System;

public partial class Main : Node
{
	public Player player;
	[Export]
	public Node2D _SceneContainer;
	[Export]
	public Control _UI;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Managers.Instance.sceneManager.SetSceneContainer(_SceneContainer);
		Managers.Instance.sceneManager.LoadInitialScene();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
