using Godot;
using System;

public partial class AudioManager : Node
{
	public float MasterVolume {get; private set;} = 0f;
	public float SFXVolume {get; private set;} = 0f;
	public float MusicVolume {get; private set;} = 0f;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	#region Called when need 
	public void PlayMusic()
	{
		
	}
	public void ChangeMusic()
	{
		
	}
	public void CleanSceneAudios()
	{
		
	}
	#endregion 
	
	#region Internal Function
	private void CleanAudio(float time)
	{
		
	}
	#endregion
}
