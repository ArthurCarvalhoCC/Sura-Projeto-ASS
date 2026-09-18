using Godot;
using System;

public partial class SceneManager : Node
{
    [Export]
    private PackedScene _initialScene;
    [Export]
    private Node SceneContainer;
    private Node _currentScene;

    public override void _EnterTree()
    {
        loadNewScene(_initialScene);
    }

    private void loadNewScene(PackedScene scene)
    {
        if(_currentScene != null) _currentScene.QueueFree();
        _currentScene = scene.Instantiate();
        SceneContainer.AddChild(_currentScene);
    }

}
