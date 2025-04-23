using UnityEngine;

public class StartDarkMusic : MonoBehaviour
{

    private bool startedAlready = false;
    public bool comingFromIce = false;
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
            if (!startedAlready && !comingFromIce)
            {
                AudioManager.instance.Stop("Main Music");
                AudioManager.instance.Play("Dark Music");
                startedAlready = true;
            }

            else if(startedAlready && comingFromIce)
            {
                AudioManager.instance.Stop("Main Music");
                AudioManager.instance.Stop("Wind");
                AudioManager.instance.Play("Dark Music");
                startedAlready = false;
            }

            else if(!startedAlready && comingFromIce)
            {
                AudioManager.instance.Stop("Main Music");
                AudioManager.instance.Stop("Wind");
                AudioManager.instance.Play("Dark Music");
                startedAlready = true;
            }
            
        }

    }
}
