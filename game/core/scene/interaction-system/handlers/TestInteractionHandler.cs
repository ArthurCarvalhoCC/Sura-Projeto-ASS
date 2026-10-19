using Godot;
using System;

public partial class TestInteractionHandler : Godot.Node
{
	[Export] private InteractBox box;

	public override void _Ready()
	{
		box.Interacted += () =>
		{
			var dialog = Dialog.of(
				DialogLine.of(
					DialogSpeed.NORMAL, // <- velocidade
					"NPC_MARIA_1", // <- fala
					null,
					"NPC_MARIA_NAME" // <- nome
				),
				DialogLine.of(
					DialogSpeed.FAST,
					"NPC_MARIA_2",
					null,
					"NPC_MARIA_NAME"
				),
				DialogLine.of(
					DialogSpeed.FASTER,
					"NPC_MARIA_3",
					null, // <- portrato
					"NPC_MARIA_NAME"
				),
				DialogLine.of(
					DialogSpeed.SLOWER,
					". . . s i x   s e v e n",
					null,
					"enzo da silva"
				)
			);

			DialogManager.Instance.start(dialog);
		};
	}

}
