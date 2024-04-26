﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shogi
{
    public partial class FormMovimenti : Form
    {

        string PERCORSOIMMAGINE = Application.StartupPath;

        public FormMovimenti()
        {
            InitializeComponent();
        }

        private void FormMovimenti_Load(object sender, EventArgs e)
        {
            this.Size = new Size(690, 600);

            ScriviLabel();
            DisegnaKoma();
            
            this.BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/extra/sfondo1.jpg");
        }

        private void DisegnaKoma()
        {
            Panel bott_pedone = new Panel();
            Panel bott_torre = new Panel();
            Panel bott_alfiere = new Panel();
            Panel bott_genOro = new Panel();
            Panel bott_genArg = new Panel();
            Panel bott_cavallo = new Panel();
            Panel bott_lancia = new Panel();
            Panel bott_re = new Panel();
            Panel bott_PROMtorre = new Panel();
            Panel bott_PROMalfiere = new Panel();
            Panel bott_PROMpedone = new Panel();
            Panel bott_PROMgenArg = new Panel();
            Panel bott_PROMcavallo = new Panel();
            Panel bott_PROMlancia = new Panel();

            bott_pedone.Size = new Size(60, 60);
            bott_torre.Size = new Size(60, 60);
            bott_alfiere.Size = new Size(60, 60);
            bott_genOro.Size = new Size(60, 60);
            bott_genArg.Size = new Size(60, 60);
            bott_lancia.Size = new Size(60, 60);
            bott_cavallo.Size = new Size(60, 60);
            bott_re.Size = new Size(60, 60);
            bott_PROMpedone.Size = new Size(60, 60);
            bott_PROMtorre.Size = new Size(60, 60);
            bott_PROMalfiere.Size = new Size(60, 60);
            bott_PROMgenArg.Size = new Size(60, 60);
            bott_PROMlancia.Size = new Size(60, 60);
            bott_PROMcavallo.Size = new Size(60, 60);

            bott_pedone.BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/pedone.png");
            bott_torre.BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/torre.png");
            bott_alfiere.BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/alfiere.png");
            bott_genOro.BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/generaleOro.png");
            bott_genArg.BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/generaleArgento.png");
            bott_lancia.BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/lancia.png");
            bott_cavallo.BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/cavallo.png");
            bott_re.BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/re.png");
            bott_PROMpedone.BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/Promossa/pedone.png");
            bott_PROMtorre.BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/Promossa/torre.png");
            bott_PROMalfiere.BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/Promossa/alfiere.png");
            bott_PROMgenArg.BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/Promossa/generaleArgento.png");
            bott_PROMlancia.BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/Promossa/lancia.png");
            bott_PROMcavallo.BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/Promossa/cavallo.png");

            bott_pedone.Cursor = Cursors.Hand;
            bott_torre.Cursor = Cursors.Hand;
            bott_alfiere.Cursor = Cursors.Hand;
            bott_genOro.Cursor = Cursors.Hand;
            bott_genArg.Cursor = Cursors.Hand;
            bott_cavallo.Cursor = Cursors.Hand;
            bott_lancia.Cursor = Cursors.Hand;
            bott_re.Cursor = Cursors.Hand;
            bott_PROMpedone.Cursor = Cursors.Hand;
            bott_PROMtorre.Cursor = Cursors.Hand;
            bott_PROMalfiere.Cursor = Cursors.Hand;
            bott_PROMgenArg.Cursor = Cursors.Hand;
            bott_PROMcavallo.Cursor = Cursors.Hand;
            bott_PROMlancia.Cursor = Cursors.Hand;

            bott_pedone.BackgroundImageLayout = ImageLayout.Stretch;
            bott_torre.BackgroundImageLayout = ImageLayout.Stretch;
            bott_alfiere.BackgroundImageLayout = ImageLayout.Stretch;
            bott_genOro.BackgroundImageLayout = ImageLayout.Stretch;
            bott_genArg.BackgroundImageLayout = ImageLayout.Stretch;
            bott_lancia.BackgroundImageLayout = ImageLayout.Stretch;
            bott_cavallo.BackgroundImageLayout = ImageLayout.Stretch;
            bott_re.BackgroundImageLayout = ImageLayout.Stretch;
            bott_PROMpedone.BackgroundImageLayout = ImageLayout.Stretch;
            bott_PROMtorre.BackgroundImageLayout = ImageLayout.Stretch;
            bott_PROMalfiere.BackgroundImageLayout = ImageLayout.Stretch;
            bott_PROMgenArg.BackgroundImageLayout = ImageLayout.Stretch;
            bott_PROMlancia.BackgroundImageLayout = ImageLayout.Stretch;
            bott_PROMcavallo.BackgroundImageLayout = ImageLayout.Stretch;

            bott_pedone.BackColor = Color.Transparent;
            bott_torre.BackColor = Color.Transparent;
            bott_alfiere.BackColor = Color.Transparent;
            bott_genOro.BackColor = Color.Transparent;
            bott_genArg.BackColor = Color.Transparent;
            bott_lancia.BackColor = Color.Transparent;
            bott_cavallo.BackColor = Color.Transparent;
            bott_re.BackColor = Color.Transparent;
            bott_PROMpedone.BackColor = Color.Transparent;
            bott_PROMtorre.BackColor = Color.Transparent;
            bott_PROMalfiere.BackColor = Color.Transparent;
            bott_PROMgenArg.BackColor = Color.Transparent;
            bott_PROMlancia.BackColor = Color.Transparent;
            bott_PROMcavallo.BackColor = Color.Transparent;

            bott_pedone.Text = null;
            bott_torre.Text = null;
            bott_alfiere.Text = null;
            bott_genOro.Text = null;
            bott_genArg.Text = null;
            bott_lancia.Text = null;
            bott_cavallo.Text = null;
            bott_re.Text = null;
            bott_PROMpedone.Text = null;
            bott_PROMtorre.Text = null;
            bott_PROMalfiere.Text = null;
            bott_PROMgenArg.Text = null;
            bott_PROMlancia.Text = null;
            bott_PROMcavallo.Text = null;

            bott_pedone.Location = new Point(50, 80);
            bott_torre.Location = new Point(220, 80);
            bott_alfiere.Location = new Point(390, 80);
            bott_genOro.Location = new Point(560, 80);
            bott_genArg.Location = new Point(50, 190);
            bott_lancia.Location = new Point(220, 190);
            bott_cavallo.Location = new Point(390, 190);
            bott_re.Location = new Point(560, 190);

            bott_PROMpedone.Location = new Point(110, 350);
            bott_PROMtorre.Location = new Point(300, 350);
            bott_PROMalfiere.Location = new Point(490, 350);
            bott_PROMgenArg.Location = new Point(110, 460);
            bott_PROMlancia.Location = new Point(300, 460);
            bott_PROMcavallo.Location = new Point(490, 460);

            this.Controls.Add(bott_pedone);
            this.Controls.Add(bott_alfiere);
            this.Controls.Add(bott_torre);
            this.Controls.Add(bott_genOro);
            this.Controls.Add(bott_genArg);
            this.Controls.Add(bott_cavallo);
            this.Controls.Add(bott_lancia);
            this.Controls.Add(bott_re);
            this.Controls.Add(bott_PROMpedone);
            this.Controls.Add(bott_PROMalfiere);
            this.Controls.Add(bott_PROMtorre);
            this.Controls.Add(bott_PROMgenArg);
            this.Controls.Add(bott_PROMcavallo);
            this.Controls.Add(bott_PROMlancia);
        }

        private void ScriviLabel()
        {
            Label lbl_MOVIMENTI = new Label();
            Label lbl_pedone = new Label();
            Label lbl_torre = new Label();
            Label lbl_alfiere = new Label();
            Label lbl_genOro = new Label();
            Label lbl_genArg = new Label();
            Label lbl_cavallo = new Label();
            Label lbl_lancia = new Label();
            Label lbl_re = new Label();
            Label lbl_PROMOSSE = new Label();
            Label lbl_Ppedone = new Label();
            Label lbl_Ptorre = new Label();
            Label lbl_Palfiere = new Label();
            Label lbl_PgenArg = new Label();
            Label lbl_Pcavallo = new Label();
            Label lbl_Plancia = new Label();

            lbl_MOVIMENTI.Text = "MOVIMENTI";
            lbl_pedone.Text = "Fuhyo";
            lbl_torre.Text = "Hisha";
            lbl_alfiere.Text = "Kakugyo";
            lbl_genOro.Text = "Kinsho";
            lbl_genArg.Text = "Ginsho";
            lbl_cavallo.Text = "Keima";
            lbl_lancia.Text = "Kyosha";
            lbl_re.Text = "Osho";
            lbl_PROMOSSE.Text = "PROMOSSE";
            lbl_Ppedone.Text = "Fuhyo";
            lbl_Ptorre.Text = "Hisha";
            lbl_Palfiere.Text = "Kakugyo";
            lbl_PgenArg.Text = "Ginsho";
            lbl_Pcavallo.Text = "Keima";
            lbl_Plancia.Text = "Kyosha";

            lbl_MOVIMENTI.Location = new Point(this.Width / 2 - 100, 30);
            lbl_pedone.Location = new Point(50, 60);
            lbl_torre.Location = new Point(220, 60);
            lbl_alfiere.Location = new Point(390, 60);
            lbl_genOro.Location = new Point(560, 60);
            lbl_genArg.Location = new Point(50, 170);
            lbl_cavallo.Location = new Point(220, 170);
            lbl_lancia.Location = new Point(390, 170);
            lbl_re.Location = new Point(560, 170);
            lbl_PROMOSSE.Location = new Point(this.Width / 2 - 100, 280);
            lbl_Ppedone.Location = new Point(115, 330);
            lbl_Ptorre.Location = new Point(305, 330);
            lbl_Palfiere.Location = new Point(488, 330);
            lbl_PgenArg.Location = new Point(110, 430);
            lbl_Pcavallo.Location = new Point(490, 430);
            lbl_Plancia.Location = new Point(300, 430);

            this.Controls.Add(lbl_MOVIMENTI);
            this.Controls.Add(lbl_pedone);
            this.Controls.Add(lbl_torre);
            this.Controls.Add(lbl_alfiere);
            this.Controls.Add(lbl_genOro);
            this.Controls.Add(lbl_genArg);
            this.Controls.Add(lbl_cavallo);
            this.Controls.Add(lbl_lancia);
            this.Controls.Add(lbl_re);
            this.Controls.Add(lbl_PROMOSSE);
            this.Controls.Add(lbl_Ppedone);
            this.Controls.Add(lbl_Ptorre);
            this.Controls.Add(lbl_Palfiere);
            this.Controls.Add(lbl_PgenArg);
            this.Controls.Add(lbl_Pcavallo);
            this.Controls.Add(lbl_Plancia);

            convertiFont();

        }

        private void convertiFont()
        {
            foreach (Control elemento in this.Controls)
            {
                if (elemento is Label)
                {
                    PrivateFontCollection fontCollection = new PrivateFontCollection();
                    fontCollection.AddFontFile($@"{PERCORSOIMMAGINE}/shogiPieces/extra/movimentiFont.ttf");
                    Font customFont = new Font(fontCollection.Families[0], 10, FontStyle.Bold);
                    elemento.BackColor = Color.Transparent;
                    elemento.Font = customFont;
                }
            }
        }

        private void mouse_enter(object sender, EventArgs e)
        {

        }
    }
}
