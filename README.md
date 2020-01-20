# My Tiny Tiny Project
This project is a fork of the [Tiny3D project sample](https://github.com/Unity-Technologies/ProjectTinySamples/tree/master/Tiny3D) released by Unity.
Since it's a preview is [kinda limited](https://docs.google.com/document/d/1A8hen2hLFY5FLkC5gd3JP2Z-IpHfnAX-CpYLK3aOdwA/edit#) but I thought it was fun to try to implement a small game using thig new preview package (I think the Data Oriented Technology Stack, [DOTS](https://unity.com/dots) in short, is really cool)

## Development process
First I needed some sort of control over the player, so I created the input system.
It's barebone (only left and right input) but works well enough (could definitely be improved).
- Player Component to mark an Entity as the player,
- PlayerInput as Component to store the input of the player,
- InputShow to retrieve the input and PlayerSystem to manage the input -> moves the player
 
Then, since I wanted the player to be able to move infinitely along the x axis I needed the camera to follow the player.
- FollowPlayerSystem get the player pos and the camera pos and lerp between them

Finally I wanted to create a path to follow for the player so I created a grid of instances of a prefab, and with only 30(x) * 2 * 30(y) * 2 (=3600) instances I was able to cover the entire screen (the rendering is fixed to 1920*1080 no matter what) with a bit of margin, so I was able to create the illusion of infinite cubes indipendently of the player movement moving the cubes once they become too much far from the player.
To hide a cube I simply set its z position @ -100 (behind the camera).
![TinyDev](https://user-images.githubusercontent.com/15329035/72682762-f4081380-3ad0-11ea-8932-442fd849eb40.png)
>I know that there must be a better solution but this was the easiest one since it keeps the position in synch.

- Spawner Component tells how many cubes to instantiate and gets the prefab to instantiate,
- SpawnerSystem is in charge of the spawning process on the first Update frame then it autodestroys himself,
- ObstacleTag tells what entities are obstacles and store some useful params for repositioning and moving them,
- ObstacleMovementSystem moves the cubes and reposition the according to the distance from the player

Finally I needed to check if the player is colliding with something
- CheckPlayerCollisionsSystem if the player collided then set the isDead variable of the Dead Component of the player to true 
- Dead Component tells if the player is dead -> if that's the case then triggers the DeathSystem
- DeathSystem -> do stuff when the player dies (reset the level)

So i created some paths to make the game more interesting, I used the noise function to decide whenever or not the cube on pos(x,y) has to be shown
- SpawnerSystem -> set the position with noise and store the initial position in the ObstacleTag Component

At this point the game was starting to get interesting, however after a few tests it was getting boring. So I decided to introduce some "confusion" to the game mechanics.
I added a countdown that every fixed amount of time decides to change the gameplay (reversing the moving direction or rotating the map).
- Confusion Component -> stores the Confusion System status
- Confusion System -> responsable for changing the gameplay, slow down the movement (modifying the ObstacleTag Component) then randomly decides between rotating the map, fake rotating it or swapping the movement direction.


## TODO:
	main menu
	speed increase
	score system (ui? -> is kinda distracting during the playmode)
	random color for cubes player and background

> Bugfix (I'm not good at my own game so it's hardto discover bugs)
