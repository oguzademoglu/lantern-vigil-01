using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class Enemy_DeadState : EnemyState
{
    public Enemy_DeadState(Enemy enemy, StateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("enter dead state");
        rb.linearVelocity = new Vector2(10f, 5f);
    }


}
