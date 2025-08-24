using UnityEngine;

public class EnemyState : EntityState
{
    protected Enemy enemy;
    public EnemyState(Enemy enemy, StateMachine stateMachine, string stateName) : base(stateMachine, stateName)
    {
        this.enemy = enemy;
        rb = enemy.Rb;
        anim = enemy.Anim;
    }

    public override void Update()
    {
        base.Update();
        // if (Input.GetKeyDown(KeyCode.F))
        // {
        //     stateMachine.ChangeState(enemy.AttackState);
        // }
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
    }
}
