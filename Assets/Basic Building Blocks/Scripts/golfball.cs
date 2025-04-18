using UnityEngine;
using System.Collections;
using UnityEngine.XR;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem;

private HitCounterUI hitCounter;

public class golfball : MonoBehaviour
{

    // Reference to the hole 
    public GameObject hole;
   
    // Hole properties
    private Vector3 holePos;
    private float holeRadius;
    


    private bool ballInHole;

    // timers for how long ball is in the hole
    public float requiredTimeInHole = 0.5f;
    private float timeInHole = 0f;

    // ball information
    public Vector3 ballBeforeHitPosition;

    // Input action for resetting ball's position
    public InputActionProperty restBallAction;

    // reference to ball object
    private Rigidbody ball;

    public int numberOfHits = 0;

    public GameObject courseCompleteMenu;
    public GameObject firstStar;
    public GameObject secondStar;
    public GameObject thirdStar;



    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ball = GetComponent<Rigidbody>();
        InitializeHole();
        restBallAction.action.Enable();
        numberOfHits = 0;

        // Find the HitCounterUI in the scene
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
        // Save the best score when the hole is completed
        if (hitCounter != null)
        {
            hitCounter.CompleteHole();
        }
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);

        courseCompleteMenu.SetActive(true);

        if(numberOfHits > 25)
        {
            firstStar.SetActive(true);
        }
        else if(numberOfHits <= 25 && numberOfHits > 15)
        {
            firstStar.SetActive(true);
            secondStar.SetActive(true);
        }
        else if(numberOfHits <= 15)
        {
            firstStar.SetActive(true);
            secondStar.SetActive(true);
            thirdStar.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Golf Club Head")
        {
            ballBeforeHitPosition = transform.position;
            //Debug.Log("Hit");
            numberOfHits++;

            // Update the HitCounterUI
            if (hitCounter != null)
            {
                hitCounter.IncrementHitCount();
            }
        }
    }

    private void ResetBallPosition()
    {
        transform.position = ballBeforeHitPosition;
        ball.linearVelocity = Vector3.zero;
        ball.angularVelocity = Vector3.zero;
    }

    private int ReturnNumberofHits()
    {
        return numberOfHits;
    }
}
