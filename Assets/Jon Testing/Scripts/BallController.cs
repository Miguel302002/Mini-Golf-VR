using UnityEngine;
using UnityEngine.InputSystem; // Add this for the new Input System

/// <summary>
/// Controls the ball's behavior, applying forces based on player input and environmental factors (sand, water).
/// Worked on by Jonathan R
/// </summary>
public class BallController : MonoBehaviour
{
    public float pushForce = 1f; // Speed of the ball when hit
    private Rigidbody rb;
    private SurfaceType currentSurface = SurfaceType.Normal; // Keeps track of the surface the ball is on
    private bool canInteract = true; // Flag to manage input during sinking

    // Reference to the Keyboard class for input
    private Keyboard keyboard;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Get the keyboard device
        keyboard = Keyboard.current;
        if (keyboard == null)
        {
            Debug.LogError("No keyboard detected!");
        }
    }

    private void Update()
    {
        if (!canInteract || keyboard == null) return; // Prevent input if canInteract is false or no keyboard

        // Prevent input when in water
        if (currentSurface == SurfaceType.Water) return;

        // Handle player input to push the ball in the direction of the key press
        if (keyboard.wKey.wasPressedThisFrame) ApplyHit(Vector3.forward);
        if (keyboard.sKey.wasPressedThisFrame) ApplyHit(Vector3.back);
        if (keyboard.aKey.wasPressedThisFrame) ApplyHit(Vector3.left);
        if (keyboard.dKey.wasPressedThisFrame) ApplyHit(Vector3.right);
    }

    private void OnCollisionEnter(Collision collision)
    {
        SurfaceType surface = GetSurfaceType(collision.gameObject.tag);
        if (surface != SurfaceType.Normal) currentSurface = surface;
    }

    private void OnCollisionExit(Collision collision)
    {
        // Reset surface type when the ball leaves a surface
        currentSurface = SurfaceType.Normal;
    }

    private void FixedUpdate()
    {
        // Apply surface resistance if ball is on a non-normal surface
        ApplySurfaceResistance();
    }

    private void ApplySurfaceResistance()
    {
        switch (currentSurface)
        {
            case SurfaceType.Sand:
                ApplySandResistance();
                break;
            case SurfaceType.Water:
                ApplyWaterResistance();
                break;
        }
    }

    private void ApplySandResistance()
    {
        if (rb.linearVelocity.magnitude > 0.05f)
        {
            rb.linearVelocity *= 0.9f; // Slowdown in sand
        }
    }

    private void ApplyWaterResistance()
    {
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            rb.linearVelocity *= 0.5f; // Slowdown in water
        }
    }

    private void ApplyHit(Vector3 direction)
    {
        // Reduce the push force if the ball is in sand or water
        float finalPushForce = pushForce;
        if (currentSurface == SurfaceType.Sand)
        {
            finalPushForce *= 0.7f; // Sand penalty
        }
        else if (currentSurface == SurfaceType.Water)
        {
            finalPushForce *= 0.5f; // Additional water resistance
        }

        rb.AddForce(direction * finalPushForce, ForceMode.Impulse);
    }

    private SurfaceType GetSurfaceType(string tag)
    {
        switch (tag)
        {
            case "Sand": return SurfaceType.Sand;
            case "Water": return SurfaceType.Water;
            default: return SurfaceType.Normal;
        }
    }

    // Methods to stop and allow input
    public void StopInput()
    {
        canInteract = false;
    }

    public void AllowInput()
    {
        canInteract = true;
    }
}

/// <summary>
/// Enum to define different surface types the ball can interact with.
/// </summary>
public enum SurfaceType
{
    Normal,
    Sand,
    Water
}











