using UnityEngine;
using UnityEngine.InputSystem;

public class Display_Manager : MonoBehaviour
{
    public GameObject quad;
    public Camera ballCam;
    public GameObject infoDisplay;

    public InputActionProperty toggleDisplay;

    private int displayMode = 0;

    private void Start()
    {
        toggleDisplay.action.Enable();
        ApplyDisplayMode();

    }

    private void Update()
    {
        if(toggleDisplay.action.triggered)
        {
            displayMode = (displayMode + 1) % 3;
            ApplyDisplayMode();
        }
    }

    void ApplyDisplayMode()
    {
        switch(displayMode)
        {
            case 0:
                quad?.SetActive(true);
                ballCam?.gameObject.SetActive(true);
                infoDisplay?.SetActive(false);
                break;

            case 1:
                quad?.SetActive(false);
                ballCam?.gameObject.SetActive(false);
                infoDisplay?.SetActive(true);
                break;

            case 2:
                quad?.SetActive(false);
                ballCam?.gameObject.SetActive(false);
                infoDisplay?.SetActive(false);
                break;
        }
    }

}
