namespace Shogi {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            kubomawashi1 = new PictureBox();
            kubomawashi2 = new PictureBox();
            pbox_timer1 = new PictureBox();
            pbox_timer2 = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
            button1 = new Button();
            lbl_Min1 = new Label();
            lbl_Sec1 = new Label();
            lbl_Min2 = new Label();
            lbl_Sec2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kubomawashi1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kubomawashi2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbox_timer1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbox_timer2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Location = new Point(1037, 500);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(13, 13);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(241, 35);
            pictureBox2.Margin = new Padding(4, 3, 4, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(13, 13);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new Point(167, 163);
            pictureBox3.Margin = new Padding(4, 3, 4, 3);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(13, 13);
            pictureBox3.TabIndex = 2;
            pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.Location = new Point(507, 143);
            pictureBox4.Margin = new Padding(4, 3, 4, 3);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(13, 13);
            pictureBox4.TabIndex = 3;
            pictureBox4.TabStop = false;
            // 
            // kubomawashi1
            // 
            kubomawashi1.Location = new Point(180, 315);
            kubomawashi1.Margin = new Padding(4, 5, 4, 5);
            kubomawashi1.Name = "kubomawashi1";
            kubomawashi1.Size = new Size(143, 83);
            kubomawashi1.TabIndex = 4;
            kubomawashi1.TabStop = false;
            // 
            // kubomawashi2
            // 
            kubomawashi2.Location = new Point(491, 572);
            kubomawashi2.Margin = new Padding(4, 5, 4, 5);
            kubomawashi2.Name = "kubomawashi2";
            kubomawashi2.Size = new Size(143, 83);
            kubomawashi2.TabIndex = 5;
            kubomawashi2.TabStop = false;
            // 
            // pbox_timer1
            // 
            pbox_timer1.Location = new Point(111, 457);
            pbox_timer1.Margin = new Padding(4, 5, 4, 5);
            pbox_timer1.Name = "pbox_timer1";
            pbox_timer1.Size = new Size(104, 198);
            pbox_timer1.TabIndex = 6;
            pbox_timer1.TabStop = false;
            // 
            // pbox_timer2
            // 
            pbox_timer2.Location = new Point(241, 457);
            pbox_timer2.Margin = new Padding(4, 5, 4, 5);
            pbox_timer2.Name = "pbox_timer2";
            pbox_timer2.Size = new Size(104, 198);
            pbox_timer2.TabIndex = 7;
            pbox_timer2.TabStop = false;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Tick += timer_tick;
            // 
            // button1
            // 
            button1.Cursor = Cursors.Hand;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(161, 745);
            button1.Margin = new Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new Size(283, 93);
            button1.TabIndex = 8;
            button1.Text = "CAMBIO TURNO (temp)";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // lbl_Min1
            // 
            lbl_Min1.AutoSize = true;
            lbl_Min1.Location = new Point(389, 373);
            lbl_Min1.Margin = new Padding(4, 0, 4, 0);
            lbl_Min1.Name = "lbl_Min1";
            lbl_Min1.Size = new Size(59, 25);
            lbl_Min1.TabIndex = 9;
            lbl_Min1.Text = "label1";
            // 
            // lbl_Sec1
            // 
            lbl_Sec1.AutoSize = true;
            lbl_Sec1.Location = new Point(389, 418);
            lbl_Sec1.Margin = new Padding(4, 0, 4, 0);
            lbl_Sec1.Name = "lbl_Sec1";
            lbl_Sec1.Size = new Size(59, 25);
            lbl_Sec1.TabIndex = 10;
            lbl_Sec1.Text = "label2";
            // 
            // lbl_Min2
            // 
            lbl_Min2.AutoSize = true;
            lbl_Min2.Location = new Point(530, 198);
            lbl_Min2.Margin = new Padding(4, 0, 4, 0);
            lbl_Min2.Name = "lbl_Min2";
            lbl_Min2.Size = new Size(59, 25);
            lbl_Min2.TabIndex = 11;
            lbl_Min2.Text = "label3";
            // 
            // lbl_Sec2
            // 
            lbl_Sec2.AutoSize = true;
            lbl_Sec2.Location = new Point(530, 245);
            lbl_Sec2.Margin = new Padding(4, 0, 4, 0);
            lbl_Sec2.Name = "lbl_Sec2";
            lbl_Sec2.Size = new Size(59, 25);
            lbl_Sec2.TabIndex = 12;
            lbl_Sec2.Text = "label4";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1920, 1046);
            Controls.Add(lbl_Sec2);
            Controls.Add(lbl_Min2);
            Controls.Add(lbl_Sec1);
            Controls.Add(lbl_Min1);
            Controls.Add(button1);
            Controls.Add(pbox_timer2);
            Controls.Add(pbox_timer1);
            Controls.Add(kubomawashi2);
            Controls.Add(kubomawashi1);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "Form1";
            Text = "Shogi";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)kubomawashi1).EndInit();
            ((System.ComponentModel.ISupportInitialize)kubomawashi2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbox_timer1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbox_timer2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private PictureBox kubomawashi1;
        private PictureBox kubomawashi2;
        private PictureBox pbox_timer1;
        private PictureBox pbox_timer2;
        private System.Windows.Forms.Timer timer1;
        private Button button1;
        private Label lbl_Min1;
        private Label lbl_Sec1;
        private Label lbl_Min2;
        private Label lbl_Sec2;
    }
}