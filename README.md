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

### Iterative Walkthrough

An iterative algorithm will create a pointer to the row currently being processed as well as a one-dimensional field for storing the positions of the queens per row. As long as the pointer indexes a row of the field, the algorithm checks if there is already a queen in the same column, row or diagonal row. If this is the case, this position is skipped, and the algorithm continues with the next one. If, on the other hand, the field is still free, the place is stored in the position field, and the pointer is moved to the next row.

If not queen has been placed, but the end of the row is still reached, the pointer is moved to the previous row. If there already is a queen from the step before, it is moved, and the pointer is transferred backwards another time.

However, if the pointer implies a row outside the field, the algorithm has found all solutions and terminates. With the help of the backtracking algorithm, all possible unique queen positions and their rotations, reflections, and combinations will be found.

### Recursive Walkthrough

In the recursive algorithm, a variable (N) of the chessboard size is initially created. Afterwards, an additional one-dimensional list of column arrays with size N is needed for storing the column results.

The recursive function starts with the counter on zero and finished if the counter is greater than or equal to N. Before that, the recursion function is calling itself over and over again for different rows.

As in the iterative algorithm, the program checks if a queen already occupies the same column, row, or diagonal row. If so, a collision flag is set to true, and the loop is exited. Here, the recursion is called again with the counter increased by one, to go backwards. If the last column, e.g. N, is reached, the result is stored in the list.

## Contributors

- Mariska Steinfeldt
- Lukas Schmitz
- Lukas Brüggeman
- Marc Ulbricht

## Tools

[Unity](https://unity.com/)
