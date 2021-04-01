# PashmakCore
![Unity 2019.4.7+](https://img.shields.io/badge/unity-2019.4.7%2B-blue.svg)](https://unity3d.com/get-unity/download)
![License: MIT](https://img.shields.io/badge/License-MIT-brightgreen.svg)](https://github.com/mohammadroohian/PashmakCore/blob/master/LICENSE)

A number of useful scripts for every things in unity!
Work with sprites, user interface, physics and more!
____________
Each script written to perform a specific action.
We call them component unit (CU).
Some parts are included in this pack:
  * Application
  * Audio
  * Camera
  * Conditions
  * GameObject
  * Input
  * Rigidbody & Rigidbody2D
  * SceneManager
  * Screen
  * ScreenCapture
  * Sprite
  * StoreData
  * Time
  * Timer
  * Transform
  * UI
  * MonoEvents
  * Color fader - sprite renderer - renderer - UI
  
  and so on.

This code snippet can be used for 2D and 3D projects.
Each component has a simple task that can be combined with other components to perform more complex tasks.

### Why should you use PashmakCore?

Use these pieces of code instead of writing code for some small tasks. Use them to build prototypes and speed things up.
Put them together and do more complex tasks without the need for coding.

## Requirements
* Unity 2019.4.7 or later versions.
* [NaughtyAttributes](https://github.com/dbrizov/NaughtyAttributes)
* TextMesh Pro 2.0.1
* Post Processing 2.3.0

## Installation
* First install `TextMesh Pro` and `Post Processing` packages in Unity through Package Manager.`MenuItem - Window - Package Manager`
* Add `TextMesh Pro` sample scenes.

### Perform one of the following methods:
#### zip file (The simple way)
1. Download a `source code` zip from [releases](https://github.com/mohammadroohian/PashmakCore/releases).
2. Extract it.
3. Copy the items in the `Assets` folder into the `Assets` folder of your project. (Click replace files if necessary)

#### unitypackage file
1. Install [NaughtyAttributes](https://github.com/dbrizov/NaughtyAttributes#installation).
2. Download `.unitypackage` file from [releases](https://github.com/mohammadroohian/PashmakCore/releases).
3. Import it into your project.

## Overview
To learn more about how these components work, check out the sample scenes.
The following are a number of practical cases.

### CU_ColorFader

![image](https://user-images.githubusercontent.com/80090999/112787349-8e68cd00-906d-11eb-8a94-6077ff5edf13.png)

2D sprites and 3D objects:

![CU_ColorFader](https://user-images.githubusercontent.com/80090999/112787603-1f3fa880-906e-11eb-8301-cfa3e79fd579.gif)

Unity UI system:

![CU_ColorFader_2](https://user-images.githubusercontent.com/80090999/112787894-db00d800-906e-11eb-8a32-fd14e8ae43d7.gif)

### CU_MonoEvent_Collider

![image](https://user-images.githubusercontent.com/80090999/112798795-ab5bcb00-9082-11eb-836e-2300284e4178.png)

The `CU_MonoEvent_Collider` component is used to detect all types of collisions and triggers in 2D and 3D.

![MonoEvent_Collider](https://user-images.githubusercontent.com/80090999/112799285-66846400-9083-11eb-9fae-aa22b716cfcc.gif)

### CU_MonoEvent_Mouse

![image](https://user-images.githubusercontent.com/80090999/112799912-35f0fa00-9084-11eb-8131-02e9605eac08.png)

The `CU_MonoEvent_Mouse` component is used to detect mouse events. 
Enter - Exit - Down - Up - Over - Drag

![MonoEvent_Mouse](https://user-images.githubusercontent.com/80090999/112799979-4dc87e00-9084-11eb-930e-52f59180bead.gif)

### Snapable objects

![image](https://user-images.githubusercontent.com/80090999/112800692-3a69e280-9085-11eb-887c-977251a05d74.png)

The `CU_Transform_SnapObj` component and `CU_Transform_Dragable` are used to snap one object to a specific point.

![CU_Transform_SnapObj2](https://user-images.githubusercontent.com/80090999/112801875-aac53380-9086-11eb-941d-b69b61675e04.gif)

### CU_GameObject_Instantiate

![image](https://user-images.githubusercontent.com/80090999/112802162-1909f600-9087-11eb-99d6-199a1a1f3156.png)

The `CU_GameObject_Instantiate` component is used to spawn game objects.

![CU_GameObject_Instantiate ](https://user-images.githubusercontent.com/80090999/112802558-8e75c680-9087-11eb-979a-539d6308f361.gif)

### CU_GameObject_Destroy

![image](https://user-images.githubusercontent.com/80090999/112802753-ced54480-9087-11eb-812d-00c256be8523.png)

Ther `CU_GameObject_Destroy` component is used to delete game objects or delete the children of a specific  game object  from the scene.

![CU_GameObject_Destroy](https://user-images.githubusercontent.com/80090999/112803011-1b208480-9088-11eb-9ade-88fa2ef126e2.gif)

### CU_Input_GetKey

![image](https://user-images.githubusercontent.com/80090999/112806128-b49d6580-908b-11eb-9c05-e7c8646ae641.png)

The `CU_Input_GetKey component` is used to detect input keys.

![CU_Input_GetKey](https://user-images.githubusercontent.com/80090999/112806299-df87b980-908b-11eb-8f2f-116d2efe9894.gif)

### CU_Input_GetMouseButton

![image](https://user-images.githubusercontent.com/80090999/112806678-4ad18b80-908c-11eb-9da7-f1fc27813b92.png)

The `CU_Input_GetMouseButton` component is used to detect mouse inputs.

![CU_Input_GetMouseButton](https://user-images.githubusercontent.com/80090999/112806695-50c76c80-908c-11eb-894c-fbe92535f9c2.gif)

### CU_Transform_Sync

![image](https://user-images.githubusercontent.com/80090999/112806946-9421db00-908c-11eb-8004-d49b70753f7f.png)

The `CU_Transform_Sync component` is used to synchronize the transform properties (position, rotation, scale) of an object game.

![CU_Transform_Sync](https://user-images.githubusercontent.com/80090999/112807139-d0edd200-908c-11eb-99f8-74165f7a81e7.gif)

### CU_Transform_LerpPositions

![image](https://user-images.githubusercontent.com/80090999/112807353-0abed880-908d-11eb-9cca-6336465314fa.png)

The `CU_Transform_LerpPositions` component is used to move an agent between multiple points. This component also has a loop capability.

![CU_Transform_LerpPositions](https://user-images.githubusercontent.com/80090999/112807618-5a050900-908d-11eb-82ea-cdd32f7781b6.gif)

### CU_Transform_Dragable

![image](https://user-images.githubusercontent.com/80090999/112807819-90db1f00-908d-11eb-8f0e-a242922fd83c.png)

The `CU_Transform_Dragable` component is used to enable the ability to drag objects.

![CU_Transform_Dragable](https://user-images.githubusercontent.com/80090999/112807988-c253ea80-908d-11eb-9a79-cffe41a0485c.gif)

### CU_Transform_Position_SyncToMouse

![image](https://user-images.githubusercontent.com/80090999/112808100-e283a980-908d-11eb-870b-27e2565f8f78.png)

The `CU_Transform_Position_SyncToMouse` is used to synchronize the position of an object game with mouse.

![CU_Transform_Position_SyncToMouse](https://user-images.githubusercontent.com/80090999/112808320-1bbc1980-908e-11eb-96f3-113f592f4d17.gif)

### CU_Transform_Translate

![image](https://user-images.githubusercontent.com/80090999/112808569-5b830100-908e-11eb-9e89-4fb8ffa981b7.png)

The `CU_Transform_Translate` component is used to move an object game on the screen.

![CU_Transform_Translate](https://user-images.githubusercontent.com/80090999/112808778-96853480-908e-11eb-9718-df53643db8f0.gif)

### CU_Transform_LerpRotation

![image](https://user-images.githubusercontent.com/80090999/112808943-c16f8880-908e-11eb-8f62-59cfeb55be86.png)

The `CU_Transform_LerpRotation` component is used to change the angle of a game object from one angle to another smoothly.

![CU_Transform_LerpRotation](https://user-images.githubusercontent.com/80090999/112809176-fbd92580-908e-11eb-9e2f-e9eaa59e2a74.gif)

### CU_Transform_LookAt

![image](https://user-images.githubusercontent.com/80090999/112809720-89b51080-908f-11eb-957f-9d2d08619e8b.png)

The `CU_Transform_LookAt` component is used to look from one object to another.

![CU_Transform_LookAt](https://user-images.githubusercontent.com/80090999/112809757-98032c80-908f-11eb-8599-f12e2005a0e3.gif)

