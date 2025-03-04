using UnityEngine;

public class BallController : MonoBehaviour
{
    public float pushForce = 1f; // Speed of the ball
    private Rigidbody rb;
    private bool inSand = false; // Check if the ball is in sand
    private bool inWater = false; // Check if the ball is in water
    private float sandPenalty = 0.7f; // Reduce power in sand
    private float waterResistance = 0.5f; // Reduce ball speed in water

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (inWater)
        {
            // Prevent player input while in water
            return;
        }

        // Push the ball using WASD or arrow keys
        if (Input.GetKeyDown(KeyCode.W)) 
        {
            ApplyHit(Vector3.forward, pushForce);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ApplyHit(Vector3.back, pushForce);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            ApplyHit(Vector3.left, pushForce);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ApplyHit(Vector3.right, pushForce);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sand"))
        {
            Debug.Log("Ball in Sand! Applying friction.");
            inSand = true;
        }

        if (collision.gameObject.CompareTag("Water"))
        {
            Debug.Log("Ball in Water! Applying resistance.");
            inWater = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sand"))
        {
            inSand = false;
        }

        if (collision.gameObject.CompareTag("Water"))
        {
            inWater = false;
        }
    }

    void FixedUpdate()
    {
        if (inSand)
        {
            ApplySandResistance();
        }
        
        if (inWater)
        {
            ApplyWaterResistance();
        }
    }

    void ApplySandResistance()
    {
        if (rb.linearVelocity.magnitude > 0.05f)
        {
            rb.linearVelocity *= 0.9f; // Slowdown in sand
        }
    }

    void ApplyWaterResistance()
    {
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            rb.linearVelocity *= waterResistance; // Slowdown in water
        }
    }

    void ApplyHit(Vector3 direction, float power)
    {
        if (inSand)
        {
            power *= sandPenalty; // Reduce hit strength in sand
        }

        if (inWater)
        {
            power *= 0.5f; // Additional slowdown in water
        }

        rb.AddForce(direction * power, ForceMode.Impulse);
    }
}










