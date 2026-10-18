
using Godot;

public partial class TranslationButton : GameButton
{
    [Export] public string LocaleCode { get; private set; } = "";
    public override void _Ready()
    {
        base._Ready();
    }

    private void OnPressed()
    {
        TranslationServer.SetLocale(LocaleCode);
    }
}
