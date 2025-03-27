using UnityEngine;
using System.Collections;

public class BoostUpPowerup : MonoBehaviour, Powerup_Interface
{
    [Header("Power-Up Settings")]
    public float upwardForce = 2.0f;      // How much the ball goes up.
    public float powerUpDuration = 10f;    // How long the power-up lasts.
    public float respawnTime = 10f;        // How long before the item reappears.

    [Header("Direction of Force")]
    [Tooltip("up = (0,1,0), left = (-1,0,0), forward = (0,0,1)")]
    public Vector3 directionOfForce;

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



    // Implementation of IPowerUp: apply the lift effect to the ball.
    public IEnumerator ApplyPowerUp(GameObject ball)
    {
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
        
        ballRb.AddForce(directionOfForce * upwardForce, ForceMode.Impulse);

        yield return new WaitForSeconds(powerUpDuration);
        
    }
}
