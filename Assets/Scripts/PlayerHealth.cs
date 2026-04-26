using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Invulnerability")]
    [SerializeField] private float iframeDuration = 1.5f;
    [SerializeField] private float flashInterval = 0.1f;

    [Header("Knockback")]
    [SerializeField] private float knockbackForce = 8f;
    [SerializeField] private float knockbackDuration = 0.2f;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private PlayerController playerController;
    private bool isInvulnerable;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerController =
            GetComponent<PlayerController>();
    }

    public void TakeDamage(Vector2 damageSource)
    {
        if (isInvulnerable) return;

        // Registrar daño en el GameManager
        GameManager.Instance.TakeDamage();

        // Iniciar iframes con parpadeo
        StartCoroutine(InvulnerabilityCoroutine());

        // Aplicar knockback
        StartCoroutine(
            KnockbackCoroutine(damageSource)
        );
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        // Parpadeo visual
        float elapsed = 0f;
        while (elapsed < iframeDuration)
        {
            spriteRenderer.enabled =
                !spriteRenderer.enabled;
            yield return new WaitForSeconds(
                flashInterval
            );
            elapsed += flashInterval;
        }

        // Asegurar que el sprite quede visible
        spriteRenderer.enabled = true;
        isInvulnerable = false;
    }

    private IEnumerator KnockbackCoroutine(
        Vector2 damageSource)
    {
        // Desactivar control del jugador brevemente
        playerController.enabled = false;

        // Dirección opuesta al daño
        Vector2 knockDir = (
            (Vector2)transform.position - damageSource
        ).normalized;

        rb.linearVelocity = new Vector2(
            knockDir.x * knockbackForce,
            knockbackForce * 0.5f  // Empuje hacia arriba
        );

        yield return new WaitForSeconds(
            knockbackDuration
        );

        // Reactivar control
        playerController.enabled = true;
    }
}

