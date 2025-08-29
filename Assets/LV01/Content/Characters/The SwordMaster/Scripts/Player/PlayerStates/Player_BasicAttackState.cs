using System;
using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    public const int FirstComboIndex = 1;
    public int comboLimit = 3;
    public int comboIndex = 1;
    private float lastTimeAttacked;
    private bool comboAttackQueued;
    private int attackDirection;
    public Player_BasicAttackState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
        if (comboLimit != player.attackVelocity.Length)
            comboLimit = player.attackVelocity.Length;
    }

    private float attackVelocityTimer;

    public override void Enter()
    {
        base.Enter();
        player.swordCollider.SetActive(true);
        attackDirection = player.MoveInput.x != 0 ? (int)player.MoveInput.x : player.facingDirection;
        comboAttackQueued = false;
        ResetComboIndexIfNeeded();
        anim.SetInteger("basicAttackIndex", comboIndex);
        ApplyAttackVelocity();
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        HandleAttackVelocity();

        if (playerInputs.Player.Attack.WasPressedThisFrame())
            QueueNextAttack();

        if (triggerCalled)
            HandleStateExit();
    }

    public override void Exit()
    {
        base.Exit();
        comboIndex++;
        lastTimeAttacked = Time.time;
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
        Vector2 attackVelocity = new(player.attackVelocity[comboIndex - 1].x, player.attackVelocity[comboIndex - 1].y);
        player.SetVelocity(attackVelocity.x * attackDirection, attackVelocity.y);
    }

    void HandleStateExit()
    {
        if (comboAttackQueued)
        {
            anim.SetBool(stateName, false);
            player.EnterAttackStateWithDelay();
        }
        else
            stateMachine.ChangeState(player.IdleState);
    }

    void QueueNextAttack()
    {
        if (comboIndex < comboLimit)
            comboAttackQueued = false;
    }
    void ResetComboIndexIfNeeded()
    {
        if (comboIndex > comboLimit || Time.time > lastTimeAttacked + player.comboResetTime)
            comboIndex = FirstComboIndex;
    }
}


