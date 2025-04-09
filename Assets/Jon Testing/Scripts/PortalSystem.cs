using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSystem : MonoBehaviour
{
    [Header("Portal Management")]
    public static PortalSystem Instance;
    
    [Tooltip("List of all linked portal pairs in the scene")]
    public List<PortalPair> portalPairs = new List<PortalPair>();
    
    // Track if a ball is currently being teleported to prevent loops
    private Dictionary<Rigidbody, float> teleportCooldowns = new Dictionary<Rigidbody, float>();
    private float cooldownDuration = 0.5f;
    
    // In PortalSystem.cs, add this method:
public void InitializeAllPortals()
{
    // Find all portals in the scene
    Portal[] allPortals = FindObjectsOfType<Portal>();
    //Debug.Log($"Found {allPortals.Length} portals:");
    
    // Create portal pairs for all entrance portals with linked exits
    foreach (Portal portal in allPortals)
    {
        if (portal.isEntrance && portal.linkedPortal != null && !portal.linkedPortal.isEntrance)
        {
            // Check if they're already paired
            bool alreadyPaired = false;
            foreach (var pair in portalPairs)
            {
                if ((pair.entrancePortal == portal && pair.exitPortal == portal.linkedPortal) ||
                    (pair.exitPortal == portal && pair.entrancePortal == portal.linkedPortal))
                {
                    alreadyPaired = true;
                    break;
                }
            }
            
            // Create new pair if not already paired
            if (!alreadyPaired)
            {
                PortalPair newPair = new PortalPair
                {
                    entrancePortal = portal,
                    exitPortal = portal.linkedPortal,
                    portalColor = Color.blue // Or any default color
                };
                
                RegisterPortalPair(newPair);
                
                // Update visuals on both portals
                portal.UpdatePortalVisuals(newPair.portalColor);
                portal.linkedPortal.UpdatePortalVisuals(newPair.portalColor);
            }
        }
    }
}

// Then in the Awake method, after setting Instance:
private void Awake()
{
    // Singleton pattern
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional - keeps it between scenes
    }
    else if (Instance != this)
    {
        //Debug.LogWarning($"Destroying duplicate PortalSystem on {gameObject.name}");
        Destroy(gameObject);
        return; // Important - return to avoid running the rest of Awake
    }
    
    // Initialize all portals
    InitializeAllPortals();
}
    
    private void Update()
{
    // Update cooldowns
    List<Rigidbody> keysToRemove = new List<Rigidbody>();

    // Iterate over a copy of keys
    foreach (var key in new List<Rigidbody>(teleportCooldowns.Keys))
    {
        teleportCooldowns[key] -= Time.deltaTime;

        if (teleportCooldowns[key] <= 0)
        {
            keysToRemove.Add(key);
        }
    }

    // Remove expired cooldowns
    foreach (var key in keysToRemove)
    {
        teleportCooldowns.Remove(key);
    }
}

    
    public void RegisterPortalPair(PortalPair pair)
    {
        if (!portalPairs.Contains(pair))
        {
            portalPairs.Add(pair);
        }
    }
    
    public bool CanTeleport(Rigidbody rb)
    {
        if (!teleportCooldowns.ContainsKey(rb))
        {
            return true;
        }
        
        return teleportCooldowns[rb] <= 0;
    }
    
    public void SetTeleportCooldown(Rigidbody rb)
    {
        teleportCooldowns[rb] = cooldownDuration;
    }
}

[System.Serializable]
public class PortalPair
{
    public Portal entrancePortal;
    public Portal exitPortal;
    public Color portalColor = Color.blue;
    
    [Tooltip("Velocity multiplier when exiting")]
    public float velocityMultiplier = 1.0f;
    
    [Tooltip("Should the exit angle match the entry angle?")]
    public bool preserveAngle = true;
}