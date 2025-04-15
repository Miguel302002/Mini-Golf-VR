using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    
    public Transform target;
    NavMeshAgent agent;
    Animator animator;

    public int hitsRemaining = 2;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius )
        {
            agent.SetDestination(target.position);

            if (agent.velocity.magnitude > 0.1f)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }

            if (distance <= agent.stoppingDistance)
            {
                // attack target
                FaceTarget();
            }
        }
        else
        {
            animator.SetBool("isWalking", false); // stop walking when out of range
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Golf Club Head"))
        {
            hitsRemaining--;
            Debug.Log("Hit");

            if(hitsRemaining <= 0)
            {
                Debug.Log("layer changed");
                gameObject.layer = LayerMask.NameToLayer("Sliceable");
            }
        }
    }
}
