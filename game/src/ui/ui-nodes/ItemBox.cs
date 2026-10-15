using Godot;

/// Um slot da hotbar. Mostra o item (sprite + quantidade) e avisa quando é clicado/tocado.
public partial class ItemBox : PanelContainer
{
    [Signal] public delegate void PressedEventHandler(int slot);

    public int Slot { get; set; }

    private TextureRect sprite;
    private Label quantity;

    public override void _Ready()
    {
        sprite = GetNode<TextureRect>("Sprite");
        quantity = GetNode<Label>("Quantity");
        GuiInput += OnGuiInput;
        Clear();
    }

    private void OnGuiInput(InputEvent @event)
    {
        bool pressed =
            @event is InputEventMouseButton mouse && mouse.Pressed && mouse.ButtonIndex == MouseButton.Left ||
            @event is InputEventScreenTouch touch && touch.Pressed;
        if (!pressed) return;

        EmitSignal(SignalName.Pressed, Slot);
        AcceptEvent();
    }

    public void SetStack(ItemStack stack)
    {
        if (stack == null)
        {
            Clear();
            return;
        }

        sprite.Texture = stack.getItem().getTexture2D();
        sprite.Visible = true;
        quantity.Text = stack.getQuantity().ToString();
        quantity.Visible = stack.getQuantity() > 1;
    }

    public void Clear()
    {
        sprite.Texture = null;
        sprite.Visible = false;
        quantity.Text = string.Empty;
        quantity.Visible = false;
    }

    public void SetSelected(bool selected)
        => Modulate = selected ? Colors.White : new Color(0.55f, 0.55f, 0.55f);
}
