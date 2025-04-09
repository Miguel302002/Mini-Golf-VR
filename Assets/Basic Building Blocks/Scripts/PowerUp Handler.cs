using UnityEngine;
using UnityEngine.InputSystem;

public class PowerUpHandler : MonoBehaviour
{
    private Powerup_Interface storedPowerUp;
    public InputActionProperty activatePowerUp;


    public void SetPowerUp(Powerup_Interface powerup)
    {
        storedPowerUp = powerup;
    }
    void Start()
    {
        activatePowerUp.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (storedPowerUp != null && activatePowerUp.action.triggered)
        {
            StartCoroutine(storedPowerUp.ApplyPowerUp(gameObject));
            storedPowerUp = null;
        }
    }
}
