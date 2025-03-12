# Ball Reset Documentation

## Overview
The `BallReset.cs` script handles resetting the ball's position when it goes out of bounds or falls into a hole.

## Class: `BallReset`

### Dependencies
- UnityEngine

### Properties

| Property | Type | Description |
|----------|------|-------------|
| ball | Transform | Reference to the golf ball transform |
| startPosition | Vector3 | Initial position to reset the ball to |
| boundaryXMin | float | Minimum X boundary (default: -5f) |
| boundaryXMax | float | Maximum X boundary (default: 5f) |
| boundaryZMin | float | Minimum Z boundary (default: -10f) |
| boundaryZMax | float | Maximum Z boundary (default: 10f) |
| resetHeight | float | Y threshold for resetting the ball (default: -5f) |

### Methods

#### `void Start()`
- Captures the initial position of the ball for future resets

#### `void Update()`
- Continuously checks if the ball is outside the defined boundaries
- If out of bounds, calls the `ResetBall()` method

#### `void OnTriggerEnter(Collider other)`
- Detects when the ball enters a hole trigger collider
- Logs a "WINNER!!" message and resets the ball

#### `void ResetBall()`
- Resets the ball to the starting position
- Sets the ball's linear and angular velocity to zero

## Usage

1. Attach the script to a GameObject in the scene
2. Assign the ball transform reference
3. Configure the boundary values for out-of-bounds detection
4. Ensure the hole has a collider with the "Hole" tag and isTrigger set to true

## Integration

This script works with:
- The hole detection system
- The ball physics system (by resetting velocities)
- The overall course boundaries and gameplay flow