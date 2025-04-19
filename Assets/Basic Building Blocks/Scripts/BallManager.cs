using UnityEngine;
using UnityEngine.SceneManagement;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance; // Singleton

    public int maxHealth = 100;
    public int currentHealth;

    public int maxLives = 3;
    public int currentLives;

    public GameObject gameOverMenu;

    public GameObject ball;

    public golfball ball_information;

    public Transform head;
    public float spawnDistance = 2f;

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
            
        }
    }

    public void TakeDamageBarrier(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            LoseLifeBarrier();
        }
    }


    void LoseLifeBarrier()
    {
        currentLives--;

        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            currentHealth = maxHealth;
            ball.transform.position = ball_information.ballBeforeHitPosition;
            ball.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            
        }
    }


    public void TakeDamageExplosion(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            LoseLifeExplosion();
        }
    }


    void LoseLifeExplosion()
    {
        currentLives--;

        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            currentHealth = maxHealth;
            ball.transform.position = ball_information.ballBeforeHitPosition;
            ball.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        }
    }


    public void TakeDamageWater(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            LoseLifeWater();
        }
    }


    void LoseLifeWater()
    {
        currentLives--;

        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            currentHealth = maxHealth;
            ball.transform.position = ball_information.ballBeforeHitPosition;
            ball.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        }
    }



    void GameOver()
    {
        //Debug.Log("Game Over!");
        ball.SetActive(false);

        Vector3 forward = head.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 spawnPosition = head.position + forward * spawnDistance;
        spawnPosition.y = head.position.y;

        gameOverMenu.transform.position = spawnPosition;

        Vector3 lookDirection = head.position - gameOverMenu.transform.position;
        lookDirection.y = 0;
        gameOverMenu.transform.rotation = Quaternion.LookRotation(-lookDirection);
        


        //gameOverMenu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        gameOverMenu.SetActive(true);
        
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
   

