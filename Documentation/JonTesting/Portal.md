Step 1: Set Up the Portal Manager

Create a Portal System GameObject:

Create an empty GameObject named "PortalSystem"
Add the PortalSystem script to it



Step 2: Create Portal Objects

Create Entrance Portal:

Create a new GameObject for your entrance portal
Add a collider component (Box, Sphere, or Capsule) and check "Is Trigger"
Add the Portal script to this GameObject
Make sure "Is Entrance" is checked
Add visual elements as child objects (portal ring, effects, etc.)
Add a Particle System component for portal effects (optional)


Create Exit Portal:

Create another GameObject for your exit portal
Add a collider component and check "Is Trigger"
Add the Portal script to this GameObject
Uncheck "Is Entrance" (or leave it false by default)
Add visual elements as child objects
Add a Particle System for exit effects (optional)



Step 3: Link the Portals

Set Up the Connections:

Select your entrance portal GameObject
In the Portal component inspector, drag your exit portal into the "Linked Portal" field
(Optional) You can also link the exit back to the entrance if you want two-way teleportation


Configure Portal Settings:

Set your portal colors
Adjust the "Velocity Multiplier" to control how fast objects exit the portal
Toggle "Preserve Angle" to maintain the entry angle or have objects exit straight forward



Step 4: Place Portals in Your Course

Position entrance and exit portals where you want them in your mini golf course
Make sure the "Teleportable Tag" matches your golf ball's tag (default is "GolfBall")
Orient the portals properly - objects will exit in the direction the exit portal is facing

Key Features

Directional Teleportation:

Objects maintain their relative angle when teleporting
Velocity can be preserved or boosted using the multiplier


Visual Feedback:

Portals share the same color to indicate they're linked
Particle effects play when teleportation occurs


Anti-Loop Protection:

Cooldown system prevents objects from immediately re-entering portals
Helps avoid infinite teleportation loops


Multiple Portal Pairs:

The system supports multiple portal pairs with different colors
Each pair can have different settings (velocity multiplier, etc.)



You can create multiple portal pairs by duplicating your entrance and exit portals and setting up new links between them. This allows for complex portal networks throughout your mini golf course.