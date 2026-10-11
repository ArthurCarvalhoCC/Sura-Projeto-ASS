using Godot;
using System;

public partial class Gui : Control
{
	public static Gui Instance { get; private set; }
	[Export] private Control _MobileControls;
	[Export] private DialogBox _DialogBox;
	[Export] private SceneTransition _sceneTransition;
	public override void _Ready()
	{
		Instance = this;
	}
	public SceneTransition GetSceneTransition()
	{
		return _sceneTransition;
	}
	public void SetMobileControlsVisible(bool turn)
	{
		_MobileControls.Visible = turn;
	}
}
