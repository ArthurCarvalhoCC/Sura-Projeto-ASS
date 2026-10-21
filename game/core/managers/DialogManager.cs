using Godot;
using System;
using System.IO;

public partial class DialogManager : Node
{
	public static DialogManager Instance;

	[Signal] public delegate void DigalogFinishedEventHandler();

	private Dialog dialog;
	private long startedFrame = -1;

	public bool isActive()
	{
		return dialog != null && !dialog.isFinished();
	} 

	private DialogBox getDialogBox()
	{
		return Gui.Instance._dialogBox;
	}
	public override void _Ready()
	{
		Instance = this;
	}
	public override void _Process(double delta)
	{
	}

	public void start(Dialog dialog)
	{
		this.dialog = dialog;
		this.startedFrame = (long) Engine.GetProcessFrames();
		showCurrentLine();
	}
	public void showCurrentLine()
	{
		DialogLine curLine = dialog.getCurrentDialogLine();
		DialogBox box = getDialogBox();

		box.ShowDialogue(
			curLine.getTexture(),
			curLine.getDialogSpeed(),
			curLine.getSpeakerName(),
			curLine.getContentTranslationKey()
		);
	}
	public void advance()
	{
		if (!isActive()) return;
		
		DialogBox box = getDialogBox();
		if (box.IsWriting)
		{
			box.FinishWriting();
			return;
		}

		if (dialog.next()) showCurrentLine();
		else end();
	}

	public void end()
	{
		getDialogBox().HideDialogue();
		dialog = null;
		EmitSignal(SignalName.DigalogFinished);
	}

    public override void _UnhandledInput(InputEvent @event)
    {
        if (!isActive()) return;
		if ((long)Engine.GetProcessFrames() == startedFrame) return;
		if (!@event.IsActionPressed("game_accept")) return;

		advance();
		GetViewport().SetInputAsHandled();
    }
}
