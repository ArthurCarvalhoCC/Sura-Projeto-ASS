using Godot;
using System;

public partial class MobileControls : Control
{
	[Export] private TouchScreenButton _interactButton;
	[Export] private Vector2 _margin = new(40, 40);

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
        _interactButton.Position = new Vector2(
            viewportSize.X - _margin.X - _interactButton.TextureNormal.GetSize().X,
            viewportSize.Y - _margin.Y - _interactButton.TextureNormal.GetSize().Y
        );
	}
}