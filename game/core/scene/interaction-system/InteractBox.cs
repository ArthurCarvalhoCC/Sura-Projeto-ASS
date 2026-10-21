
using Godot;

[GlobalClass]
public partial class InteractBox : Area2D
{
    [Signal]
    public delegate void InteractedEventHandler();

    [Export] private Control balloon;
    [Export] private AnimationPlayer _animationPlayer;
    [Export] private bool oneShot = false;
    [Export] private float fadeDuration = 0.2f;

    private bool playerInRange;
    private bool used;

    private Tween _fadeTween;

    public override void _Ready()
    {
        BodyEntered += BodyEnteredHandler;
        BodyExited += BodyExitedHandler;

        if (balloon != null)
        {
            Color color = balloon.SelfModulate;
            color.A = 0.0f;
            balloon.SelfModulate = color;
        }
    }

    private void BodyEnteredHandler(Node2D body)
    {
        if (body is not Player || used)
            return;

        playerInRange = true;

        if (balloon == null)
            return;

        FadeBalloon(1.0f);
    }

    private void BodyExitedHandler(Node2D body)
    {
        if (body is not Player)
            return;

        playerInRange = false;

        if (balloon == null)
            return;

        FadeBalloon(0.0f);
    }

    private void FadeBalloon(float targetAlpha)
    {
        _fadeTween?.Kill();

        _fadeTween = CreateTween();

        _fadeTween.TweenProperty(
            balloon,
            "self_modulate:a",
            targetAlpha,
            fadeDuration
        );
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (!playerInRange || used)
            return;

        if (Managers.Instance._dialogManager.isActive())
            return;

        if (!@event.IsActionPressed("game_accept"))
            return;

        used = oneShot;

        EmitSignal(SignalName.Interacted);
        GetViewport().SetInputAsHandled();
    }
}
