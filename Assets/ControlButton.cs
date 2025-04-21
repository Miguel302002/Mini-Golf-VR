using UnityEngine;

public class ControlButton : MonoBehaviour
{

    public GameObject wholeMenu;
    public GameObject controlsInformation;
     
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleControlInformation()
    {
        AudioManager.instance.Play("UI Sounds");
        wholeMenu.SetActive(false);
        controlsInformation.SetActive(true);
    }

    public void toggleWholeMenuViaBackButton()
    {
        AudioManager.instance.Play("UI Sounds");
        controlsInformation.SetActive(false);
        wholeMenu.SetActive(true);
        
    }
}
