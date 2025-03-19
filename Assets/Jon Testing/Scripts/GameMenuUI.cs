using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenuUI : MonoBehaviour
{
    [Header("Menu References")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject creditsPanel;
    
    [Header("Button References")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backToMainFromOptions;
    [SerializeField] private Button backToMainFromCredits;
    
    [Header("Options")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Toggle vrComfortModeToggle;
    
    [Header("VR Interaction")]
    [SerializeField] private float pointerHoverScale = 1.1f;
    [SerializeField] private float buttonScaleSpeed = 5f;
    
    // References to original button scales
    private Dictionary<Button, Vector3> originalButtonScales = new Dictionary<Button, Vector3>();
    private Button currentlyHoveredButton = null;
    
    private void Start()
    {
        // Store original button scales
        StoreButtonScales();
        
        // Set up button events
        SetupButtonEvents();
        
        // Show main menu, hide others
        ShowMainMenu();
        
        // Load saved settings if any
        LoadSettings();
    }
    
    private void StoreButtonScales()
    {
        Button[] allButtons = GetComponentsInChildren<Button>(true);
        foreach (Button button in allButtons)
        {
            originalButtonScales[button] = button.transform.localScale;
        }
    }
    
    private void SetupButtonEvents()
    {
        // Main menu buttons
        playButton.onClick.AddListener(StartGame);
        optionsButton.onClick.AddListener(ShowOptions);
        creditsButton.onClick.AddListener(ShowCredits);
        quitButton.onClick.AddListener(QuitGame);
        
        // Back buttons
        backToMainFromOptions.onClick.AddListener(ShowMainMenu);
        backToMainFromCredits.onClick.AddListener(ShowMainMenu);
        
        // Options events
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        vrComfortModeToggle.onValueChanged.AddListener(SetComfortMode);
    }
    
    #region UI Navigation
    
    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }
    
    public void ShowOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }
    
    public void ShowCredits()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
    
    #endregion
    
    #region Button Actions
    
    public void StartGame()
    {
        // You can replace "GameScene" with your actual game scene name
        SceneManager.LoadScene("GameScene");
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    
    #endregion
    
    #region Settings
    
    private void LoadSettings()
    {
        // Load saved settings from PlayerPrefs
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        vrComfortModeToggle.isOn = PlayerPrefs.GetInt("ComfortMode", 1) == 1;
        
        // Apply loaded settings
        SetMusicVolume(musicVolumeSlider.value);
        SetSFXVolume(sfxVolumeSlider.value);
        SetComfortMode(vrComfortModeToggle.isOn);
    }
    
    public void SetMusicVolume(float volume)
    {
        // Implement your audio manager call here
        // Example: AudioManager.Instance.SetMusicVolume(volume);
        
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
    
    public void SetSFXVolume(float volume)
    {
        // Implement your audio manager call here
        // Example: AudioManager.Instance.SetSFXVolume(volume);
        
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }
    
    public void SetComfortMode(bool enabled)
    {
        // Implement VR comfort mode settings here
        // Example: VRManager.Instance.SetComfortMode(enabled);
        
        PlayerPrefs.SetInt("ComfortMode", enabled ? 1 : 0);
        PlayerPrefs.Save();
    }
    
    #endregion
    
    #region VR Interaction
    
    // Call this from your VR pointer/controller
    public void OnButtonHoverEnter(Button button)
    {
        currentlyHoveredButton = button;
        StopAllCoroutines();
        StartCoroutine(ScaleButton(button, originalButtonScales[button] * pointerHoverScale));
    }
    
    // Call this from your VR pointer/controller
    public void OnButtonHoverExit(Button button)
    {
        if (currentlyHoveredButton == button)
        {
            currentlyHoveredButton = null;
        }
        
        StopAllCoroutines();
        StartCoroutine(ScaleButton(button, originalButtonScales[button]));
    }
    
    private IEnumerator ScaleButton(Button button, Vector3 targetScale)
    {
        Transform buttonTransform = button.transform;
        Vector3 startScale = buttonTransform.localScale;
        float time = 0;
        
        while (time < 1)
        {
            buttonTransform.localScale = Vector3.Lerp(startScale, targetScale, time);
            time += Time.deltaTime * buttonScaleSpeed;
            yield return null;
        }
        
        buttonTransform.localScale = targetScale;
    }
    
    #endregion
}