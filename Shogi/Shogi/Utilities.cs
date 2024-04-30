using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shogi {
    public static class Utilities {
        public static void generaPosizioneIniziale(Shogiban shogiban, Panel[,] tiles) {
            //pedoni
            for (int i = 0; i < 9; i++) {
                Fuhyo fuhyo = new Fuhyo((i, 2), Koma.Giocatore.Gote);
                mostraCasella(shogiban, tiles, fuhyo);
            }
            for (int i = 0; i < 9; i++) {
                Fuhyo fuhyo = new Fuhyo((i, 6), Koma.Giocatore.Sente);
                mostraCasella(shogiban, tiles, fuhyo);
            }

            //Re
            Osho blackKing = new Osho((4, 0), Koma.Giocatore.Gote);
            Osho whiteKing = new Osho((4, 8), Koma.Giocatore.Sente);
            mostraCasella(shogiban, tiles, blackKing);
            mostraCasella(shogiban, tiles, whiteKing);


            //Lancieri
            Kyosha lanciereNero1 = new Kyosha((0, 0), Koma.Giocatore.Gote);
            Kyosha lanciereNero2 = new Kyosha((8, 0), Koma.Giocatore.Gote);
            Kyosha lanciereBianco1 = new Kyosha((0, 8), Koma.Giocatore.Sente);
            Kyosha lanciereBianco2 = new Kyosha((8, 8), Koma.Giocatore.Sente);
            mostraCasella(shogiban, tiles, lanciereNero1);
            mostraCasella(shogiban, tiles, lanciereNero2);
            mostraCasella(shogiban, tiles, lanciereBianco1);
            mostraCasella(shogiban, tiles, lanciereBianco2);

            //Cavalli
            Keima cavalloNero1 = new Keima((1, 0), Koma.Giocatore.Gote);
            Keima cavalloNero2 = new Keima((7, 0), Koma.Giocatore.Gote);
            Keima cavalloBianco1 = new Keima((1, 8), Koma.Giocatore.Sente);
            Keima cavalloBianco2 = new Keima((7, 8), Koma.Giocatore.Sente);
            mostraCasella(shogiban, tiles, cavalloNero1);
            mostraCasella(shogiban, tiles, cavalloNero2);
            mostraCasella(shogiban, tiles, cavalloBianco1);
            mostraCasella(shogiban, tiles, cavalloBianco2);

            //Generali Argento
            Ginsho generaleArgentoNero1 = new Ginsho((2, 0), Koma.Giocatore.Gote);
            Ginsho generaleArgentoNero2 = new Ginsho((6, 0), Koma.Giocatore.Gote);
            Ginsho generaleArgentoBianco1 = new Ginsho((2, 8), Koma.Giocatore.Sente);
            Ginsho generaleArgentoBianco2 = new Ginsho((6, 8), Koma.Giocatore.Sente);
            mostraCasella(shogiban, tiles, generaleArgentoNero1);
            mostraCasella(shogiban, tiles, generaleArgentoNero2);
            mostraCasella(shogiban, tiles, generaleArgentoBianco1);
            mostraCasella(shogiban, tiles, generaleArgentoBianco2);

            //Generali Oro
            Kinsho generaleOroNero1 = new Kinsho((3, 0), Koma.Giocatore.Gote);
            Kinsho generaleOroNero2 = new Kinsho((5, 0), Koma.Giocatore.Gote);
            Kinsho generaleOroBianco1 = new Kinsho((3, 8), Koma.Giocatore.Sente);
            Kinsho generaleOroBianco2 = new Kinsho((5, 8), Koma.Giocatore.Sente);
            mostraCasella(shogiban, tiles, generaleOroNero1);
            mostraCasella(shogiban, tiles, generaleOroNero2);
            mostraCasella(shogiban, tiles, generaleOroBianco1);
            mostraCasella(shogiban, tiles, generaleOroBianco2);

            //Alfieri
            Kakugyo alfiereNero = new Kakugyo((7, 1), Koma.Giocatore.Gote);
            Kakugyo alfiereBianco = new Kakugyo((1, 7), Koma.Giocatore.Sente);
            mostraCasella(shogiban, tiles, alfiereNero);
            mostraCasella(shogiban, tiles, alfiereBianco);

            //Torri
            Hisha torreNera = new Hisha((1, 1), Koma.Giocatore.Gote);
            Hisha torreBianca = new Hisha((7, 7), Koma.Giocatore.Sente);
            mostraCasella(shogiban, tiles, torreNera);
            mostraCasella(shogiban, tiles, torreBianca);
        }

        private static void mostraCasella(Shogiban shogiban, Panel[,] tiles, Koma koma) {
            shogiban.aggiungiKoma(koma);
            tiles[koma.Posizione.Item1, koma.Posizione.Item2].BackgroundImage = koma.Icona;
            tiles[koma.Posizione.Item1, koma.Posizione.Item2].BackgroundImageLayout = ImageLayout.Center;
        }

        public static void disegnaCaselle(Panel[,] tiles, Control.ControlCollection controls, int gridsize, int tilesize, int margineX, int margineY, Color tileColor, EventHandler tile_click) {
            for (int c = 0; c < gridsize; c++) {
                for (int r = 0; r < gridsize; r++) {
                    Panel Tile = new Panel {
                        Size = new Size(tilesize, tilesize),
                        //582 = margine orizzontale, 162 = margine verticale, 40 = altezza taskbar windows
                        Location = new Point(tilesize * c + margineX, tilesize * r + margineY),
                        BackColor = tileColor,
                        BorderStyle = BorderStyle.FixedSingle,
                    };
                    Tile.Click += new EventHandler(tile_click);
                    controls.Add(Tile);
                    tiles[c, r] = Tile;
                }
            }
        }

        public static void disegnaZonaPromozione(Color colore, PictureBox pictureBox1, PictureBox pictureBox2, PictureBox pictureBox3, PictureBox pictureBox4, int tilesize) {
            Image sfondo = new Bitmap(10, 10);
            Graphics g = Graphics.FromImage(sfondo);
            Brush brush = new SolidBrush(Color.FromArgb(60, 60, 60));
            g.FillEllipse(brush, -1, -1, 11, 11);
            (int, int) puntoPartenza = (834 - 5, 374 - 5);


            pictureBox1.Image = sfondo;
            pictureBox1.BackColor = colore;
            pictureBox1.Location = new Point(puntoPartenza.Item1, puntoPartenza.Item2);
            pictureBox1.Size = new Size(10, 10);
            pictureBox2.Image = sfondo;
            pictureBox2.BackColor = colore;
            pictureBox2.Location = new Point(puntoPartenza.Item1 + (tilesize * 3), puntoPartenza.Item2);
            pictureBox2.Size = new Size(10, 10);
            pictureBox3.Image = sfondo;
            pictureBox3.BackColor = colore;
            pictureBox3.Location = new Point(puntoPartenza.Item1, puntoPartenza.Item2 + (tilesize * 3));
            pictureBox3.Size = new Size(10, 10);
            pictureBox4.Image = sfondo;
            pictureBox4.BackColor = colore;
            pictureBox4.Location = new Point(puntoPartenza.Item1 + (tilesize * 3), puntoPartenza.Item2 + (tilesize * 3));
            pictureBox4.Size = new Size(10, 10);

        }

        public static void scriviNumeriCaselle(int tilesize, float scaleFactor, Control.ControlCollection controls) {
            // 582+38 = Posizione orizzontale della scritta rispetto al bordo sinistro della scacchiera, 82 = altezza della scritta
            (int, int) puntoPartenza = (582 + 38, 82);

            for (int i = 0; i < 9; i++) {
                Label lbl = new Label();
                lbl.Location = new Point(puntoPartenza.Item1 + (i * tilesize) - 10, puntoPartenza.Item2);
                lbl.Text = (i + 1).ToString();
                lbl.Size = new Size(30, 30);     //scala la grandezza del font in base alla scala dello schermo
                lbl.Font = new Font("Arial", 18 * (1 / scaleFactor));
                lbl.AutoSize = true;
                lbl.BackColor = Color.Transparent;
                controls.Add(lbl);
            }

            for (int i = 0; i < 9; i++) {
                Label lbl = new Label();
                lbl.Location = new Point(puntoPartenza.Item1 + (tilesize * 9) - 30, puntoPartenza.Item2 + (i * tilesize) + 10 + 55);
                lbl.Text = Math.Abs((i - 9)).ToString();
                lbl.Size = new Size(30, 30);
                lbl.Font = new Font("Arial", 18 * (1 / scaleFactor));
                lbl.AutoSize = true;
                lbl.BackColor = Color.Transparent;
                controls.Add(lbl);
            }
        }

        public static void disegnaKubomawashi(int width, PictureBox kubomawashi1, PictureBox kubomawashi2, Color tileColor, int tilesize, int gridsize)//quello giusto
        {
            kubomawashi1.Location = new Point(100, 162 - 40);
            kubomawashi1.BackColor = tileColor;
            kubomawashi1.Size = new Size(width, width);
            kubomawashi1.BorderStyle = BorderStyle.FixedSingle;
            TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.BackColor = Color.Transparent;
            tableLayoutPanel1.Name = "TableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;    //stabilisco un numero di righe per la tabella 1
            tableLayoutPanel1.ColumnCount = 5; //stabilisco un numero di colonne per la tabella 1
            kubomawashi1.Controls.Add(tableLayoutPanel1);

            kubomawashi2.Location = new Point(1920 - 100 - width, 162 - 40 + tilesize * gridsize - width);
            kubomawashi2.BackColor = tileColor;
            kubomawashi2.Size = new Size(width, width);
            kubomawashi2.BorderStyle = BorderStyle.FixedSingle;
            TableLayoutPanel tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.BackColor = Color.Transparent;
            tableLayoutPanel2.Name = "TableLayoutPanel2";
            tableLayoutPanel2.RowCount = 5;    //stabilisco un numero di righe per la tabella 2
            tableLayoutPanel2.ColumnCount = 5; //stabilisco un numero di colonne per la tabella 2
            kubomawashi2.Controls.Add(tableLayoutPanel2);
        }

        public static void disegnaTimer((int, int) grandezza, int min, int sec, int tilesize, int gridsize, PictureBox pbox_timer1, PictureBox pbox_timer2, string percorsoImmagine, Label lbl_Min1, Label lbl_Min2, Label lbl_Sec1, Label lbl_Sec2, float scaleFactor) {
            int width = grandezza.Item1;
            int height = grandezza.Item2;


            pbox_timer1.Size = new Size(width, height);
            pbox_timer1.Location = new Point(60, 1080 - height - (1080 - (gridsize * tilesize)) / 2);
            pbox_timer1.BackColor = Color.Transparent;
            pbox_timer1.Image = Image.FromFile($"{percorsoImmagine}/shogiPieces/extra/timerReal.png");
            pbox_timer1.SizeMode = PictureBoxSizeMode.StretchImage;

            pbox_timer2.Size = new Size(width, height);
            pbox_timer2.Location = new Point(1920 - width - 60, height - 160);
            pbox_timer2.BackColor = Color.Transparent;
            pbox_timer2.Image = Image.FromFile($"{percorsoImmagine}/shogiPieces/extra/timerReal.png");
            pbox_timer2.SizeMode = PictureBoxSizeMode.StretchImage;

            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile($@"{percorsoImmagine}/shogiPieces/extra/numberFont.ttf");

            lbl_Min1.Text = min.ToString();           //Scalo la grandezza del font in base alla scala dello schermo  
            lbl_Min1.Font = new Font(pfc.Families[0], 80 * (1 / scaleFactor));
            lbl_Min1.ForeColor = Color.FromArgb(255, 38, 42);
            lbl_Min1.BackColor = Color.FromArgb(40, 40, 40);

            lbl_Sec1.Text = sec.ToString();
            lbl_Sec1.Font = new Font(pfc.Families[0], 80 * (1 / scaleFactor));
            lbl_Sec1.ForeColor = Color.FromArgb(255, 38, 42);
            lbl_Sec1.BackColor = Color.FromArgb(40, 40, 40);
            lbl_Sec1.Location = new Point(60 + 225, 1080 - height - (1080 - (gridsize * tilesize)) + 227); // x +215 e y +233 per centrare il testo

            lbl_Min2.Text = min.ToString();
            lbl_Min2.Font = new Font(pfc.Families[0], 80 * (1 / scaleFactor));
            lbl_Min2.ForeColor = Color.FromArgb(255, 38, 42);
            lbl_Min2.BackColor = Color.FromArgb(40, 40, 40);


            lbl_Sec2.Text = sec.ToString();
            lbl_Sec2.Font = new Font(pfc.Families[0], 80 * (1 / scaleFactor));
            lbl_Sec2.ForeColor = Color.FromArgb(255, 38, 42);
            lbl_Sec2.BackColor = Color.FromArgb(40, 40, 40);
            lbl_Sec2.Location = new Point(1920 - width - 60 + 223, height - 160 + 62); // x +42 e y +233 per centrare il testo

            lbl_Min1.Location = new Point(60 + 62, 1080 - height - (1080 - (gridsize * tilesize)) + 227); // x +62 e y +233 per centrare il testo
            lbl_Min2.Location = new Point(1920 - width - 60 + 60, height - 160 + 62); // x +60 e y +67 per centrare il testo
        }

        public static float GetScreenScaleFactor(Form form) { //restituisce la scala dello schermo (100%, 125%, 150%, 175%) sapendo che 96DPI = 100%
            Graphics graphics = form.CreateGraphics();
            float dpiX = graphics.DpiX;
            graphics.Dispose();

            return dpiX / 96f;
        }

        public static (int, int) getRowColFromLocation(Point point, int tilesize, int margineX, int margineY) {
            return ((point.X - margineX) / tilesize, (point.Y - margineY) / tilesize);
        }

        public static void DisegnaKoma(EventHandler chiudi, EventHandler provaKoma, string percorsoImmagine, Form form, int distanzaBordo, int panelsize) {
            Panel bott_INDIETRO = new Panel();
            bott_INDIETRO.Click += chiudi;
            bott_INDIETRO.Name = "indietro";
            bott_INDIETRO.Size = new Size(50, 50);
            bott_INDIETRO.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/extra/turnBack.png");
            bott_INDIETRO.Cursor = Cursors.Hand;
            bott_INDIETRO.BackgroundImageLayout = ImageLayout.Stretch;
            bott_INDIETRO.Location = new Point(form.Width - bott_INDIETRO.Width - 70, 15);
            form.Controls.Add(bott_INDIETRO);

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

            bott_pedone.Size = new Size(80, 80);
            bott_torre.Size = new Size(80, 80);
            bott_alfiere.Size = new Size(80, 80);
            bott_genOro.Size = new Size(80, 80);
            bott_genArg.Size = new Size(80, 80);
            bott_lancia.Size = new Size(80, 80);
            bott_cavallo.Size = new Size(80, 80);
            bott_re.Size = new Size(80, 80);
            bott_PROMpedone.Size = new Size(80, 80);
            bott_PROMtorre.Size = new Size(80, 80);
            bott_PROMalfiere.Size = new Size(80, 80);
            bott_PROMgenArg.Size = new Size(80, 80);
            bott_PROMlancia.Size = new Size(80, 80);
            bott_PROMcavallo.Size = new Size(80, 80);

            bott_pedone.Location = new Point(50 + distanzaBordo, 105 + 50);
            bott_torre.Location = new Point(220 + distanzaBordo, 105 + 50);
            bott_alfiere.Location = new Point(390 + distanzaBordo, 105 + 50);
            bott_genOro.Location = new Point(560 + distanzaBordo, 105 + 50);
            bott_genArg.Location = new Point(50 + distanzaBordo, 215 + 100);
            bott_lancia.Location = new Point(220 + distanzaBordo, 215 + 100);
            bott_cavallo.Location = new Point(390 + distanzaBordo, 215 + 100);
            bott_re.Location = new Point(560 + distanzaBordo, 215 + 100);
            bott_PROMpedone.Location = new Point(110 + distanzaBordo, 400 + 50 + 130);
            bott_PROMtorre.Location = new Point(300 + distanzaBordo, 400 + 50 + 130);
            bott_PROMalfiere.Location = new Point(490 + distanzaBordo, 400 + 50 + 130);
            bott_PROMgenArg.Location = new Point(110 + distanzaBordo, 510 + 50 + 180);
            bott_PROMlancia.Location = new Point(300 + distanzaBordo, 510 + 50 + 180);
            bott_PROMcavallo.Location = new Point(490 + distanzaBordo, 510 + 50 + 180);

            bott_pedone.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/pedone.png");
            bott_torre.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/torre.png");
            bott_alfiere.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/alfiere.png");
            bott_genOro.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/generaleOro.png");
            bott_genArg.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/generaleArgento.png");
            bott_lancia.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/lancia.png");
            bott_cavallo.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/cavallo.png");
            bott_re.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/re.png");
            bott_PROMpedone.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/Promossa/pedone.png");
            bott_PROMtorre.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/Promossa/torre.png");
            bott_PROMalfiere.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/Promossa/alfiere.png");
            bott_PROMgenArg.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/Promossa/generaleArgento.png");
            bott_PROMlancia.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/Promossa/lancia.png");
            bott_PROMcavallo.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/Promossa/cavallo.png");

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

            form.Controls.Add(bott_pedone);
            form.Controls.Add(bott_torre);
            form.Controls.Add(bott_alfiere);
            form.Controls.Add(bott_genOro);
            form.Controls.Add(bott_genArg);
            form.Controls.Add(bott_cavallo);
            form.Controls.Add(bott_lancia);
            form.Controls.Add(bott_re);
            form.Controls.Add(bott_PROMtorre);
            form.Controls.Add(bott_PROMpedone);
            form.Controls.Add(bott_PROMalfiere);
            form.Controls.Add(bott_PROMgenArg);
            form.Controls.Add(bott_PROMcavallo);
            form.Controls.Add(bott_PROMlancia);

            foreach (Control panel in form.Controls) {
                if (panel is Panel && panel.Name != "indietro") {
                    panel.Click += provaKoma;
                }
            }
        }

        public static void ScriviLabel(Form form, string percorsoImmagine) {
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

            lbl_MOVIMENTI.Text = "KOMA";
            lbl_pedone.Text = "FUHYO";
            lbl_torre.Text = "HISHA";
            lbl_alfiere.Text = "KAKUGYO";
            lbl_genOro.Text = "KINSHO";
            lbl_genArg.Text = "GINSHO";
            lbl_cavallo.Text = "KEIMA";
            lbl_lancia.Text = "KYOSHA";
            lbl_re.Text = "OSHO";
            lbl_PROMOSSE.Text = "PROMOSSE";
            lbl_Ppedone.Text = "FUHYO";
            lbl_Ptorre.Text = "HISHA";
            lbl_Palfiere.Text = "KAKUGYO";
            lbl_PgenArg.Text = "GINSHO";
            lbl_Pcavallo.Text = "KEIMA";
            lbl_Plancia.Text = "KYOSHA";

            int distanzaBordo = form.Width / 2 - 610 / 2 - 39;
            lbl_MOVIMENTI.Location = new Point(form.Width / 2 - 110, 30);
            lbl_pedone.Location = new Point(55 + distanzaBordo - 20, 85 + 40); //+5
            lbl_torre.Location = new Point(225 + distanzaBordo - 15, 85 + 40); //+5
            lbl_alfiere.Location = new Point(388 + distanzaBordo - 30, 85 + 40); //-2
            lbl_genOro.Location = new Point(563 + distanzaBordo - 20, 85 + 40); //+3
            lbl_genArg.Location = new Point(53 + distanzaBordo - 20, 195 + 90); //+3
            lbl_cavallo.Location = new Point(394 + distanzaBordo - 10, 195 + 90); //+4
            lbl_lancia.Location = new Point(222 + distanzaBordo - 20, 195 + 90); //+2
            lbl_re.Location = new Point(560 + distanzaBordo - 7, 195 + 90);
            lbl_PROMOSSE.Location = new Point(form.Width / 2 - 150, 305 + 50 + 100);
            lbl_Ppedone.Location = new Point(115 + distanzaBordo - 20, 380 + 50 + 120);
            lbl_Ptorre.Location = new Point(305 + distanzaBordo - 15, 380 + 50 + 120);
            lbl_Palfiere.Location = new Point(488 + distanzaBordo - 30, 380 + 50 + 120);
            lbl_PgenArg.Location = new Point(110 + distanzaBordo - 20, 490 + 50 + 170);
            lbl_Pcavallo.Location = new Point(494 + distanzaBordo - 10, 490 + 50 + 170);
            lbl_Plancia.Location = new Point(302 + distanzaBordo - 20, 490 + 50 + 170);

            form.Controls.Add(lbl_MOVIMENTI);
            form.Controls.Add(lbl_pedone);
            form.Controls.Add(lbl_torre);
            form.Controls.Add(lbl_alfiere);
            form.Controls.Add(lbl_genOro);
            form.Controls.Add(lbl_genArg);
            form.Controls.Add(lbl_cavallo);
            form.Controls.Add(lbl_lancia);
            form.Controls.Add(lbl_re);
            form.Controls.Add(lbl_PROMOSSE);
            form.Controls.Add(lbl_Ppedone);
            form.Controls.Add(lbl_Ptorre);
            form.Controls.Add(lbl_Palfiere);
            form.Controls.Add(lbl_PgenArg);
            form.Controls.Add(lbl_Pcavallo);
            form.Controls.Add(lbl_Plancia);

            convertiFont(form, percorsoImmagine);
            PrivateFontCollection fontCollection = new PrivateFontCollection();
            fontCollection.AddFontFile($@"{percorsoImmagine}/shogiPieces/extra/MISTV___.ttf");
            lbl_MOVIMENTI.Font = new Font(fontCollection.Families[0], 40 * (1 / GetScreenScaleFactor(form)), FontStyle.Bold);
            lbl_PROMOSSE.Font = new Font(fontCollection.Families[0], 40 * (1 / GetScreenScaleFactor(form)), FontStyle.Bold);
            lbl_MOVIMENTI.Size = new Size(400, 60);
            lbl_PROMOSSE.Size = new Size(400, 60);
        }

        private static void convertiFont(Form form, string percorsoImmagine) {
            foreach (Control elemento in form.Controls) {
                if (elemento is Label) {
                    PrivateFontCollection fontCollection = new PrivateFontCollection();
                    fontCollection.AddFontFile($@"{percorsoImmagine}/shogiPieces/extra/movimentiFont.ttf");
                    Font customFont = new Font(fontCollection.Families[0], 20 * (1 / Utilities.GetScreenScaleFactor(form)), FontStyle.Bold);
                    elemento.BackColor = Color.Transparent;
                    elemento.Font = customFont;
                    elemento.Size = new Size(150, 30);
                }
            }
        }

        public static void generaKoma(Form form, EventHandler chiudi, EventHandler pedinaMezzo, int grandezzaImmagine, string percorsoImmagine, int gridsize, int tilesize) {

            Panel bott_INDIETRO = new Panel();
            bott_INDIETRO.Click += chiudi;

            Panel bott_pedone = new Panel { Tag = new Fuhyo((4, 6), Koma.Giocatore.Sente) };
            Panel bott_torre = new Panel() { Tag = new Hisha((4, 6), Koma.Giocatore.Sente) };
            Panel bott_alfiere = new Panel() { Tag = new Kakugyo((4, 6), Koma.Giocatore.Sente) };
            Panel bott_genOro = new Panel() { Tag = new Kinsho((4, 6), Koma.Giocatore.Sente) };
            Panel bott_genArg = new Panel() { Tag = new Ginsho((4, 6), Koma.Giocatore.Sente) };
            Panel bott_cavallo = new Panel() { Tag = new Keima((4, 6), Koma.Giocatore.Sente) };
            Panel bott_lancia = new Panel() { Tag = new Kyosha((4, 6), Koma.Giocatore.Sente) };
            Panel bott_re = new Panel() { Tag = new Osho((4, 6), Koma.Giocatore.Sente) };
            Panel bott_PROMtorre = new Panel() { Tag = new Hisha((4, 6), Koma.Giocatore.Sente) };
            Panel bott_PROMalfiere = new Panel() { Tag = new Kakugyo((4, 6), Koma.Giocatore.Sente) };
            Panel bott_PROMpedone = new Panel() { Tag = new Fuhyo((4, 6), Koma.Giocatore.Sente) };
            Panel bott_PROMgenArg = new Panel() { Tag = new Ginsho((4, 6), Koma.Giocatore.Sente) };
            Panel bott_PROMcavallo = new Panel() { Tag = new Keima((4, 6), Koma.Giocatore.Sente) };
            Panel bott_PROMlancia = new Panel() { Tag = new Kyosha((4, 6), Koma.Giocatore.Sente) };

            List<Panel> komaNormali = new List<Panel> { bott_pedone, bott_torre, bott_alfiere, bott_genOro, bott_genArg, bott_cavallo, bott_lancia, bott_re };
            List<Panel> komaPromosse = new List<Panel> { bott_PROMpedone, bott_PROMtorre, bott_PROMalfiere, bott_PROMgenArg, bott_PROMcavallo, bott_PROMlancia };

            bott_PROMpedone.Name = "p";
            bott_PROMtorre.Name = "p";
            bott_PROMalfiere.Name = "p";
            bott_PROMgenArg.Name = "p";
            bott_PROMlancia.Name = "p";
            bott_PROMcavallo.Name = "p";

            bott_INDIETRO.Size = new Size(70, 70);
            bott_pedone.Size = new Size(grandezzaImmagine, grandezzaImmagine);
            bott_torre.Size = new Size(grandezzaImmagine, grandezzaImmagine);
            bott_alfiere.Size = new Size(grandezzaImmagine, grandezzaImmagine);
            bott_genOro.Size = new Size(grandezzaImmagine, grandezzaImmagine);
            bott_genArg.Size = new Size(grandezzaImmagine, grandezzaImmagine);
            bott_lancia.Size = new Size(grandezzaImmagine, grandezzaImmagine);
            bott_cavallo.Size = new Size(grandezzaImmagine, grandezzaImmagine);
            bott_re.Size = new Size(grandezzaImmagine, grandezzaImmagine);
            bott_PROMpedone.Size = new Size(grandezzaImmagine, grandezzaImmagine);
            bott_PROMtorre.Size = new Size(grandezzaImmagine, grandezzaImmagine);
            bott_PROMalfiere.Size = new Size(grandezzaImmagine, grandezzaImmagine);
            bott_PROMgenArg.Size = new Size(grandezzaImmagine, grandezzaImmagine);
            bott_PROMlancia.Size = new Size(grandezzaImmagine, grandezzaImmagine);
            bott_PROMcavallo.Size = new Size(grandezzaImmagine, grandezzaImmagine);

            bott_INDIETRO.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/extra/turnBack.png");
            bott_pedone.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/pedone.png");
            bott_torre.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/torre.png");
            bott_alfiere.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/alfiere.png");
            bott_genOro.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/generaleOro.png");
            bott_genArg.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/generaleArgento.png");
            bott_lancia.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/lancia.png");
            bott_cavallo.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/cavallo.png");
            bott_re.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/re.png");
            bott_PROMpedone.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/Promossa/pedone.png");
            bott_PROMtorre.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/Promossa/torre.png");
            bott_PROMalfiere.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/Promossa/alfiere.png");
            bott_PROMgenArg.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/Promossa/generaleArgento.png");
            bott_PROMlancia.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/Promossa/lancia.png");
            bott_PROMcavallo.BackgroundImage = Image.FromFile($"{percorsoImmagine}/shogiPieces/Promossa/cavallo.png");

            bott_INDIETRO.Cursor = Cursors.Hand;
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

            bott_INDIETRO.BackgroundImageLayout = ImageLayout.Stretch;
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

            bott_INDIETRO.BackColor = Color.Transparent;
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

            int posInizNorm = gridsize * tilesize + 150;
            int posInizProm = gridsize * tilesize + 150 + ((grandezzaImmagine * 4 + 110 * 3) / 4 - grandezzaImmagine - 10);
            int cont = 0;

            foreach (Panel panel in komaNormali) {
                if (cont < 4) {
                    panel.Location = new Point(posInizNorm + cont * 140, 350);
                } else panel.Location = new Point(posInizNorm + (cont - 4) * 140, 460);
                cont++;
            }
            cont = 0;
            foreach (Panel panel in komaPromosse) {
                if (cont < 3) {
                    panel.Location = new Point(posInizProm + cont * 140, 570);
                } else panel.Location = new Point(posInizProm + (cont - 3) * 140, 680);
                cont++;
            }
            bott_INDIETRO.Location = new Point(form.Width - 39 - 20 - bott_INDIETRO.Width, form.Height - 39 - 20 - bott_INDIETRO.Height);

            bott_pedone.Click += pedinaMezzo;
            bott_torre.Click += pedinaMezzo;
            bott_alfiere.Click += pedinaMezzo;
            bott_genOro.Click += pedinaMezzo;
            bott_genArg.Click += pedinaMezzo;
            bott_lancia.Click += pedinaMezzo;
            bott_cavallo.Click += pedinaMezzo;
            bott_re.Click += pedinaMezzo;
            bott_PROMpedone.Click += pedinaMezzo;
            bott_PROMtorre.Click += pedinaMezzo;
            bott_PROMalfiere.Click += pedinaMezzo;
            bott_PROMgenArg.Click += pedinaMezzo;
            bott_PROMlancia.Click += pedinaMezzo;
            bott_PROMcavallo.Click += pedinaMezzo;

            form.Controls.Add(bott_INDIETRO);
            form.Controls.Add(bott_pedone);
            form.Controls.Add(bott_alfiere);
            form.Controls.Add(bott_torre);
            form.Controls.Add(bott_genOro);
            form.Controls.Add(bott_genArg);
            form.Controls.Add(bott_cavallo);
            form.Controls.Add(bott_lancia);
            form.Controls.Add(bott_re);
            form.Controls.Add(bott_PROMpedone);
            form.Controls.Add(bott_PROMalfiere);
            form.Controls.Add(bott_PROMtorre);
            form.Controls.Add(bott_PROMgenArg);
            form.Controls.Add(bott_PROMcavallo);
            form.Controls.Add(bott_PROMlancia);

        }
    }
}
