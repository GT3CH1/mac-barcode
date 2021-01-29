using System;
using System.Drawing;
using System.Windows.Forms;
using OnBarcode.Barcode;

namespace MacBarcode
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
            TextBox textBox = new TextBox();
            PictureBox pbox = new PictureBox();
            pbox.Width = 500;
            pbox.Height = 200;
            
            textBox.Text = "test";
            GenerateBacode("test", "C:/Users/gcpease/test.png");
            Image img = Image.FromFile("C:/Users/gcpease/test.png");
            pbox.Image = img;
            pbox.Location = new Point(300, 150);
            this.Controls.Add(pbox);
        }

        #endregion
        private void GenerateBacode(string _data, string _filename)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            Image img = b.Encode(BarcodeLib.TYPE.CODE39, "5CD8155SX0", Color.Black, Color.White, 290, 120);
            img.Save(_filename);
        }
    }
}

