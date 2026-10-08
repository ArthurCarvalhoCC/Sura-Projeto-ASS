using Godot;
using System;

public interface ISceneRoot
{
	public AudioStream InitialMusic { get; }
	public bool hideMobileControls {get; }
	void OnSceneChangeRequest(PackedScene targetScene, Vector2I targetCoords);
}
