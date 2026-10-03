using Godot;

public partial class SceneManager : Node
{
    private Node2D _sceneContainerNode;
    private SceneTransition _transitionNode;
    private SceneRoot _currentScene;
    private Player _player;
    public Vector2I playerInitialNewSceneCoords {get; private set;}

    public void SetSceneContainerNode(Node2D sceneConteiner)
    {
        _sceneContainerNode = sceneConteiner;
    }
    public void SetTransitionNode(SceneTransition transitionNode)
    {
        _transitionNode = transitionNode;
    }

    public void LoadInitialScene(PackedScene initialScene)
    {
        StartSceneChange(initialScene);
    }
    public void StartSceneChange(PackedScene scene)
    {
        CallDeferred(nameof(LoadSceneDeferred), scene);
    }
    private async void LoadSceneDeferred(PackedScene scene) // lógica de StartSceneChange "empacotada" para poder usar na função CallDeferred
    {
        if (_currentScene != null)
        {
            Tween fadeOut = _transitionNode.FadeOut();

            fadeOut.Finished += () =>
            {
                _currentScene.QueueFree();
                ContinueSceneChange(scene);
            };
            return;
        }
        ContinueSceneChange(scene);
    }
    private void ContinueSceneChange(PackedScene scene)
    {
        _currentScene = scene.Instantiate<SceneRoot>();
        _sceneContainerNode.AddChild(_currentScene);

        UpdatePlayerReference();
    }
    public void FinishSceneChange()
    {
        _transitionNode.FadeIn();
    }
    private void UpdatePlayerReference()
    {
        if (_currentScene._player == null) return;
        _player = _currentScene._player;
    }
    public void SetPlayerInitialNewSceneCoords(Vector2I coords)
    {
        playerInitialNewSceneCoords = coords;
    }
}