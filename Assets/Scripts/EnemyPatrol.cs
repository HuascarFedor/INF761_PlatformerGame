using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float moveSpeed = 2f;

    [Header("Edge Detection")]
    [SerializeField] private Transform edgeCheck;
    [SerializeField] private float edgeCheckDistance = 1f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Vector2 currentTarget;
    private bool facingRight = true;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentTarget = pointB.position;
    }

    private void FixedUpdate()
    {
        Patrol();
        CheckEdge();
    }

    private void Patrol()
    {
        // Mover hacia el objetivo actual
        Vector2 direction = (
            currentTarget - (Vector2)transform.position
        ).normalized;

        rb.linearVelocity = new Vector2(
            direction.x * moveSpeed,
            rb.linearVelocity.y
        );

        // Verificar si llegó al waypoint
        float distance = Vector2.Distance(
            transform.position, currentTarget
        );
        if (distance < 0.2f)
        {
            // Cambiar de objetivo
            currentTarget =
                currentTarget == (Vector2)pointA.position
                    ? pointB.position
                    : pointA.position;

            Flip();
        }
    }

    private void CheckEdge()
    {
        // Raycast hacia abajo desde el frente
        if (edgeCheck == null) return;
        RaycastHit2D hit = Physics2D.Raycast(
            edgeCheck.position,
            Vector2.down,
            edgeCheckDistance,
            groundLayer
        );

        // Si no detecta suelo, invertir
        if (!hit.collider)
        {
            currentTarget =
                currentTarget == (Vector2)pointA.position
                    ? pointB.position
                    : pointA.position;

            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(
            facingRight ? 1f : -1f, 1f, 1f
        );
    }

    // Visualizar waypoints y edge check en el editor
    private void OnDrawGizmosSelected()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(pointA.position, 0.2f);
            Gizmos.DrawSphere(pointB.position, 0.2f);
            Gizmos.DrawLine(
                pointA.position, pointB.position
            );
        }
        if (edgeCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(
                edgeCheck.position,
                Vector2.down * edgeCheckDistance
            );
        }
    }
}

