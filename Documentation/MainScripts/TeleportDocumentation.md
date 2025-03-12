# Teleport System Documentation

## Overview
The `TeleportToBall.cs` script implements functionality to teleport the player to the ball's location in VR, making it easier to position for the next shot.

## Class: `TeleportToBall`

### Dependencies
- UnityEngine
- UnityEngine.InputSystem

### Properties

| Property | Type | Description |
|----------|------|-------------|
| ballTransform | Transform | Reference to the golf ball transform |
| playerRig | Transform | Reference to the XR Origin or player rig transform |
| teleportAction | InputActionProperty | Input action for triggering teleportation |

### Methods

#### `void OnEnable()`
- Enables the teleport input action
- Subscribes to the performed event on the teleport action

#### `void OnDisable()`
- Unsubscribes from the performed event
- Disables the teleport input action

#### `void OnTeleportButton(InputAction.CallbackContext context)`
- Callback method that's called when the teleport button is pressed
- Calls the `TeleportPlayer()` method to execute the teleportation

#### `void TeleportPlayer()`
- Moves the player rig to the ball's position
- Maintains the player's Y position to avoid placing them inside the ground

## Usage

1. Attach the script to a GameObject in the scene
2. Assign the ball transform reference
3. Assign the player rig (XR Origin) transform
4. Configure the teleport input action in the Inspector

## Integration

This script works with:
- Unity's new Input System for VR controller input
- The XR Origin/player rig movement system
- The ball tracking system

## Notes

- The script preserves the player's Y-level during teleportation to maintain proper height
- Proper cleanup in OnDisable prevents memory leaks from lingering event subscriptions
- This teleport system works independently of any locomotion system that might be in the project