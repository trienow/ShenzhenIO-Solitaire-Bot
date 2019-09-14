# ShenzhenIO-Solitaire-Bot
[![Video Demonstration: https://youtu.be/N1A92QK_4sw](https://img.shields.io/badge/Demonstration-YouTube-critical)](https://youtu.be/N1A92QK_4sw)

A bot to help solve the Solitaire mini-game in ShenzhenIO.

# Procedure Overview
1. Extract the card layout from the game via template matching
2. Find all possible moves or moves that make the most sense
3. Simulate the moves on the first board and save their resulting card layouts (GameStates) in a list
4. Count the left over cards
5. Eliminate GameStates, which have too many cards, since they most likely won't result into a quick solution.
6. Repeat the process for each GameState from 2 until no more cards are left
7. Simulate the moves on screen...

# Sub-Project Descriptions
 - `SHENZHENSolitaire`: Main Program
 - `CardFingerprintGenerator`: Generates embeddable byte arrays, from 16x16 template images, found in the image class of the `SHENZHENSolitaire`.
 - `Tests`: Some unit tests to make sure components behave