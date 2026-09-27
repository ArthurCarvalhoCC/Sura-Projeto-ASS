using Godot;

public partial class SceneManager : Node
{
    [Export]
    private PackedScene _initialScene;
    [Export]
    private Node2D _sceneContainer;
    private SceneRoot _currentScene;
    private Player _player;

    public void SetSceneContainer(Node2D sceneConteiner)
    {
        _sceneContainer = sceneConteiner;
    }


    public void LoadInitialScene()
    {
        LoadNewScene(_initialScene);
    }
    public void LoadNewScene(PackedScene scene)
    {
        if (_currentScene != null)
        {
            _currentScene.QueueFree();
        }

        _currentScene = scene.Instantiate<SceneRoot>();
        _sceneContainer.AddChild(_currentScene);

        UpdatePlayerReference();
    }
    private void UpdatePlayerReference()
    {
        if(_currentScene._player == null) return;
        _player = _currentScene._player;
    }
}