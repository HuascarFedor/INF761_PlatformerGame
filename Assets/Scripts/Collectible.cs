using UnityEngine;
 
public class Collectible : MonoBehaviour
{
    [SerializeField] private ItemData data;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Sumar puntos
            GameManager.Instance.AddScore(data.pointValue);
 
            // Reproducir sonido
            if (data.collectSound != null)
            {
                AudioSource.PlayClipAtPoint(
                    data.collectSound,
                    transform.position
                );
            }
 
            // Destruir el coleccionable
            Destroy(gameObject);
        }
    }
}

