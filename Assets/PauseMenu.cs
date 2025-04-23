using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{

    public InputActionProperty toggleMenu;
    private bool toggled = false;

    public Transform head;
    public float spawnDistance = 2f;

    public GameObject pauseMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        toggleMenu.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (toggleMenu.action.triggered)
        {
            //Debug.Log("Button Pressed");
            if (!toggled)
            {
                toggled = true;

                Vector3 forward = head.forward;
                forward.y = 0;
                forward.Normalize();

                Vector3 spawnPosition = head.position + forward * spawnDistance;
                spawnPosition.y = head.position.y;

                pauseMenu.transform.position = spawnPosition;

                Vector3 lookDirection = head.position - pauseMenu.transform.position;
                lookDirection.y = 0;
                pauseMenu.transform.rotation = Quaternion.LookRotation(-lookDirection);

                pauseMenu.SetActive(true);
            }
            else
            {
                toggled = false;
                pauseMenu.SetActive(false);
            }
        }
    }
}
