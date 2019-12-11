# Game Basic Information #

## Summary ##


## Gameplay explanation ##

**In this section, explain how the game should be played. Treat this as a manual within a game. It is encouraged to explain the button mappings and the most optimal gameplay strategy.**

* Core Game Mechanics:
* Goals: 
* Controls:


# Main Roles #

* Animation and Visuals :     Ruifeng Zhang/Steven Tan
* Game Logic :                Travis Garcia
* User Interface :            Steven Tan
* Input :                     Steven Tan/Travis Garcia/Ruifeng Zhang/Ethan Chiang
* Movement/Physics :          Ethan Chiang

Your goal is to relate the work of your role and sub-role in terms of the content of the course. Please look at the role sections below for specific instructions for each role.

Below is a template for you to highlight items of your work. These provide the evidence needed for your work to be evaluated. Try to have at least 4 such descriptions. They will be assessed on the quality of the underlying system and how they are linked to course content. 

*Short Description* - Long description of your work item that includes how it is relevant to topics discussed in class. [link to evidence in your repository](https://github.com/dr-jam/ECS189L/edit/project-description/ProjectDocumentTemplate.md)

Here is an example:  
*Procedural Terrain* - The background of the game consists of procedurally-generated terrain that is produced with Perlin noise. This terrain can be modified by the game at run-time via a call to its script methods. The intent is to allow the player to modify the terrain. This system is based on the component design pattern and the procedural content generation portions of the course. [The PCG terrain generation script](https://github.com/dr-jam/CameraControlExercise/blob/513b927e87fc686fe627bf7d4ff6ff841cf34e9f/Obscura/Assets/Scripts/TerrainGenerator.cs#L6).

You should replay any **bold text** with your relevant information. Liberally use the template when necessary and appropriate.

## User Interface

Each scene within our game have similar/identical UI components to give the player a sense of familiarity throughout the game. The majority of text are used with TextMeshPro.
* Menu: The menu is very simplistic with only a “Play”, “Options” and “Exit” button focus we carry throughout our whole game is design and functionality. Using panels, buttons, and images, we create a very simple yet pleasant to look at menu. The incorporation of music ties in with the tone of our story and game, sad, yet beautiful. 
Art: (https://github.com/istevenful/Fragmented-Memories/blob/master/Fragmented%20Memories/Assets/Artwork%20and%20Game%20Assets/Title%20Screen.jpg) and Music: (https://github.com/istevenful/Fragmented-Memories/blob/master/Fragmented%20Memories/Assets/Music/Sadness%20and%20Sorrow%20-%20Naruto%20%5BPiano%20Tutorial%5D%20(Synthesia).mp3). 
Using UnityEngine.SceneManagement; we are able to go to the next scene on play. Main Menu Script here: (https://github.com/istevenful/Fragmented-Memories/blob/master/Fragmented%20Memories/Assets/Scripts/MainMenu.cs)
* UI used across all playable scenes:
	Each playable scene is able to open a pause menu through the Escape Key. Once pressed the game will freeze and three clickable buttons will be presented “Resume”, “Menu” and “Quit”. This is similar to the main menu UI and also uses UnityEngine.SceneManagement. Script Here: (https://github.com/istevenful/Fragmented-Memories/blob/master/Fragmented%20Memories/Assets/Scripts/PauseMenu.cs)
	When transitioning scenes through collision detection, a loading screen will arise, transporting the player into a new scene. We wanted to make the loading screen last a certain amount of time so the player can hear the subtle heartbeats in the background. We acquired this by using incorporating a variable that gets Updated with Time.deltatime multiplied with a random decimal amount. This amount, over time, will increase an invisible slider amount and once the slider hits 100, it will load the next scene. We did not use the games “True” load time because it was almost instant. 
Script to get to the loading screen: (https://github.com/istevenful/Fragmented-Memories/blob/master/Fragmented%20Memories/Assets/Scripts/SceneSwitch.cs)
Loading Screen Script: (https://github.com/istevenful/Fragmented-Memories/blob/master/Fragmented%20Memories/Assets/Scripts/LoadNewScene.cs)
* Health System UI (NOT INCLUDED IN CURRENT VERSION): Our current system of health was inspired with the idea of “pooling” but does not create gameobjects during runtime. There is currently 4 premade gameobjects in each scene and through the health script, the player is able to lose health “roses” which turn the rose into a dead/black rose. This system is is able to follow health systems in games like “The Legend of Zelda” where the character is able to obtain power-ups to increase his health over the course of the game. However currently the enemy AI is too strong that the character dies to fast and makes the game experience not enjoyable. 
HealthScript: (https://github.com/istevenful/Fragmented-Memories/blob/master/Fragmented%20Memories/Assets/Scripts/Health.cs)
[Contributors: Steven Tan]

 
## Movement/Physics

**Describe the basics of movement and physics in your game. Is it the standard physics model? What did you change or modify? Did you make your movement scripts that do not use the physics system?**


 
## Animation and Visuals



**Describe how your work intersects with game feel, graphic design, and world-building.**


## Input

**Describe the default input configuration.**

**Add an entry for each platform or input style your project supports.**


 
## Game Logic

**Document what game states and game data you managed and what design patterns you used to complete your task.**

# Sub-Roles

* Gameplay Testing :          Steven Tan/Travis Garcia/Ruifeng Zhang/Ethan Chiang
* Press Kit and Trailer :     Ruifeng Zhang
* Audio :                     Travis Garcia/Steven Tan
* Narrative Design :          Steven Tan/Travis Garcia
* Game Feel :                 Ethan Chian/Steven Tan/Travis Garcia

## Audio

* 
* 
* Implementation: 

## Gameplay Testing

**Add a link to the full results of your gameplay tests.**

**Summarize the key findings from your gameplay tests.**

## Narrative Design

**Document how the narrative is present in the game via assets, gameplay systems, and gameplay.** 


## Press Kit and Trailer
* Press Kit materials:
* Link to trailer: [Fragmented Memories: A Journey Through Light and Dark](https://www.youtube.com/watch?v=dIQyp2skE5w&feature=youtu.be)
* 

## Game Feel

**Document what you added to and how you tweaked your game to improve its game feel.**

