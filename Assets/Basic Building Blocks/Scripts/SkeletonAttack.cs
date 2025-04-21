using UnityEngine;

public class SkeletonAttack : MonoBehaviour
{
    public int damageAmount = 25;
    public float attackCooldown = 4f;
    public float knockbackForce = 5f;

    private float lastAttackTime = -Mathf.Infinity;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && Time.time >= lastAttackTime + attackCooldown)
        {
           
                BallManager.Instance.TakeDamage(damageAmount);
                lastAttackTime = Time.time;

                Rigidbody ballRb = other.GetComponent<Rigidbody>();
                if (ballRb != null)
                {
                    Vector3 knockbackDir = (other.transform.position - transform.position).normalized;
                    ballRb.AddForce(knockbackDir * knockbackForce, ForceMode.Impulse);
                    AudioManager.instance.Play("Skeleton Attack");
                }


        }
    }
}
