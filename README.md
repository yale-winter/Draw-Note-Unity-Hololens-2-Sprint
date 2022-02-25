# Draw-Note-Unity-Hololens-2-Sprint
(C#, Unity3D, Hololens 2, AR, MRTK2, UWP)

## Overview:
Draw lines in Augmented Reality on the Hololens 2 with MRTK2 and a variety of features.

## Screenshots:
![drawnote5_caption](https://user-images.githubusercontent.com/5803874/155637629-c08800d3-f7db-428d-a46d-39c2a2e713db.jpg)
![drawnote4_caption](https://user-images.githubusercontent.com/5803874/155637627-836450b2-05db-421b-8312-846fac8029b2.jpg)
![drawnote1_caption](https://user-images.githubusercontent.com/5803874/155637618-66b3bad0-d822-455b-a620-a2abca995623.jpg)
![drawnote2_caption](https://user-images.githubusercontent.com/5803874/155637623-df23a4a5-f941-4b2f-9f1d-e234c88e9626.jpg)

## Controls:

The controls in Draw Note are the regular style of Hololens 2 controls. 

There is 3 ways to interact:

1. The User's Gaze is directed at the Button from a distance, and then they make the Air Tap gesture (focus gaze then right click in emulator)
2. The User says the Voice Command associated with the button/action ex. "RED", "DRAW", "STOP"
3. The User does a nearby hand interaction, by actually pressing the button with their finger

There are two Menus that are context dependant:
1. Small Note Drawing Menu - Is smaller and is active while the User is drawing. It simply directs the user to say "STOP" to stop, and it has a Stop Button
2. Drawing Details Menu - Is larger and is active while the User is not drawing. It contains buttons for all input options listed below.

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
| GREEN | Set draw color to Green |

## Drawing Modes:

It's important for the User to understand and switch between the different Drawing Modes to get the expected behavior.

| Drawing Modes | Description |
| --- | --- |
| Normal | Draw only on draw plane with hand (Ex. you want to write clearly like on a piece of paper) |
| Mesh | Draw only on meshes with hand (Ex. you want to write on a wall, or floor) |
| Finger | Draw from your finger (Ex. you want to draw a line exactly from your finger position) |

## Implementation:

Basic implementation outline chart pictured below.

![drawnote_implementation_diagram](https://user-images.githubusercontent.com/5803874/155637616-33301d4b-4607-403f-b2fe-3b81429eaaf9.jpg)

- User Inputs are interpreted by MRTK2 for the correct enviornment
- Some custom configurations are applied directly to MRTK profile (ex. custom voice inputs)
- Drawings are stored in an Object with a ParticleTrail component
