using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public StateMachine StateMachine { get; private set; }
    public PlayerInputs PlayerInputs { get; private set; }
    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }
    public Player_JumpState JumpState { get; private set; }
    public Player_FallState FallState { get; private set; }

    [Header("Player Components")]
    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }

    [Header("Movement Details")]
    public Vector2 MoveInput { get; private set; }
    public Vector2 wallJumpForce;
    public float moveSpeed;
    public float dashSpeed;
    public float dashDuration;
    public float jumpForce;
    public float inAirSlowMultiplier;
    public float wallSlideSlowMultiplier;
    private bool facingRight = true;
    public int facingDirection = 1;
    [Header("Jump Assist")]
    public float coyoteTime = .12f;
    public float jumpBuffer = .12f;
    [HideInInspector] public float coyoteCounter;
    [HideInInspector] public float jumpBufferCounter;
    [Header("Collision Detection")]
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    [SerializeField] float groundCheckDistance = 1f;
    [SerializeField] float wallCheckDistance = 1f;
    [SerializeField] Transform wallCheckFirst;
    [SerializeField] Transform wallCheckSecond;
    public bool GroundDetected { get; private set; }
    public bool WallDetected { get; private set; }


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

    void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponentInChildren<Animator>();
        StateMachine = new();
        PlayerInputs = new();
        IdleState = new Player_IdleState(this, StateMachine, "idle");
        MoveState = new Player_MoveState(this, StateMachine, "move");
        JumpState = new Player_JumpState(this, StateMachine, "jumpFall");
        FallState = new Player_FallState(this, StateMachine, "jumpFall");
    }
    void Start()
    {
        StateMachine.InitializeState(IdleState);
    }
    void Update()
    {
        StateMachine.UpdateActiveState();
    }

    void FixedUpdate()
    {
        HandleCollisionDetection();
        StateMachine.PhysicsUpdateActiveState();

        if (GroundDetected) coyoteCounter = coyoteTime;
        else coyoteCounter = Mathf.Max(0f, coyoteCounter - Time.fixedDeltaTime);
        if (jumpBufferCounter > 0)
            jumpBufferCounter = Mathf.Max(0, jumpBufferCounter - Time.fixedDeltaTime);
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        Rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    void HandleFlip(float xVelocity)
    {
        if (xVelocity < 0 && facingRight) Flip();
        else if (xVelocity > 0 && facingRight == false) Flip();
    }

    public void Flip()
    {
        // transform.Rotate(0, 180, 0);
        Vector3 s = transform.localScale;
        s.x *= -1f;
        transform.localScale = s;
        facingRight = !facingRight;
        facingDirection *= -1;
    }


    void HandleCollisionDetection()
    {
        GroundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        WallDetected = Physics2D.Raycast(wallCheckFirst.position, Vector2.right * facingDirection, wallCheckDistance, wallLayer)
                        && Physics2D.Raycast(wallCheckSecond.position, Vector2.right * facingDirection, wallCheckDistance, wallLayer);
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawLine(wallCheckFirst.position, wallCheckFirst.position + new Vector3(wallCheckDistance * facingDirection, 0));
        Gizmos.DrawLine(wallCheckSecond.position, wallCheckSecond.position + new Vector3(wallCheckDistance * facingDirection, 0));
    }

}
