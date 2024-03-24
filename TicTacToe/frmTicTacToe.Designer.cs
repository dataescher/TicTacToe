namespace TicTacToe {
	partial class frmTicTacToe {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTicTacToe));
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.tmMorph = new System.Windows.Forms.Timer(this.components);
			this.GameContainer = new System.Windows.Forms.SplitContainer();
			this.lblWinner = new System.Windows.Forms.Label();
			this.pctCursor = new System.Windows.Forms.PictureBox();
			this.pctCurColor = new System.Windows.Forms.PictureBox();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblPlaceholder = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblProgress = new System.Windows.Forms.ToolStripStatusLabel();
			this.btnExit = new System.Windows.Forms.Button();
			this.btnNewGame = new System.Windows.Forms.Button();
			this.gpPlayerColor = new System.Windows.Forms.GroupBox();
			this.optBoth = new System.Windows.Forms.RadioButton();
			this.optNeither = new System.Windows.Forms.RadioButton();
			this.optX = new System.Windows.Forms.RadioButton();
			this.optO = new System.Windows.Forms.RadioButton();
			this.optTraining = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.GameContainer)).BeginInit();
			this.GameContainer.Panel1.SuspendLayout();
			this.GameContainer.Panel2.SuspendLayout();
			this.GameContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pctCursor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pctCurColor)).BeginInit();
			this.statusStrip.SuspendLayout();
			this.gpPlayerColor.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "OGamePiece.png");
			this.imageList.Images.SetKeyName(1, "XGamePiece.png");
			// 
			// tmMorph
			// 
			this.tmMorph.Interval = 10;
			this.tmMorph.Tick += new System.EventHandler(this.TmMorph_Tick);
			// 
			// GameContainer
			// 
			this.GameContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GameContainer.Location = new System.Drawing.Point(0, 0);
			this.GameContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.GameContainer.Name = "GameContainer";
			this.GameContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// GameContainer.Panel1
			// 
			this.GameContainer.Panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("GameContainer.Panel1.BackgroundImage")));
			this.GameContainer.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.GameContainer.Panel1.Controls.Add(this.lblWinner);
			this.GameContainer.Panel1.Controls.Add(this.pctCursor);
			// 
			// GameContainer.Panel2
			// 
			this.GameContainer.Panel2.Controls.Add(this.pctCurColor);
			this.GameContainer.Panel2.Controls.Add(this.statusStrip);
			this.GameContainer.Panel2.Controls.Add(this.btnExit);
			this.GameContainer.Panel2.Controls.Add(this.btnNewGame);
			this.GameContainer.Panel2.Controls.Add(this.gpPlayerColor);
			this.GameContainer.Size = new System.Drawing.Size(975, 1188);
			this.GameContainer.SplitterDistance = 1000;
			this.GameContainer.SplitterWidth = 6;
			this.GameContainer.TabIndex = 6;
			// 
			// lblWinner
			// 
			this.lblWinner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblWinner.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.lblWinner.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblWinner.ForeColor = System.Drawing.Color.Black;
			this.lblWinner.Location = new System.Drawing.Point(326, 449);
			this.lblWinner.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblWinner.Name = "lblWinner";
			this.lblWinner.Size = new System.Drawing.Size(317, 94);
			this.lblWinner.TabIndex = 4;
			this.lblWinner.Text = "Black Wins!!!";
			this.lblWinner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblWinner.Visible = false;
			this.lblWinner.Click += new System.EventHandler(this.LblWinner_Click);
			// 
			// pctCursor
			// 
			this.pctCursor.BackColor = System.Drawing.Color.Transparent;
			this.pctCursor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pctCursor.BackgroundImage")));
			this.pctCursor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.pctCursor.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pctCursor.Location = new System.Drawing.Point(0, 0);
			this.pctCursor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.pctCursor.Name = "pctCursor";
			this.pctCursor.Size = new System.Drawing.Size(233, 232);
			this.pctCursor.TabIndex = 3;
			this.pctCursor.TabStop = false;
			this.pctCursor.Visible = false;
			this.pctCursor.Click += new System.EventHandler(this.PctCursor_Click);
			// 
			// pctCurColor
			// 
			this.pctCurColor.BackColor = System.Drawing.Color.Silver;
			this.pctCurColor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.pctCurColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pctCurColor.Location = new System.Drawing.Point(424, 12);
			this.pctCurColor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.pctCurColor.Name = "pctCurColor";
			this.pctCurColor.Size = new System.Drawing.Size(119, 122);
			this.pctCurColor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pctCurColor.TabIndex = 5;
			this.pctCurColor.TabStop = false;
			// 
			// statusStrip
			// 
			this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.lblStatus,
			this.lblPlaceholder,
			this.lblProgress});
			this.statusStrip.Location = new System.Drawing.Point(0, 148);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
			this.statusStrip.Size = new System.Drawing.Size(975, 34);
			this.statusStrip.TabIndex = 4;
			this.statusStrip.Text = "statusStrip";
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(60, 29);
			this.lblStatus.Text = "Status";
			// 
			// lblPlaceholder
			// 
			this.lblPlaceholder.AutoSize = false;
			this.lblPlaceholder.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
			this.lblPlaceholder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
			this.lblPlaceholder.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.lblPlaceholder.Name = "lblPlaceholder";
			this.lblPlaceholder.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.lblPlaceholder.Size = new System.Drawing.Size(709, 29);
			this.lblPlaceholder.Spring = true;
			// 
			// lblProgress
			// 
			this.lblProgress.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
			| System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
			| System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Size = new System.Drawing.Size(183, 29);
			this.lblProgress.Text = "toolStripStatusLabel2";
			// 
			// btnExit
			// 
			this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnExit.Location = new System.Drawing.Point(216, 48);
			this.btnExit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(166, 46);
			this.btnExit.TabIndex = 3;
			this.btnExit.Text = "&Exit";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
			// 
			// btnNewGame
			// 
			this.btnNewGame.Location = new System.Drawing.Point(24, 48);
			this.btnNewGame.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnNewGame.Name = "btnNewGame";
			this.btnNewGame.Size = new System.Drawing.Size(166, 46);
			this.btnNewGame.TabIndex = 2;
			this.btnNewGame.Text = "&New Game";
			this.btnNewGame.UseVisualStyleBackColor = true;
			this.btnNewGame.Click += new System.EventHandler(this.BtnNewGame_Click);
			// 
			// gpPlayerColor
			// 
			this.gpPlayerColor.Controls.Add(this.optTraining);
			this.gpPlayerColor.Controls.Add(this.optBoth);
			this.gpPlayerColor.Controls.Add(this.optNeither);
			this.gpPlayerColor.Controls.Add(this.optX);
			this.gpPlayerColor.Controls.Add(this.optO);
			this.gpPlayerColor.Location = new System.Drawing.Point(578, 18);
			this.gpPlayerColor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.gpPlayerColor.Name = "gpPlayerColor";
			this.gpPlayerColor.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.gpPlayerColor.Size = new System.Drawing.Size(366, 105);
			this.gpPlayerColor.TabIndex = 0;
			this.gpPlayerColor.TabStop = false;
			this.gpPlayerColor.Text = "Human Player";
			// 
			// optBoth
			// 
			this.optBoth.AutoSize = true;
			this.optBoth.Location = new System.Drawing.Point(141, 29);
			this.optBoth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.optBoth.Name = "optBoth";
			this.optBoth.Size = new System.Drawing.Size(68, 24);
			this.optBoth.TabIndex = 3;
			this.optBoth.Text = "Both";
			this.optBoth.UseVisualStyleBackColor = true;
			// 
			// optNeither
			// 
			this.optNeither.AutoSize = true;
			this.optNeither.Location = new System.Drawing.Point(141, 65);
			this.optNeither.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.optNeither.Name = "optNeither";
			this.optNeither.Size = new System.Drawing.Size(85, 24);
			this.optNeither.TabIndex = 2;
			this.optNeither.Text = "Neither";
			this.optNeither.UseVisualStyleBackColor = true;
			this.optNeither.CheckedChanged += new System.EventHandler(this.OptNeither_CheckedChanged);
			// 
			// optX
			// 
			this.optX.AutoSize = true;
			this.optX.Checked = true;
			this.optX.Location = new System.Drawing.Point(9, 29);
			this.optX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.optX.Name = "optX";
			this.optX.Size = new System.Drawing.Size(45, 24);
			this.optX.TabIndex = 1;
			this.optX.TabStop = true;
			this.optX.Text = "&X";
			this.optX.UseVisualStyleBackColor = true;
			this.optX.CheckedChanged += new System.EventHandler(this.OptX_CheckedChanged);
			// 
			// optO
			// 
			this.optO.AutoSize = true;
			this.optO.Location = new System.Drawing.Point(9, 65);
			this.optO.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.optO.Name = "optO";
			this.optO.Size = new System.Drawing.Size(46, 24);
			this.optO.TabIndex = 0;
			this.optO.Text = "&O";
			this.optO.UseVisualStyleBackColor = true;
			this.optO.CheckedChanged += new System.EventHandler(this.OptO_CheckedChanged);
			// 
			// optTraining
			// 
			this.optTraining.AutoSize = true;
			this.optTraining.Location = new System.Drawing.Point(254, 29);
			this.optTraining.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.optTraining.Name = "optTraining";
			this.optTraining.Size = new System.Drawing.Size(90, 24);
			this.optTraining.TabIndex = 4;
			this.optTraining.Text = "Training";
			this.optTraining.UseVisualStyleBackColor = true;
			this.optTraining.CheckedChanged += new System.EventHandler(this.OptTraining_CheckedChanged);
			// 
			// frmTicTacToe
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ClientSize = new System.Drawing.Size(975, 1188);
			this.ControlBox = false;
			this.Controls.Add(this.GameContainer);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmTicTacToe";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "TicTacToe";
			this.Shown += new System.EventHandler(this.FrmTicTacToe_Shown);
			this.GameContainer.Panel1.ResumeLayout(false);
			this.GameContainer.Panel2.ResumeLayout(false);
			this.GameContainer.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.GameContainer)).EndInit();
			this.GameContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pctCursor)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pctCurColor)).EndInit();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.gpPlayerColor.ResumeLayout(false);
			this.gpPlayerColor.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.Timer tmMorph;
		private System.Windows.Forms.PictureBox pctCursor;
		private System.Windows.Forms.SplitContainer GameContainer;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Button btnNewGame;
		private System.Windows.Forms.GroupBox gpPlayerColor;
		private System.Windows.Forms.RadioButton optX;
		private System.Windows.Forms.RadioButton optO;
		private System.Windows.Forms.RadioButton optNeither;
		private System.Windows.Forms.RadioButton optBoth;
		private System.Windows.Forms.PictureBox pctCurColor;
		private System.Windows.Forms.Label lblWinner;
		private System.Windows.Forms.ToolStripStatusLabel lblPlaceholder;
		private System.Windows.Forms.ToolStripStatusLabel lblProgress;
		private System.Windows.Forms.RadioButton optTraining;
	}
}

