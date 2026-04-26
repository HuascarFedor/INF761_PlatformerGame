using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private void OnTriggerEnter2D(
        Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth health =
                collision.GetComponent<PlayerHealth>();

            if (health != null)
            {
                health.TakeDamage(
                    transform.position
                );
            }
        }
    }
}

