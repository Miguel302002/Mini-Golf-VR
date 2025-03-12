# Input System Documentation

## Overview
The Mini-Golf VR project uses Unity's new Input System to handle VR controller inputs. The system is configured through several input action assets that define mappings for different controllers and actions.

## Input Action Assets

### 1. InputSystem_Actions.inputactions
This is the main input action asset for the game, likely containing general gameplay actions such as:
- Club swinging mechanics
- UI interaction
- Menu navigation
- General game controls

### 2. XRInputButtonActions.inputactions
This input action asset focuses specifically on button inputs for XR controllers, which may include:
- Grip and trigger button actions
- Primary and secondary button controls
- Thumbstick and touchpad inputs
- Hand presence detection

## Unity XR Input System Integration

The project uses Unity's XR Interaction Toolkit and Input System to connect VR controller inputs to in-game actions. This integration typically includes:

- **Action-based controllers**: Using the action-based XR controller components
- **Input action properties**: Components like `InputActionProperty` used in scripts
- **Cross-platform support**: Input mappings that work across various VR platforms
- **Interaction profiles**: Configuration for different controller types (Oculus, OpenXR, etc.)

## Usage in Scripts

Scripts in the project reference these input actions using InputActionProperty fields:

```csharp
// Example from TeleportToBall.cs
public InputActionProperty teleportAction;

private void OnEnable()
{
    teleportAction.action.Enable();
    teleportAction.action.performed += OnTeleportButton;
}
```

And from the golfball.cs script:

```csharp
// Input action for resetting ball's position
public InputActionProperty restBallAction;

void Start()
{
    // ...
    restBallAction.action.Enable();
}

void Update()
{
    // ...
    if(restBallAction.action.triggered)
    {
        ResetBallPosition();
    }
}
```

## Proper Input Lifecycle Management

The scripts in the project typically follow these best practices for input handling:

1. **Enable actions** when needed (usually in Start or OnEnable)
2. **Subscribe to events** for action-based responses
3. **Unsubscribe from events** when done to prevent memory leaks
4. **Disable actions** when not needed (usually in OnDisable or OnDestroy)

## Setting Up New Input Actions

To create new input actions for the project:
1. Open the relevant .inputactions asset in Unity
2. Add new action maps as needed
3. Define actions with appropriate binding paths for VR controllers
4. Reference the actions in scripts using InputActionProperty fields
5. Handle the input lifecycle (enable, subscribe, unsubscribe, disable)