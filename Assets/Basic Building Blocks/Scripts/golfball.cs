using UnityEngine;
using System.Collections;
using UnityEngine.XR;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem;

public class golfball : MonoBehaviour
{

    // Reference to the hole 
    public GameObject hole;
   
    // Hole properties
    private Vector3 holePos;
    private float holeRadius;
    

    // Hit tracking and state
    //private int hitCount = 0;
    private bool ballInHole;

    // timers for how long ball is in the hole
    public float requiredTimeInHole = 0.5f;
    private float timeInHole = 0f;

    // ball information
    private Vector3 ballBeforeHitPosition;

    // Input action for resetting ball's position
    public InputActionProperty restBallAction;

    // reference to ball object
    private Rigidbody ball;

    // ***** Change By Jonathan for Hitcount UI START ***** 
    private HitCounterUI hitCounter;
    // ***** END *****



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ball = GetComponent<Rigidbody>();
        InitializeHole();
        restBallAction.action.Enable();
        hitCounter = FindObjectOfType<HitCounterUI>();
    }

    private void OnDestroy()
    {
        restBallAction.action.Disable();
    }

    private void InitializeHole()
    {
        ballBeforeHitPosition = transform.position;
        holePos = hole.transform.position;
        holeRadius = hole.transform.localScale.x / 2;
        ballInHole = false;
        GetComponent<SphereCollider>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the ball is within the hole's radius.
        // Once it is, start the "sinking" process.
        if (Vector3.Distance(transform.position, holePos) < holeRadius)
        {
            timeInHole += Time.deltaTime;
            if(timeInHole >= requiredTimeInHole && !ballInHole)
            {
                ballInHole = true;
                GetComponent<SphereCollider>().enabled = false;      // Disable the collider to avoid further triggers.
                StartCoroutine(SinkBall());                          // Begin sinking animation/effect
            }
        }
        else{timeInHole = 0f;}

        if(restBallAction.action.triggered)                         // if user presses reset button
        {
            ResetBallPosition();
        }
    }

    private IEnumerator SinkBall()
    {
        // ***** Change By Jonathan for Hitcount UI START ***** 
        // This will check to see if the course has been completed it will then save the hit score to the player memory, if the hit score is better than it will replace it otherwise it will stay the previous known best hit count
        if (hitCounter != null){
            hitCounter.CompleteHole();
        }
        // ***** END *****
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Golf Club Head")
        {
            ballBeforeHitPosition = transform.position;

            // ***** Change By Jonathan for Hitcount UI START ***** 
            OnBallHit();
            // ***** END ******  

            //Debug.Log("Hit");
            //hitCount++;

            //GetComponent<Rigidbody>().linearVelocity = collision.gameObject.GetComponent<GolfClubHead>().getVelocity() * 1.25F;          //Transfer velocity
        }
    }

    private void ResetBallPosition()
    {
        transform.position = ballBeforeHitPosition;
        ball.linearVelocity = Vector3.zero;
        ball.angularVelocity = Vector3.zero;
    }

    // ***** Hit counter UI helper by Jonathan START ***** 
    // Call this whenever the ball is hit
    private void OnBallHit()
    {
        if (hitCounter != null)
        {
            hitCounter.IncrementHitCount();
        }
    }
    // ***** END ***** 
}
