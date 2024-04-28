namespace Shogi
{
    partial class FormPartitaFinita
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
            label1 = new Label();
            pbox_sente = new PictureBox();
            pbox_gote = new PictureBox();
            pbox_crown = new PictureBox();
            button1 = new Button();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)pbox_sente).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbox_gote).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbox_crown).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 0;
            label1.Text = "label1";
            // 
            // pbox_sente
            // 
            pbox_sente.Location = new Point(26, 38);
            pbox_sente.Name = "pbox_sente";
            pbox_sente.Size = new Size(100, 50);
            pbox_sente.TabIndex = 16;
            pbox_sente.TabStop = false;
            // 
            // pbox_gote
            // 
            pbox_gote.Location = new Point(26, 104);
            pbox_gote.Name = "pbox_gote";
            pbox_gote.Size = new Size(100, 50);
            pbox_gote.TabIndex = 17;
            pbox_gote.TabStop = false;
            // 
            // pbox_crown
            // 
            pbox_crown.Location = new Point(165, 9);
            pbox_crown.Name = "pbox_crown";
            pbox_crown.Size = new Size(56, 42);
            pbox_crown.TabIndex = 18;
            pbox_crown.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(12, 180);
            button1.Name = "button1";
            button1.Size = new Size(170, 57);
            button1.TabIndex = 19;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Cursor = Cursors.Hand;
            button2.Font = new Font("Showcard Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(179, 270);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Size = new Size(193, 62);
            button2.TabIndex = 20;
            button2.Text = "MENU INIZIALE";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // FormPartitaFinita
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(584, 361);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(pbox_crown);
            Controls.Add(pbox_gote);
            Controls.Add(pbox_sente);
            Controls.Add(label1);
            Name = "FormPartitaFinita";
            Text = "FormPartitaFinita";
            Load += FormPartitaFinita_Load;
            ((System.ComponentModel.ISupportInitialize)pbox_sente).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbox_gote).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbox_crown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private PictureBox pbox_sente;
        private PictureBox pbox_gote;
        private PictureBox pbox_crown;
        private Button button1;
        private Button button2;
    }
}