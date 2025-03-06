using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform ball;      // Reference to the ball
    public Vector3 offset;      // Distance between the camera and the ball
    public float smoothSpeed = 0.125f; // Smoothness factor for camera movement
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // If no ball is assigned, find the ball in the scene
        if (ball == null)
        {
            ball = GameObject.FindGameObjectWithTag("Ball").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the desired position of the camera
        Vector3 desiredPosition = ball.position + offset;
        
        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        // Apply the new position to the camera
        transform.position = smoothedPosition;
        
        // Optionally, make the camera always look at the ball
        transform.LookAt(ball);
    }
}
