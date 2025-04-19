using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float delay = 3f;
    //float countdown;
   // private bool hasExploded = false;
    public float force = 700f;
    public float radius = 5f;

    public GameObject explosionEffect;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //countdown = delay;
    }

    // Update is called once per frame
    /*void Update()
    {
        countdown -= Time.deltaTime;

        if(countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
        
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if(other.tag == "Fake Portal")
            {
                ExplodeFakePortal(rb);
            }
            else
            {
                Explode(rb);
            }
            
            BallManager.Instance.TakeDamageExplosion(100);
            
        }
    }

    void Explode(Rigidbody ball)
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        ball.AddExplosionForce(force, transform.position, radius);

        Destroy(transform.parent.gameObject);
       

    }

    void ExplodeFakePortal(Rigidbody ball)
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        ball.AddExplosionForce(force, transform.position, radius);

        Destroy(gameObject);


    }


}
