using UnityEngine;

public class Enemy : MonoBehaviour

{
    public float maxSpeed;
    private float speed;

    private Collider[] hitColliders;
    private RaycastHit hit;

    public float sightRange;
    public float detectionRange;

    public Rigidbody rb;
    public GameObject target;

    private bool seePlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        if (!seePlayer)
        {
            hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
            foreach (var colliders_ in hitColliders)
            {
                if(colliders_.tag == "Ball")
                {
                    target = colliders_.gameObject;
                    seePlayer = true;
                }
            }
        }
        else
        {
            if(Physics.Raycast(transform.position, (target.transform.position - transform.position), out hit, sightRange)) 
            {
                if(hit.collider.tag != "Ball")
                {
                    seePlayer = false;
                }
                else
                {
                    var heading = target.transform.position - transform.position;
                    var distance = heading.magnitude;
                    var direction = heading/distance;

                    Vector3 move = new Vector3(direction.x * speed, 0, direction.z * speed);
                    rb.linearVelocity = move;
                    transform.forward = move;
                }
            }
        }
        
    }
}
