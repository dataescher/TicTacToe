// <copyright file="frmTicTacToe.cs" company="www.feucht.us">
// Copyright (c) 2018 www.feucht.us. All rights reserved.
// </copyright>
// <author>Jonathan Feucht</author>
// <date>1/21/2018</date>
// <summary>Implements the form tic tac toe class</summary>

using System;
using System.Drawing;
using System.Windows.Forms;

using TicTacToe.Properties;

namespace TicTacToe {
	/// <summary>A form tic tac toe.</summary>
	public partial class frmTicTacToe : Form {
		/// <summary>The cursor location.</summary>
		private Point cursorLocation = new();

		/// <summary>Contains information about the tiles on the current game board.</summary>
		private readonly GameEngine.boardSquareTypes[,] boardData = new GameEngine.boardSquareTypes[GameEngine.BOARD_SIZE, GameEngine.BOARD_SIZE];

		/// <summary>A 2-d array of pictureboxes containing the tile located on that square.</summary>
		private readonly BlendPictureBox[,] tiles = new BlendPictureBox[3, 3];

		/// <summary>Initializes a new instance of the TicTacToe.frmTicTacToe class.</summary>
		public frmTicTacToe() {
			InitializeComponent();
		}

		/// <summary>Create the game board.</summary>
		private void CreateBoard() {
			DeleteBoard();

			for (Int32 y = 0; y < GameEngine.BOARD_SIZE; y++) {
				for (Int32 x = 0; x < GameEngine.BOARD_SIZE; x++) {
					Random rnd = new();

					boardData[x, y] = GameEngine.boardSquareTypes.Blank;
					tiles[x, y] = new BlendPictureBox {
						RotationAngle = 0, // rnd.Next(0, 360);
						Size = new Size(GameContainer.Panel1.ClientSize.Width / GameEngine.BOARD_SIZE, GameContainer.Panel1.ClientSize.Height / GameEngine.BOARD_SIZE),
						Location = new Point(x * (GameContainer.Panel1.ClientSize.Width / GameEngine.BOARD_SIZE), y * (GameContainer.Panel1.ClientSize.Height / GameEngine.BOARD_SIZE)),
						SizeMode = PictureBoxSizeMode.StretchImage,
						BackColor = Color.Transparent,
						Image1 = Resources.OGamePiece,
						Image2 = Resources.XGamePiece,
						Blend = 1F,
						Visible = true,
						Empty = true
					};
					tiles[x, y].MouseMove += Tile_MouseMove;
					GameContainer.Panel1.Controls.Add(tiles[x, y]);

				}
			}
		}

		/// <summary>Refreshes and displays the current game board.</summary>
		private void DisplayBoard() {
			Point thisLocation = new();

			for (thisLocation.Y = 0; thisLocation.Y < GameEngine.BOARD_SIZE; thisLocation.Y++) {
				for (thisLocation.X = 0; thisLocation.X < GameEngine.BOARD_SIZE; thisLocation.X++) {
					PlaceChip(TicTacToe.GameEngine.GetSquare(thisLocation.X, thisLocation.Y), thisLocation);
				}
			}

			if (GameEngine.IsGameOver() == 0) {
				lblWinner.Visible = false;
				if (GameEngine.IsXTurn() == 0) {
					lblStatus.Text = "O player's turn";
					pctCurColor.Image = Resources.OGamePiece;
				} else {
					lblStatus.Text = "X player's turn";
					pctCurColor.Image = Resources.XGamePiece;
				}
			} else {
				Int32 winner = GameEngine.Winner();
				pctCurColor.Image = null;
				lblStatus.Text = "Game over";
				lblWinner.Visible = true;
				if (winner > 0) {
					lblWinner.Text = "X player wins!";
				} else if (winner < 0) {
					lblWinner.Text = "O player wins!";
				} else {
					lblWinner.Text = "Draw";
				}
			}

			lblProgress.Text = "Number of plays: " + GameEngine.GetNumPlays();

			while (tmMorph.Enabled) {
				Application.DoEvents();
			}
		}

		/// <summary>Place a new tile on the board.</summary>
		/// <param name="type">The type of tile to place.</param>
		/// <param name="location">The location to place the tile.</param>
		private void PlaceChip(GameEngine.boardSquareTypes type, Point location) {
			if (boardData[location.X, location.Y] == GameEngine.boardSquareTypes.Blank) {
				switch (type) {
					case GameEngine.boardSquareTypes.Blank:
						tiles[location.X, location.Y].Empty = true;
						break;
					case GameEngine.boardSquareTypes.PlayerX:
						boardData[location.X, location.Y] = GameEngine.boardSquareTypes.PlayerX;
						tiles[location.X, location.Y].Blend = 0;
						tiles[location.X, location.Y].Visible = true;
						tiles[location.X, location.Y].Empty = false;
						tmMorph.Enabled = true;
						break;
					case GameEngine.boardSquareTypes.PlayerO:
						boardData[location.X, location.Y] = GameEngine.boardSquareTypes.PlayerO;
						tiles[location.X, location.Y].Blend = 0;
						tiles[location.X, location.Y].Visible = true;
						tiles[location.X, location.Y].Empty = false;
						tmMorph.Enabled = true;
						break;
				}
			}
		}

		/// <summary>Delete all game pieces from the game board.</summary>
		private void DeleteBoard() {
			for (Int32 y = 0; y < GameEngine.BOARD_SIZE; y++) {
				for (Int32 x = 0; x < GameEngine.BOARD_SIZE; x++) {
					if (tiles[x, y] != null) {
						GameContainer.Panel1.Controls.Remove(tiles[x, y]);
						tiles[x, y] = null;
					}
				}
			}
		}

		/// <summary>The application main window has been displayed.</summary>
		/// <param name="sender">Source of the event.</param>
		/// <param name="e">Event information.</param>
		private void FrmTicTacToe_Shown(Object sender, EventArgs e) {
			TicTacToe.GameEngine.NewGame();

			CreateBoard();
			DisplayBoard();
			ComputerPlay();
		}

		/// <summary>User moved the mouse over a tile. Move the cursor over the selected tile.</summary>
		/// <param name="sender">Source of the event.</param>
		/// <param name="e">Mouse event information.</param>
		private void Tile_MouseMove(Object sender, MouseEventArgs e) {
			BlendPictureBox refObject = sender as BlendPictureBox;

			if (refObject.Empty && (GameEngine.IsGameOver() == 0)) {
				if ((!IsComputerTurn()) && (tmMorph.Enabled == false)) {
					cursorLocation.X = GameEngine.BOARD_SIZE * (refObject.Left + (refObject.Width / 2)) / GameContainer.Panel1.ClientSize.Width;
					cursorLocation.Y = GameEngine.BOARD_SIZE * (refObject.Top + (refObject.Height / 2)) / GameContainer.Panel1.ClientSize.Height;

					if (GameEngine.IsValidMove(cursorLocation.X, cursorLocation.Y) != 0) {
						pctCursor.Location = refObject.Location;
						pctCursor.Size = refObject.Size;
						pctCursor.Visible = true;
					} else {
						pctCursor.Visible = false;
					}
				}
			} else {
				pctCursor.Visible = false;
			}
		}

		/// <summary>User clicked on the game board. Play the selected move.</summary>
		/// <param name="sender">Source of the event.</param>
		/// <param name="e">Event information.</param>
		private void PctCursor_Click(Object sender, EventArgs e) {
			pctCursor.Visible = false;
			Application.DoEvents();
			GameEngine.Play(cursorLocation.X, cursorLocation.Y);
			DisplayBoard();
			ComputerPlay();
		}

		/// <summary>Make a computer AI play.</summary>
		private void ComputerPlay() {
			while (IsComputerTurn()) {
				pctCursor.Visible = false;
				lblStatus.Text += ": Waiting on computer...";
				Application.DoEvents();
				GameEngine.ComputerPlay();
				DisplayBoard();

				if (optTraining.Checked && (GameEngine.IsGameOver() != 0)) {
					System.Threading.Thread.Sleep(500);
					// Start a new game with no user input
					TicTacToe.GameEngine.NewGame();
					CreateBoard();
					DisplayBoard();
					ComputerPlay();
				}
			}
		}

		/// <summary>Query if it is currently the computer's turn.</summary>
		/// <returns>True if computer turn, false if not.</returns>
		private Boolean IsComputerTurn() {
			if (GameEngine.IsGameOver() == 0) {
				if (GameEngine.IsXTurn() == 0) {
					if (optX.Checked || optNeither.Checked || optTraining.Checked) {
						return true;
					}
				} else {
					if (optO.Checked || optNeither.Checked || optTraining.Checked) {
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Timer tick event used to morph one player tile into another.</summary>
		/// <param name="sender">Source of the event.</param>
		/// <param name="e">Event information.</param>
		private void TmMorph_Tick(Object sender, EventArgs e) {
			Boolean done = true;

			for (Int32 y = 0; y < GameEngine.BOARD_SIZE; y++) {
				for (Int32 x = 0; x < GameEngine.BOARD_SIZE; x++) {
					GameEngine.boardSquareTypes thisSquare = boardData[x, y];
					Single curBlend = tiles[x, y].Blend;

					if (thisSquare == GameEngine.boardSquareTypes.PlayerO) {
						if (curBlend != -1F) {
							done = false;
							curBlend -= 0.05F;
							if (curBlend < -1F) {
								curBlend = -1F;
							}
							tiles[x, y].Blend = curBlend;
						}
					} else if (thisSquare == GameEngine.boardSquareTypes.PlayerX) {
						if (curBlend != 1F) {
							done = false;
							curBlend += 0.05F;
							if (curBlend > 1F) {
								curBlend = 1F;
							}
							tiles[x, y].Blend = curBlend;
						}
					}
				}
			}

			if (done) {
				tmMorph.Enabled = false;
			}
		}

		/// <summary>User clicked the button to exit the application.</summary>
		/// <param name="sender">Source of the event.</param>
		/// <param name="e">Event information.</param>
		private void BtnExit_Click(Object sender, EventArgs e) {
			Application.Exit();
		}

		/// <summary>User clicked the button to start the new game.</summary>
		/// <param name="sender">Source of the event.</param>
		/// <param name="e">Event information.</param>
		private void BtnNewGame_Click(Object sender, EventArgs e) {
			TicTacToe.GameEngine.NewGame();
			CreateBoard();
			DisplayBoard();
			ComputerPlay();
		}

		/// <summary>User clicked the label indicating the winner.</summary>
		/// <param name="sender">Source of the event.</param>
		/// <param name="e">Event information.</param>
		private void LblWinner_Click(Object sender, EventArgs e) {
			lblWinner.Visible = false;
		}

		/// <summary>User selected the option to be the O player.</summary>
		/// <param name="sender">Source of the event.</param>
		/// <param name="e">Event information.</param>
		private void OptO_CheckedChanged(Object sender, EventArgs e) {
			ComputerPlay();
		}

		/// <summary>User selected the option to be the X player.</summary>
		/// <param name="sender">Source of the event.</param>
		/// <param name="e">Event information.</param>
		private void OptX_CheckedChanged(Object sender, EventArgs e) {
			ComputerPlay();
		}

		/// <summary>User selected the option to have both players be computer AI.</summary>
		/// <param name="sender">Source of the event.</param>
		/// <param name="e">Event information.</param>
		private void OptNeither_CheckedChanged(Object sender, EventArgs e) {
			ComputerPlay();
		}

		/// <summary>
		/// 	User selected the option to have both players be computer AI, and to run repeatedly.
		/// </summary>
		/// <param name="sender">Source of the event.</param>
		/// <param name="e">Event information.</param>
		private void OptTraining_CheckedChanged(Object sender, EventArgs e) {
			ComputerPlay();
		}
	}
}