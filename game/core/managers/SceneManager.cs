using Godot;

public partial class SceneManager : Node
{
    [Export]
    private PackedScene _initialScene;
    [Export]
    private Node2D _sceneContainer;
    private SceneRoot _currentScene;
    private Player _player;
    [Signal]
    public delegate void playerChangedEventHandler(Player player);

    public override void _EnterTree()
    {
        LoadNewScene(_initialScene);
    }

    private void LoadNewScene(PackedScene scene)
    {
        if (_currentScene != null)
        {
            _currentScene.QueueFree();
        }

        _currentScene = scene.Instantiate<SceneRoot>();
        _sceneContainer.AddChild(_currentScene);

        _player = _currentScene._player;
        NotifyPlayerChanged(_player);
    }

    private void NotifyPlayerChanged(Player player)
    {
        EmitSignal(SignalName.playerChanged, player);
    }
}