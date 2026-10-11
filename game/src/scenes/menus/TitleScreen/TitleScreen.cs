using Godot;
using System;

public partial class TitleScreen : UISceneRoot
{
	[Export] private PackedScene targetScene;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
    }
	public void OnPlayPressed()
	{
		GD.Print("aaa");
		Managers.Instance.sceneManager.SetPlayerInitialNewSceneCoords(new Vector2I(0,0));
		Managers.Instance.sceneManager.StartSceneChange(targetScene);
	}
	public void OnReturnPressed(Control currentTab, Control targetMenu)
	{
		currentTab.Hide();
		targetMenu.Show();
	}
}
