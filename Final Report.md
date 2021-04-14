# Mobile Game Titled Paper in the Wind

Team Merge Conflict

Greg Cantrall, Ryan Forsten, Joey Lemon, Brandon Mingledorff


## I. Introduction

Our project is a video game with the main marketed platform being mobile phones running iOS and Android operating systems. The game may also be available on web browsers and desktop computers. The general premise of the game involves the player as a piece of paper, flying through different environments and avoiding obstacles along the way. The goal of the game is to make it as far as possible in the level without dying—the player will earn points for every meter travelled.

The player is infinitely moving in one direction, with the camera placed behind the paper. The player has no option to slow down their movement or turn around. They have a limited control set where they are only able to move in predefined positions, forcing them to continuously make last-second decisions on the best route to take. There are several games with a similar nature, aptly titled “endless runner” games where the player is constantly moving in a predefined direction and avoiding anything that could end their run.

The main motivation for this product is the fact that video games are among the most engaging ways to use one’s free time. In 2020, Statista estimated that around 2.7 billion people played video games actively [1]. Even further, 2.5 billion of these gamers played on their mobile devices (potentially in addition to other platforms). Therefore, our game reaches an extremely large market by being developed for iOS and Android. Additionally, our team is a group of avid gamers and very much enjoy the pleasure found by playing video games. We were around for the explosion of mobile games and therefore see the value in developing our own—we enjoyed the process of creating a game as much as we enjoy playing a game.

Between the proposal of our project and the completion of our project, we unfortunately had to modify some of our goals. Time constraints as well as other responsibilites led to a decreased number of goals met by the group. Some of the major changes we had to make to our plans include:
- Only offering one level to play
- Removing the idea of visual markers in the level to indicate progress
- Not providing the ability to upgrade the player's paper visually or perfomance-wise
- Not providing a global leaderboards to compare scores between other players

Although we had to let go of many features we were excited to implement in our game, we believe we still achieved a reasonable product that met a fair amount of our initial goals. Many of our planned features took much more time than we thought they would. For example, creating an infinite level system that generates by itself as the player moves forward, with twists and turns throughout, took at least 10 hours of development time. Then, developing the animation systems that reset accordingly upon death again took far more hours than we thought neccessary.

## II. Customer Value

When we first proposed this project, we definied the primary customers of our project to be mobile gamers. According to several studies, these customers commonly want only a few simple things [2]:
- A free and stimulating way to pass time.
-	A sense of accomplishment.
-	The ability to replay a game without getting sick of it.
-	The ability to start a game that does not take too long.

Our initial proposal predicted that we would meet these needs by the following:
-	Our game will be free and accessible from both iOS and Android application stores. The player will be stimulated by the active, on-the-fly decisions required to make it far into the level.
-	Players will have a sense of accomplishment as they continuously beat their previous scores, reach higher into the global leaderboard, and upgrade their paper and unlock new environments with their earned points.
-	There will hopefully be strong replayability with the upgrade options and the ever-changing levels that players explore and unlock.
-	The game will last as long or as short as the player wants—there is no expectation for players to play for a specified amount of time.

As stated in the previous section, we unfortunately had to make some changes to our plans. In turn, some of the customer value was likely lost. Without a global leaderboard or upgrade system, a fair portion of the customer's sense of accomplishment is diminished. They will still be able to continuously beat their previous scores which will give them some pride, but not as much as they would have received had we met all of our goals. Additionally, the ability to replay the game without getting sick of it is also diminished without the upgrades or new levels that would make the game less monotone and more exciting.

Nevertheless, our game still delivers on each of the customer values, just maybe not as much as we had hoped. Therefore, our project accomplished our initial proposal on customer value: it is free and stimulating, it provides a sense of accomplishment, it can be replayed several times, and there is no expectation for it to be played for a certain amount of time.

## III. Technology

Our proposal gave some technical details about how we expected the game to work:
- Exportable to all major gaming platforms
- Simple interface with only a few inputs to drive the gameplay
- Infinite levels that generate new sections in front of the player and destroy old ones behind them
- Menu system for selecting levels, upgrading the paper, and changing settings with a parent-child structure similar to webpages
- Levels created by a variety of prefabricated models

Our final product ended up delivering on every single technical detail listed above, except for the menu system with multiple pages since we didn't implement the functionality to upgrade or choose different levels. We still created a basic menu system, however.

Below was our proposed component design for the game.
<p align="center"><img src="https://i.imgur.com/vhw2H0v.png" /></p>

And below is our current component design for the game.
<p align="center"><img src="https://i.imgur.com/URKwfwB.png" /></p>

We didn't need the global manager since we ended up making each component manager a singleton on its own. This means we had a static reference to PlayerManager, LevelManager, or MenuManager from anywhere in the code. The PlayerController provides the ability to manipulate the player and their data, such as whether or not they are dead, how far they are into the level, or how long they have been alive. The PlayerMovement class handles moving the player's paper object throughout the level by dealing with vectors and quaternions to ensure they are following the defined movement grid. The PlayerCollision class detects when the player's paper object collides with an obstacle in the level, and deals with it accordingly. The PlayerInput class handles the different types of inputs from the player and translates them to actions in the game. The Level class provides the ability to generate new sections in the level, destroy old sections, and completely restart the level altogether. The MenuController class handles the interaction and displaying of the start and death menu.

The below video shows how the infinite level generation works by destroying an old section and generating a new one. Our current implementation has an array of pre-created sections that it randomly chooses from to put at the end of the most-recently generated section. This on-the-fly generation makes the levels seemingly infinite.
<p align="center"><img src="https://media1.giphy.com/media/qRUuEHAm6BtQwKsR9N/giphy.gif" /></p>

To ensure our codebase functioned properly across the multitude of commits we made, we set up continuous integration to automatically run unit tests upon a pull request. This helped us make sure our changes didn't end up breaking other functionality in the game. For example, we had a suite of tests that would test our player movement functionality. These tests would make sure the PlayerMovement class didn't allow certain invalid movements or inputs, such as off-by-one rotational vectors or invalid enumeration values when turning a direction.

## IV. Team

In our project proposal, we defined static roles for each team member:
-	Joey Lemon: project manager, base project structure to get team rolling
-	Ryan Forsten: level design
-	Brandon Mingledorff: user experience
-	Greg Cantrall: game logic

We did end up following these roles very well as we maintained the staticness of them. Every team member contributed as equally as we could have hoped.

## V. Project Management

Below was the schedule we defined in our project proposal:

|Date|Class Submission|Sprint Goal|
|----|--------|------------|
|Thursday, Feb. 18| Submit project proposal| Begin first level, finish basic graphics, movement system, and level generation
|Thursday, Mar. 4| Submit iteration 1 status report| Complete point and menu system, enhance first level|
|Thursday, Mar. 18| Submit iteration 2 status report| Complete leaderboard, finalize entire first level|
|Thursday, Apr. 1| Submit iteration 3 status report| Complete upgrade system with unlocks|
|Thursday, Apr. 15| Submit project report and present final product| Complete second level, refinement period|

We definitely had to shift some of the due dates around for goals such as the menu system, leaderboards, upgrade system, and the creation of a second level. As stated in the introduction, we did not have the opportunity to complete all of our goals due to time constraints caused by other responsibilities and an underestimation of difficulty of some aspects of the project. For example, the infinite level generation system took almost twice as long as we had initially planned due to repetitive bugs and unforeseen issues.

## VI. Reflection

Although we did not achieve all of our goals, we still consider our project a success. We achieved the minimum viable product we defined in the proposal by creating a usable game with an infinite level system, unique movement system, and a fun environment to explore and survive through. Our team collaborated effectively as we maintained constant communication in our group chat. We stuck to our roles and we each feel like we succeeded in them. Although the development of the game had hiccups such as bugs and other issues, we still feel like it went rather smooth. Our testing infrastructure definitely helped in catching things for us, especially with continuous integration performing automatic tests. One thing that didn't go well was sticking to the schedule. As already mentioned, this was almost entirely out of our control. However, we could have probably planned more effectively upfront to reduce the need of rescheduling our project.

## References

[1]   	J. Clement, “Number of gamers worldwide 2023,” Statista, 29-Jan-2021. [Online]. Available: https://www.statista.com/statistics/748044/number-video-gamers-world/.

[2]   	G. Saldana, “Why mobile games are so popular,” Gamesradar, 07-Jan-2014. [Online]. Available: https://www.gamesradar.com/why-mobile-games-are-so-popular.
