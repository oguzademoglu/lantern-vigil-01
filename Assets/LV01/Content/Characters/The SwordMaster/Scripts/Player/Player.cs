using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : EntityBase
{
    public static event Action OnPlayerDeath;
    public PlayerInputs PlayerInputs { get; private set; }
    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }
    public Player_JumpState JumpState { get; private set; }
    public Player_FallState FallState { get; private set; }
    public Player_BasicAttackState BasicAttackState { get; private set; }
    public Player_JumpAttackState JumpAttackState { get; private set; }
    public Player_DeadState DeadState { get; private set; }

    [Header("Movement Details")]
    public Vector2 MoveInput { get; private set; }

    [Header("Jump Assist")]
    public float coyoteTime = .12f;
    public float jumpBuffer = .12f;
    [HideInInspector] public float coyoteCounter;
    [HideInInspector] public float jumpBufferCounter;

    [Header("Combat Details")]
    public Vector2[] attackVelocity;
    public Vector2 jumpAttackVelocity;
    public float attackVelocityDuration = .1f;
    public float comboResetTime = 1f;
    public bool comboAttackQueued;
    public Coroutine comboAttackCo;
    public GameObject swordCollider;


    void OnEnable()
    {
        PlayerInputs.Enable();
        PlayerInputs.Player.Movement.performed += OnMove;
        PlayerInputs.Player.Movement.canceled += OnMoveCancel;
        PlayerInputs.Player.Jump.performed += OnJump;
    }

    void OnDisable()
    {
        PlayerInputs.Player.Movement.performed -= OnMove;
        PlayerInputs.Player.Movement.canceled -= OnMoveCancel;
        PlayerInputs.Player.Jump.performed -= OnJump;
        PlayerInputs.Disable();
    }

    void OnMove(InputAction.CallbackContext ctx) => MoveInput = ctx.ReadValue<Vector2>();
    void OnMoveCancel(InputAction.CallbackContext ctx) => MoveInput = Vector2.zero;
    void OnJump(InputAction.CallbackContext _) => jumpBufferCounter = jumpBuffer;

    protected override void Awake()
    {
        base.Awake();
        PlayerInputs = new();
        IdleState = new Player_IdleState(this, StateMachine, "idle");
        MoveState = new Player_MoveState(this, StateMachine, "move");
        JumpState = new Player_JumpState(this, StateMachine, "jumpFall");
        FallState = new Player_FallState(this, StateMachine, "jumpFall");
        DeadState = new Player_DeadState(this, StateMachine, "dead");
        BasicAttackState = new Player_BasicAttackState(this, StateMachine, "basicAttack");
        JumpAttackState = new Player_JumpAttackState(this, StateMachine, "jumpAttack");
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.InitializeState(IdleState);
    }


    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        // HandleCollisionDetection();
        // StateMachine.PhysicsUpdateActiveState();

        if (GroundDetected) coyoteCounter = coyoteTime;
        else coyoteCounter = Mathf.Max(0f, coyoteCounter - Time.fixedDeltaTime);
        if (jumpBufferCounter > 0)
            jumpBufferCounter = Mathf.Max(0, jumpBufferCounter - Time.fixedDeltaTime);
    }

    public override void EntityDeath()
    {
        base.EntityDeath();
        OnPlayerDeath?.Invoke();
        StateMachine.ChangeState(DeadState);
    }

    public void EnterAttackStateWithDelay()
    {
        if (comboAttackCo != null)
            StopCoroutine(comboAttackCo);
        comboAttackCo = StartCoroutine(EnterAttackStateWithDelayCo());
    }

    IEnumerator EnterAttackStateWithDelayCo()
    {
        yield return new WaitForEndOfFrame();
        StateMachine.ChangeState(BasicAttackState);
    }




}
