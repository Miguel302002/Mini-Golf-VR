using UnityEngine;

/// <summary>
/// Manages the interaction between the ball and various surface types (e.g., ice, water, sand, etc.) to apply unique physical properties like drag and bounciness.
/// Worked on by: Jonathan R
/// </summary>
public class BallSurfaceInteraction : MonoBehaviour
{
    private Rigidbody rb;

    // Surface properties
    public float sandDrag = 1f, iceDrag = 0.1f, waterDrag = 0.05f, lavaDrag = 1f, normalDrag = 0.2f;
    public float sandBounciness = 0.1f, lavaBounciness = 0.8f, normalBounciness = 0.3f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionStay(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ice":
                ApplySurfaceProperties(iceDrag, normalBounciness);
                break;
            case "Water":
                ApplySurfaceProperties(waterDrag, normalBounciness);
                break;
            case "Lava":
                ApplySurfaceProperties(lavaDrag, lavaBounciness);
                break;
            case "Sand":
                ApplySurfaceProperties(sandDrag, sandBounciness);
                break;
            default:
                ApplySurfaceProperties(normalDrag, normalBounciness);
                break;
        }
    }

    private void ApplySurfaceProperties(float drag, float bounciness)
    {
        rb.linearDamping = drag;
        rb.GetComponent<Collider>().material.bounciness = bounciness;
    }
}





