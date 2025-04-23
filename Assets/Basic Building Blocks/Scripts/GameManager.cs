using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public void RetryLevel()

    {
        AudioManager.instance.Play("UI Sounds");
        // Reload the currently active scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void LoadLevelOne()

    {
        AudioManager.instance.Play("UI Sounds");
        SceneManager.LoadScene("ActualCourse1");
    }

    public void LoadStartScreen()
    {
        AudioManager.instance.Play("UI Sounds");
        SceneManager.LoadScene("Start Screen");
    }

    public void LoadLevelTwo()
    {
        AudioManager.instance.Play("UI Sounds");
        SceneManager.LoadScene("Course2");
    }

    public void QuitAppllication()
    {
        AudioManager.instance.Play("UI Sounds");
        Application.Quit();
    }
}
