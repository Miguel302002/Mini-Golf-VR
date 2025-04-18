using UnityEngine;

public class VRHudFollower : MonoBehaviour
{
    public Transform cameraTransform;  // Reference to your VR camera
    public Vector3 positionOffset = new Vector3(0, -0.2f, 0.5f);  // Adjust these values
    public bool smoothFollow = true;
    public float followSpeed = 5f;
    
    void Start()
    {
        // If camera reference is not set, use the main camera
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }
    
    void Update()
    {
        if (cameraTransform == null)
            return;
            
        Vector3 targetPosition = cameraTransform.position + 
                                 cameraTransform.forward * positionOffset.z +
                                 cameraTransform.up * positionOffset.y + 
                                 cameraTransform.right * positionOffset.x;
                                 
        Quaternion targetRotation = cameraTransform.rotation;
        
        if (smoothFollow)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * followSpeed);
        }
        else
        {
            transform.position = targetPosition;
            transform.rotation = targetRotation;
        }
    }
}