using Godot;
using System;

public interface ISceneRoot
{
	public AudioStream InitialMusic { get; }
	void OnSceneChangeRequest(PackedScene targetScene, Vector2I targetCoords);
	// Called when the node enters the scene tree for the first time.
}
