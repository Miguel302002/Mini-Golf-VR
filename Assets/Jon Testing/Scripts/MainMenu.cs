using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button settingsButton;
    public Button instructionsButton;

    void Start()
    {
        // Link buttons to methods
        startButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettings);
        instructionsButton.onClick.AddListener(OpenInstructions);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Demo with vr Jon");
    }

    public void OpenSettings()
    {
        Debug.Log("Settings menu opened.");
    }

    public void OpenInstructions()
    {
        Debug.Log("Instructions menu opened.");
    }
}

