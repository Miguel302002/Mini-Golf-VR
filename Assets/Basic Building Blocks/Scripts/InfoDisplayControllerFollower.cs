using UnityEngine;

public class InfoDisplayControllerFollower : MonoBehaviour
{
    Rigidbody rb;
    public Transform controller;

    // Adjust this offset to position the club relative to the controller
    public Vector3 positionOffset = new Vector3(0, -0.50f, 0); // Example offset
    public Vector3 rotationOffset = new Vector3(174, 280, 182);



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 adjustedPosition = controller.TransformPoint(positionOffset);
        transform.position = adjustedPosition;
        transform.rotation = controller.rotation * Quaternion.Euler(rotationOffset);
    }
}
