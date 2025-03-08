using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class golfclub : MonoBehaviour
{

    Rigidbody rb;                           
    public Transform controller;

    
    public Vector3 positionOffset = new Vector3(0, -0.50f, 0);          // Adjust this offset to position the club relative to the controller
    public Vector3 rotationOffset = new Vector3(174, 280, 182);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
       
    }

    void FixedUpdate()
    {
      Vector3 adjustedPosition = controller.TransformPoint(positionOffset);
      rb.MovePosition(adjustedPosition);
      rb.MoveRotation(controller.rotation * Quaternion.Euler(rotationOffset));
        
    }
}
