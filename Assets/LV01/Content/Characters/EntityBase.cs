using UnityEngine;

public class EntityBase : MonoBehaviour
{
    public StateMachine StateMachine { get; private set; }


    [Header("Character Components")]
    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }

    [Header("Movement Details")]
    public Vector2 wallJumpForce;
    public float moveSpeed;
    public float dashSpeed;
    public float dashDuration;
    public float jumpForce;
    public float inAirSlowMultiplier;
    public float wallSlideSlowMultiplier;
    protected bool facingRight = true;
    public int facingDirection = 1;

    [Header("Combat Details")]
    public int damageAmount;
    public SimpleHealth Health { get; private set; }
    public int maxHealth;

    [Header("Collision Detection")]
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    [SerializeField] float groundCheckDistance = 1f;
    [SerializeField] float wallCheckDistance = 1f;
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform wallCheckFirst;
    [SerializeField] Transform wallCheckSecond;
    public bool GroundDetected;
    public bool WallDetected { get; private set; }

    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponentInChildren<Animator>();
        Health = GetComponent<SimpleHealth>();
        StateMachine = new();
    }
    protected virtual void Start()
    {
        Health.Initialize(maxHealth);
    }
    void Update()
    {
        StateMachine.UpdateActiveState();
    }
    protected virtual void FixedUpdate()
    {
        HandleCollisionDetection();
        StateMachine.PhysicsUpdateActiveState();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        Rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    public void HandleFlip(float xVelocity)
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
        GroundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
        WallDetected = Physics2D.Raycast(wallCheckFirst.position, Vector2.right * facingDirection, wallCheckDistance, wallLayer)
                        && Physics2D.Raycast(wallCheckSecond.position, Vector2.right * facingDirection, wallCheckDistance, wallLayer);
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawLine(wallCheckFirst.position, wallCheckFirst.position + new Vector3(wallCheckDistance * facingDirection, 0));
        Gizmos.DrawLine(wallCheckSecond.position, wallCheckSecond.position + new Vector3(wallCheckDistance * facingDirection, 0));
    }

    public void CallAnimTrig()
    {
        StateMachine.CurrentState.CallAnimTrigger();
    }
}
