using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;

    private void OnTriggerEnter2D(
        Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Quitar una vida
            GameManager.Instance.TakeDamage();

            // Reposicionar al jugador
            collision.transform.position =
                respawnPoint.position;

            // Resetear velocidad
            Rigidbody2D rb =
                collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }
}

