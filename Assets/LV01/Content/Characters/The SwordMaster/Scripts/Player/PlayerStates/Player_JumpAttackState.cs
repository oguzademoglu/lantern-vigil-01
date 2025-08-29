using UnityEngine;

public class Player_JumpAttackState : PlayerState
{
    private bool touchedGround;
    public Player_JumpAttackState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        touchedGround = false;
        player.SetVelocity(player.jumpAttackVelocity.x * player.facingDirection, player.jumpAttackVelocity.y);
    }

    public override void Update()
    {
        base.Update();
        if (player.GroundDetected && touchedGround == false)
        {
            touchedGround = true;
            anim.SetTrigger("jumpAttackTrigger");
            player.SetVelocity(0, rb.linearVelocity.y);
        }

        if (triggerCalled && player.GroundDetected)
            stateMachine.ChangeState(player.IdleState);
    }
}
