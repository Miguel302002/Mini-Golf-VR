# Ball Physics Documentation

## Overview
The Jon Testing module contains several scripts for advanced ball physics and interactions, offering more complex gameplay mechanics than the basic implementation.

## Class: `BallController`

### Dependencies
- UnityEngine

### Properties

| Property | Type | Description |
|----------|------|-------------|
| pushForce | float | Speed of the ball when hit (default: 1f) |

### Private Variables
- **rb** (Rigidbody): Reference to the ball's rigidbody
- **currentSurface** (SurfaceType): Tracks the current surface the ball is on
- **canInteract** (bool): Flag to manage input during special states

### Methods

#### `void Start()`
- Initializes the rigidbody reference

#### `void Update()`
- Handles player keyboard input for pushing the ball
- Prevents input when the ball is in water or can't interact

#### `void OnCollisionEnter(Collision collision)`
- Detects the surface type based on collision tags

#### `void OnCollisionExit(Collision collision)`
- Resets surface type when leaving a surface

#### `void FixedUpdate()`
- Applies appropriate surface resistance effects

#### `void ApplySurfaceResistance()`
- Determines which surface resistance to apply

#### `void ApplySandResistance()`
- Slows the ball down in sand (multiplies velocity by 0.9f)

#### `void ApplyWaterResistance()`
- Applies heavy slowdown in water (multiplies velocity by 0.5f)

#### `void ApplyHit(Vector3 direction)`
- Applies force to the ball in the specified direction
- Adjusts force based on surface type

#### `SurfaceType GetSurfaceType(string tag)`
- Converts collision tags to SurfaceType enum values

#### `void StopInput()` / `void AllowInput()`
- Controls whether player input is processed

## Class: `BallSurfaceInteraction`

### Dependencies
- UnityEngine

### Properties

| Property | Type | Description |
|----------|------|-------------|
| sandDrag | float | Drag value for sand surfaces (default: 1f) |
| iceDrag | float | Drag value for ice surfaces (default: 0.1f) |
| waterDrag | float | Drag value for water surfaces (default: 0.05f) |
| lavaDrag | float | Drag value for lava surfaces (default: 1f) |
| normalDrag | float | Drag value for standard surfaces (default: 0.2f) |
| sandBounciness | float | Bounciness for sand impacts (default: 0.1f) |
| lavaBounciness | float | Bounciness for lava impacts (default: 0.8f) |
| normalBounciness | float | Standard bounciness value (default: 0.3f) |

### Methods

#### `void Start()`
- Initializes the rigidbody reference

#### `void OnCollisionStay(Collision collision)`
- Detects surfaces by tag and applies appropriate properties

#### `void ApplySurfaceProperties(float drag, float bounciness)`
- Sets drag and bounciness values on the ball

## Class: `GolfBallPhysics`

### Dependencies
- UnityEngine
- System.Collections

### Private Variables
- **rb** (Rigidbody): Reference to the ball's rigidbody
- **inWater** (bool): Tracks if the ball is in water
- **canInteract** (bool): Controls player interaction
- **sinkAmount** (float): Rate at which the ball sinks in water
- **sinkDuration** (float): How long it takes to sink
- **resetDelay** (float): Delay before resetting position after sinking

### Properties
- **minPosition** / **maxPosition** (Vector3): Boundaries for randomizing reset position

### Methods

#### `void Start()`
- Sets up rigidbody properties (mass, damping, etc.)

#### `void OnCollisionEnter(Collision collision)`
- Detects water and triggers sinking behavior
- Identifies sand surfaces

#### `void OnCollisionExit(Collision collision)`
- Updates state when leaving water

#### `IEnumerator SinkInWater()`
- Animated sinking behavior when in water
- Delays then resets ball position

#### `void ResetBallPosition()`
- Resets to a random position within boundaries
- Zeros out velocities

#### `void FixedUpdate()`
- Applies additional slowdown when in water

## Enum: `SurfaceType`
- **Normal**: Standard surfaces
- **Sand**: Sandy areas with high friction
- **Water**: Water hazards with sinking behavior

## Usage

1. Attach these scripts to the golf ball GameObject
2. Configure physics properties in the inspector
3. Ensure the course has properly tagged surfaces (Sand, Water, Ice, Lava)
4. Set boundary values for ball reset positions

## Integration

These scripts work together to create a comprehensive physics system for the golf ball, handling:
- Different surface interactions
- Environmental hazards
- Realistic physics properties
- Automatic recovery from water hazards