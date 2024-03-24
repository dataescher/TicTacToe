// <copyright file="TicTacToeGameEngine.cpp" company="www.feucht.us">
// Copyright (c) 2018 www.feucht.us. All rights reserved.
// </copyright>
// <author>Jonathan Feucht</author>
// <date>1/21/2018</date>
// <summary>
// 		The TicTacToeGameEngine class contains the main game engine for the TicTacToe application. This
// 		class has all functions and routines for keeping track of board states, player turns, and
// 		contains routines for generating AI plays.
// </summary> 

#include "TicTacToeGameEngine.h"
#include <Windows.h>
#include <stdio.h>
#include <fstream>
#include "Shlwapi.h"

using namespace std;

/// <summary>Statistical information generated for various game sequences.</summary>
unordered_map<string, TicTacToeGameEngine::stat>* TicTacToeGameEngine::statsData;

/// <summary>
/// 	A string containing the current board state using A-I for board coordinates.
/// </summary>
char TicTacToeGameEngine::_moveHistory[BOARD_SIZE * BOARD_SIZE + 1];

/// <summary>Initializes a new instance of the <see cref="TicTacToeGameCoord"/> class.</summary>
TicTacToeGameEngine::TicTacToeGameCoord::TicTacToeGameCoord() {
	this->x = 0;
	this->y = 0;
}

/// <summary>Initializes a new instance of the <see cref="TicTacToeGameCoord"/> class.</summary>
/// <param name="x">The x coordinate.</param>
/// <param name="y">The y coordinate.</param>
TicTacToeGameEngine::TicTacToeGameCoord::TicTacToeGameCoord(uint8_t x, uint8_t y) {
	this->x = x;
	this->y = y;
}

/// <summary>Equality operator.</summary>
/// <param name="other">
/// 	The other <see cref="TicTacToeGameCoord"/> to compare this one with.
/// </param>
/// <returns>True if the parameters are considered equivalent.</returns>
inline bool TicTacToeGameEngine::TicTacToeGameCoord::operator==(const TicTacToeGameCoord& other) {
	return ((x == other.x) && (y == other.y));
}

/// <summary>Converts the coordinate to a character, A-I, depending on position.</summary>
/// <returns>The coordinate represented by a character A-I.</returns>
char TicTacToeGameEngine::TicTacToeGameCoord::toChar() {
	if (y == 0) {
		if (x == 0) {
			return 'A';
		} else if (x == 1) {
			return 'B';
		} else {
			return 'C';
		}
	} else if (y == 1) {
		if (x == 0) {
			return 'D';
		} else if (x == 1) {
			return 'E';
		} else {
			return 'F';
		}
	} else {
		if (x == 0) {
			return 'G';
		} else if (x == 1) {
			return 'H';
		} else {
			return 'I';
		}
	}
}

/// <summary>Set the coordinate based on a character, A-I.</summary>
/// <param name="value">The coordinate character representation, A-I.</param>
/// <returns>A new <see cref="TicTacToeGameCoord"/> object.</returns>
TicTacToeGameEngine::TicTacToeGameCoord TicTacToeGameEngine::TicTacToeGameCoord::fromChar(char value) {
	switch (value) {
		default:
		case 'A':
			return TicTacToeGameCoord(0, 0);
		case 'B':
			return TicTacToeGameCoord(1, 0);
		case 'C':
			return TicTacToeGameCoord(2, 0);
		case 'D':
			return TicTacToeGameCoord(0, 1);
		case 'E':
			return TicTacToeGameCoord(1, 1);
		case 'F':
			return TicTacToeGameCoord(2, 1);
		case 'G':
			return TicTacToeGameCoord(0, 2);
		case 'H':
			return TicTacToeGameCoord(1, 2);
		case 'I':
			return TicTacToeGameCoord(2, 2);
	}
}

/// <summary>Initializes a new instance of the <see cref="stat"/> struct.</summary>
TicTacToeGameEngine::stat::stat() {
	this->samples = 0;
	this->weight = 0;
}

/// <summary>Initializes a new instance of the <see cref="TicTacToeGameEngine"/> class.</summary>
TicTacToeGameEngine::TicTacToeGameEngine() {
	// Create a new board
	Reset();

	_mainGame = true;
	char pBuf[MAX_PATH];
	int bytes = GetModuleFileName(NULL, pBuf, MAX_PATH);
	_path = string(pBuf);
	size_t loc = _path.find_last_of("/\\");
	_path = _path.substr(0, loc + 1);
	statsData = NULL;
	_loadStats();

	return;
}

/// <summary>Finalizes an instance of the <see cref="TicTacToeGameEngine"/> class.</summary>
TicTacToeGameEngine::~TicTacToeGameEngine() {
	if (_mainGame) {
		_saveStats();
	}
}

/// <summary>Resets the game to the initial state.</summary>
void TicTacToeGameEngine::Reset(void) {
	for (unsigned int y = 0; y < BOARD_SIZE; y++) {
		for (unsigned int x = 0; x < BOARD_SIZE; x++) {
			board[y][x] = boardSquare::blankSquare;
		}
	}

	_playIndex = 0;
	_winner = winner::tie;
	_player = player::xPlayer;
	_gameOver = false;
	_findAvailableMoves();
	_moveHistory[0] = 0;
}

/// <summary>Query if a move at a position on the board is valid move.</summary>
/// <param name="move">The move to check.</param>
/// <returns>True if valid move, false if not.</returns>
bool TicTacToeGameEngine::isValidMove(TicTacToeGameCoord move) {
	for (list<TicTacToeGameCoord>::iterator it = availableMoves.begin(); it != availableMoves.end(); it++) {
		if (*it == move) {
			return true;
		}
	}

	return false;
}

/// <summary>Query if this game is over.</summary>
/// <returns>True if game is over, false if not.</returns>
bool TicTacToeGameEngine::isGameOver() {
	return _gameOver;
}

/// <summary>Get the game winner.</summary>
/// <returns>The winner of the game.</returns>
TicTacToeGameEngine::winner TicTacToeGameEngine::findWinner() {
	return _winner;
}

/// <summary>Makes a play at the given location.</summary>
/// <param name="loc">The location to play.</param>
void TicTacToeGameEngine::Play(TicTacToeGameCoord loc) {
	if (!_gameOver) {
		// Check if the move is an available move
		TicTacToeGameCoord* thisMove = NULL;
		for (list<TicTacToeGameCoord>::iterator it = availableMoves.begin(); it != availableMoves.end(); it++) {
			if (*it == loc) {
				thisMove = &(*it);
			}
		}

		if (thisMove != NULL) {
			_moveHistory[_playIndex++] = thisMove->toChar();
			// End the string
			_moveHistory[_playIndex] = 0;

			board[thisMove->x][thisMove->y] = (boardSquare)_player;
			_findWinCondition(*thisMove);
			_applyFeedback();

			_player = (player)(-_player);

			_findAvailableMoves();
			if (availableMoves.size() == 0) {
				_gameOver = true;
			}
		} // TODO: Otherwise, handle this error
	}
}

/// <summary>Make a computer AI play.</summary>
void TicTacToeGameEngine::computerPlay() {
	// Select the best move
	Play(_computer_AI());
	return;
}

/// <summary>Print out an ASCII representation of the board to the file "output.log".</summary>
void TicTacToeGameEngine::printBoard() {
	FILE* myFile = fopen("output.log", "a");
	if (myFile) {
		fprintf(myFile, "-----------------------\n");
		for (unsigned int y = 0; y < BOARD_SIZE; y++) {
			for (unsigned int x = 0; x < BOARD_SIZE; x++) {
				boardSquare thisTile = board[x][y];
				char thisChar = '\0';

				switch (thisTile) {
					case boardSquare::blankSquare:
						thisChar = '.';
						break;
					case boardSquare::xSquare:
						thisChar = 'X';
						break;
					case boardSquare::oSquare:
						thisChar = 'O';
						break;
				}

				fprintf(myFile, "%c ", thisChar);
			}
			fprintf(myFile, "\n");
		}
		fprintf(myFile, "-----------------------\n");
		fclose(myFile);
	}

	return;
}

/// <summary>Get the current player.</summary>
/// <returns>The current player.</returns>
TicTacToeGameEngine::player TicTacToeGameEngine::curPlayer() {
	return _player;
}

/// <summary>Returns the number of plays which have been made.</summary>
/// <returns>The total number of plays.</returns>
int TicTacToeGameEngine::numPlays() {
	return _playIndex;
}

/// <summary>
/// 	This constructor is only called by the computer AI to simulate various game moves.
/// </summary>
/// <param name="copyGameData">Another <see cref="TicTacToeGameEngine"/> to copy.</param>
TicTacToeGameEngine::TicTacToeGameEngine(const TicTacToeGameEngine& copyGameData) {
	// Copy the board
	for (unsigned int y = 0; y < BOARD_SIZE; y++) {
		for (unsigned int x = 0; x < BOARD_SIZE; x++) {
			board[x][y] = copyGameData.board[x][y];
		}
	}
	_mainGame = false;
	_player = copyGameData._player;
	_gameOver = copyGameData._gameOver;
	availableMoves = copyGameData.availableMoves;
	_winner = copyGameData._winner;
	_playIndex = copyGameData._playIndex;
}

/// <summary>
/// 	Searches the game board for available moves. If none exist, the game is over.
/// </summary>
void TicTacToeGameEngine::_findAvailableMoves() {
	TicTacToeGameCoord thisAvailableMove;
	availableMoves.clear();
	for (thisAvailableMove.y = 0; thisAvailableMove.y < BOARD_SIZE; thisAvailableMove.y++) {
		for (thisAvailableMove.x = 0; thisAvailableMove.x < BOARD_SIZE; thisAvailableMove.x++) {
			if (board[thisAvailableMove.x][thisAvailableMove.y] == boardSquare::blankSquare) {
				availableMoves.push_back(thisAvailableMove);
			}
		}
	}
}

// Helper function to findNextPlayer
// Finds the squares which are flipped due to a game play
inline void TicTacToeGameEngine::_findWinCondition(const TicTacToeGameCoord& thisMove) {
	TicTacToeGameCoord endPos;
	list<TicTacToeGameCoord> _reversedSquares;
	bool bWinCondition;
	bool bWon = false;

	// Check the row
	bWinCondition = true;
	for (uint8_t thisX = 0; thisX < BOARD_SIZE; thisX++) {
		if (board[thisX][thisMove.y] != _player) {
			bWinCondition = false;
			break;
		}
	}
	bWon |= bWinCondition;

	// Check the column
	bWinCondition = true;
	for (uint8_t thisY = 0; thisY < BOARD_SIZE; thisY++) {
		if (board[thisMove.x][thisY] != _player) {
			bWinCondition = false;
			break;
		}
	}
	bWon |= bWinCondition;

	if (thisMove.x == thisMove.y) {
		// Check the down diagonal
		bWinCondition = true;
		for (uint8_t thisX = 0, thisY = 0; thisX < BOARD_SIZE; thisX++, thisY++) {
			if (board[thisX][thisY] != _player) {
				bWinCondition = false;
				break;
			}
		}
		bWon |= bWinCondition;
	}

	if (thisMove.x == (BOARD_SIZE - thisMove.y - 1)) {
		// Check the up diagonal
		bWinCondition = true;
		for (uint8_t thisX = 0, thisY = BOARD_SIZE - 1; thisX < BOARD_SIZE; thisX++, thisY--) {
			if (board[thisX][thisY] != _player) {
				bWinCondition = false;
				break;
			}
		}
		bWon |= bWinCondition;
	}

	if (bWon) {
		_gameOver = true;
		_winner = (winner)_player;
	} else if (_playIndex == 9) {
		_gameOver = true;
		_winner = tie;
	}
}

/// <summary>
/// 	Recursively generate statistical data for a tile for the current board state. If the
/// 	statistics have been previously generated, return the previously generated data.
/// </summary>
/// <returns>
/// 	A double between -1 to 1 representing the weight of the tile for X (positive) or O
/// 	(negative).
/// </returns>
double TicTacToeGameEngine::_recurseGetTileStats() {
	if (_gameOver) {
		return (double)_winner;
	} else {
		unordered_map<string, stat>::iterator it = statsData->find(string(_moveHistory, _playIndex));
		if (it == statsData->end()) {
			for (list<TicTacToeGameCoord>::iterator it = availableMoves.begin(); it != availableMoves.end(); it++) {
				TicTacToeGameEngine nextGame = *this;
				nextGame.Play(*it);
				nextGame._recurseGetTileStats();
			}
			it = statsData->find(string(_moveHistory, _playIndex));
		}
		return it->second.weight;
	}
}

/// <summary>Computer AI routine. Looks for the optimal move for the current AI player.</summary>
/// <returns>The optimal play coordinate.</returns>
inline TicTacToeGameEngine::TicTacToeGameCoord TicTacToeGameEngine::_computer_AI(void) {
	double bestStats = -1;
	TicTacToeGameCoord bestMove;
	double theseStats;

	for (list<TicTacToeGameCoord>::iterator it = availableMoves.begin(); it != availableMoves.end(); it++) {
		// Copy the current class
		TicTacToeGameEngine nextGame = *this;
		nextGame.Play(*it);

		// Recurse multiple levels
		theseStats = nextGame._recurseGetTileStats() * (double)_player;

		if (theseStats >= bestStats) {
			bestStats = theseStats;
			bestMove = *it;
		}
	}

	return bestMove;
}

/// <summary>Loads the AI statistics from a file.</summary>
void TicTacToeGameEngine::_loadStats() {
	ifstream InFile;
	InFile.open(_path + STATS_FILE, ios::in | ios::binary);
	if (InFile) {
		if (statsData) {
			delete statsData;
		}
		statsData = new unordered_map<string, stat>();
		char buffer[10];
		stat thisStat;
		thisStat.samples = 1;
		while (!InFile.eof()) {
			InFile.read(buffer, 9);
			InFile.read((char*)&(thisStat.weight), sizeof(double));
			statsData->emplace(buffer, thisStat);
		}
		InFile.close();
	} else {
		if (statsData) {
			delete statsData;
		}
		statsData = new unordered_map<string, stat>();
	}
}

/// <summary>Saves the statistics to a file.</summary>
void TicTacToeGameEngine::_saveStats() {
	ofstream OutFile;
	OutFile.open(_path + STATS_FILE, ios::out | ios::binary);
	if (OutFile) {
		for (unordered_map<string, stat>::iterator it = statsData->begin(); it != statsData->end(); it++) {
			OutFile.write(it->first.c_str(), 9);
			OutFile.write((const char*)&(it->second.weight), sizeof(double));
		}
		OutFile.close();
	}
}

/// <summary>Apply feedback to the statistics for all moves leading to a player win.</summary>
void TicTacToeGameEngine::_applyFeedback() {
	if (_gameOver) {
		stat* thisStat;
		// Apply feedback to improve algorithm
		for (int32_t playIndex = _playIndex; playIndex > 0; playIndex--) {
			thisStat = getStatsData(playIndex);
			if (thisStat == NULL) {
				statsData->emplace(string(_moveHistory, playIndex), stat());
				thisStat = getStatsData(playIndex);
			}
			if (playIndex >= ((int32_t)_playIndex - 1)) {
				thisStat->weight = (double)_winner;
				thisStat->samples = 1;
			} else {
				thisStat->weight = ((thisStat->weight * thisStat->samples) + (double)_winner) / (thisStat->samples + 1);
				thisStat->samples++;
			}
		}
	}
}

/// <summary>Gets statistics for a single move based on the current board state.</summary>
/// <param name="playIdx">The index of the play to get statistics for.</param>
/// <returns>Null if statistics do not exist, else the statistics data for the play index.</returns>
TicTacToeGameEngine::stat* TicTacToeGameEngine::getStatsData(uint8_t playIdx) {
	unordered_map<string, stat>::iterator it = statsData->find(string(_moveHistory, playIdx));
	if (it == statsData->end()) {
		return NULL;
	}

	return &(it->second);
}