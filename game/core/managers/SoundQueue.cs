using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class SoundQueue : Node
{
	private int _next = 0;
	private List<AudioStreamPlayer> _audioStreamPlayers = new List<AudioStreamPlayer>(); 
	[Export] private int Count { get; set;} = 1;
	// Music uses its own player so SFX playback and scene cleanup cannot interrupt it.
	private AudioStreamPlayer _musicPlayer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (GetChildCount() == 0)
		{
			_musicPlayer = new AudioStreamPlayer { Name = "MusicPlayer" };
			AddChild(_musicPlayer);
		}
		else if (GetChild(0) is AudioStreamPlayer audioStreamPlayer)
		{
			_musicPlayer = audioStreamPlayer;

			// Build the initial SFX pool from the configured player.
			for (int i = 0; i < Count; i++)
			{
				AudioStreamPlayer duplicate = (AudioStreamPlayer)audioStreamPlayer.Duplicate();
				duplicate.Name = $"SFXPlayer{i}";
				AddChild(duplicate);
				_audioStreamPlayers.Add(duplicate);
			}
		}
		else
		{
			GD.PushError("SoundQueue requires an AudioStreamPlayer child for music playback.");
		}
	}

	public void PlayMusic(AudioStream stream)
	{
		// Null music requests stop the current track.
		if (stream == null)
		{
			StopMusic();
			return;
		}

		if (_musicPlayer.Stream == stream && _musicPlayer.Playing) // Avoid restarting the same track when a scene requests it again.
		{
			return;
		}

		_musicPlayer.Stop();
		_musicPlayer.Stream = stream; 
		_musicPlayer.Play();
	}

	public void StopMusic()
	{
		_musicPlayer.Stop();
		_musicPlayer.Stream = null;
	}

	public void PlaySFX(AudioStream stream)
	{
		if (stream == null)
		{
			GD.PushError("Cannot play an SFX without an audio stream.");
			return;
		}

		AudioStreamPlayer player = GetAvailableSfxPlayer(); // Get an available player from the pool or create a new one if all are busy.
		player.Stream = stream;
		player.Play();
	}

	public void CleanSceneAudios()
	{
		// Reset only SFX players; music persists across scene changes.
		foreach (AudioStreamPlayer player in _audioStreamPlayers)
		{
			player.Stop();
			player.Stream = null;
		}

		_next = 0;
	}

	private AudioStreamPlayer GetAvailableSfxPlayer()
	{
		// Reuse idle players first, creating another only when all pooled players are busy.
		for (int i = 0; i < _audioStreamPlayers.Count; i++)
		{
			int index = (_next + i) % _audioStreamPlayers.Count;
			if (!_audioStreamPlayers[index].Playing)
			{
				_next = (index + 1) % _audioStreamPlayers.Count;
				return _audioStreamPlayers[index];
			}
		}

		AudioStreamPlayer player = new AudioStreamPlayer { Name = $"SFXPlayer{_audioStreamPlayers.Count}" };
		AddChild(player);
		_audioStreamPlayers.Add(player);
		_next = 0;
		return player;
	}
}
