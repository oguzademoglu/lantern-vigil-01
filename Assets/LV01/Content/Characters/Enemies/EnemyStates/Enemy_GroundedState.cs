using UnityEngine;

public class Enemy_GroundedState : EnemyState
{
    public Enemy_GroundedState(Enemy enemy, StateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (enemy.PlayerDetection() == true)
        {
            stateMachine.ChangeState(enemy.BattleState);
        }
    }




}
