using Godot;
using System;

public partial class Main : Node
{
	[Export] public Node _sceneContainer { get; private set; }
	[Export] public Gui _GUI { get; private set; }
	[Export] private PackedScene InitialScene;
	[Export] private Vector2I initialPlayerCoords;
	[Export] private Texture2D cursorNormalSprite;
	[Export] private Texture2D cursorClickSprite;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TranslationServer.SetLocale("pt_BR");

		Managers.Instance.sceneManager.SetPlayerInitialNewSceneCoords(initialPlayerCoords);

		Managers.Instance.sceneManager.SetTransitionNode(_GUI.GetSceneTransition());
		Managers.Instance.sceneManager.SetSceneContainerNode(_sceneContainer);
		Managers.Instance.sceneManager.LoadInitialScene(InitialScene);
		SetCursorSprites(cursorNormalSprite, cursorClickSprite);
	}
	private void SetCursorSprites(Texture2D normalCursor, Texture2D clickCursor)
	{
		    Input.SetCustomMouseCursor(
			normalCursor,
			Input.CursorShape.Arrow,
			new Vector2(0, 0)
		);

		Input.SetCustomMouseCursor(
			clickCursor,
			Input.CursorShape.PointingHand,
			new Vector2(0, 0)
		);
	}

}
