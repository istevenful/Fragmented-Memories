# Game Basic Information #

## Summary ##


## Gameplay explanation ##

**In this section, explain how the game should be played. Treat this as a manual within a game. It is encouraged to explain the button mappings and the most optimal gameplay strategy.**

* Core Game Mechanics: Platforming, reading story text, attacking enemies
* Goals: To reach the end and figure out how you got to this strange place
* Controls: Based controls used for movement, alt key used for attack


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

We impletmented a classic platformer movement/physics as the base for out game. On top of that we impletementd ADSR for horizontial movements. Jumping is where we spend the most focus on, since it ties into combat and platformer the most. We designed 3 types of jumping: normal jump, air jump and forgiving jump. Normal jump is when a player is grounded and jumps. Air jump is the ability to jump in air once per takeoff (jumpForce is smaller then normal jump). Forgiving jump is when for when users pressed jump just few frames before the charater grounded and this logic will rememeber that user did press jump and jump for them when character grounded, ie the forgivness. (Note: Air jump and forgiving jump are commented out in this version because there are odd chances it creates a super jump and shot player out into the sky. To active it please uncomment the code in `PlayerMovement.cs`.)


 
## Animation and Visuals
Map visuals: Each scene in game is a parallex map, which is a map composed of different images with different Z coordinates. When the camera moves, the player will be able to see some degree of depth illusion. There are also particle effects and lighting throughout the maps to make the map alived and realistic.
Animation:


**Describe how your work intersects with game feel, graphic design, and world-building.**


## Input

Defualt: arrowkeys/A or D to move, space to jump and leftAlt to attack. All inputs are taken from the keybroad. 


 
## Game Logic

**Document what game states and game data you managed and what design patterns you used to complete your task.**
This game was broken down into 4 scenes, each with their individual story that built towards the overall story. Scenes were loaded using a scene loading script: https://github.com/istevenful/Fragmented-Memories/blob/25df54de461d46f4cc5bc36c5a9c3ca9c02af4c9/Fragmented%20Memories/Assets/Scripts/LoadNewScene.cs#L1
Data wasn't carried over in between scenes and each scene was carefully crafted by the animation and visuals team members.
ADSR was used for the movements https://github.com/istevenful/Fragmented-Memories/blob/25df54de461d46f4cc5bc36c5a9c3ca9c02af4c9/Fragmented%20Memories/Assets/Scripts/PlayerMovement.cs#L72
Player location was tracked using the Vector2.Distance function to determine how enemies, music, and text should respond.
https://github.com/istevenful/Fragmented-Memories/blob/25df54de461d46f4cc5bc36c5a9c3ca9c02af4c9/Fragmented%20Memories/Assets/Scripts/StoryText/TextTrigger.cs#L55

# Sub-Roles

* Gameplay Testing :          Steven Tan/Travis Garcia/Ruifeng Zhang/Ethan Chiang
* Press Kit and Trailer :     Ruifeng Zhang
* Audio :                     Travis Garcia/Steven Tan
* Narrative Design :          Steven Tan/Travis Garcia
* Game Feel :                 Ethan Chiang/Steven Tan/Travis Garcia

## Audio

Audio was setup in a zone system. When inside the zone, audio would fade in or out using lerp. The zones were placed in a way to enhance player experience for when other sounds would need to be heard such as leaves crunching or when silence was used to build tension. https://github.com/istevenful/Fragmented-Memories/blob/25df54de461d46f4cc5bc36c5a9c3ca9c02af4c9/Fragmented%20Memories/Assets/Scripts/StoryText/AudioTrigger.cs#L37

* Audio was obtained from the following sources:
https://www.salamisound.com/info1
https://freesound.org/people/RawBeef/downloaded_sounds/
and a music pack was provided by Steven who had purchased it from the assets store a few years ago

We went for a more serious game where each level had a slightly different tone that built towards the overall narrative and sense of progression in the game. The menu uses sad music to set the initial tone. When the game starts, upbeat music encourages exploration and wonder, instilling a similar sense of intrigue in the player as the character expresses. When the music stops, things should feel tense. Leaves crunching in silence in the next level further builds this tension before the first important encounter with a mysterious shadow. This encounter is punctuated with more frantic music so that the player begins to feel a similar sense of discomfort. The next zone uses ominous hymns to slow down the pace of the game while still building tension. The last zone uses slightly creepy music to reveal the true theme of the narrative and punctuate the story text as the player uncovers what’s really going on.

## Gameplay Testing

https://drive.google.com/open?id=1JJS6BolwAGsbFIvSvnAEqOuuN1g_6zDiFRZ7rwxnHdk

This role was splited by all the team memebers since we are down one person. Each team memeber had tested the game intensivly in all phases of development, here are the key foundings:

* Feedbacks on different type of movements tested during development: Eventhough the player is a human, however, the game is set in a deam land, therefore, a more light, fast-reacting and fluid movement is best fitted for the character. 
* Feadbacks on combat: First, we found that hit-boxes are a good method to adjust the difficulting of combat. Cooldowns and attack and gaps between when the character can takes another damage is also something to think about when setting the numbers. 

## Narrative Design

As a narrative driven game, it was important that the narrative system felt interesting and that the narrative itself was intriguing. This meant creating custom scripts to give the words themselves juice and to take in feedback about where the narrative was unclear and how the gameplay elements did or didn’t add to the overall narrative experience. The narrative was revised multiple times based on the feedback of others.https://github.com/istevenful/Fragmented-Memories/blob/25df54de461d46f4cc5bc36c5a9c3ca9c02af4c9/Fragmented%20Memories/Assets/Scripts/StoryText/TextTrigger.cs#L55

Each level was meant to have it’s own tone and story, while still building towards the overall theme and narrative. Large words were used at the beginning of new levels to try and create tone and build the story and the characters changing responses to the world around him created a sense of progression within each level. Colors were used to highlight important words and the duality between certain elements. Text would get larger when the words being said were important to the story. Text would shake to give the player a sense of uncertainty.

## Press Kit and Trailer
* Press Kit materials: Raw gameplay footage
* Link to trailer: [Fragmented Memories: A Journey Through Light and Dark](https://www.youtube.com/watch?v=dIQyp2skE5w&feature=youtu.be)
* Software Used: Adobe After Effects 2020

## Game Feel

* Movement: tested different ADSR curves to find the best movement type for the main character.
* Hit-box: adjust the size of hit-boxes, size of hit-box is directly realating to the difficultiy of combat.
* Jumping: jumpforce has tested with a few different numbers, the ability of air jump is to make players has bette rcomtrol over the character and make moving arround platforms easier. Forgiving jump is to aviod the situation that player might think the game is "broken" when the jump has not executed when pressed too eariler. (Note: this idea came from the GDC talk by developers from Celeste.)
* Combat: since the combat system is farily simply, the way we adjust the difficulty to by changing the size of hit-boxes and cooldown on attacks and damage taken.
* Narrative: Used colors, movements, and fading effects to give the dialogue more juice. https://github.com/istevenful/Fragmented-Memories/blob/25df54de461d46f4cc5bc36c5a9c3ca9c02af4c9/Fragmented%20Memories/Assets/Scripts/StoryText/TextTrigger.cs#L55
* Vignette: Added a subtle vignette to improve aesthetic. https://github.com/istevenful/Fragmented-Memories/blob/25df54de461d46f4cc5bc36c5a9c3ca9c02af4c9/Fragmented%20Memories/Assets/Scripts/StoryText/CustomVignette.cs#L17
