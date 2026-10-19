
using Godot;

public partial class GameButton : BaseButton
{
    [ExportGroup("Animations")]
    [Export] private string _hoverAnimation = "hover";
    [Export] private string _pressedAnimation = "pressed";
    [Export] private string _normalAnimation = "normal";

    [ExportGroup("Audios")]
    [Export] protected AudioStream hoverButtonSound;
    [Export] protected AudioStream clickButtonSound;

    [ExportGroup("Audio Behaviors")]
    [Export] private string soundCategory = "UI";

    [Export(PropertyHint.Range, "0.1,3.0,0.05")]
    private float hoverPitchMin = 0.95f;
    [Export(PropertyHint.Range, "0.1,3.0,0.05")]
    private float hoverPitchMax = 1.05f;

    [Export(PropertyHint.Range, "-40,6,0.5")]
    private float hoverVolumeMinDb = -2.0f;
    [Export(PropertyHint.Range, "-40,6,0.5")]
    private float hoverVolumeMaxDb = 0.0f;

    [Export(PropertyHint.Range, "0.1,3.0,0.05")]
    private float clickPitchMin = 0.95f;
    [Export(PropertyHint.Range, "0.1,3.0,0.05")]
    private float clickPitchMax = 1.05f;

    [Export(PropertyHint.Range, "-40,6,0.5")]
    private float clickVolumeMinDb = -2.0f;
    [Export(PropertyHint.Range, "-40,6,0.5")]
    private float clickVolumeMaxDb = 0.0f;

    [ExportGroup("Animations Behaviors")]
    [Export] private bool animatedFromCenter = true;
    [Export] private Vector2 animatedScale = Vector2.One;
    [Export] private float animationDuration = 0.1f;
    [Export] private Tween.TransitionType transitionType;

    private AnimationPlayer _animationPlayer;
    private Vector2 defaultScale;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

        MouseEntered += OnMouseEntered;
        FocusEntered += OnMouseEntered;
        MouseExited += OnMouseExited;
        FocusExited += OnMouseExited;
        ButtonDown += OnButtonDown;
        ButtonUp += OnButtonUp;
    }

    protected virtual void OnMouseEntered()
    {
        PlayButtonAudio(hoverButtonSound, hoverPitchMin, hoverPitchMax,
            hoverVolumeMinDb, hoverVolumeMaxDb);
        PlayAnimation(_hoverAnimation);
    }

    protected virtual void OnMouseExited()
    {
        PlayAnimation(_normalAnimation);
    }

    protected virtual void OnButtonDown()
    {
        PlayButtonAudio(clickButtonSound, clickPitchMin, clickPitchMax,
            clickVolumeMinDb, clickVolumeMaxDb);
        PlayAnimation(_pressedAnimation);
    }

    protected virtual void OnButtonUp()
    {
        PlayAnimation(_normalAnimation);
    }

    private void PlayAnimation(string animationName)
    {
        if (_animationPlayer.HasAnimation(animationName))
            _animationPlayer.Play(animationName);
    }

    protected void SetElementPivot()
    {
        if (animatedFromCenter)
            PivotOffset = Size / 2.0f;

        defaultScale = Scale;
    }

    private void PlayButtonAudio(
        AudioStream audio,
        float pitchMin,
        float pitchMax,
        float volumeMinDb,
        float volumeMaxDb)
    {
        if (audio == null || AudioManager.Instance == null)
            return;

        float pitch = (float)GD.RandRange(pitchMin, pitchMax);
        float volumeDb = (float)GD.RandRange(volumeMinDb, volumeMaxDb);

        AudioManager.Instance.PlaySFX(
            audio,
            soundCategory,
            pitch,
            volumeDb
        );
    }

	protected void initTween(Godot.Node target, 
		string property, 
		Variant value, 
		float duration)
	{
		Tween _tween = CreateTween();
		
		_tween?.Kill();
		_tween.TweenProperty( target, property, value, duration).SetTrans(transitionType);
	}

}
