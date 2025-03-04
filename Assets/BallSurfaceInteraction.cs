using UnityEngine;

public class BallSurfaceInteraction : MonoBehaviour
{
    private Rigidbody rb;

    public float sandDrag = 1f;
    public float iceDrag = 0.1f;
    public float waterDrag = 0.05f;
    public float lavaDrag = 1f;
    public float normalDrag = 0.2f;

    public float sandBounciness = 0.1f;
    public float lavaBounciness = 0.8f;
    public float normalBounciness = 0.3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ice"))
        {
            rb.linearDamping = iceDrag;
            rb.GetComponent<Collider>().material.bounciness = normalBounciness;
        }
        else if (collision.gameObject.CompareTag("Water"))
        {
            rb.linearDamping = waterDrag;
            rb.GetComponent<Collider>().material.bounciness = normalBounciness;
        }
        else if (collision.gameObject.CompareTag("Lava"))
        {
            rb.linearDamping = lavaDrag;
            rb.GetComponent<Collider>().material.bounciness = lavaBounciness;
        }
        else if (collision.gameObject.CompareTag("Sand"))
        {
            rb.linearDamping = sandDrag;
            rb.GetComponent<Collider>().material.bounciness = sandBounciness;
        }
        else
        {
            // Default behavior (e.g., on normal grass)
            rb.linearDamping = normalDrag;
            rb.GetComponent<Collider>().material.bounciness = normalBounciness;
        }
    }
}




