// <copyright file="TicTacToeDLL.h" company="www.feucht.us">
// Copyright (c) 2018 www.feucht.us. All rights reserved.
// </copyright>
// <author>Jonathan Feucht</author>
// <date>1/21/2018</date>
// <summary>Deckares the public interface routines for the TicTacToe dll.</summary>

#pragma once

#ifdef TicTacToeDLL_EXPORTS
#define TicTacToeDLL_API __declspec(dllexport) 
#else
#define TicTacToeDLL_API __declspec(dllimport) 
#endif

/// <summary>.</summary>
extern "C" {
	/// <summary>Creates a new game.</summary>
	TicTacToeDLL_API void NewGame();

	/// <summary>Get the state of one coordinate in the game board.</summary>
	/// <exception cref="invalid_argument">
	/// 	Thrown when an invalid argument error condition occurs.
	/// </exception>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <returns>An integer representing the state of the given tile.</returns>
	TicTacToeDLL_API int GetSquare(int x, int y);

	/// <summary>Return if the player can move at a specified location.</summary>
	/// <exception cref="invalid_argument">
	/// 	Thrown when an invalid argument error condition occurs.
	/// </exception>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <returns>Non-zero if the move is valid, zero otherwise.</returns>
	TicTacToeDLL_API int IsValidMove(int x, int y);

	/// <summary>Determine whether or not it is the X player's turn..</summary>
	/// <returns>True if it is X's turn, false otherwise.</returns>
	TicTacToeDLL_API int IsXTurn();

	/// <summary>Call to make a human move.</summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	TicTacToeDLL_API void Play(int x, int y);

	/// <summary>Make a human play at the given coordinate.</summary>
	/// <exception cref="invalid_argument">
	/// 	Thrown when an invalid argument error condition occurs.
	/// </exception>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	TicTacToeDLL_API int IsGameOver();

	/// <summary>Determine the winner of the game.</summary>
	/// <returns>
	/// 	An integer corresponding to the current winner of the game. This result is only valid if
	/// 	<see cref="IsGameOver"/> returns non-zero.
	/// </returns>
	TicTacToeDLL_API int Winner();

	/// <summary>Gets the number of plays which have been made so far in the game.</summary>
	/// <returns>The number plays.</returns>
	TicTacToeDLL_API int GetNumPlays();

	/// <summary>Make a computer AI play.</summary>
	TicTacToeDLL_API void ComputerPlay();
}
