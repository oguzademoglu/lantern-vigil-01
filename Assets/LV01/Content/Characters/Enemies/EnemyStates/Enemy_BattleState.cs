using System;
using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    private Transform player;
    private float lastTimeInBattle;
    public float inGameTime;
    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (player == null)
            player = enemy.PlayerDetected().transform;
        if (ShouldRetreat())
        {
            rb.linearVelocity = new Vector2(enemy.retreatVelocity.x * -DirectionToPlayer(), enemy.retreatVelocity.y);
            enemy.HandleFlip(DirectionToPlayer());
        }
    }

    public override void Update()
    {
        base.Update();

        if (enemy.PlayerDetected() == true)
            UpdateBattleTimer();

        if (BattleTimeIsOver())
            stateMachine.ChangeState(enemy.IdleState);

        if (WithinAttackRange() && enemy.PlayerDetected())
            stateMachine.ChangeState(enemy.AttackState);
        else
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.linearVelocity.y);
    }



    void UpdateBattleTimer() => lastTimeInBattle = Time.time;
    bool BattleTimeIsOver() => Time.time > lastTimeInBattle + enemy.battleTimeDuration;
    bool WithinAttackRange() => DistanceToPlayer() < enemy.attackDistance;
    bool ShouldRetreat() => DistanceToPlayer() < enemy.minRetreatDistance;

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
