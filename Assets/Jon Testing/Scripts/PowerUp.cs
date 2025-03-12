using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    LargeBall,
    SmallBall,
    FoggyVision,
    IncreasedDrag
}

public class PowerUpBox : MonoBehaviour
{
    [Header("Visual Settings")]
    public float rotationSpeed = 50f;
    public GameObject questionMarkModel;
    public ParticleSystem activationEffect;
    
    [Header("Power-up Settings")]
    public float respawnTime = 10f;
    public float powerUpDuration = 10f;
    
    private bool isAvailable = true;
    private MeshRenderer meshRenderer;
    private Collider boxCollider;
    
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<Collider>();
    }
    
    private void Update()
    {
        // Rotate the question mark box
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!isAvailable) return;
        
        if (other.CompareTag("Ball"))
        {
            // Activate a random power-up
            PowerUpManager powerUpManager = FindObjectOfType<PowerUpManager>();
            if (powerUpManager != null)
            {
                powerUpManager.ActivateRandomPowerUp(other.gameObject, powerUpDuration);
                
                // Play effect
                if (activationEffect != null)
                {
                    activationEffect.Play();
                }
                
                // Deactivate the box temporarily
                StartCoroutine(DeactivateAndRespawn());
            }
        }
    }
    
    private IEnumerator DeactivateAndRespawn()
    {
        isAvailable = false;
        
        // Hide the question mark
        if (questionMarkModel != null)
        {
            questionMarkModel.SetActive(false);
        }
        else
        {
            meshRenderer.enabled = false;
        }
        
        // Disable collider
        boxCollider.enabled = false;
        
        // Wait for respawn
        yield return new WaitForSeconds(respawnTime);
        
        // Reactivate the box
        if (questionMarkModel != null)
        {
            questionMarkModel.SetActive(true);
        }
        else
        {
            meshRenderer.enabled = true;
        }
        
        boxCollider.enabled = true;
        isAvailable = true;
    }
}

