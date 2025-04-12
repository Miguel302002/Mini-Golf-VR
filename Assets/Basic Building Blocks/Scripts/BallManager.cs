using UnityEngine;
using UnityEngine.SceneManagement;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance; // Singleton

    public int maxHealth = 100;
    public int currentHealth;

    public int maxLives = 3;
    public int currentLives;

    //public GameObject gameOverMenu;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        currentHealth = maxHealth;
        currentLives = maxLives;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            LoseLife();
        }
    }

    void LoseLife()
    {
        currentLives--;

        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            currentHealth = maxHealth;
            // Reset ball position maybe?
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        //gameOverMenu.SetActive(true);
        // Pause game or show retry options here
    }

    public void RetryHole()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        //SceneManager.LoadScene("MainMenu");
    }
}
   

