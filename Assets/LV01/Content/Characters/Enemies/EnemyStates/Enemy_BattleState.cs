using System;
using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    private Transform player;
    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (player == null)
            player = enemy.PlayerDetected().transform;
    }

    public override void Update()
    {
        base.Update();
        if (WithinAttackRange() && enemy.PlayerDetected())
            stateMachine.ChangeState(enemy.AttackState);
        else
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.linearVelocity.y);
    }

    bool WithinAttackRange() => DistanceToPlayer() < enemy.attackDistance;

    float DistanceToPlayer()
    {
        if (player == null)
            return float.MaxValue;

        return MathF.Abs(player.position.x - enemy.transform.position.x);
    }

    int DirectionToPlayer()
    {
        if (player == null) return 0;
        return player.position.x > enemy.transform.position.x ? 1 : -1;
    }


}
