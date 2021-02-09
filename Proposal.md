# A Proposal for a Mobile Game Titled Paper in the Wind

Team Merge Conflict

Greg Cantrall, Ryan Forsten, Joey Lemon, Brandon Mingledorff


## I. Introduction

Our proposed project is a video game with the main marketed platform being mobile phones running iOS and Android operating systems. The game may also be available on web browsers and desktop computers. The general premise of the game involves the player as a piece of paper, flying through different environments and avoiding obstacles along the way. The goal of the game is to make it as far as possible in the level without dying—the player will earn points for every meter travelled.

The player is infinitely moving in one direction, with the camera placed behind the paper. The player has no option to slow down their movement or turn around. The environment in front of the player will include static and moving objects that they must avoid. Some environments the team has in mind are listed below:
- An office setting with desks, moving chairs, vending machines, opening and closing doors, and other miscellaneous items to avoid.
- A small portion of the University of Tennessee campus, such as the student union, the Min Kao building, or the pedestrian walkway. Obstacles to avoid could include tables, chairs, walking students, etc.

The player has a limited control set where they are only able to move in predefined positions, forcing them to continuously make last-second decisions on the best route to take. By making a wrong move, the player will risk hitting an obstacle and ending their run. When a player dies, their score will be posted on the global leaderboards where all players compete to boast the highest score. Players will have the option to restart immediately, and they will be able to see two visual markers in the level: one placed at their previous score and one placed at their personal best score. These aspects will provide players with an urge to continuously play the game.

The player will also be provided with upgradeability in the form of purchasable designs to provide a new look to the paper, new paper types to provide unique abilities, and unlockable levels to provide different landscapes to beat. All these aspects will keep the player engaged, and they could potentially drive up revenue if the game is later decided to be monetized.

This proposed project is not a novel idea. There are several games with a similar nature, aptly titled “endless runner” games where the player is constantly moving in a predefined direction and avoiding anything that could end their run. A couple of endless runner games include Temple Run [1] and Subway Surfers [2]. Both games have reached the top of the iOS application store at some point, providing proof to the value of these types of games.

While our game shares the base aspects with these games, it will differ in several crucial ways, outlined below:
- No other endless runner games involve our proposed playstyle as a piece of paper flying through the air.
- The proposed game design and levels are unique.
- The levels will mimic real-life environments, whereas the other games are typically in fictional or fantasy environments.
- The player’s movement is much less restricted than the other games listed, where the player can typically only jump or move left and right.

The main motivation for this product is the fact that video games are among the most engaging ways to use one’s free time. In 2020, Statista estimated that around 2.7 billion people played video games actively [3]. Even further, 2.5 billion of these gamers played on their mobile devices (potentially in addition to other platforms). Therefore, developing our game with the main intent to be played on mobile devices will provide us with an extremely large market. Additionally, our team is a group of avid gamers and very much enjoy the pleasure found by playing video games. We were around for the explosion of mobile games and therefore see the value in developing our own—we will enjoy the process of creating a game as much as we enjoy playing a game.

## II. Customer Value

The primary customers of our project are mobile gamers. These customers commonly want only a few simple things [4]:
- A free and stimulating way to pass time.
-	A sense of accomplishment.
-	The ability to replay a game without getting sick of it.
-	The ability to start a game that does not take too long.

Based on this list, we can observe how our project will deliver on each of the customer’s needs:
-	Our game will be free and accessible from both iOS and Android application stores. The player will be stimulated by the active, on-the-fly decisions required to make it far into the level.
-	Players will have a sense of accomplishment as they continuously beat their previous scores, reach higher into the global leaderboard, and upgrade their paper and unlock new environments with their earned points.
-	There will hopefully be strong replayability with the upgrade options and the ever-changing levels that players explore and unlock.
-	The game will last as long or as short as the player wants—there is no expectation for players to play for a specified amount of time.

Customers will benefit from a mobile game that hits all the marks of what they are looking for in an application to pass their free time. The game expands the endless runner market with a fresh idea on the environments, characters, and maneuverability.

To generate analytics on the game’s success, we can investigate a few statistics on the players. Namely, the average duration of a play session, the amount of interaction with the in-game upgrades, and the amount of times a customer has returned to the game. These statistics will give us a direct insight into how the game provides a solution to each of the customers’ needs. The average duration of a play session can tell us how stimulating the game is and how a customer can start a game that captures their attention without taking too long. The amount of interaction with in-game upgrades shows us that players are gaining a sense of accomplishment and progression. Finally, the amount of returning customers provides insight into the replayability of the game.

## III. Technology

Our project will provide a usable application that can be exported to almost all major gaming platforms with a single shared codebase. It will provide a simple interface with only a few inputs that drive the gameplay—the player can choose to move right, left, up, or down. On mobile devices, this will translate into directional swipes. The game levels will be seemingly infinite by constantly generating new sections in front of the player and destroying old sections behind them. The game will provide a menu for selecting levels, upgrading the paper, and changing settings. The menu will have a parent-child structure, where each page may lead to other pages in a tree-like manner. Game levels will be created by using a variety of prefabricated models to design an environment filled with meshes and textures.

<p align="center"><img src="https://i.imgur.com/vhw2H0v.png" /></p>

The main entry point to all functionality in the game is the GameManager singleton class, which provides static references to sub-managers as well as general game management functions. The current layout has three sub-managers that are static singletons: PlayerManager, LevelManager, and MenuManager. The PlayerManager provides functions to manipulate the player, such as detecting collisions and reacting to player inputs. The LevelManager provides functions to select a level as well as generate the current level as the player moves forward. Finally, the MenuManager provides functions to move through the menu system and enter sub-menus such as game settings or the upgrade store.

The minimum viable product for our system would be a mobile game that moves the player’s piece of paper through an infinite environment and responds to player swipes to avoid objects. If the player hits an object, the level will reset, and the player may try again.

Enhancements upon the minimal system that customers would value are the previously mentioned functionalities: level selection with multiple levels, menu system to manage the experience, ability to upgrade the player’s paper, and score-keeping functionality, including the leaderboards. As the product reaches a point where all functionality described in this proposal is achieved, other possible enhancements could include:
-	Many more levels and player upgrades that are released throughout the lifetime of the product to keep customers engaged.
-	Multiplayer duels where players can directly compete in real-time.
-	Upgrades and levels that are only available to paying customers, therefore adding to their rarity.

Our product will be built using the Unity game engine, with the game logic in C#. Unity provides a vast number of tools to leverage, including simplified level design, animations, common libraries, an asset store, and much more. It also provides a testing library, which we will utilize to create unit and integration tests for our system.

The unit tests will be written for each major component shown above. They will test the individual functions within for accuracy and correctness, such as correctly transforming a vector to a new vector. The integration tests will analyze combinations of the components in the context of the end user, ensuring the game itself is running as it should. For example, one integration test could ensure the collision detection system which leads to the player dying is working correctly.

## IV. Team

Our team is generally new to both Unity and C#. Only one team member has built a similar game with the same technology, while the rest of the team will be learning a completely new workflow. We have chosen to define set roles for each member of the team, which we will maintain throughout the development process. We all intend to contribute to most aspects of the project; however, below are the more granular roles that should divide the project scope into manageable components:
-	Joey Lemon: project manager
-	Ryan Forsten: level design
-	Brandon Mingledorff: game logic
-	Greg Cantrall: game logic

## V. Project Management

The product defined in this document is feasible to be completed in a semester-long schedule. Biweekly team meetings will occur on Discord to keep everyone on track and to reevaluate our scope as time goes on. Below is a tentative schedule that should allow us to end with a finished product:

|Date|Activity|Sprint Goals|
|----|--------|------------|
|Thursday, Feb. 18| Begin work on product: commit skeleton structure to repository to expand upon.| Begin first level, finish basic graphics, finish movement system
|Friday, Feb. 26| Begin second sprint| Expand first level, begin point system, add obstacles and animations|
|Thursday, Mar. 4| Submit iteration 1 status report| ...|
|Friday, Mar. 12| Begin third sprint| Begin second level, begin menu system, begin upgrade system|
|Thursday, Mar. 18| Submit iteration 2 status report| ...|
|Friday, Mar. 26| Begin fourth sprint| Finish all core functionality|
|Thursday, Apr. 1| Submit iteration 3 status report| ...|
|Friday, Apr. 9| Begin final sprint| Test product, fix bugs, tweak to our liking|
|Thursday, Apr. 15| Submit project report and present final product| ...|

The only constraint to publishing our finished product is the hefty review process by Apple when applying to the App Store. Other than that, there are no other regulatory or legal constraints, nor are there any immediately obvious ethical or social concerns. We will have access to everything required to start and finish the project defined in this document. An additional step we could possibly take to finish with a more professional product is to purchase Unity Pro to remove the Unity logo and entitle us to earning more than $200,000 in revenue if the game becomes successful. By following the schedule outlined previously, we should end with a viable product that can be played and enjoyed. Therefore, even if we end up descoping, we will still have a product that is functional.

## References

[1]   	“Temple Run,” Wikipedia, 22-Jan-2021. [Online]. Available: https://en.wikipedia.org/wiki/Temple_Run.

[2]   	“Subway Surfers,” Wikipedia, 21-Jan-2021. [Online]. Available: https://en.wikipedia.org/wiki/Subway_Surfers.

[3]   	J. Clement, “Number of gamers worldwide 2023,” Statista, 29-Jan-2021. [Online]. Available: https://www.statista.com/statistics/748044/number-video-gamers-world/.

[4]   	G. Saldana, “Why mobile games are so popular,” Gamesradar, 07-Jan-2014. [Online]. Available: https://www.gamesradar.com/why-mobile-games-are-so-popular.
