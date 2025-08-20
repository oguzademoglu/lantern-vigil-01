using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string stateName;
    protected int animBoolHash;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected PlayerInputs playerInputs;
    public EntityState(Player player, StateMachine stateMachine, string stateName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.stateName = stateName;
        rb = player.Rb;
        anim = player.Anim;
        animBoolHash = Animator.StringToHash(stateName);
        playerInputs = player.PlayerInputs;
    }

    public virtual void Enter()
    {
        anim.SetBool(animBoolHash, true);
    }
    public virtual void Update() { }

    public virtual void PhysicsUpdate()
    {
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }
    public virtual void Exit()
    {
        anim.SetBool(animBoolHash, false);
    }
}
