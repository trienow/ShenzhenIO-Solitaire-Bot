# ShenzhenIO-Solitaire-Bot
A bot to help solve the Solitaire mini-game in ShenzhenIO.

# Quick Overview of the procedure
1. Extract the card layout from the game
2. Find all possible moves or moves that make the most sense
3. Simulate the moves on the first board and save their resulting card layouts (GameStates) in a list
4. Count the left over cards
5. Eliminate GameStates, which have too many cards, since they most likely won't result into a quick solution.
6. Repeat the process for each GameState from 2 until no more cards are left
7. Simulate the moves on screen...

# Demonstration
Video: https://youtu.be/6G9z2s4faVU