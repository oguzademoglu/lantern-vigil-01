using UnityEngine;

public class Enemy : EntityBase
{
    public Enemy_IdleState IdleState { get; private set; }
    public Enemy_MoveState MoveState { get; private set; }
    public Enemy_AttackState AttackState { get; private set; }

    public float idleTime;
    protected override void Awake()
    {
        base.Awake();
        IdleState = new Enemy_IdleState(this, StateMachine, "idle");
        MoveState = new Enemy_MoveState(this, StateMachine, "move");
        AttackState = new Enemy_AttackState(this, StateMachine, "attack");
    }
    protected override void Start()
    {
        base.Start();
        StateMachine.InitializeState(IdleState);
    }
}
