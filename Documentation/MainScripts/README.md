# Main Scripts Documentation

This module contains the main game scripts that tie together the core functionality and connect the VR experience.

## Scripts Overview

### TeleportToBall.cs
Implements functionality to teleport the player to the ball's location in VR, making it easier to position for the next shot. This script:
- Uses Unity's Input System for VR controller input
- Maintains the player's Y-level while teleporting to the ball's position
- Handles input action events properly with enable/disable logic

## Input Actions

The project uses several input action assets:
- InputSystem_Actions.inputactions
- XRInputButtonActions.inputactions

These define the mappings for VR controller inputs across different platforms.