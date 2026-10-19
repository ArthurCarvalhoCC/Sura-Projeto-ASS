using Godot;


public abstract partial class PlayerState : Node, IState
{
    [Export] protected Player _player;
    public StateMachine _stateMachine { get; set; }
	
    public virtual void Enter() {}
	public virtual void Update(double delta) { }
	public virtual void PhysicsUpdate(double delta) { }
	public virtual void Exit() { }
    public virtual void SetStateMachine(StateMachine stateMachineReference)
    {
        _stateMachine = stateMachineReference;
    }

}