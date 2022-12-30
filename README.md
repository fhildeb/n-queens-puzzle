# n-queens-puzzle

A Visualization Program for solving the N-Queens-Puzzle as final thesis for Algorithms and Data Structures in 2019, led by Felix Hildebrandt.

Further modified from the original thesis for minimalistic look and feel.

## Application Download

[Current Release for Mac, Windows, or Linux](https://github.com/fhildeb/n-queens-puzzle/releases)

## Features

- solve N-Queens-Puzzle from 1-12 on a visual board
- run step-by-step recursive algorithm with visual feedback
- run step-by-step iterative algorithm with visual feedback
- manual placement of queens with a check function
- iterating through solutions
- playing queens board

## The Puzzle's Origin

The "N-Queens-Puzzle" is a chess mathematical problem. Here, **N** queens are placed on an **N** x **N** chessboard so that they cannot beat each other according to the chess rules. No two queens may be in the same column, row, and diagonal. In 1848 it was formulated for the first time by Max Bezel, a chess master, for eight queens. It was not until 1850 that Franz Nauck published the solution of 92, which was proved by an English mathematician in 1874. Later, the complex problem was generalized by Nauck to the N-Queens-Puzzle, where n queens were to be placed on an **N** x **N** chessboard. Its a viral example of backtracking algorithms.

## Showcase

![N-Queens-Problem Screenshot 1](/img/n-queens-problem-screen_01.png)
![N-Queens-Problem Screenshot 2](/img/n-queens-problem-screen_02.png)
![N-Queens-Problem Screenshot 3](/img/n-queens-problem-screen_03.png)
![N-Queens-Problem Screenshot 4](/img/n-queens-problem-screen_04.png)

## Description

This program illustrates and solves the N-queen problem using two different algorithms. Based on backtracking, an iterative and recursive method can be used.

### Backtracking

To solve the puzzle, backtracking is used algorithmically. The problem is decomposed into hierarchically more minor problems until a solution is found. If none is found, the next higher problem is modified and restarted. A backtracking algorithm stops when no more solutions are found for the first minor subproblem.

Backtracking is based on depth-first search and belongs to the group of trial-and-error methods. In contrast to a brute-force algorithm, where all possible solutions are generated, and unsuitable ones are filtered out. The backtracking method immediately skips inappropriate explanations or solution branches by testing, which makes it faster. However, it can come to very long runtimes as the depth-first search is only good for smaller solution trees, due to in-depth problem division.

### Duplication Management

To only filter out unique solutions from the list, a vertical and horizontal mirror image must be created for the individual comparison. If one or more duplicates are found, they must be removed before the algorithm outputs the final solution. Therefore, all board rotations (90°, 180°, and 270°) of the referenced queen elements or their mirror images must be checked.

## Contributors

- Mariska Steinfeldt
- Lukas Schmitz
- Lukas Brüggeman
- Marc Ulbricht

## Tools

[Unity](https://unity.com/)
