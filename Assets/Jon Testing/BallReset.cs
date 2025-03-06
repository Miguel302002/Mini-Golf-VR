using UnityEngine;

/// <summary>
/// Handles the resetting of the ball when it goes out of bounds or falls into a hole.
/// Worked on by Jonathan R
/// </summary>
public class BallReset : MonoBehaviour
{
    public Transform ball;
    public Vector3 startPosition;
    public float boundaryXMin = -5f, boundaryXMax = 5f, boundaryZMin = -10f, boundaryZMax = 10f, resetHeight = -5f;

    private void Start()
    {
        startPosition = ball.position;
    }

    private void Update()
    {
        // Check if the ball is outside the boundaries and reset if necessary
        if (ball.position.x < boundaryXMin || ball.position.x > boundaryXMax ||
            ball.position.z < boundaryZMin || ball.position.z > boundaryZMax ||
            ball.position.y < resetHeight)
        {
            ResetBall();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hole"))
        {
            Debug.Log("WINNER!!");
            ResetBall();
        }
    }

    private void ResetBall()
    {
        ball.position = startPosition;
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}


