using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitCounterUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI hitCountText;
    [SerializeField] private TextMeshProUGUI bestScoreText; // Add reference for best score display
    
    [Header("Animation Settings")]
    [SerializeField] private float hitCounterPopDuration = 0.3f;
    [SerializeField] private float hitCounterPopScale = 1.2f;
    
    [Header("Level Settings")]
    [SerializeField] private string levelId = "level1"; // Unique identifier for each level
    
    // Track hit count
    private int hitCount = 0;
    private int bestHitCount = 0;
    
    private Vector3 originalHitCountScale;
    
    private void Start()
    {
        if (hitCountText != null)
        {
            originalHitCountScale = hitCountText.transform.localScale;
            LoadBestScore();
            UpdateHitCountDisplay();
            UpdateBestScoreDisplay();
        }
    }
    
    // Load the best score from PlayerPrefs
    private void LoadBestScore()
    {
        // If no best score exists yet, it will return 0
        // You could also set a default high value (like 999) if you want to ensure
        // any real score will be better than the default
        bestHitCount = PlayerPrefs.GetInt($"BestScore_{levelId}", 999);
    }
    
    // Save the best score to PlayerPrefs
    private void SaveBestScore()
    {
        PlayerPrefs.SetInt($"BestScore_{levelId}", bestHitCount);
        PlayerPrefs.Save(); // Ensure the data is written immediately
    }
    
    // Call this whenever the ball is hit
    public void IncrementHitCount()
    {
        hitCount++;
        UpdateHitCountDisplay();
        AnimateHitCounter();
    }
    
    // Call this when the hole is completed
    public void CompleteHole()
    {
        // Only update if this score is better (lower) than the previous best
        // Or if there was no previous best score (bestHitCount == 0)
        if (bestHitCount == 0 || hitCount < bestHitCount)
        {
            bestHitCount = hitCount;
            SaveBestScore();
            UpdateBestScoreDisplay();
            // Optionally animate or highlight the new best score
        }
    }
    
    // Reset the hit counter for a new game
    public void ResetHitCount()
    {
        hitCount = 0;
        UpdateHitCountDisplay();
    }
    
    private void UpdateHitCountDisplay()
    {
        if (hitCountText != null)
        {
            hitCountText.text = $"Hits: {hitCount}";
        }
    }
    
    private void UpdateBestScoreDisplay()
    {
        if (bestScoreText != null)
        {
            if (bestHitCount == 999) // No score recorded yet
            {
                bestScoreText.text = "Best: --";
            }
            else
            {
                bestScoreText.text = $"Best: {bestHitCount}";
            }
        }
    }
    
    private void AnimateHitCounter()
    {
        if (hitCountText != null)
        {
            StopAllCoroutines();
            StartCoroutine(PunchScaleAnimation(hitCountText.transform));
        }
    }
    
    private IEnumerator PunchScaleAnimation(Transform target)
    {
        // Your existing animation code remains unchanged
        Vector3 startScale = originalHitCountScale;
        Vector3 maxScale = originalHitCountScale * hitCounterPopScale;
        
        // Scale up
        float elapsed = 0f;
        while (elapsed < hitCounterPopDuration / 2)
        {
            target.localScale = Vector3.Lerp(startScale, maxScale, elapsed / (hitCounterPopDuration / 2));
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        // Scale down
        elapsed = 0f;
        while (elapsed < hitCounterPopDuration / 2)
        {
            target.localScale = Vector3.Lerp(maxScale, startScale, elapsed / (hitCounterPopDuration / 2));
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        target.localScale = startScale;
    }
    
    // Test functions for the editor
    [ContextMenu("Test Hit")]
    public void TestHit()
    {
        IncrementHitCount();
    }
    
    [ContextMenu("Test Complete Hole")]
    public void TestCompleteHole()
    {
        CompleteHole();
    }
    
    [ContextMenu("Reset Counter")]
    public void TestReset()
    {
        ResetHitCount();
    }
    
    [ContextMenu("Clear Saved Data")]
    public void ClearSavedData()
    {
        PlayerPrefs.DeleteKey($"BestScore_{levelId}");
        bestHitCount = 999;
        UpdateBestScoreDisplay();
    }
}
// Insert the following into the ball scrips, call the OnBallHit() function after a hit is registered
/* private HitCounterUI hitCounter;

private void Start()
{
    // Find the hit counter in the scene
    hitCounter = FindObjectOfType<HitCounterUI>();
}

// Call this whenever the ball is hit
private void OnBallHit()
{
    if (hitCounter != null)
    {
        hitCounter.IncrementHitCount();
    }
} */