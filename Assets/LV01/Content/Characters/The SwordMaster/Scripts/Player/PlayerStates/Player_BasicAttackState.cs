using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    public Player_BasicAttackState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    private float attackVelocityTimer;

    public override void Enter()
    {
        base.Enter();
        player.swordCollider.SetActive(true);
        ApplyAttackVelocity();
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        HandleAttackVelocity();

        if (triggerCalled)
        {
            if (player.GroundDetected) stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.swordCollider.SetActive(false);
    }

    void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.fixedDeltaTime;
        if (attackVelocityTimer < 0f)
            player.SetVelocity(0, rb.linearVelocity.y);
    }

    void ApplyAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(player.attackVelocity.x * player.facingDirection, player.attackVelocity.y);
    }
}


