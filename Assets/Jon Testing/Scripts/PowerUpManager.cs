using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Main PowerUp Manager
public class PowerUpManager : MonoBehaviour
{
    [System.Serializable]
    public class PowerUpSettings
    {
        public PowerUpType type;
        public float probability = 1f;
        public Color effectColor = Color.white;
        public GameObject effectPrefab;
    }
    
    [Header("Power-up Configuration")]
    public PowerUpSettings[] availablePowerUps;
    
    [Header("Power-up Parameters")]
    public float enlargeScale = 1.5f;
    public float shrinkScale = 0.5f;
    public float increasedDragAmount = 5f;
    public Color fogColor = new Color(0.5f, 0.5f, 0.5f, 0.8f);
    public float fogDensity = 0.05f;
    
    [Header("UI References")]
    public UnityEngine.UI.Image powerUpIcon;
    public UnityEngine.UI.Text timerText;
    public GameObject powerUpPanel;
    
    private GameObject currentBall;
    private PowerUpType currentPowerUp = PowerUpType.LargeBall;
    private float remainingDuration = 0f;
    private Vector3 originalBallScale;
    private float originalDrag;
    private bool isPowerUpActive = false;
    
    private Color originalFogColor;
    private float originalFogDensity;
    private bool originalFogEnabled;
    
    private void Start()
    {
        // Store initial fog settings
        originalFogColor = RenderSettings.fogColor;
        originalFogDensity = RenderSettings.fogDensity;
        originalFogEnabled = RenderSettings.fog;
        
        // Hide power-up UI initially
        if (powerUpPanel != null)
        {
            powerUpPanel.SetActive(false);
        }
    }
    
    private void Update()
    {
        if (isPowerUpActive)
        {
            // Update timer
            remainingDuration -= Time.deltaTime;
            
            // Update UI
            if (timerText != null)
            {
                timerText.text = remainingDuration.ToString("F1") + "s";
            }
            
            // Check if power-up has expired
            if (remainingDuration <= 0)
            {
                DeactivateCurrentPowerUp();
            }
        }
    }
    
    public void ActivateRandomPowerUp(GameObject ball, float duration)
    {
        // If there's an active power-up, deactivate it first
        if (isPowerUpActive)
        {
            DeactivateCurrentPowerUp();
        }
        
        // Set the ball reference
        currentBall = ball;
        
        // Calculate total probability
        float totalProbability = 0;
        foreach (PowerUpSettings powerUp in availablePowerUps)
        {
            totalProbability += powerUp.probability;
        }
        
        // Select a random power-up based on probability
        float randomValue = Random.Range(0, totalProbability);
        float probabilitySum = 0;
        
        PowerUpSettings selectedPowerUp = availablePowerUps[0];
        
        foreach (PowerUpSettings powerUp in availablePowerUps)
        {
            probabilitySum += powerUp.probability;
            if (randomValue <= probabilitySum)
            {
                selectedPowerUp = powerUp;
                break;
            }
        }
        
        // Activate the selected power-up
        currentPowerUp = selectedPowerUp.type;
        remainingDuration = duration;
        
        // Store original properties
        Rigidbody ballRb = currentBall.GetComponent<Rigidbody>();
        if (ballRb != null)
        {
            originalDrag = ballRb.linearDamping;
        }
        
        originalBallScale = currentBall.transform.localScale;
        
        // Apply power-up effect
        ApplyPowerUpEffect(selectedPowerUp);
        
        // Show power-up UI
        if (powerUpPanel != null)
        {
            powerUpPanel.SetActive(true);
        }
        
        // Update power-up icon
        if (powerUpIcon != null && selectedPowerUp.effectPrefab != null)
        {
            powerUpIcon.color = selectedPowerUp.effectColor;
        }
        
        isPowerUpActive = true;
    }
    
    private void ApplyPowerUpEffect(PowerUpSettings powerUp)
    {
        switch (powerUp.type)
        {
            case PowerUpType.LargeBall:
                // Make the ball larger
                currentBall.transform.localScale = originalBallScale * enlargeScale;
                break;
                
            case PowerUpType.SmallBall:
                // Make the ball smaller
                currentBall.transform.localScale = originalBallScale * shrinkScale;
                break;
                
            case PowerUpType.FoggyVision:
                // Add fog to the scene
                RenderSettings.fog = true;
                RenderSettings.fogColor = fogColor;
                RenderSettings.fogDensity = fogDensity;
                break;
                
            case PowerUpType.IncreasedDrag:
                // Increase the ball's drag
                Rigidbody ballRb = currentBall.GetComponent<Rigidbody>();
                if (ballRb != null)
                {
                    ballRb.linearDamping = originalDrag + increasedDragAmount;
                }
                break;
        }
        
        // Instantiate visual effect if available
        if (powerUp.effectPrefab != null)
        {
            GameObject effect = Instantiate(powerUp.effectPrefab, currentBall.transform.position, Quaternion.identity);
            effect.transform.SetParent(currentBall.transform);
            Destroy(effect, remainingDuration);
        }
    }
    
    private void DeactivateCurrentPowerUp()
    {
        if (!isPowerUpActive) return;
        
        // Reset based on power-up type
        switch (currentPowerUp)
        {
            case PowerUpType.LargeBall:
            case PowerUpType.SmallBall:
                // Reset ball size
                if (currentBall != null)
                {
                    currentBall.transform.localScale = originalBallScale;
                }
                break;
                
            case PowerUpType.FoggyVision:
                // Reset fog
                RenderSettings.fog = originalFogEnabled;
                RenderSettings.fogColor = originalFogColor;
                RenderSettings.fogDensity = originalFogDensity;
                break;
                
            case PowerUpType.IncreasedDrag:
                // Reset drag
                Rigidbody ballRb = currentBall?.GetComponent<Rigidbody>();
                if (ballRb != null)
                {
                    ballRb.linearDamping = originalDrag;
                }
                break;
        }
        
        // Hide power-up UI
        if (powerUpPanel != null)
        {
            powerUpPanel.SetActive(false);
        }
        
        isPowerUpActive = false;
    }
}