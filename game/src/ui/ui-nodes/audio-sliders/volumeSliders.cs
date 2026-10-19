using Godot;

public partial class volumeSliders : Godot.Node
{
	private HSlider _masterSlider;
	private HSlider _sfxSlider;
	private HSlider _musicSlider;
	private HSlider _ambientSlider;

	public override void _Ready()
	{
		_masterSlider = GetNode<HSlider>("MasterSlider");
		_sfxSlider = GetNode<HSlider>("SFXSlider");
		_musicSlider = GetNode<HSlider>("MusicSlider");
		_ambientSlider = GetNode<HSlider>("AmbienceSlider");

		_masterSlider.ValueChanged += value => SetBusVolumeFromSlider("Master", value);
		_sfxSlider.ValueChanged += value => SetBusVolumeFromSlider("SFX", value);
		_musicSlider.ValueChanged += value => SetBusVolumeFromSlider("Music", value);
		_ambientSlider.ValueChanged += value => SetBusVolumeFromSlider("Ambience", value);

		ApplySliderValue("Master", _masterSlider);
		ApplySliderValue("SFX", _sfxSlider);
		ApplySliderValue("Music", _musicSlider);
		ApplySliderValue("Ambience", _ambientSlider);
	}

	private static void ApplySliderValue(string bus, HSlider slider)
	{
		SetBusVolumeFromSlider(bus, slider.Value);
	}

	private static void SetBusVolumeFromSlider(string bus, double value)
	{
		Managers.Instance._audioManager.SetBusVolume(bus, (float)(value / 100.0));
	}
}
