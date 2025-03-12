# Basic Building Blocks Documentation

This module contains the core components needed for the VR mini-golf game. These scripts implement the fundamental mechanics of the golf game, including the golf club interaction, ball physics, and camera behavior.

## Scripts Overview

### 1. GolfClubHead.cs
Controls the physics of the golf club head, calculates velocity for realistic ball impact.

### 2. Hole.cs
Manages hole interactions, scoring logic, and UI updates.

### 3. SmoothCameraFollow.cs
Implements smooth camera movement that follows a target (typically the golf ball).

### 4. camera_map.cs
Maps the VR controller to a camera view, positioning it appropriately in the VR space.

### 5. golfball.cs
Handles golf ball behavior, including hole detection, sinking animation, and position resetting.

### 6. golfclub.cs
Manages the positioning and movement of the golf club based on VR controller input.

## Prefabs and Components

This module includes several prefab components:
- Golf Ball
- Golf Club
- Hole
- Ball Following Camera
- Ball Viewing Screen
- Grass Field
- VR Camera rig

## Materials and Physics

Contains all necessary materials for the golf course components and physics materials for ball interactions.