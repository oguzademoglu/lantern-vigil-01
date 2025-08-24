using UnityEngine;

public class Enemy_AttackState : EnemyState
{
    public Enemy_AttackState(Enemy enemy, StateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
            stateMachine.ChangeState(enemy.IdleState);
    }
}
