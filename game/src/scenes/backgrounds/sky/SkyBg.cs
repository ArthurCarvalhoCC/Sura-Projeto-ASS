using Godot;
using System;

public partial class SkyBg : CanvasLayer
{
    [Export]
    private Sprite2D _sprite;

    [Export]
    private Color firstColor;

    [Export]
    private Color secondColor;

    [Export(PropertyHint.Range, "0,1,0.01")]
    private float fillFromX = 0.0f;

    [Export(PropertyHint.Range, "0,1,0.01")]
    private float fillFromY = 0.0f;

    [Export(PropertyHint.Range, "0,1,0.01")]
    private float fillToX = 1.0f;

    [Export(PropertyHint.Range, "0,1,0.01")]
    private float fillToY = 1.0f;

    public override void _Ready()
    {
        GradientTexture2D gradientTexture = (GradientTexture2D)_sprite.Texture;
        Gradient gradient = gradientTexture.Gradient;

        // Cores
        gradient.SetColor(0, firstColor);
        gradient.SetColor(1, secondColor);

        // Coordenadas do gradiente
        gradientTexture.FillFrom = new Vector2(fillFromX, fillFromY);
        gradientTexture.FillTo = new Vector2(fillToX, fillToY);
    }
}