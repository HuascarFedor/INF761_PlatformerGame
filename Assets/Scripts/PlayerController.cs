using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float acceleration = 60f;
    [SerializeField] private float deceleration = 50f;
    [SerializeField] private float airAcceleration = 40f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private float fallMultiplier = 3f;
    [SerializeField] private float lowJumpMultiplier = 5f;

    [Header("Coyote Time & Jump Buffer")]
    [SerializeField] private float coyoteTime = 0.12f;
    [SerializeField] private float jumpBufferTime = 0.15f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    // Componentes
    private Rigidbody2D rb;
    private Animator animator;

    // Estado interno
    private float horizontalInput;
    private bool isGrounded;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    private bool isJumping;

    // ── IDs de parámetros del Animator (optimización) ──
    private static readonly int SpeedParam =
        Animator.StringToHash("Speed");
    private static readonly int IsGroundedParam =
        Animator.StringToHash("isGrounded");
    private static readonly int YVelocityParam =
        Animator.StringToHash("yVelocity");

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        // ── Input ──
        horizontalInput = Input.GetAxisRaw("Horizontal");
        // ── Ground Check ──
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        // ── Coyote Time ──
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            isJumping = false;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // ── Jump Buffer ──
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // ── Ejecutar salto ──
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f
            && !isJumping)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x, jumpForce
            );
            isJumping = true;
            jumpBufferCounter = 0f;
            coyoteTimeCounter = 0f;
        }

        // ── Salto variable (soltar = cortar) ──
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                rb.linearVelocity.y * 0.5f
            );
        }

        // ── Voltear sprite ──
        if (horizontalInput != 0)
        {
            transform.localScale = new Vector3(
                -Mathf.Sign(horizontalInput), 1f, 1f
            );
        }

        // ── Actualizar Animator ──
        animator.SetFloat(SpeedParam,
            Mathf.Abs(horizontalInput));
        animator.SetBool(IsGroundedParam, isGrounded);
        animator.SetFloat(YVelocityParam,
            rb.linearVelocity.y);
    }

    private void FixedUpdate()
    {
        // ── Movimiento horizontal con aceleración ──
        float targetSpeed = horizontalInput * moveSpeed;
        float accelRate = isGrounded
            ? (Mathf.Abs(targetSpeed) > 0.01f
                ? acceleration : deceleration)
            : airAcceleration;

        float speedDiff = targetSpeed - rb.linearVelocity.x;
        float movement = speedDiff * accelRate
            * Time.fixedDeltaTime;

        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x + movement,
            rb.linearVelocity.y
        );

        // ── Gravedad mejorada ──
        if (rb.linearVelocity.y < 0f)
        {
            // Cayendo: gravedad extra
            rb.linearVelocity += Vector2.up
                * (Physics2D.gravity.y * (fallMultiplier - 1f)
                * Time.fixedDeltaTime);
        }
        else if (rb.linearVelocity.y > 0f
            && !Input.GetButton("Jump"))
        {
            // Subiendo pero botón suelto: caer más rápido
            rb.linearVelocity += Vector2.up
                * (Physics2D.gravity.y * (lowJumpMultiplier - 1f)
                * Time.fixedDeltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(
            groundCheck.position, groundCheckRadius
        );
    }
}
