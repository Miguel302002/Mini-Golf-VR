using UnityEngine;

public class ZeroGravityZone : MonoBehaviour
{
    [Header("Zone Settings")]
    [Tooltip("Tag of the golf ball")]
    public string ballTag = "Ball";
    
    [Tooltip("How strong the upward force is in the zero-G channel")]
    public float upwardForce = 2.0f;
    
    [Tooltip("How strong the forward push at the top of the channel")]
    public float exitForce = 10.0f;
    
    [Tooltip("Direction of the exit force")]
    public Vector3 exitDirection = Vector3.forward;
    
    [Header("Exit Trigger")]
    [Tooltip("Reference to the trigger collider at the top of the channel")]
    public Collider exitTrigger;

    [Header("Direction of Force")]
    [Tooltip("up = (0,1,0), left = (-1,0,0), forward = (0,0,1)")]
    public Vector3 directionOfForce;



    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is the golf ball
        if (other.CompareTag(ballTag))
        {
            // Get the rigidbody of the ball
            Rigidbody ballRb = other.GetComponent<Rigidbody>();
            
            if (ballRb != null)
            {
                // Store the original gravity settings
                ballRb.useGravity = false;
                
                // Add a script to the ball to handle zero-G movement
                ZeroGBallHandler handler = other.gameObject.AddComponent<ZeroGBallHandler>();
                handler.Initialize(this, ballRb);
            }
        }
    }
}

public class ZeroGBallHandler : MonoBehaviour
{
    private ZeroGravityZone zeroGZone;
    private Rigidbody ballRb;
    private bool hasExited = false;
    private float originalDrag;
    private float originalAngularDrag;

    public void Initialize(ZeroGravityZone zone, Rigidbody rb)
    {
        zeroGZone = zone;
        ballRb = rb;
        
        // Store original physics values
        originalDrag = ballRb.linearDamping;
        originalAngularDrag = ballRb.angularDamping;
        
        // Adjust physics for zero-G movement
        ballRb.linearDamping = 0.5f;
        ballRb.angularDamping = 0.5f;
    }
    
    private void FixedUpdate()
    {
        if (!hasExited)
        {
            // Apply constant upward force while in the zone
            ballRb.AddForce(zeroGZone.directionOfForce * zeroGZone.upwardForce, ForceMode.Force);

            // Check if the ball has reached the exit trigger
             if (zeroGZone.exitTrigger != null && 
                 zeroGZone.exitTrigger.bounds.Intersects(ballRb.GetComponent<Collider>().bounds))
             {
                 ExitZeroGZone();
             }
        }
    }
    
    private void ExitZeroGZone()
    {
        hasExited = true;
        
        // Apply strong exit force in the specified direction
        ballRb.linearVelocity = Vector3.zero; // Clear any existing velocity
        ballRb.AddForce(zeroGZone.exitDirection.normalized * zeroGZone.exitForce, ForceMode.Impulse);
        
        // Restore original physics
        ballRb.useGravity = true;
        ballRb.linearDamping = originalDrag;
        ballRb.angularDamping = originalAngularDrag;
        
        // Schedule this component to be destroyed after the force is applied
        Destroy(this, 0.5f);
    }
    
    private void OnDestroy()
    {
        // Make sure gravity is restored if the component is destroyed for any reason
        if (ballRb != null)
        {
            ballRb.useGravity = true;
            ballRb.linearDamping = originalDrag;
            ballRb.angularDamping = originalAngularDrag;
        }
    }
    
    // If the ball exits the main trigger without hitting the exit trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ZeroGravityZone>() != null && !hasExited)
        {
            // Restore physics if leaving the zone without proper exit
            ballRb.useGravity = true;
            ballRb.linearDamping = originalDrag;
            ballRb.angularDamping = originalAngularDrag;
            Destroy(this);
        }
    }
}