using Godot;

[Tool]
public partial class SkyBg : CanvasLayer
{
    [Export]
    private Sprite2D _sprite;

    [Export]
    private Node2D _parallaxLayer;

    [Export]
    private Color firstColor
    {
        get => _firstColor;
        set
        {
            _firstColor = value;
            UpdateGradient();
        }
    }

    [Export]
    private Color secondColor
    {
        get => _secondColor;
        set
        {
            _secondColor = value;
            UpdateGradient();
        }
    }

    [Export(PropertyHint.Range, "0,1,0.01")]
    private float fillFromX
    {
        get => _fillFromX;
        set
        {
            _fillFromX = value;
            UpdateGradient();
        }
    }

    [Export(PropertyHint.Range, "0,1,0.01")]
    private float fillFromY
    {
        get => _fillFromY;
        set
        {
            _fillFromY = value;
            UpdateGradient();
        }
    }

    [Export(PropertyHint.Range, "0,1,0.01")]
    private float fillToX
    {
        get => _fillToX;
        set
        {
            _fillToX = value;
            UpdateGradient();
        }
    }

    [Export(PropertyHint.Range, "0,1,0.01")]
    private float fillToY
    {
        get => _fillToY;
        set
        {
            _fillToY = value;
            UpdateGradient();
        }
    }

    [Export(PropertyHint.Range, "0,1,0.01")]
    private float parallaxOpacity
    {
        get => _parallaxOpacity;
        set
        {
            _parallaxOpacity = value;
            UpdateParallaxModulate();
        }
    }

    [Export]
    private Color parallaxModulate
    {
        get => _parallaxModulate;
        set
        {
            _parallaxModulate = value;
            UpdateParallaxModulate();
        }
    }

    private Color _firstColor;
    private Color _secondColor;

    private float _fillFromX = 0.0f;
    private float _fillFromY = 0.0f;
    private float _fillToX = 1.0f;
    private float _fillToY = 1.0f;

    private float _parallaxOpacity = 1.0f;
    private Color _parallaxModulate = Colors.White;

    public override void _Ready()
    {
        UpdateGradient();
        UpdateParallaxModulate();
    }

    private void UpdateGradient()
    {
        if (!IsInstanceValid(_sprite))
            return;

        if (_sprite.Texture is not GradientTexture2D gradientTexture)
            return;

        Gradient gradient = gradientTexture.Gradient;

        if (gradient == null)
            return;

        gradient.SetColor(0, _firstColor);
        gradient.SetColor(1, _secondColor);

        gradientTexture.FillFrom = new Vector2(
            _fillFromX,
            _fillFromY
        );

        gradientTexture.FillTo = new Vector2(
            _fillToX,
            _fillToY
        );
    }

    private void UpdateParallaxModulate()
    {
        if (!IsInstanceValid(_parallaxLayer))
            return;

        Color color = _parallaxModulate;
        color.A = _parallaxOpacity;

        _parallaxLayer.Modulate = color;
    }
}