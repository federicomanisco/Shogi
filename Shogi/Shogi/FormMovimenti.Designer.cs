namespace Shogi
{
    partial class FormMovimenti
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
            bott_torre = new Button();
            bott_alfiere = new Button();
            bott_genOro = new Button();
            bott_genArg = new Button();
            bott_cavallo = new Button();
            bott_lancia = new Button();
            bott_re = new Button();
            bott_pedone = new Panel();
            SuspendLayout();
            // 
            // bott_torre
            // 
            bott_torre.Location = new Point(45, 104);
            bott_torre.Name = "bott_torre";
            bott_torre.Size = new Size(94, 29);
            bott_torre.TabIndex = 1;
            bott_torre.Text = "button1";
            bott_torre.UseVisualStyleBackColor = true;
            // 
            // bott_alfiere
            // 
            bott_alfiere.Location = new Point(45, 173);
            bott_alfiere.Name = "bott_alfiere";
            bott_alfiere.Size = new Size(94, 29);
            bott_alfiere.TabIndex = 2;
            bott_alfiere.Text = "button1";
            bott_alfiere.UseVisualStyleBackColor = true;
            // 
            // bott_genOro
            // 
            bott_genOro.Location = new Point(45, 240);
            bott_genOro.Name = "bott_genOro";
            bott_genOro.Size = new Size(94, 29);
            bott_genOro.TabIndex = 3;
            bott_genOro.Text = "button1";
            bott_genOro.UseVisualStyleBackColor = true;
            // 
            // bott_genArg
            // 
            bott_genArg.Location = new Point(45, 295);
            bott_genArg.Name = "bott_genArg";
            bott_genArg.Size = new Size(94, 29);
            bott_genArg.TabIndex = 4;
            bott_genArg.Text = "button1";
            bott_genArg.UseVisualStyleBackColor = true;
            // 
            // bott_cavallo
            // 
            bott_cavallo.Location = new Point(41, 354);
            bott_cavallo.Name = "bott_cavallo";
            bott_cavallo.Size = new Size(94, 29);
            bott_cavallo.TabIndex = 5;
            bott_cavallo.Text = "button1";
            bott_cavallo.UseVisualStyleBackColor = true;
            // 
            // bott_lancia
            // 
            bott_lancia.Location = new Point(40, 407);
            bott_lancia.Name = "bott_lancia";
            bott_lancia.Size = new Size(94, 29);
            bott_lancia.TabIndex = 6;
            bott_lancia.Text = "button2";
            bott_lancia.UseVisualStyleBackColor = true;
            // 
            // bott_re
            // 
            bott_re.Location = new Point(180, 418);
            bott_re.Name = "bott_re";
            bott_re.Size = new Size(94, 29);
            bott_re.TabIndex = 7;
            bott_re.Text = "button1";
            bott_re.UseVisualStyleBackColor = true;
            // 
            // bott_pedone
            // 
            bott_pedone.Location = new Point(200, 30);
            bott_pedone.Name = "bott_pedone";
            bott_pedone.Size = new Size(250, 125);
            bott_pedone.TabIndex = 8;
            // 
            // FormMovimenti
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(801, 551);
            Controls.Add(bott_pedone);
            Controls.Add(bott_re);
            Controls.Add(bott_lancia);
            Controls.Add(bott_cavallo);
            Controls.Add(bott_genArg);
            Controls.Add(bott_genOro);
            Controls.Add(bott_alfiere);
            Controls.Add(bott_torre);
            Name = "FormMovimenti";
            Text = "FormMovimenti";
            Load += FormMovimenti_Load;
            ResumeLayout(false);
        }

        #endregion
        private Button bott_torre;
        private Button bott_alfiere;
        private Button bott_genOro;
        private Button bott_genArg;
        private Button bott_cavallo;
        private Button bott_lancia;
        private Button bott_re;
        private Panel bott_pedone;
    }
}