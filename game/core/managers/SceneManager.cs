using Godot;

public partial class SceneManager : Node
{
    private Node _sceneContainerNode;
    private SceneTransition _transitionNode;
    private Node _currentScene;
    public Vector2I playerInitialNewSceneCoords {get; private set;}

    public void SetSceneContainerNode(Node sceneConteiner)
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
            _sceneContainerNode.ProcessMode = Node.ProcessModeEnum.Disabled; // Para o processamento do sceneContainer

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
        _currentScene = scene.Instantiate<Node>();
        _sceneContainerNode.AddChild(_currentScene);
    }
    public void FinishSceneChange()
    {
        // Volta o processamento do SceneContainer para o padrão
        _sceneContainerNode.ProcessMode = Node.ProcessModeEnum.Inherit;
        Tween fadeIn = _transitionNode.FadeIn();
    }

    public void SetPlayerInitialNewSceneCoords(Vector2I coords)
    {
        playerInitialNewSceneCoords = coords;
    }
}