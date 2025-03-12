# Golf Club Components Documentation

## Overview
The golf club functionality is managed by two main scripts: `golfclub.cs` and `GolfClubHead.cs`. These scripts work together to implement realistic golf club behavior in VR.

## Class: `golfclub`

### Dependencies
- UnityEngine
- UnityEngine.XR
- UnityEngine.XR.Interaction.Toolkit

### Properties

| Property | Type | Description |
|----------|------|-------------|
| controller | Transform | Reference to the VR controller |
| positionOffset | Vector3 | Offset to position the club relative to the controller (default: (0, -0.50f, 0)) |
| rotationOffset | Vector3 | Rotation offset for the club (default: (174, 280, 182)) |

### Private Variables
- **rb** (Rigidbody): Reference to the golf club's rigidbody

### Methods

#### `void Start()`
- Initializes the Rigidbody and sets interpolation mode

#### `void FixedUpdate()`
- Updates the club position and rotation based on the controller's movement
- Applies position and rotation offsets for realistic club positioning

## Class: `GolfClubHead`

### Dependencies
- UnityEngine

### Properties

| Property | Type | Description |
|----------|------|-------------|
| maxVel | float | Maximum velocity for the club head |

### Private Variables
- **posI** (Vector3): Initial position for velocity calculation
- **posF** (Vector3): Final position for velocity calculation
- **vel** (Vector3): Current calculated velocity vector
- **velMag** (float): Magnitude of the velocity vector

### Methods

#### `void Start()`
- Initializes position tracking variables

#### `void FixedUpdate()`
- Updates position tracking for velocity calculation

#### `Vector3 getVelocity()`
- Calculates the velocity of the club head
- Limits velocity to the maximum allowed
- Returns the calculated velocity vector (used for ball impact)

## Usage

1. Attach the `golfclub.cs` script to the main golf club GameObject
2. Attach the `GolfClubHead.cs` script to the club head child GameObject
3. Configure the position and rotation offsets in the inspector
4. Set the maximum velocity for the club head

## Integration

These scripts work with:
- The XR Interaction system for controller tracking
- The `golfball.cs` script for ball impact detection and physics