using System;
using UnityEngine;

public class Enemy : EntityBase
{
    public Enemy_IdleState IdleState { get; private set; }
    public Enemy_MoveState MoveState { get; private set; }
    public Enemy_AttackState AttackState { get; private set; }
    public Enemy_BattleState BattleState { get; private set; }

    public float idleTime;
    [Header("Battle Details")]
    public float battleMoveSpeed = 3;
    public float attackDistance = 2;
    [Header("Movement Details")]
    [SerializeField] private LayerMask playerLayer;
    [Header("Player Detection")]
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance = 10f;

    protected override void Awake()
    {
        base.Awake();
        IdleState = new Enemy_IdleState(this, StateMachine, "idle");
        MoveState = new Enemy_MoveState(this, StateMachine, "move");
        AttackState = new Enemy_AttackState(this, StateMachine, "attack");
        BattleState = new Enemy_BattleState(this, StateMachine, "battle");
    }
    protected override void Start()
    {
        base.Start();
        StateMachine.InitializeState(IdleState);
    }

    public RaycastHit2D PlayerDetected()
    {
        RaycastHit2D hit =
            Physics2D.Raycast(playerCheck.position, Vector2.right * facingDirection, playerCheckDistance, playerLayer | groundLayer);
        if (hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            return default;
        return hit;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position,
            new Vector3(playerCheck.position.x + (playerCheckDistance * facingDirection), playerCheck.position.y));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(playerCheck.position,
            new Vector3(playerCheck.position.x + (attackDistance * facingDirection), playerCheck.position.y));
    }
}
