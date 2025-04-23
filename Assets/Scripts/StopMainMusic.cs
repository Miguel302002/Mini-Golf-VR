using UnityEngine;

public class StopMainMusic : MonoBehaviour
{
    public bool startedAlready = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (!startedAlready)
            {
                AudioManager.instance.Stop("Dark Music");
                AudioManager.instance.Play("Main Music");
                AudioManager.instance.Play("Wind");
                startedAlready = true;
            }
            
        }

    }
}
