using Godot;
using System;

public partial class Main : Node
{
	public Player player;
	[Export]
	public Node2D _SceneContainer;
	[Export]
	public Control _UI;
	[Export]
	public SceneTransition _SceneTransition;
	[Export]
	private PackedScene InitialScenePath;
	[Export]
	private Vector2I InitialPlayerCoords;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TranslationServer.SetLocale("pt_BR");
		
		Managers.Instance.sceneManager.SetTransitionNode(_SceneTransition);	
		Managers.Instance.sceneManager.SetSceneContainerNode(_SceneContainer);
		Managers.Instance.sceneManager.LoadInitialScene(InitialScenePath, InitialPlayerCoords);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
