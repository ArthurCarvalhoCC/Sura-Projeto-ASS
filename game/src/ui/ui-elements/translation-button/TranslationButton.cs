
using Godot;

public partial class TranslationButton : Button
{
    [Export]
    public string LocaleCode { get; set; } = "";
public override void _Ready()
{
    Pressed += OnPressed;
}

    private void OnPressed()
    {
        TranslationServer.SetLocale(LocaleCode);
    }
}
