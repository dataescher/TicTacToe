// <copyright file="TicTacToeDLL.cpp" company="www.feucht.us">
// Copyright (c) 2018 www.feucht.us. All rights reserved.
// </copyright>
// <author>Jonathan Feucht</author>
// <date>1/21/2018</date>
// <summary>Defines the public interface routines for the TicTacToe dll.</summary>

#include "stdafx.h"
#include "TicTacToeDLL.h"
#include <stdexcept>
#include "../TicTacToeGameEngine/TicTacToeGameEngine.h"

using namespace std;
#pragma warning(disable: 4297)

/// <summary>The game.</summary>
TicTacToeGameEngine myGame;

/// <summary>Resets the game state.</summary>
void NewGame() {
	myGame.Reset();
}



/// <summary>Get the state of one coordinate in the game board.</summary>
/// <exception cref="invalid_argument">
/// 	Thrown when an invalid argument error condition occurs.
/// </exception>
/// <param name="x">The x coordinate.</param>
/// <param name="y">The y coordinate.</param>
/// <returns>An integer representing the state of the given tile.</returns>
int GetSquare(int x, int y) {
	if ((x >= BOARD_SIZE) || (y >= BOARD_SIZE) || (x < 0) || (y < 0)) {
		throw invalid_argument("Position does not exist");
	}

	return (myGame.board[x][y]);
}

/// <summary>Return if the player can move at a specified location.</summary>
/// <exception cref="invalid_argument">
/// 	Thrown when an invalid argument error condition occurs.
/// </exception>
/// <param name="x">The x coordinate.</param>
/// <param name="y">The y coordinate.</param>
/// <returns>Non-zero if the move is valid, zero otherwise.</returns>
int IsValidMove(int x, int y) {
	if ((x >= BOARD_SIZE) || (y >= BOARD_SIZE) || (x < 0) || (y < 0)) {
		throw invalid_argument("Position does not exist");
	}

	return (myGame.isValidMove(TicTacToeGameEngine::TicTacToeGameCoord(x, y)));
}

/// <summary>Determine whether or not it is the X player's turn..</summary>
/// <returns>True if it is X's turn, false otherwise.</returns>
int IsXTurn() {
	return (myGame.curPlayer() == TicTacToeGameEngine::player::xPlayer);
}

/// <summary>Make a human play at the given coordinate.</summary>
/// <exception cref="invalid_argument">
/// 	Thrown when an invalid argument error condition occurs.
/// </exception>
/// <param name="x">The x coordinate.</param>
/// <param name="y">The y coordinate.</param>
void Play(int x, int y) {
	if ((x >= BOARD_SIZE) || (y >= BOARD_SIZE) || (x < 0) || (y < 0)) {
		throw invalid_argument("Position does not exist");
	}

	myGame.Play(TicTacToeGameEngine::TicTacToeGameCoord(x, y));
}

/// <summary>Query whether the game is over.</summary>
/// <returns>Non-zero if the game is in progress, zero if it is over.</returns>
int IsGameOver() {
	return (myGame.isGameOver());
}

/// <summary>Determine the winner of the game.</summary>
/// <returns>
/// 	An integer corresponding to the current winner of the game. This result is only valid if
/// 	<see cref="IsGameOver"/> returns non-zero.
/// </returns>
int Winner() {
	return ((int)myGame.findWinner());
}

/// <summary>Gets the number of plays which have been made so far in the game.</summary>
/// <returns>The number plays.</returns>
int GetNumPlays() {
	return (myGame.numPlays());
}

/// <summary>Make a computer AI play.</summary>
void ComputerPlay() {
	// Computer makes a move
	myGame.computerPlay();
}
