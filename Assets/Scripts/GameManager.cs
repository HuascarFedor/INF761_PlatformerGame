using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game State")]
    [SerializeField] private int maxLives = 3;

    private int currentScore;
    private int currentLives;

    public int Score => currentScore;
    public int Lives => currentLives;

    private void Awake()
    {
        // Singleton: solo una instancia
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        currentLives = maxLives;
        currentScore = 0;
    }

    public void AddScore(int points)
    {
        currentScore += points;
        Debug.Log($"Score: {currentScore}");
        // En la Guía 8 se conectará con la UI
    }

    public void TakeDamage()
    {
        currentLives--;
        Debug.Log($"Lives: {currentLives}");

        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    public void ResetLives()
    {
        currentLives = maxLives;
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER");
        // En la Guía 8 se mostrará pantalla de Game Over
        // Por ahora, reiniciar posición y vidas
        currentLives = maxLives;
        currentScore = 0;
    }
}

