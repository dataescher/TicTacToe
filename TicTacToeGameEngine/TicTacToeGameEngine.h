// <copyright file="TicTacToeGameEngine.h" company="www.feucht.us">
// Copyright (c) 2018 www.feucht.us. All rights reserved.
// </copyright>
// <author>Jonathan Feucht</author>
// <date>1/21/2018</date>
// <summary>
// 		The TicTacToeGameEngine class contains the main game engine for the TicTacToe application. This
// 		class has all functions and routines for keeping track of board states, player turns, and
// 		contains routines for generating AI plays.
// </summary> 

#pragma once

#include <list>
#include <unordered_map>
#include <string>

/// <summary>The name of the statistics file</summary>
#define STATS_FILE "gamedata.bin"

/// <summary>A value representing a blank tile or unknown player.</summary>
#define BLANK 0

/// <summary>A value representing the X player.</summary>
#define VALUE_X 1

/// <summary>A value representing the O player.</summary>
#define VALUE_O -1

/// <summary>The x and y dimensions of the board.</summary>
#define BOARD_SIZE 3

/// <summary>A tic tac toe game engine.</summary>
class TicTacToeGameEngine {
public:
	/// <summary>Values that represent players.</summary>
	typedef enum {
		xPlayer = VALUE_X,
		oPlayer = VALUE_O
	} player;

	/// <summary>Values that represent board squares.</summary>
	typedef enum {
		blankSquare = BLANK,
		xSquare = VALUE_X,
		oSquare = VALUE_O
	} boardSquare;

	/// <summary>Values that represent winners.</summary>
	typedef enum {
		tie = BLANK,
		xWins = VALUE_X,
		oWins = VALUE_O
	} winner;


	/// <summary>A tic tac toe game coordinate.</summary>
	struct TicTacToeGameCoord {
		/// <summary>An uint8_t to process.</summary>
		uint8_t x;
		/// <summary>An uint8_t to process.</summary>
		uint8_t y;

		/// <summary>Initializes a new instance of the <see cref="TicTacToeGameCoord"/> class.</summary>
		TicTacToeGameCoord();

		/// <summary>Initializes a new instance of the <see cref="TicTacToeGameCoord"/> class.</summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		TicTacToeGameCoord(uint8_t x, uint8_t y);

		/// <summary>Equality operator.</summary>
		/// <param name="other">
		/// 	The other <see cref="TicTacToeGameCoord"/> to compare this one with.
		/// </param>
		/// <returns>True if the parameters are considered equivalent.</returns>
		bool operator==(const TicTacToeGameCoord& other);

		/// <summary>Converts the coordinate to a character, A-I, depending on position.</summary>
		/// <returns>The coordinate represented by a character A-I.</returns>
		char toChar();

		/// <summary>Set the coordinate based on a character, A-I.</summary>
		/// <param name="value">The coordinate character representation, A-I.</param>
		/// <returns>A new <see cref="TicTacToeGameCoord"/> object.</returns>
		static TicTacToeGameCoord fromChar(char value);
	};

	/// <summary>
	/// 	A struct representing statistical game data for a play in a certain game sequence.
	/// </summary>
	struct stat {
		/// <summary>The weight representing an AI score -1 to 1 for the play. Positive values represent a good play score for the X player, while nevative values represent a good play score for the O player.</summary>
		double weight;
		/// <summary>The number of samples which have contributed to the score.</summary>
		uint64_t samples;
		/// <summary>Initializes a new instance of the <see cref="stat"/> struct.</summary>
		stat();
	};

	/// <summary>A list of the available moves.</summary>
	std::list<TicTacToeGameCoord> availableMoves;

	/// <summary>The current state of the game board.</summary>
	boardSquare board[BOARD_SIZE][BOARD_SIZE];

	/// <summary>Initializes a new instance of the <see cref="TicTacToeGameEngine"/> class.</summary>
	TicTacToeGameEngine();
	/// <summary>Finalizes an instance of the <see cref="TicTacToeGameEngine"/> class.</summary>
	~TicTacToeGameEngine();
	/// <summary>Resets the game to the initial state.</summary>
	void Reset();

	/// <summary>Query if a move at a position on the board is valid move.</summary>
	/// <param name="move">The move to check.</param>
	/// <returns>True if valid move, false if not.</returns>
	bool isValidMove(TicTacToeGameCoord move);

	/// <summary>Query if this game is over.</summary>
	/// <returns>True if game is over, false if not.</returns>
	bool isGameOver();

	/// <summary>Get the game winner.</summary>
	/// <returns>The winner of the game.</returns>
	winner findWinner();
	/// <summary>Makes a play at the given location.</summary>
	/// <param name="loc">The location to play.</param>
	void Play(TicTacToeGameCoord loc);
	/// <summary>Make a computer AI play.</summary>
	void computerPlay();
	/// <summary>Print out an ASCII representation of the board to the file "output.log".</summary>
	void printBoard();

	/// <summary>Get the current player.</summary>
	/// <returns>The current player.</returns>
	player curPlayer();

	/// <summary>Returns the number of plays which have been made.</summary>
	/// <returns>The total number of plays.</returns>
	int numPlays();

private:
	/// <summary>Statistical information generated for various game sequences.</summary>
	static std::unordered_map<std::string, stat>* statsData;

	/// <summary>
	/// 	A string containing the current board state using A-I for board coordinates.
	/// </summary>
	static char _moveHistory[BOARD_SIZE * BOARD_SIZE + 1];

	/// <summary>The current player.</summary>
	player _player;

	/// <summary>The index for the current game play.</summary>
	unsigned int _playIndex;

	/// <summary>True if the game is over.</summary>
	bool _gameOver;

	/// <summary>If <see cref="_gameOver"/> is true, indicates the winner of the game.</summary>
	winner _winner;

	/// <summary>
	/// 	Indicates whether this instance of <see cref="TicTacToeGameEngine"/> is the current game
	/// 	or an AI-generated copy.
	/// </summary>
	bool _mainGame;


	/// <summary>Full pathname of the AI statistics file.</summary>
	std::string _path;

	/// <summary>
	/// 	This constructor is only called by the computer AI to simulate various game moves.
	/// </summary>
	/// <param name="copyGameData">Another <see cref="TicTacToeGameEngine"/> to copy.</param>
	TicTacToeGameEngine(const TicTacToeGameEngine& copyGameData);

	/// <summary>
	/// 	Searches the game board for available moves. If none exist, the game is over.
	/// </summary>
	void _findAvailableMoves();

	/// <summary>Checks the board state to see if a move leads to a win.</summary>
	/// <param name="thisMove">The move to check.</param>
	void _findWinCondition(const TicTacToeGameCoord& thisMove);

	/// <summary>
	/// 	Recursively generate statistical data for a tile for the current board state. If the
	/// 	statistics have been previously generated, return the previously generated data.
	/// </summary>
	/// <returns>
	/// 	A double between -1 to 1 representing the weight of the tile for X (positive) or O
	/// 	(negative).
	/// </returns>
	double _recurseGetTileStats();

	/// <summary>Computer AI routine. Looks for the optimal move for the current AI player.</summary>
	/// <returns>The optimal play coordinate.</returns>
	TicTacToeGameCoord _computer_AI();

	/// <summary>Loads the AI statistics from a file.</summary>
	void _loadStats();
	/// <summary>Saves the statistics to a file.</summary>
	void _saveStats();
	/// <summary>Apply feedback to the statistics for all moves leading to a player win.</summary>
	void _applyFeedback();

	/// <summary>Gets statistics for a single move based on the current board state.</summary>
	/// <param name="playIdx">The index of the play to get statistics for.</param>
	/// <returns>Null if statistics do not exist, else the statistics data for the play index.</returns>
	stat* getStatsData(uint8_t playIdx);
};
