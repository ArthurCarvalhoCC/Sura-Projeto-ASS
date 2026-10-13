using Godot;
using System;

public partial class Gui : Control
{
	public static Gui Instance { get; private set; }
	[Export] public MobileControls _mobileControls { get; private set; }
	[Export] public DialogBox _dialogBox { get; private set; }
	[Export] public SceneTransition _sceneTransition { get; private set; }
	[Export] public PauseMenu _pauseMenu { get; private set; }
	public override void _Ready()
	{
		Instance = this;
	}
	public SceneTransition GetSceneTransition()
	{
		return _sceneTransition;
	}

}
