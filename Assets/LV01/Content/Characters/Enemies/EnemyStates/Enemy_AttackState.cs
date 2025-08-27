using UnityEngine;

public class Enemy_AttackState : EnemyState
{
    public Enemy_AttackState(Enemy enemy, StateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
            // stateMachine.ChangeState(enemy.BattleState);
            stateMachine.ChangeState(enemy.PostAttackWaitState);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastAttackTime = Time.time;
    }
}
