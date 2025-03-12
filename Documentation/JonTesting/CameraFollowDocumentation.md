# Camera Follow Documentation

## Overview
The `CameraFollow.cs` script implements a camera that smoothly follows the golf ball, providing an optimal viewing angle during gameplay.

## Class: `CameraFollow`

### Dependencies
- UnityEngine

### Properties

| Property | Type | Description |
|----------|------|-------------|
| ball | Transform | Reference to the golf ball transform |
| offset | Vector3 | Distance and position offset from the ball |
| smoothSpeed | float | Smoothness factor for camera movement (default: 0.125f) |

### Methods

#### `void Start()`
- If no ball transform is assigned, attempts to find a GameObject with the "Ball" tag
- This allows for automatic setup without manual reference assignment

#### `void Update()`
- Calculates the desired position based on the ball's position plus the offset
- Smoothly interpolates the camera position for natural movement
- Makes the camera look at the ball

## Usage

1. Attach the script to a camera GameObject
2. Assign the golf ball's transform (or ensure it has the "Ball" tag)
3. Configure the offset vector to position the camera appropriately
4. Adjust the smooth speed for desired camera movement behavior

## Integration

This script works with:
- The golf ball tracking system
- The overall camera system of the game
- Player view management

## Notes

- The `smoothSpeed` property controls how quickly the camera catches up to the ball
  - Lower values (e.g., 0.05f) create very smooth but slower camera movement
  - Higher values (e.g., 0.5f) create more responsive but potentially jerky movement
- The `offset` vector determines not just distance but viewing angle
  - For a top-down view: (0, 10, 0)
  - For a behind view: (0, 2, -5)
  - For a side view: (5, 2, 0)