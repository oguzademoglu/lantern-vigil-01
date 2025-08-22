using UnityEngine;

public class Player_AirState : PlayerState
{
    public Player_AirState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }


    public override void Update()
    {
        base.Update();

        // if (player.WallDetected)
        //     stateMachine.ChangeState(player.WallSlideState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // if (Math.Abs(player.MoveInput.x) > 0.001f)
        //     player.SetVelocity(player.MoveInput.x * player.moveSpeed * player.inAirSlowMultiplier, rb.linearVelocity.y);

        player.SetVelocity(player.MoveInput.x * player.moveSpeed * player.inAirSlowMultiplier, rb.linearVelocity.y);

    }

}
