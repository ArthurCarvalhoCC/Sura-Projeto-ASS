using Godot;
using System;
using Godot.Collections;

public partial class Player : CharacterBody2D
{
	[Signal]
	public delegate void StateChangedEventHandler(string stateName);
	[Export] public float MoveSpd = 40f;
	[Export] public AnimationTree _animTree;
	[Export] public CollisionShape2D _collision;
	[Export] public Camera2D _camera;
	[Export] public float acceleration = 3000f;
	[Export] public float fricttion = 5000f;
	
	public AnimationNodeStateMachinePlayback _animStateMachine;
	public Vector2 velocity = Vector2.Zero;
	public Vector2 direction = Vector2.Zero;
	public bool debugMode = false;
	public bool collisionActive = true;
	public bool back = false;

	[ExportGroup("Footsteps")]
	[Export] public AudioStream FootstepAudio;
	[Export] public float StepInterval = 0.25f; // isso foi no feeling, ajuste para melhor sincronizar com animação
	[Export] public string SoundQueueCategory = "Footsteps"; // não precisa ser desse jeito tmb
	private double _stepCooldown = 0;

	public override void _Ready()
	{
		_camera.ProcessCallback = Camera2D.Camera2DProcessCallback.Physics;
		_animStateMachine = (AnimationNodeStateMachinePlayback)_animTree.Get("parameters/playback");
	}

	public override void _PhysicsProcess(double delta)
	{
		direction = Input.GetVector("game_left", "game_right", "game_up", "game_down");
		debugMode = Input.IsActionJustPressed("debug_mode");
		back = Input.IsActionJustPressed("game_back");
		if (_stepCooldown > 0) _stepCooldown -= delta;
	}

	public void NotifyStateChanged(string newState)
	{
		GD.Print("State: " + newState);
		EmitSignal(SignalName.StateChanged, newState);
		GD.Print(Tr("MENU_MAIN_TITLE"));
	}
	public void SetNewPosition(Vector2I position)
	{
		Position = position;	
	}

	public void TryPlayFootstep() // "try" pois o cooldown impede que o som seja metralhado a cada frame, mesmo que o player esteja andando
	{
		if (FootstepAudio == null || _stepCooldown > 0) return;

		// small pitch/volume variation so repeated steps don't sound identical
		float pitch = (float)GD.RandRange(0.92, 1.08);
		float volumeDb = (float)GD.RandRange(-3.0, 0.0);
		AudioManager.Instance.PlaySFX(FootstepAudio, SoundQueueCategory, pitch, volumeDb); //pode passar uma string direto pela categoria
		// desde que a string mandada seja um nome dentro de SoundPool
		_stepCooldown = StepInterval;
	}
}