using UnityEngine;
using System.Collections;

public class GolfBallPhysics : MonoBehaviour
{
    private Rigidbody rb;

    // Sinking variables
    private bool inWater = false;
    private bool canInteract = true; // Flag to prevent player input when sinking
    private float sinkAmount = 1.0f; // How fast the ball sinks
    private float sinkDuration = 2.0f; // How long it takes to sink
    private float resetDelay = 1.0f; // Delay before resetting the ball after sinking

    // Boundary for randomizing reset position (set X, Y, and Z limits)
    public Vector3 minPosition = new Vector3(-5f, 0.5f, -5f);  // Minimum boundary
    public Vector3 maxPosition = new Vector3(5f, 0.5f, 5f);   // Maximum boundary

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 0.5f;            // Set the mass for the ball
        rb.linearDamping = 5f;     // Add linear damping to slow the ball
        rb.angularDamping = 5f;    // Add angular damping to slow the spin
        rb.useGravity = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water") && canInteract)
        {
            inWater = true;
            canInteract = false; // Disable interaction while sinking
            StartCoroutine(SinkInWater());
        }

        if (collision.gameObject.CompareTag("Sand"))
        {
            // Apply sand resistance, handled in BallController
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            inWater = false;
        }
    }

    private IEnumerator SinkInWater()
    {
        float elapsedTime = 0f;

        // Sink the ball slowly over time
        while (elapsedTime < sinkDuration)
        {
            transform.position += Vector3.down * sinkAmount * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Wait a moment before resetting the ball
        yield return new WaitForSeconds(resetDelay);
        ResetBallPosition();
    }

    private void ResetBallPosition()
    {
        // Randomly generate the position inside the boundary range
        float randomX = Random.Range(minPosition.x, maxPosition.x);
        float randomZ = Random.Range(minPosition.z, maxPosition.z);
        Vector3 randomPosition = new Vector3(randomX, minPosition.y, randomZ);

        // Reset the ball's position and stop any movement
        transform.position = randomPosition; 
        rb.linearVelocity = Vector3.zero;          // Stop the ball's velocity
        rb.angularVelocity = Vector3.zero;  // Stop the ball from spinning
        canInteract = true; // Enable interaction again
    }

    void FixedUpdate()
    {
        if (inWater)
        {
            rb.linearVelocity *= 0.5f; // Slow the ball down in water
        }
    }
}












