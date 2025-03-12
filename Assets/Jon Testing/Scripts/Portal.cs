using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("Portal Settings")]
    [Tooltip("Is this an entrance or exit portal?")]
    public bool isEntrance = true;
    
    [Tooltip("Reference to the paired portal")]
    public Portal linkedPortal;
    
    [Tooltip("Tag of objects that can be teleported")]
    public string teleportableTag = "GolfBall";
    
    [Header("Visual Settings")]
    public GameObject portalVisuals;
    public ParticleSystem portalEffect;
    
    private Renderer portalRenderer;
    private Color originalColor;
    private void OnDestroy()
{
    Debug.Log($"Portal {name} being destroyed! isEntrance: {isEntrance}");
}

    private void Start()
{
    // Store reference to renderer
    portalRenderer = portalVisuals?.GetComponent<Renderer>();
    Debug.Log($"Portal {name} Start. isEntrance: {isEntrance}");
    if (portalRenderer != null)
    {
        originalColor = portalRenderer.material.color;
    }
    
    // The PortalSystem will handle pairing now, so we don't need to call FindOrCreatePortalPair
    // FindOrCreatePortalPair(); <- Remove or comment this out
}
    
    private void FindOrCreatePortalPair()
    {
        if (PortalSystem.Instance == null)
        {
            Debug.LogError("No PortalSystem found in the scene!");
            return;
        }
        
        // Find if we're already part of a pair
        foreach (var pair in PortalSystem.Instance.portalPairs)
        {
            if (pair.entrancePortal == this || pair.exitPortal == this)
            {
                // Already registered
                UpdatePortalVisuals(pair.portalColor);
                return;
            }
        }
        
        // If this is an entrance and we have a linked exit, create a new pair
        if (isEntrance && linkedPortal != null && !linkedPortal.isEntrance)
        {
            PortalPair newPair = new PortalPair
            {
                entrancePortal = this,
                exitPortal = linkedPortal,
                portalColor = originalColor
            };
            
            PortalSystem.Instance.RegisterPortalPair(newPair);
            
            // Update visuals on both portals
            UpdatePortalVisuals(newPair.portalColor);
            linkedPortal.UpdatePortalVisuals(newPair.portalColor);
        }
    }
    
    public void UpdatePortalVisuals(Color color)
    {
        if (portalRenderer != null)
        {
            portalRenderer.material.color = color;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Only process if this is an entrance portal
        if (!isEntrance || linkedPortal == null) return;
        
        // Check if the object can be teleported
        if (other.CompareTag(teleportableTag))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            
            if (rb != null && PortalSystem.Instance.CanTeleport(rb))
            {
                TeleportObject(rb);
            }
        }
    }
    
    private void TeleportObject(Rigidbody rb)
    {
        // Find our portal pair
        PortalPair pair = null;
        
        foreach (var p in PortalSystem.Instance.portalPairs)
        {
            if (p.entrancePortal == this)
            {
                pair = p;
                break;
            }
        }
        
        if (pair == null || pair.exitPortal == null)
        {
            Debug.LogWarning("Portal pair not found or exit portal is missing!");
            return;
        }
        
        // Get the exit portal transform
        Transform exitTransform = pair.exitPortal.transform;
        
        // Store original velocity
        Vector3 originalVelocity = rb.linearVelocity;
        
        // Apply teleport
        if (pair.preserveAngle)
        {
            // Calculate the relative direction and position
            Vector3 relativePos = transform.InverseTransformPoint(rb.position);
            Vector3 relativeVel = transform.InverseTransformDirection(originalVelocity);
            
            // Apply to exit portal's transform
            rb.position = exitTransform.TransformPoint(relativePos);
            rb.linearVelocity = exitTransform.TransformDirection(relativeVel) * pair.velocityMultiplier;
        }
        else
        {
            // Simple teleport with forward direction
            rb.position = exitTransform.position + (exitTransform.forward * 0.5f);
            rb.linearVelocity = exitTransform.forward * originalVelocity.magnitude * pair.velocityMultiplier;
        }
        
        // Play effects
        if (portalEffect != null)
        {
            portalEffect.Play();
        }
        
        if (pair.exitPortal.portalEffect != null)
        {
            pair.exitPortal.portalEffect.Play();
        }
        
        // Set cooldown to prevent immediate re-teleport
        PortalSystem.Instance.SetTeleportCooldown(rb);
    }
}