﻿namespace Shogi
{
    partial class PaginaIniziale
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaginaIniziale));
            button1 = new Button();
            label1 = new Label();
            button3 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Cursor = Cursors.Hand;
            button1.Font = new Font("Showcard Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(339, 203);
            button1.Name = "button1";
            button1.Size = new Size(221, 83);
            button1.TabIndex = 0;
            button1.Text = "GIOCA";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            button1.MouseEnter += button1_MouseEnter;
            button1.MouseLeave += button1_MouseLeave;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(169, 85);
            label1.Name = "label1";
            label1.Size = new Size(47, 20);
            label1.TabIndex = 1;
            label1.Text = "Shogi";
            label1.Click += label1_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Showcard Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button3.Location = new Point(339, 434);
            button3.Name = "button3";
            button3.Size = new Size(221, 83);
            button3.TabIndex = 4;
            button3.Text = "ESCI";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            button3.MouseEnter += button3_MouseEnter;
            button3.MouseLeave += button3_MouseLeave;
            // 
            // button2
            // 
            button2.Cursor = Cursors.Hand;
            button2.Font = new Font("Showcard Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(339, 316);
            button2.Name = "button2";
            button2.Size = new Size(221, 83);
            button2.TabIndex = 5;
            button2.Text = "GUIDA";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            button2.MouseEnter += button2_MouseEnter;
            button2.MouseLeave += button2_MouseLeave;
            // 
            // PaginaIniziale
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(853, 566);
            Controls.Add(button2);
            Controls.Add(button3);
            Controls.Add(label1);
            Controls.Add(button1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "PaginaIniziale";
            Text = "PaginaIniziale";
            Load += PaginaIniziale_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private Button button3;
        private Button button2;
    }
}