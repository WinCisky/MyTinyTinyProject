# My Tiny Tiny Project
This project was a fork of the [Tiny3D project sample](https://github.com/Unity-Technologies/ProjectTinySamples/tree/master/Tiny3D) released by Unity.
Since it was a preview was [kinda limited](https://docs.google.com/document/d/1A8hen2hLFY5FLkC5gd3JP2Z-IpHfnAX-CpYLK3aOdwA/edit#) but I thought it was fun to try to implement a small game using this new preview package (the Data-Oriented Technology Stack, [DOTS](https://unity.com/dots) in short, is really cool)

## Development process
First, I needed some control over the player, so I created the input system.
It was barebone (only left and right input) but worked well enough (could definitely be improved).
- Player Component to mark an Entity as the player,
- PlayerInput as Component to store the input of the player,
- InputShow to retrieve the input and PlayerSystem to manage the input -> moves the player
 
Then, since I wanted the player to be able to move infinitely along the x-axis, I needed the camera to follow the player.
- FollowPlayerSystem get the player pos and the camera pos and lerp between them

Finally, I wanted to create a path to follow for the player, so I created a grid of instances of a prefab, and with only 30(x) * 2 * 30(y) * 2 (=3600) instances I was able to cover the entire screen with a bit of margin. The rendering is fixed to 1920*1080 no matter what, so moving the cubes once they became too much far from the player I was able to create the illusion of infinite cubes independently of the player movement.
To hide a cube, I set its z position @ -100 (behind the camera).
![TinyDev](https://user-images.githubusercontent.com/15329035/72682762-f4081380-3ad0-11ea-8932-442fd849eb40.png)
>I know that there must be a better solution, but this was the easiest one since it keeps the position in synch.

- Spawner Component tells how many cubes to instantiate and gets the prefab to instantiate,
- SpawnerSystem is in charge of the spawning process on the first Update frame then it auto destroys himself,
- ObstacleTag tells what entities are obstacles and store some useful params for repositioning and moving them,
- ObstacleMovementSystem moves the cubes and repositions the according to the distance from the player

Finally, I needed to check if the player was colliding with something
- CheckPlayerCollisionsSystem if the player collided then set the isDead variable of the Dead Component of the player to true 
- Dead Component tells if the player is dead -> if that's the case then triggers the DeathSystem
- DeathSystem -> do stuff when the player dies (reset the level)

So I created some paths to make the game more interesting, I used the noise function to decide whenever or not the cube on pos(x,y) had to be shown
![TinyDev2](https://user-images.githubusercontent.com/15329035/73123058-cbc65c00-3f8b-11ea-8d11-dfd45e8bd41b.png)
- SpawnerSystem -> set the position with noise and store the initial position in the ObstacleTag Component

To make the game even more interesting, I added some "Confusion": every 10 seconds something interesting will happen (the direction will be changed, or a distracting effect will be triggered)
- Confusion Component
- Confusion System -> makes the game more challenging trying to confuse the player

Then the core gameplay was completed (only fixes after this point no gameplay additions).
So, I just had to add a menu and a scoring system to give the player the possibility to compare his matches.
Since there was no text component currently implemented and the approach used in Unity's TinyRacing project sample was kinda complex to achieve, I decided to implement my own text system.
I used 5x3 cubes to show a single character/digit inside the game and set the displayed character using the systems.
This approach used quite a lot of entities, but it was the easiest to implement that had come to my mind.
- Digit
- DigitCube -> has the current cube to be shown?
- DigitCubeSystem -> hides/shows the cube using the previous trick to hide/show the cubes

Then it was easy to implement the Main Menu and a Game Over screen as finishing touch.
Then the game was completed!

Thank you for your interest!


## FIXES:
### Compilation Error
#### ArgumentException: The entity does not exist ...
Open the digit prefab (Assets/Prefabs/Digit.prefab) inside Unity (modify the Shown Value to whatever you want, e.g. 0) and save it to fix the Component referenced entities.


## TODO:
	speed increase -> not really necessary (already challenging enough)
	random colour for cubes player and background -> seems to be not possible currently (modifying the background value of the Camera component doesn't affect the background colour)

> Bugfix (I'm not good at my own game, so it's hard to discover bugs)
