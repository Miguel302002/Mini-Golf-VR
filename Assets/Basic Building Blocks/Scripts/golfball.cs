using UnityEngine;
using System.Collections;
using UnityEngine.XR;
using UnityEngine.UI;
using TMPro;

public class golfball : MonoBehaviour
{

    // Reference to the single hole (assign via inspector)
    public GameObject hole;
    // UI Text to display final score
    //public Text finalScoreBoard;
    // Optionally, reference a flag, controllers, etc.
    //public GameObject flag;
    //public GameObject rCont;
    //public GameObject lCont;

    // to count number of hits
    //public TextMeshProUGUI hitcountText;

    // Hole properties
    private Vector3 ballStartPos;
    private Vector3 holePos;
    private float holeRadius;
    private int holePar;

    // Hit tracking and state
    private int hitCount = 0;
    private bool ballInHole;

    // timers for how long ball is in the hole
    public float requiredTimeInHole = 0.5f;
    private float timeInHole = 0f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeHole();
    }

    private void InitializeHole()
    {
        // Save the ball's starting position so you can reset if needed.
        ballStartPos = transform.position;
        // Set up hole info
        holePos = hole.transform.position;
        // Assuming the hole's transform scale.x gives its diameter
        holeRadius = hole.transform.localScale.x / 2;
        // Get the par from a Hole component on the hole (or set manually)
        holePar = hole.GetComponent<Hole>() != null ? hole.GetComponent<Hole>().holePar : 3;
        // Reset hit count and state
        hitCount = 0;
        ballInHole = false;
        // Make sure the ball collider is active
        GetComponent<SphereCollider>().enabled = true;






        // Optionally, position a flag to mark the hole location
        /*if (flag != null)
        {
            flag.transform.position = holePos;
            flag.SetActive(true);
        }*/
        // Update initial UI if needed

        //finalScoreBoard.text = $"Par: {holePar}\nHits: {hitCount}";
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

                // Disable the collider to avoid further triggers.
                GetComponent<SphereCollider>().enabled = false;

                // Optionally disable the flag to indicate the hole is "completed"
                /*if (flag != null)
                    flag.SetActive(false);*/

                // Begin sinking animation/effect
                StartCoroutine(SinkBall());
            }
            
        }
        else
        {
            timeInHole = 0f;
        }
    }


    private IEnumerator SinkBall()
    {
        // Optionally, wait a moment before sinking.
        yield return new WaitForSeconds(0.5f);
        // Here you could animate the ball shrinking or fading.
        // For simplicity, we’ll just disable the ball.
        gameObject.SetActive(false);
        // Update the final score UI.
        //finalScoreBoard.text = $"Course Completed!\nPar: {holePar}\nHits: {hitCount}";
    }



    private void OnTriggerEnter(Collider other)
    {
 
        if (other.tag == "Golf Club Head")
        {
            hitCount++;
            //Transfer velocity
            GetComponent<Rigidbody>().linearVelocity = other.GetComponent<GolfClubHead>().getVelocity() * 1.25F;
            //finalScoreBoard.text = $"Par: {holePar}\nHits: {hitCount}";
           // hitcountText.text = "Hits: " + hitCount;


        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Border"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.Reflect(rb.linearVelocity, collision.contacts[0].normal) * 0.75f;
        }
    }*/

    /*public void ResetHole()
    {
        gameObject.SetActive(true);
        transform.position = ballStartPos;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        hitCount = 0;
        ballInHole = false;
        GetComponent<SphereCollider>().enabled = true;
        // Reset flag if used.
        if (flag != null)
            flag.SetActive(true);
        // Update UI.
        finalScoreBoard.text = $"Par: {holePar}\nHits: {hitCount}";
    }*/

}
