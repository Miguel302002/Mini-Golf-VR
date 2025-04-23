using System.Collections;
using UnityEngine;

public class BreakableFloor4 : MonoBehaviour
{

    public float delay = 5f;
    //private bool triggered = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            //triggered = true;
            StartCoroutine(DisableAfterDelay());
        }
    }

    private IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        AudioManager.instance.Play("Laugh");
        gameObject.SetActive(false);
    }
}
