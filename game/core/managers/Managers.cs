using Godot;
using System;

public partial class Managers : Node
{
	public static Managers Instance { get; private set; }
	[Export]
	public SceneManager sceneManager { get; private set; }
    // Called when the node enters the scene tree for the first time.
    public override void _EnterTree()
    {
        Instance = this;
    }

	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
