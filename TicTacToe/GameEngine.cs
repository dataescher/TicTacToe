using System;
using System.Runtime.InteropServices;

namespace TicTacToe {

	/// <summary>The interface to the game engine DLL.</summary>
	internal static class GameEngine {
		/// <summary>Size of the board.</summary>
		public const Int32 BOARD_SIZE = 3;

		/// <summary>Values that represent board square types.</summary>
		public enum boardSquareTypes {
			/// <summary>The white.</summary>
			Blank = 0,
			/// <summary>The white.</summary>
			PlayerX = 1,
			/// <summary>.</summary>
			PlayerO = -1
		}

		/// <summary>Creates a new game.</summary>
		[DllImport("TicTacToeDLL.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void NewGame();

		/// <summary>Get the state of one coordinate in the game board.</summary>
		/// <exception cref="invalid_argument">
		/// 	Thrown when an invalid argument error condition occurs.
		/// </exception>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <returns>An integer representing the state of the given tile.</returns>
		[DllImport("TicTacToeDLL.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern boardSquareTypes GetSquare(Int32 x, Int32 y);

		/// <summary>Return if the player can move at a specified location.</summary>
		/// <exception cref="invalid_argument">
		/// 	Thrown when an invalid argument error condition occurs.
		/// </exception>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <returns>Non-zero if the move is valid, zero otherwise.</returns>
		[DllImport("TicTacToeDLL.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern Int32 IsValidMove(Int32 x, Int32 y);

		/// <summary>Determine whether or not it is the X player's turn..</summary>
		/// <returns>True if it is X's turn, false otherwise.</returns>
		[DllImport("TicTacToeDLL.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern Int32 IsXTurn();

		/// <summary>Call to make a human move.</summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		[DllImport("TicTacToeDLL.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void Play(Int32 x, Int32 y);

		/// <summary>Make a human play at the given coordinate.</summary>
		/// <exception cref="invalid_argument">
		/// 	Thrown when an invalid argument error condition occurs.
		/// </exception>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		[DllImport("TicTacToeDLL.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern Int32 IsGameOver();

		/// <summary>Determine the winner of the game.</summary>
		/// <returns>
		/// 	An integer corresponding to the current winner of the game. This result is only valid if
		/// 	<see cref="IsGameOver"/> returns non-zero.
		/// </returns>
		[DllImport("TicTacToeDLL.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern Int32 Winner();

		/// <summary>Gets the number of plays which have been made so far in the game.</summary>
		/// <returns>The number plays.</returns>
		[DllImport("TicTacToeDLL.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern Int32 GetNumPlays();

		/// <summary>Make a computer AI play.</summary>
		[DllImport("TicTacToeDLL.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ComputerPlay();
	}
}
