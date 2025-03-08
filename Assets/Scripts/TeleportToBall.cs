using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportToBall : MonoBehaviour
{
    public Transform ballTransform; // Assign ball transform in inspector
    public Transform playerRig;     // Assign your XR Origin or player rig
    public InputActionProperty teleportAction; // Drag your TeleportAction here in the Inspector

    private void OnEnable()
    {
        teleportAction.action.Enable();
        teleportAction.action.performed += OnTeleportButton;
    }

    private void OnDisable()
    {
        teleportAction.action.performed -= OnTeleportButton;
        teleportAction.action.Disable();
    }

    private void OnTeleportButton(InputAction.CallbackContext context)
    {
        TeleportPlayer();
    }

    private void TeleportPlayer()
    {
       
            Vector3 newPosition = ballTransform.position;
            newPosition.y = playerRig.position.y; // Maintain player's Y level
            playerRig.position = newPosition;
        
        
    }
}
