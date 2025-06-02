# The Office
A unity game developed as a part of Game design course at SBU. The player is stuck inside a maze and needs to open doors and solve puzzles for exiting this maze. There are currently 2 versions of this game available.
1. The first version which was developed with restrictions of the original GDD in mind. [link](https://sina-tb.itch.io/the-office)
2. The second version which is currently in development and changes many of the original features.

## Puzzles
In the first version, 8 Puzzles were implemented. To open some of the doors the player needs to solve puzzles. 3 riddles, 2 logical puzzles, and 2 Pattern recognition puzzles. These puzzles offer little flexibility and the riddle solving puzzles can be a bit tedious.

![puzzle1](./screenshots/puzzle1.png)

![puzzle2](./screenshots/puzzle2.png)

In the second version, the riddle puzzles are being removed and new puzzles inspired by the lockpick puzzle in elder scrolls and fallout have been implemented. Also a hacking puzzle inspired by fallout is being developed.

![puzzle3](./screenshots/Lockpick.png)

## Implementation Details and features

### Optimization
There is no optimization in the first version (just run it and you will understand 😁). In the second version there has been extensive use of pooling and culling related to players position.

![culling](./screenshots/culling.gif)

## PCG (Only in the second version)
The Procedural Generation Part of the game Consists of 2 main parts. 
1. Placing the rooms
2. creating the corridors

At first the rooms are placed on the grid. Each room has specific items inside and a specific size. after the rooms are placed, Corridors are carved out with recursive backtracking algorithm.

![RoomData](./screenshots/roomd.png)

The Base Room Scriptable object encapsulates the rooms size, objects availabe inside the room and lighting data. For easier interaction with the lights inside the room a custom editor has been created for the lights.

## Interaction and Crafting

Both versions of the game have an interaction system and a crafting system. If a player comes in the range of an interactable object and looks to the object, by pressing E and interaction happens. The crafting system only works when the player is in front of a crafting bench. At the current version the player can only craft a flashlight by having all of its components.

![CraftingBench](./screenshots/crafting%20table.png)

## GamePlay of Version1

[youtube](https://youtu.be/mAjx6QgmCHc)


## Further Work
* Improving the styling of UI
* Adding support for timed puzzles
* Adding music and sound to all parts of the game
* Adding more story bits for the story puzzle
* Adding more items and craftables

