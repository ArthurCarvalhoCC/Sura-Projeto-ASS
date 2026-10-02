using Godot;

public partial class SceneManager : Node
{
    private Node2D _sceneContainerNode;
    private SceneTransition _transitionNode;
    private SceneRoot _currentScene;
    private Player _player;

    public void SetSceneContainerNode(Node2D sceneConteiner)
    {
        _sceneContainerNode = sceneConteiner;
    }
    public void SetTransitionNode(SceneTransition transitionNode)
    {
        _transitionNode = transitionNode;
    }

    public void LoadInitialScene(PackedScene initialScene, Vector2I initialCoords)
    {
        LoadScene(initialScene, initialCoords);
    }
    public void LoadScene(PackedScene scene, Vector2I coords)
    {
        CallDeferred(nameof(LoadSceneDeferred), scene, coords);
    }
    private async void LoadSceneDeferred(PackedScene scene, Vector2I coords) // lógica de LoadScene "empacotada" para poder usar na função CallDeferred
    {
        if (_currentScene != null)
        {
            Tween fadeOut = _transitionNode.FadeOut();

            fadeOut.Finished += () =>
            {
                FinishSceneChange(scene, coords);
            };

            return;
        }
        FinishSceneChange(scene, coords);
    }
    private void FinishSceneChange(PackedScene scene, Vector2I coords)
    {
        if (_currentScene != null) _currentScene.QueueFree();

        _currentScene = scene.Instantiate<SceneRoot>();
        _sceneContainerNode.AddChild(_currentScene);

        UpdatePlayerReference();

        if (_player == null) GD.Print("cannot set player new coords. the player refernce is null."); 
        
        _player.SetNewPosition(coords);

        _transitionNode.FadeIn();
    }
    private void UpdatePlayerReference()
    {
        if (_currentScene._player == null) return;
        _player = _currentScene._player;
    }
}