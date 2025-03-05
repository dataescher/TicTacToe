// <copyright file="Program.cs" company="www.feucht.us">
// Copyright (c) 2018 www.feucht.us. All rights reserved.
// </copyright>
// <author>Jonathan Feucht</author>
// <date>1/21/2018</date>
// <summary>Implements the program class</summary>

using System;
using System.Windows.Forms;

namespace TicTacToe {
	/// <summary>A program.</summary>
	internal static class Program {
		/// <summary>The main entry point for the application.</summary>
		[STAThread]
		private static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new frmTicTacToe());
		}
	}
}