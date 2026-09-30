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
}
