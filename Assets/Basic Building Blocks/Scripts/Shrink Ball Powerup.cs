using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;



public class ShrinkBallPowerup : MonoBehaviour, Powerup_Interface
{
    [Header("Power-Up Settings")]
    public float shrinkFactor = 0.5f;      // How much the ball shrinks.
    public float powerUpDuration = 10f;    // How long the power-up lasts.
    public float respawnTime = 10f;        // How long before the item reappears.

    public float rotationSpeed = 50f;

    private bool isAvailable_ = true;
    private MeshRenderer meshRenderer;
    private Collider boxCollider;



    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isAvailable_) return;

        if (other.CompareTag("Ball"))
        {
            // Get the power-up handler from the ball.
            PowerUpHandler handler = other.GetComponent<PowerUpHandler>();
            if (handler != null)
            {
                // Store this power-up (as an IPowerUp) on the ball.
                handler.SetPowerUp(this);
            }
           

            // Deactivate the box temporarily
            StartCoroutine(DeactivateAndRespawn());
        }
    }

    private IEnumerator DeactivateAndRespawn()
    {
        isAvailable_ = false;
        meshRenderer.enabled = false;
    
        // Disable collider
        boxCollider.enabled = false;

        // Wait for respawn
        yield return new WaitForSeconds(respawnTime);

        meshRenderer.enabled = true;
       
        boxCollider.enabled = true;
        isAvailable_ = true;
    }

   

    // Implementation of IPowerUp: apply the shrink effect to the ball.
    public IEnumerator ApplyPowerUp(GameObject ball)
    {
        // Save the ball's original scale.
        Vector3 originalScale = ball.transform.localScale;
        // Apply the shrink effect.
        ball.transform.localScale = originalScale * shrinkFactor;

        // Wait for the power-up duration.
        yield return new WaitForSeconds(powerUpDuration);

        // Revert the ball back to its original size.
        ball.transform.localScale = originalScale;
    }
}
