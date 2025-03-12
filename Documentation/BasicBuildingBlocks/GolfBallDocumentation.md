# Golf Ball Component Documentation

## Overview
The `golfball.cs` script manages the behavior of the golf ball in the VR mini-golf game, handling interactions with the hole, input for resetting position, and ball physics.

## Class: `golfball`

### Dependencies
- UnityEngine
- UnityEngine.XR
- UnityEngine.UI
- TMPro
- UnityEngine.InputSystem

### Properties

| Property | Type | Description |
|----------|------|-------------|
| hole | GameObject | Reference to the hole object |
| requiredTimeInHole | float | Time the ball needs to be in the hole to register (default: 0.5s) |
| restBallAction | InputActionProperty | Input action for resetting the ball's position |

### Private Variables

- **holePos** (Vector3): Position of the hole
- **holeRadius** (float): Radius of the hole for detection
- **ballInHole** (bool): Flag indicating if the ball is in the hole
- **timeInHole** (float): Tracks how long the ball has been in the hole
- **ballBeforeHitPosition** (Vector3): Stores the ball's position before it was hit
- **ball** (Rigidbody): Reference to the ball's rigidbody component

### Methods

#### `void Start()`
- Initializes the rigidbody and hole properties
- Enables the reset ball input action

#### `void OnDestroy()`
- Disables the reset ball input action

#### `void InitializeHole()`
- Sets up initial ball and hole properties
- Enables the sphere collider

#### `void Update()`
- Checks if the ball is within the hole radius
- Starts the sinking process if the ball has been in the hole long enough
- Handles input for resetting the ball position

#### `IEnumerator SinkBall()`
- Coroutine that handles the ball sinking animation/effect
- Waits a short time before disabling the ball gameobject

#### `void OnCollisionEnter(Collision collision)`
- Detects collisions with the golf club head
- Updates the ball's last hit position

#### `void ResetBallPosition()`
- Resets the ball to its position before the last hit
- Zeros out all velocity and angular velocity

## Usage

1. Attach the script to a golf ball GameObject
2. Assign a hole GameObject in the inspector
3. Configure the required time for the ball to stay in the hole
4. Set up the reset ball input action

## Integration

This script works with:
- The `GolfClubHead.cs` script for impact detection
- XR Input System for ball reset functionality