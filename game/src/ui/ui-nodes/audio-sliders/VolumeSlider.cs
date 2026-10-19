
using Godot;

public partial class VolumeSlider : Control
{
    public enum AudioBus
    {
        Master,
        SFX,
        Music,
        Ambience
    }

    private Label _label;
    private HSlider _hSlider;
    [Export] private string labelName = "Name";
    [Export] private AudioBus _audioBus = AudioBus.Master;

    public override void _Ready()
    {
        _label = GetNode<Label>("Label");
        _hSlider = GetNode<HSlider>("HSlider");
        _label.Text = labelName;
        _hSlider.ValueChanged += OnValueChanged;

        // Sincroniza o slider com o volume atual do barramento.
        _hSlider.Value = GetBusVolume();
    }

    private void OnValueChanged(double value)
    {
        Managers.Instance._audioManager.SetBusVolume(
            _audioBus.ToString(),
            (float)(value / 100.0)
        );
    }

    private double GetBusVolume()
    {
        // Substitua pela leitura do volume atual no AudioManager,
        // caso esse método já exista nele.
        return 100.0;
    }
}
