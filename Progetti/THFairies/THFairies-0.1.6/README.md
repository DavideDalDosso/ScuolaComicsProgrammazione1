# THFairies

These instructions are made in case someone else is going to work or mod the game. I may have skipped some functionalities or did not explain some functionality so if needed, ask me to.
It is quite ugly but I am writing these as I make mistakes and fixing them.

# Development Guidelines

All monobehaviours that reference NetworkManager require an async method to wait for the singleton class creation before usage.

There are no strict rules regarding the placement of the game assets or scripts although it is preferred to place them in a proper spot.
This doesn't apply to turretSO where they're retrieved runtime via Resources class, to make sure your turret exists in the game, it must exist within the 'resources' folder.

All the classes that end with 'System' are expected to run separately and have only one instance of them running in a scene. Each of these should have a static method named 'Singleton' to allow access globally.

BEWARE! If you use a static variable, make sure to properly reset the value in OnApplicationQuit or else you'll meet odd behaviour when testing in Unity's editor.

Don't put NetworkManager in a scene. Put instead NetworkManagerBootstrapper so that it loads the scene containing the one and single NetworkManager in the build. If you change scene that contains another NetworkManagerBootstrapper, it will not create another NetworkManager since you already did it once before.

Also if you need to use LoadScene to change into a scene containing network objects, use the one provided by NetworkManager in NetworkManager.Singleton.SceneManager, it automatically spawns all network objects upon scene loading.

BEWARE! If you are in a scene containing network objects, testing the scene with 'Enter Play Mode Options' disabled, (Project Settings > Editor) will break things up. It's either starting from a scene without network objects or reloading the .NET framework and directly jump into the desired scene.

UI scripts should not be referenced by any common script or any of their implementations.
UIs purpose is to be an interface of the user of the actual game content without affecting their functionality. That includes being created and accessed from a object that is present in the game.

You should prefer instead to create a utility object whole whose purpose is only to create the UI instance when available without getting other objects to run any code. This way you have a code with less 'spaghetti' references by keeping the logic layer away from the presentation layer.

# Main Menu, LobbySystem and RelaySystem

Upon clicking Singleplayer, it gets created a local server with you as the host, this creates a server that occupies a port so if (for some reason) you create two instances of the game and run singleplayer in both of them, the last one will bug out.

Relay and Lobby are services provided by Unity to make development easy and with a free tier for indie developers usage. It has a pricing policy, after exceeding the monthly requirements of the free tier, you would need to pay per use of the services.
It is negligible for a project like this but if you wish to scale things up, you need to invest and monetize a little.

To allow you to connect to people, you would only need Relay to create connections, lobby is used to wait up people before loading the game scene.
Upon loading a level there's still a button to actually start the game, it's up to people preference to wait the other users within lobby or ingame.
You may choose to allow for new people to join after you started the game.<sup>[1](#connectionAfterStart)</sup>

The advantage of Relay is creating connections without users having to touch the firewall. It uses a server provided by Unity's services to address and forming connection without need for the host to expose a port.
The Relay server doesn't host the server instance itself, instead, it only sends client and host packets back and forth eachother to allow comunication.
Generally, having a proxy server between you and the server does give more latency than exposing a port and directly hosting yourself, therefore if you prefer to have less latency, you need to open a port and set a lobby using said port. <sup>[2](#directConnectionNote)</sup>


<a name="directConnectionNote">1</a>: Not implemented blocking connections after started

<a name="directConnectionNote">2</a>: Not implemented direct connection and hosting

# Match

This singleton class manages the flow of a single session of a game. It holds some global variables like Lives.

In a scene, you provide the instance a WaveListSO containing some WaveSO to show it what kinds of enemies are expected to spawn.
Upon starting a game, it will provide every involved EnemySpawners the list of current enemies to spawn.
Waves may be ended early so the Match can order the EnemySpawners to start spawning another Wave on top the previous with no problems.

In each WaveSO you define the enemyPrefab, the amount to spawn, the time between each spawn and the time to wait before spawning the first enemy.

# TowerPlacement System, TowerManagement System

These two systems work together to accomplish one goal that is managing your turrets, therefore these systems have to know about each other existance to work properly.

When selecting a turret for placement, it will unselect any tower you were inspecting and prevent you from selecting a turret until you disable placement mode.

Tower Placement System when in placement mode gives some feedback on the client before placement before actually making the call on the server via RPC.
Then the Placement System Server counterpart checks if it's permissable or not.

Tower Management System only has client-sided functionalities but can invoke some RPC in BaseTurret like Upgrade and Sell for the server to perform.


# Turrets
Turrets have their own implementation extending the abstract class TurretBase, unlike Enemies, all the towers may behave really differently functionally wise and aestetically wise therefore it is assumed that someone would want to create a Turret from a script from scratch.
The reason you extend the class is for allowing other systems such as the SelectionSystem to access and modify general turret information like applied upgrades.

When writing your implementation of a custom turret, you only need to ensure only the server runs the logic by first putting "if (IsServer) return;" and then you can do wherever you want in FixedUpdate or Update as always.

The game calls the base class Upgrade(int upgradeIndex) to change the bool[] upgrades to true at the passed index and automatically calls the overrideable method  OnUpgrade(int upgradeIndex).
It is allowed to you to read the upgrades variable array in your script although it's suggested to update the variables involving an upgrade in OnUpgrade(int upgradeIndex) instead of checking every frame if you have an upgrade.

The other network behaviour is Turret, this one only handles one static list of turrets to be accessed globally. It ensures that it gets updated everytime an turret gets spawned and despawned. You shouldn't add this networkbehaviour to an object manually since TurretBase does that automatically for you.

# TargettingSystem

There will be provided some utility class like TargettingSystem that has some static methods to allow to quickly retrieve the enemies in the required targetting mode without having to implement such logic inside a turret.
The advantage is that for it sorts through all the enemies and stores in its static variable an ordered array of enemy based on distance to travel to reach, a possibly heavy algorithm that is calculated before all the turret scripts.

First and Last are calculated beforehand.
Closest and Farthest are calculated on call.
Random is very lightweight but you would like to refrain from changing target until you shoot atleast one shot or the target is killed.

You may have turrets already spawned in a scene, just choose in the inspector what upgrades they have however the graphics do not update in editor until you run the scene.

# Enemies
Enemies are customizable like turrets although their movement logic is already implemented.
The EnemyBase class is extendable gives you some extra virtual methods to listen if you need to like the enemy receiving damage or about to die.

The other network behaviour is Enemy, this one only handles one static list of enemies to be accessed globally, just like the turret counterpart. It gets updated everytime an enemy gets spawned and despawned and you shouldn't add this one manually as well.

Enemies move following a EnemyPath and traversing it EnemyPathNode one by one with a random offset defined inside the EnemySpawner that spawned them. In levels with bigger paths, you may change this value in-scene in the EnemySpawner.

# EnemyPath, EnemyPathNode and EnemyPathManager

Upon starting a scene, Each enemy path create an array of EnemyPathNodes from all the gameobject's transforms parented inside EnemyPath's gameobject transform. They also tell the other EnemyPaths following one that they are the EnemyPath that come before them so you only need to define in the inspector the path that follows one EnemyPath object.
This also would allow for reverse traversal of the paths.

The enemy paths also need to calculate on start the distance between each node and paths for functionality however since on creation they don't know yet what path comes before them. EnemyPathManager does the calculation for them after they ran their initialization code.

To determine an enemy distance to the end, we take the enemy targetted path node pre-calculated distance and add the distance between the enemy and said node.

You can access all instances of EnemyPaths via static member 'enemyPaths'.

# Summed up level making guideline

First off, create a scene, create all the graphic stuff like tilemaps and some props as you would.
To make it accessible from LevelSelectionUI, you must create a LevelSO in 'resources' folder (please, put it in a separate sub-folder of 'levels' as well to maintain order) and when you add the scene to the game build, put in the LevelSO the id of the scene in the build.

Put all the following scripts in one or some empty gameobject:
- Match
- EnemyPathManager
- GameUIBootstrapper (Not really needed but important nonetheless)
- TargettingSystem
- TowerPlacementSystem
- TowerManagementSystem

It might be hard to really remember all the stuff in the previous instruction, I will have a prefab that has all of those necessary stuff packed up so you only need to drag in one thing instead of all of these individually.

Then you create your EnemyPath objects and put some several empty gameobjects with just a gizmo icon. The order in the hierarcy dictates the order the nodes will follow, if you're unsure if you screwed up the order, just run the game and click the EnemyPathManager in the hierarcy to show their gizmo.

You then create a WaveListSO, fill it with WaveSOs... maybe create some EnemySO... Just put them nicely ordered in the according folders in 'resources'.
