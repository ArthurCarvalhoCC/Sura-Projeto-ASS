using Godot;

public partial class DebugSceneShortcut : Godot.Node
{
    [Export] private PackedScene _debugScene;   // arrasta a DebugWorld.tscn aqui
    [Export] private Vector2I _spawnCoords;     // onde o player nasce na cena debug

    public override void _UnhandledInput(InputEvent @event)
    {
        if (!Input.IsActionPressed("debug_main")) return;   // F3 segurado
        if (!@event.IsActionPressed("game_up")) return;     // W pressionado agora

        Managers.Instance._sceneManager.SetPlayerInitialNewSceneCoords(_spawnCoords);
        Managers.Instance._sceneManager.StartSceneChange(_debugScene);
        GD.Print("Warping to DebugWorld");
        GetViewport().SetInputAsHandled();
    }
}