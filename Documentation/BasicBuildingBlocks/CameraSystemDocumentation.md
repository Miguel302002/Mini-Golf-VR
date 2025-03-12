# Camera System Documentation

## Overview
The Mini-Golf VR project includes two camera-related scripts: `SmoothCameraFollow.cs` and `camera_map.cs`. These scripts manage the camera behavior for both ball following and VR views.

## Class: `SmoothCameraFollow`

### Dependencies
- UnityEngine

### Properties

| Property | Type | Description |
|----------|------|-------------|
| target | Transform | The target to follow (typically the golf ball) |
| smoothSpeed | float | Smoothness factor for camera movement (default: 0.125f) |
| offset | Vector3 | Offset distance from the target |

### Methods

#### `void FixedUpdate()`
- Calculates the desired camera position based on target position and offset
- Smoothly interpolates the camera position for natural movement
- Makes the camera look at the target

## Class: `camera_map`

### Dependencies
- UnityEngine
- UnityEngine.XR
- UnityEngine.XR.Interaction.Toolkit

### Properties

| Property | Type | Description |
|----------|------|-------------|
| controller | Transform | Reference to the VR controller |
| positionOffset | Vector3 | Offset to position the camera relative to the controller (default: (0, -0.50f, 0)) |
| rotationOffset | Vector3 | Rotation offset for the camera (default: (174, 280, 182)) |

### Private Variables
- **rb** (Rigidbody): Reference to the camera's rigidbody

### Methods

#### `void Start()`
- Initializes the Rigidbody and sets interpolation mode

#### `void Update()`
- Updates the camera position and rotation based on the controller
- Applies position and rotation offsets for proper VR camera positioning

### Note
The script contains a commented-out `FixedUpdate()` method, indicating that physics-based camera movement was previously implemented but switched to transform-based positioning.

## Usage

### SmoothCameraFollow
1. Attach to a camera GameObject
2. Assign a target Transform (usually the golf ball)
3. Configure offset and smooth speed values

### camera_map
1. Attach to a camera or viewfinder GameObject
2. Assign a controller reference
3. Adjust position and rotation offsets as needed

## Integration

These camera scripts work with:
- The golf ball for tracking purposes
- VR controller for positioning in VR space
- The ball viewing screen for player feedback