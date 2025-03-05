// <copyright file="BlendPictureBox.cs" company="www.feucht.us">
// Copyright (c) 2018 www.feucht.us. All rights reserved.
// </copyright>
// <author>Jonathan Feucht</author>
// <date>1/21/2018</date>
// <summary>
//		Implements the blend picture box class. This class is inherited from the PictureBox
//		class, and has the added ability to create a blend of two different pictures.
// </summary> 

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace TicTacToe {
	/// <summary>A blend picture box.</summary>
	internal class BlendPictureBox : PictureBox {
		/// <summary>The first image.</summary>
		private Bitmap mImg1;
		/// <summary>The second image.</summary>
		private Bitmap mImg2;
		/// <summary>The rotation angle.</summary>
		private Single mRotate;
		/// <summary>The blend factor.</summary>
		private Single mBlend;
		/// <summary>True if object is empty.</summary>
		private Boolean mEmpty;

		/// <summary>Initializes a new instance of the TicTacToe.BlendPictureBox class.</summary>
		public BlendPictureBox() {
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
		}

		/// <summary>Gets or sets the rotation angle.</summary>
		/// <value>The rotation angle.</value>
		public Single RotationAngle {
			get => mRotate;
			set {
				mRotate = value;
				Invalidate();
			}
		}

		/// <summary>Gets or sets the image 1.</summary>
		/// <value>The image 1.</value>
		public Bitmap Image1 {
			get => mImg1;
			set {
				mImg1 = RotateImage(value, mRotate);
				Invalidate();
			}
		}

		/// <summary>Gets or sets the image 2.</summary>
		/// <value>The image 2.</value>
		public Bitmap Image2 {
			get => mImg2;
			set {
				mImg2 = RotateImage(value, mRotate);
				//mImg2 = value;
				Invalidate();
			}
		}

		/// <summary>Gets or sets the blend.</summary>
		/// <value>The blend.</value>
		public Single Blend {
			get => mBlend;
			set {
				mBlend = value;
				Invalidate();
			}
		}

		/// <summary>Gets or sets a value indicating whether the empty.</summary>
		/// <value>True if empty, false if not.</value>
		public Boolean Empty {
			get => mEmpty;
			set {
				mEmpty = value;
				Invalidate();
			}
		}

		/// <inheritdoc/>
		protected override void OnPaint(PaintEventArgs e) {
			if (mImg1 == null || mImg2 == null || mEmpty) {
				e.Graphics.FillRectangle(new SolidBrush(BackColor), new Rectangle(0, 0, Width, Height));
			} else {
				Rectangle rc = new(0, 0, Width, Height);
				ColorMatrix cm = new();
				ImageAttributes ia = new();

				if (mBlend >= 0) {
					cm.Matrix33 = mBlend;
					ia.SetColorMatrix(cm);
					e.Graphics.DrawImage(mImg2, rc, 0, 0, mImg2.Width, mImg2.Height, GraphicsUnit.Pixel, ia);
				} else {
					cm.Matrix33 = -mBlend;
					ia.SetColorMatrix(cm);
					e.Graphics.DrawImage(mImg1, rc, 0, 0, mImg1.Width, mImg1.Height, GraphicsUnit.Pixel, ia);
				}
			}
			base.OnPaint(e);
		}

		/// <summary>Rotate image.</summary>
		/// <param name="b">A Bitmap to process.</param>
		/// <param name="angle">The angle.</param>
		/// <returns>A Bitmap.</returns>
		private Bitmap RotateImage(Bitmap b, Single angle) {
			// Create a new empty bitmap to hold rotated image
			Bitmap returnBitmap = new(b.Width, b.Height);
			// Make a graphics object from the empty bitmap
			Graphics g = Graphics.FromImage(returnBitmap);
			Rectangle srect = new(0, 0, b.Width, b.Height);
			// Move rotation point to center of image
			g.TranslateTransform((Single)b.Width / 2, (Single)b.Height / 2);
			// Rotate
			g.RotateTransform(angle);
			// Move image back
			g.TranslateTransform(-(Single)b.Width / 2, -(Single)b.Height / 2);
			// Draw passed in image onto graphics object
			g.DrawImage(b, srect, srect, GraphicsUnit.Pixel);
			return returnBitmap;
		}
	}
}