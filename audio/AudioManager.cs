using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class AudioManager : Node
{
	public static AudioManager Instance { get; private set; }
	private Dictionary< string, SoundQueue > _soundQueueByName = new Dictionary< string, SoundQueue >();
	// Keeps the manager's current track so ChangeMusic can stop or replace it.
	private AudioStream _currentMusic;
	public float MasterVolume {get; private set;} = 0f;
	public float SFXVolume {get; private set;} = 0f;
	public float MusicVolume {get; private set;} = 0f;

	public override void _Ready()
	{
		Instance = this;
		// Resolve the playback queue once; AudioManager owns music decisions, while the queue handles playback.
		_soundQueueByName.Add("SoundQueue", GetNode<SoundQueue>("SoundQueue"));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	#region Called when needed 
	public void PlayMusic(AudioStream Music)
	{
		// Avoid restarting the same track when a scene requests it again.
		if (_currentMusic == Music)
		{
			return;
		}

		_currentMusic = Music;
		ChangeMusic();
	}

	public void ChangeMusic()
	{
		SoundQueue soundQueue = _soundQueueByName["SoundQueue"];
		// A missing track means music should stop; otherwise delegate playback to SoundQueue.
		if (_currentMusic == null)
		{
			soundQueue.StopMusic();
			return;
		}

		soundQueue.PlayMusic(_currentMusic);
	}

	public void PlaySFX(AudioStream sound)
	{
		// Keep SFX requests routed through the manager rather than exposing the queue to scenes.
		_soundQueueByName["SoundQueue"].PlaySFX(sound);
	}

	public void CleanSceneAudios()
	{
		_soundQueueByName["SoundQueue"].CleanSceneAudios();
	}
	#endregion 
	
	#region Internal Function
	private async Task CleanAudio(float time)
	{
	}
	#endregion
}
