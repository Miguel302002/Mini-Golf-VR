# Hole Component Documentation

## Overview
The `Hole.cs` script manages the hole functionality in the VR mini-golf game, handling scoring and UI updates.

## Class: `Hole`

### Dependencies
- UnityEngine
- UnityEngine.UI
- System.Collections
- System.Collections.Generic

### Properties

| Property | Type | Description |
|----------|------|-------------|
| scoreBoard | Text | Reference to the main scorecard UI Text element |
| finalScoreBoard | Text | Reference to the final scorecard UI Text element |
| puttArea | GameObject | The starting position marker for the ball |
| holeNum | int | Hole number (for identification) |
| holePar | int | Par score for the hole |
| holeScore | int | Current player score for the hole |

### Methods

#### `void Start()`
- Initializes the score to zero
- Sets up initial scorecard UI elements

#### `void UpdateScore(int num)`
- Updates the hole score with the provided value
- Updates both the main and final scoreboard Text elements

#### `Vector3 GetBallPos()`
- Returns the starting position for the golf ball (puttArea position)
- Used when resetting the ball position

## Usage

1. Attach the script to a hole GameObject
2. Assign Text UI elements for scorecards
3. Set the puttArea GameObject (ball starting position)
4. Configure hole number and par value

## Integration

This script works with:
- UI Text elements for score display
- The golf ball's reset position functionality
- The overall scoring system of the game