
using Godot;

public partial class GameButton : BaseButton
{
	[ExportGroup("Animations")]
	[Export] private string _hoverAnimation = "hover";
	[Export] private string _pressedAnimation = "pressed";
	[Export] private string _normalAnimation = "normal";
	
	[ExportGroup("Animations Behaviors")]
	[Export] private bool animatedFromCenter = true;
	[Export] private Vector2 animatedScale = Vector2.One;
	[Export] private float animationDuration = .1f;
	[Export] private Tween.TransitionType transitionType;

	
	private AnimationPlayer _animationPlayer;
	private Vector2 defaultScale;
	

	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");        
		//SetElementPivot();

		MouseEntered += OnMouseEntered;
		FocusEntered += OnMouseEntered;
		MouseExited += OnMouseExited;
		FocusExited += OnMouseExited;
		ButtonDown += OnButtonDown;
		//ButtonUp += OnButtonUp;
	}

	protected virtual void OnMouseEntered() => PlayAnimation(_hoverAnimation);
	protected virtual void OnMouseExited() => PlayAnimation(_normalAnimation);
	protected virtual void OnButtonDown() => PlayAnimation(_pressedAnimation);
	//protected virtual void OnButtonUp() => PlayAnimation(_normalAnimation);

	private void PlayAnimation(string name)
	{
		if (_animationPlayer.HasAnimation(name))
			_animationPlayer.Play(name);
	}
	protected void SetElementPivot()
	{
		if (animatedFromCenter) PivotOffset = Size / 2;
		defaultScale = Scale;
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
