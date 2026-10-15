using System.Collections.Generic;
using Godot;

/// Liga os slots visuais da hotbar ao InventoryManager.
/// A cena HotBar.tscn não é alterada: os ItemBox existentes são reutilizados.
public partial class HotBar : Control
{
    private readonly List<ItemBox> boxes = new();
    private InventoryManager inventory => Managers.Instance._inventoryManager;

    public override void _Ready()
    {
        VBoxContainer list = GetNode<VBoxContainer>(
            "MarginContainer/PanelContainer/ScrollContainer/MarginContainer/ItemList");

        foreach (Node child in list.GetChildren())
        {
            if (child is not ItemBox box) continue;
            box.Slot = boxes.Count;
            box.Pressed += OnSlotPressed;
            boxes.Add(box);
        }

        inventory.Changed += Refresh;
        Refresh();
    }

    private void OnSlotPressed(int slot) => inventory.Select(slot);

    public override void _Input(InputEvent @event)
    {
        if (!Visible) return;

        if (@event.IsActionPressed("game_accept"))
        {
            // Não consome o evento: InteractBox/DialogBox continuam recebendo "game_accept".
            if (!Managers.Instance._dialogManager.isActive() && !Gui.Instance._pauseMenu.Visible)
                inventory.UseSelected();
            return;
        }

        int step = 0;
        switch (@event)
        {
            case InputEventMouseButton { Pressed: true, ButtonIndex: MouseButton.WheelUp }: step = -1; break;
            case InputEventMouseButton { Pressed: true, ButtonIndex: MouseButton.WheelDown }: step = 1; break;
            case InputEventKey { Pressed: true, Echo: false, PhysicalKeycode: Key.Q }: step = -1; break;
            case InputEventKey { Pressed: true, Echo: false, PhysicalKeycode: Key.E }: step = 1; break;
            case InputEventJoypadButton { Pressed: true, ButtonIndex: JoyButton.LeftShoulder }: step = -1; break;
            case InputEventJoypadButton { Pressed: true, ButtonIndex: JoyButton.RightShoulder }: step = 1; break;
        }

        if (step == 0) return;
        inventory.Select(inventory.SelectedSlot + step);
        GetViewport().SetInputAsHandled();
    }

    private void Refresh()
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            boxes[i].SetStack(inventory.GetStack(i));
            boxes[i].SetSelected(i == inventory.SelectedSlot);
        }
    }
}
