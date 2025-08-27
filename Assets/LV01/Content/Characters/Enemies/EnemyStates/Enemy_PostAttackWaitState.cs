using UnityEngine;

public class Enemy_PostAttackWaitState : EnemyState
{
    float enterTime;
    public Enemy_PostAttackWaitState(Enemy enemy, StateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enterTime = Time.time;
        enemy.SetVelocity(0f, rb.linearVelocity.y);
    }

    public override void Update()
    {
        base.Update();
        if (Time.time >= enterTime + enemy.postAttackIdleDuration)
            stateMachine.ChangeState(enemy.BattleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
