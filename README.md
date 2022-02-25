# Draw-Note-Unity-Hololens-2-Sprint
C#, Unity3D, Hololens 2, AR, MRTK2, UWP

## Overview:
Draw lines in Augmented Reality on the Hololens 2 with MRTK2 and a variety of features.

### Screenshots:
![drawnote5_caption](https://user-images.githubusercontent.com/5803874/155637629-c08800d3-f7db-428d-a46d-39c2a2e713db.jpg)
![drawnote4_caption](https://user-images.githubusercontent.com/5803874/155637627-836450b2-05db-421b-8312-846fac8029b2.jpg)
![drawnote1_caption](https://user-images.githubusercontent.com/5803874/155637618-66b3bad0-d822-455b-a620-a2abca995623.jpg)
![drawnote2_caption](https://user-images.githubusercontent.com/5803874/155637623-df23a4a5-f941-4b2f-9f1d-e234c88e9626.jpg)

## Controls:

The controls in Draw Note are the regular style of Hololens 2 controls. 

**While drawing:**
The User holds out arm in front of their body in a comfortable position. In *Normal Mode* the drawing will happen just beyond this.
The User can move their hand, arm, head, body and look around while continue to draw. Read **Drawing Modes** later for more details.

**There is 3 ways to interact with buttons:**
1. The User's Gaze is directed at the Button from a distance, and then they make the Air Tap gesture (focus gaze then right click in emulator)
2. The User says the Voice Command associated with the button/action ex. "RED", "DRAW", "STOP"
3. The User does a nearby hand interaction, by actually pressing the button with their finger

**There are two Menus that are context dependant:**
- Small Note Drawing Menu - Is smaller and is active while the User is drawing. It simply directs the user to say "STOP" to stop, and it has a Stop Button
- Drawing Details Menu - Is larger and is active while the User is not drawing. It contains buttons for input options listed below, except Stop

| Voice Command | Description |
| --- | --- |
| DRAW | Start drawing |
| STOP | Stop drawing |
| MODE | Switch drawing mode |
| UNDO | Undo last drawing |
| CLEAR | Clear all drawings |
| WHITE | Set draw color to white |
| RED | Set draw color to red |
| YELLOW | Set draw color to yellow |
| GREEN | Set draw color to green |

### Drawing Modes:

It's important for the User to understand and switch between the different Drawing Modes to get the expected behavior.

| Drawing Modes | Description |
| --- | --- |
| *Normal* | Draw only on draw plane with hand (Ex. you want to write clearly like on a piece of paper) |
| *Mesh* | Draw only on meshes with hand (Ex. you want to write on a wall, or floor) |
| *Finger* | Draw from your finger (Ex. you want to draw a line exactly from your finger position) |

## Implementation:

![drawnote_implementation_diagram](https://user-images.githubusercontent.com/5803874/155637616-33301d4b-4607-403f-b2fe-3b81429eaaf9.jpg)

- User Inputs are interpreted by MRTK2 for the correct environment
- Some custom configurations are applied directly to MRTK profile (Ex. custom voice inputs)
- Drawings are stored in an Object with a Unity ParticleTrail component (Each saved node of the instance Drawing requires very little storage)
- Create new drawing Object when starting to draw, or changing colors for easy Undo layers
- Menus follow the User and stay within the camera frustum as per Hololens 2 style. They can also be repositioned

**Draw Plane description (only relevant while using *Normal Mode*):**
Default target drawing distance is 64 centimeters away (approx average human arm length) from the User. DrawPlane GameObject's center is set to 1 meter away with a depth/thickness of 72 centimeters (to more safely catch raycast). It's position and rotation is always set to be that distance and facing the User. So if the User draws directly in the middle of where their looking, that ray would hit the collider at 64 centimeters away.
