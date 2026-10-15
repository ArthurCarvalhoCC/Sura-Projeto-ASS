using Godot;

/// Inventário global do jogador (hotbar), com persistência em user://inventory.json.
public partial class InventoryManager : Node
{
    public const int SlotCount = 7;

    private const string SavePath = "user://inventory.json";

    [Signal] public delegate void ChangedEventHandler();

    private readonly Inventory inventory = Inventory.ofSlots(SlotCount);

    public int SelectedSlot { get; private set; }

    public override void _Ready()
    {
        if (!FileAccess.FileExists(SavePath)) Give("godot", 3); // conteúdo inicial de teste
        Load();
        GetStack(SelectedSlot)?.getItem().onEquip(); // item na mão ao iniciar
    }

    public ItemStack GetStack(int slot) => inventory.getSlot(slot);

    public int Give(ItemStack stack)
    {
        int leftover = inventory.giveItemStack(stack);
        EmitSignal(SignalName.Changed);
        return leftover;
    }

    public int Give(string id, int quantity = 1)
    {
        BaseItem item = ItemDatabase.get(id);
        if (item == null) return quantity;

        int max = item is IStackableItem stackable ? stackable.getMaxStackSize() : 1;
        return Give(ItemStack.of(item, Mathf.Clamp(quantity, 1, max)));
    }

    public void Select(int slot)
    {
        slot = Mathf.PosMod(slot, SlotCount);
        if (slot == SelectedSlot) return;

        GetStack(SelectedSlot)?.getItem().onUnequip(); // saiu do slot: desequipa
        SelectedSlot = slot;
        GetStack(SelectedSlot)?.getItem().onEquip();   // entrou no slot: equipa

        EmitSignal(SignalName.Changed);
    }
    public void Next() => Select(SelectedSlot + 1);
    public void Previous() => Select(SelectedSlot - 1);

    /// Usa o item do slot selecionado (disparado pelo "game_accept").
    public void UseSelected() => GetStack(SelectedSlot)?.getItem().onUse();

    public void Save()
    {
        var data = new Godot.Collections.Array();
        for (int i = 0; i < SlotCount; i++)
        {
            ItemStack stack = inventory.getSlot(i);
            data.Add(stack == null
                ? new Godot.Collections.Dictionary()
                : new Godot.Collections.Dictionary { { "id", stack.getItem().getId() }, { "qty", stack.getQuantity() } });
        }

        using FileAccess file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Write);
        file?.StoreString(Json.Stringify(data));
    }

    public void Load()
    {
        if (!FileAccess.FileExists(SavePath)) return;

        using FileAccess file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Read);
        if (file == null) return;

        Variant parsed = Json.ParseString(file.GetAsText());
        if (parsed.VariantType != Variant.Type.Array) return;

        Godot.Collections.Array data = parsed.AsGodotArray();
        for (int i = 0; i < SlotCount; i++)
        {
            inventory.clearSlot(i);
            Godot.Collections.Dictionary entry = i < data.Count ? data[i].AsGodotDictionary() : new();
            if (!entry.ContainsKey("id")) continue;

            BaseItem item = ItemDatabase.get(entry["id"].AsString());
            if (item == null) continue;

            int max = item is IStackableItem stackable ? stackable.getMaxStackSize() : 1;
            inventory.setSlot(i, ItemStack.of(item, Mathf.Clamp(entry["qty"].AsInt32(), 1, max)));
        }

        EmitSignal(SignalName.Changed);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey { Pressed: true, Echo: false, PhysicalKeycode: Key.F2 })
            Give("godot", 1); // atalho de debug para testar o inventário
    }
}
