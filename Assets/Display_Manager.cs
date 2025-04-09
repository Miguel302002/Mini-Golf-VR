using UnityEngine;
using UnityEngine.InputSystem;

public class Display_Manager : MonoBehaviour
{

    public GameObject quad;
    public Camera ballCam;
    //public GameObject scoreScreen;

    public InputActionProperty toggleDisplay;

    private bool displayActive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        toggleDisplay.action.Enable();

    }

    // Update is called once per frame
    void Update()
    {
        if (toggleDisplay.action.triggered)
        {
            displayActive = !displayActive;
            ToggleDisplay(displayActive);
        }
    }

    void ToggleDisplay(bool state)
    {
        // Toggle the quad and camera for the ball cam
        if (quad != null)
            quad.SetActive(state);

        if (ballCam != null)
            ballCam.gameObject.SetActive(state);

        // If you have a score screen, disable it when the mini golf view is active and vice versa.
        /*if (scoreScreen != null)
            scoreScreen.SetActive(!state);*/
    }
}
