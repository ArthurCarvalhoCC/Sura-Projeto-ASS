
using Godot;

public partial class GameButton : Button
{
    [ExportGroup("Animations")]
    [Export] private string _hoverAnimation = "Hover";
    [Export] private string _pressedAnimation = "Pressed";
    [Export] private string _normalAnimation = "Normal";

    private AnimationPlayer _animationPlayer;

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

    private void OnMouseEntered() => PlayAnimation(_hoverAnimation);
    private void OnMouseExited() => PlayAnimation(_normalAnimation);
    private void OnButtonDown() => PlayAnimation(_pressedAnimation);
    private void OnButtonUp() => PlayAnimation(_normalAnimation);

    private void PlayAnimation(string name)
    {
        if (_animationPlayer.HasAnimation(name))
            _animationPlayer.Play(name);
    }
}
