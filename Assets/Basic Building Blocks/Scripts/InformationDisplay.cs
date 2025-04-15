using UnityEngine;
using TMPro;

public class InformationDisplay : MonoBehaviour
{
    public TMP_Text hitsText;
    public TMP_Text ballHealthText;
    public TMP_Text livesText;
    public TMP_Text distanceText;

   
    public BallManager ballInformation;
    public golfball golfballStats;
    public Transform ballTransform;
    public Transform holeTransform;

    

        // Update is called once per frame
        void Update()
    {
        

        hitsText.text = "Number of Hits: " + golfballStats.numberOfHits;
        ballHealthText.text = "Ball Health: " + ballInformation.currentHealth;
        livesText.text = "Lives: " + ballInformation.currentLives;

        distanceText.text = "Distance to Hole: " + ((int)Vector3.Distance(ballTransform.position, holeTransform.position)).ToString() + "m";
    }
}
