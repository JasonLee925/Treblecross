# Intro
The repo is an assignment work from a university course.
The main goal of this project is to learn about Object Oriented Programing (OOP) by developing a game called *[Treblecross](https://en.wikipedia.org/wiki/Treblecross)*.
The codebase demonstrates our understanding of OOP principles and concept of design pattern.

In this project, two design patterns are used:

1. **Singlaton** is applied to a storing class GameStateHistory, allowing a quick access to the game state storage

1. **Template pattern** is used in GameOperator class where it handles the main game logic. The pattern breaks down the logics into small pieces and gives an advantage of extensibility (The code is extenable when we consider adding a new board game).


## Class Diagram

![class](/images/class_diagram.jpg)


## Object Diagram

![object](/images/object_diagram.jpg)


## Sequence Diagram
Event: The main process of the game. From starting a new game, players taking turns to make moves, validating moves, game status update, board update, to concluding the game with a result.

![sequence](/images/sequence_diagram.jpg)


# Game previews
- Menu
![menu](/images/menu.png)

- Gameplay
![gameplay](/images/gameplay.png)

- load game
![loadgame](/images/loadgame.png)