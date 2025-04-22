using UnityEngine;

public class Playconfirmation : MonoBehaviour
{

    private bool playedAlready = false;
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
            if (!playedAlready)
            {
                AudioManager.instance.Play("UI Sounds");
                playedAlready = true;
            }
        }
    }
}
