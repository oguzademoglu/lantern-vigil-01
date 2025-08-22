using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player player;

    protected PlayerInputs playerInputs;

    public PlayerState(Player player, StateMachine stateMachine, string stateName) : base(stateMachine, stateName)
    {
        this.player = player;
        rb = player.Rb;
        anim = player.Anim;
        // animBoolHash = Animator.StringToHash(stateName);
        playerInputs = player.PlayerInputs;
    }


}
