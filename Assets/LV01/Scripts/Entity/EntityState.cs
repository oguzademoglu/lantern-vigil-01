using UnityEngine;

public abstract class EntityState
{
    protected StateMachine stateMachine;
    protected string stateName;
    protected int animBoolHash;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected bool triggerCalled;
    protected float stateTimer;

    public EntityState(StateMachine stateMachine, string stateName)
    {
        this.stateMachine = stateMachine;
        this.stateName = stateName;
        animBoolHash = Animator.StringToHash(stateName);
    }

    public virtual void Enter()
    {
        anim.SetBool(animBoolHash, true);
        triggerCalled = false;
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void PhysicsUpdate()
    {
        // anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }
    public virtual void Exit()
    {
        anim.SetBool(animBoolHash, false);
    }

    public void CallAnimTrigger()
    {
        triggerCalled = true;
    }
}
