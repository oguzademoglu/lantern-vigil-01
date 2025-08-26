using UnityEngine;

public class Player_DeadState : PlayerState
{
    public Player_DeadState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
        Entity_Health entity_health = player.GetComponent<Entity_Health>();
    }

    public override void Enter()
    {
        base.Enter();
        playerInputs.Disable();
        rb.simulated = false;
        // rb.linearVelocity = new Vector2(10f * -player.facingDirection, 4f);
    }
}
