using UnityEngine;
using UnityEngine.XR;

public class golfball : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnTriggerEnter(Collider other)
    {
 
        if (other.tag == "Golf Club Head")
        {

            //Transfer velocity
            GetComponent<Rigidbody>().linearVelocity = other.GetComponent<GolfClubHead>().getVelocity() * 1.25F;


        }
    }

}
