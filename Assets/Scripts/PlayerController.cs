using UnityEngine;
 
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 14f;
 
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
 
    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput;
 
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
 
    private void Update()
    {
        // Leer input horizontal
        horizontalInput = Input.GetAxisRaw("Horizontal");
 
        // Detectar suelo
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
 
        // Saltar
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );
        }

    }
 
    private void FixedUpdate()
    {
        // Aplicar movimiento horizontal
        rb.linearVelocity = new Vector2(
            horizontalInput * moveSpeed,
            rb.linearVelocity.y
        );

        // Voltear sprite según dirección
        if (horizontalInput != 0)

{
            transform.localScale = new Vector3(
                - Mathf.Sign(horizontalInput),
                1f, 1f
            );
        }
    }
 
    // Visualizar el ground check en el editor
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundCheckRadius
        );
    }
}
