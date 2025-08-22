
public class Enemy_MoveState : EnemyState
{
    public Enemy_MoveState(Enemy enemy, StateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        if (enemy.GroundDetected == false || enemy.WallDetected)
            enemy.Flip();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDirection, rb.linearVelocity.y);

        if (enemy.GroundDetected == false || enemy.WallDetected)
            stateMachine.ChangeState(enemy.IdleState);

    }
}
