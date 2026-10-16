using Godot;
using System;

public partial class MobileControls : Control
{
    [Export] private TouchScreenButton _button;
	[Export] private Vector2 _margin = new(40, 40);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetViewport().SizeChanged += UpdatePosition;
        UpdatePosition();
	}

	public void OnPausePressed()
	{
		Gui.Instance._pauseMenu.ShowPauseMenu();
	}
	public void SetMobileControlsVisible(bool turn)
	{
		Visible = turn;
	}
	private void UpdatePosition()
    {
        Vector2 viewportSize = GetViewportRect().Size;
        _button.Position = new Vector2(
            viewportSize.X - _margin.X - _button.TextureNormal.GetSize().X,
            viewportSize.Y - _margin.Y - _button.TextureNormal.GetSize().Y
        );
    }
}
