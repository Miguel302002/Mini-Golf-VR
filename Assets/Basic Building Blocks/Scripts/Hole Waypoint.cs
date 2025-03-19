using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class HoleWaypoint : MonoBehaviour
{

    public RawImage img;
    public Transform target;
    public TextMeshProUGUI meter;
    public Vector3 offset;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        img.transform.position = target.position + offset;
        meter.text = ((int)Vector3.Distance(target.position, transform.position)).ToString() + "m";
    }
}
