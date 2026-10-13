using Godot;
using System;

[GlobalClass]
public partial class InteractBox : Area2D
{
	[Signal] public delegate void InteractedEventHandler();

	[Export] private Control balloon;
	[Export] private bool oneShot = false;

	private bool playerInRange;
	private bool used;

	public override void _Ready()
	{
		BodyEntered += bodyEntered;
		BodyExited += bodyExited;
		if (balloon != null) balloon.Visible = false;
	}

	public void bodyEntered(Node2D body)
	{
		if (body is not Player || used)
		{
			return;
		}
		playerInRange = true;
		if (balloon != null)
		{
			balloon.Visible = true;
		}
	}
	public void bodyExited(Node2D body)
	{
		if (balloon != null)
		{
			balloon.Visible = false;
		}
		playerInRange = false;
	}

    public override void _UnhandledInput(InputEvent @event)
    {
        if (!playerInRange || used) return;
		if (Managers.Instance._dialogManager.isActive()) return;
		if (!@event.IsActionPressed("game_accept")) return;

		used = oneShot;
		EmitSignal(SignalName.Interacted);
		GetViewport().SetInputAsHandled();
    }
}
