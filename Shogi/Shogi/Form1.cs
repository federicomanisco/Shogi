using System.CodeDom;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Shogi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            float fattoreScalaSchermo = GetScreenScaleFactor();
            float fattoreScalaSchermoInverso = 1 / fattoreScalaSchermo;

            Scale(new SizeF(fattoreScalaSchermoInverso, fattoreScalaSchermoInverso)); // scala i componenti della form in base alla scala dello schermo
        }

        float GetScreenScaleFactor()
        { //restituisce la scala dello schermo (100%, 125%, 150%, 175%) sapendo che 96DPI = 100%
            Graphics graphics = CreateGraphics();
            float dpiX = graphics.DpiX;
            graphics.Dispose();

            return dpiX / 96f;
        }

        bool pannelloCliccato = false;
        Panel[,] Tiles;
        //84x84
        const int TILESIZE = 84;
        const int GRIDSIZE = 9;
        Color TileColor = Color.FromArgb(238, 182, 115);
        Shogiban shogiban = new Shogiban();
        Kubomawashi kubomawashi_sfidante = new Kubomawashi(); //lo sfidante inizia sotto
        Kubomawashi kubomawashi_sfidato = new Kubomawashi();  //lo sfidato inizia sopra
        int kubomawashi_width = 300; //lunghezza lato kubomawashi, quadrato

        static protected string PERCORSOIMMAGINE = Application.StartupPath;
        int timer_width = 400;
        int timer_height = 240;
        int tempoMin = 10; //tempo di gioco per giocatore, minuti
        int tempoSec = 30; //tempo di gioco per giocatore, secondi
        bool turno = true;  //true Sente (muove x primo, generalmente lo sfidante), false Gote (lo sfidato)

        (int, int) posizioneChiamante;

        System.Media.SoundPlayer sound_muoviKoma = new System.Media.SoundPlayer($"{PERCORSOIMMAGINE}/shogiPieces/extra/movingPiece.wav");


        private void Form1_Load(object sender, EventArgs e)
        {
            Location = new Point(0, 0);
            Size = Screen.PrimaryScreen.WorkingArea.Size;
            WindowState = FormWindowState.Maximized;

            Tiles = new Panel[GRIDSIZE, GRIDSIZE];
            for (int c = 0; c < GRIDSIZE; c++)
            {
                for (int r = 0; r < GRIDSIZE; r++)
                {
                    Panel Tile = new Panel
                    {
                        Size = new Size(TILESIZE, TILESIZE),
                        //582 = margine orizzontale, 162 = margine verticale, 40 = altezza taskbar windows
                        Location = new Point(TILESIZE * c + 582, TILESIZE * r + 162 - 40),
                        BackColor = TileColor,
                        BorderStyle = BorderStyle.FixedSingle,
                    };
                    Tile.Click += new EventHandler(Tile_Click);
                    Controls.Add(Tile);
                    Tiles[c, r] = Tile;
                }
            }

            disegnaZonaPromozione(TileColor);
            scriviNumeriCaselle();
            generaPosizioneIniziale();
            disegnaKubomawashi(kubomawashi_width);
            disegnaTimer((timer_width, timer_height), tempoMin, tempoSec);
            this.BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/extra/woodenTable.jpg");
        }

        public (int, int) getRowColFromLocation(Point point)
        {
            return ((point.X - 582) / TILESIZE, (point.Y - 162 + 40) / TILESIZE);
        }

        private void disegnaZonaPromozione(Color colore)
        {
            Image sfondo = new Bitmap(10, 10);
            Graphics g = Graphics.FromImage(sfondo);
            Brush brush = new SolidBrush(Color.FromArgb(60, 60, 60));
            g.FillEllipse(brush, -1, -1, 11, 11);
            (int, int) puntoPartenza = (834 - 5, 374 - 5);


            pictureBox1.Image = sfondo;
            pictureBox1.BackColor = colore;
            pictureBox1.Location = new Point(puntoPartenza.Item1, puntoPartenza.Item2);
            pictureBox2.Image = sfondo;
            pictureBox2.BackColor = colore;
            pictureBox2.Location = new Point(puntoPartenza.Item1 + (TILESIZE * 3), puntoPartenza.Item2);
            pictureBox3.Image = sfondo;
            pictureBox3.BackColor = colore;
            pictureBox3.Location = new Point(puntoPartenza.Item1, puntoPartenza.Item2 + (TILESIZE * 3));
            pictureBox4.Image = sfondo;
            pictureBox4.BackColor = colore;
            pictureBox4.Location = new Point(puntoPartenza.Item1 + (TILESIZE * 3), puntoPartenza.Item2 + (TILESIZE * 3));

        }

        private void scriviNumeriCaselle()
        {
            // 582+38 = Posizione orizzontale della scritta rispetto al bordo sinistro della scacchiera, 82 = altezza della scritta
            (int, int) puntoPartenza = (582 + 38, 82);

            for (int i = 0; i < 9; i++)
            {
                Label lbl = new Label();
                lbl.Location = new Point(puntoPartenza.Item1 + (i * TILESIZE) - 10, puntoPartenza.Item2);
                lbl.Text = (i + 1).ToString();
                lbl.Size = new Size(30, 30);     //scala la grandezza del font in base alla scala dello schermo
                lbl.Font = new Font("Arial", 18 * (1 / GetScreenScaleFactor()));
                lbl.AutoSize = true;
                lbl.BackColor = Color.Transparent;
                Controls.Add(lbl);
            }

            for (int i = 0; i < 9; i++)
            {
                Label lbl = new Label();
                lbl.Location = new Point(puntoPartenza.Item1 + (TILESIZE * 9) - 30, puntoPartenza.Item2 + (i * TILESIZE) + 10 + 55);
                lbl.Text = Math.Abs((i - 9)).ToString();
                lbl.Size = new Size(30, 30);
                lbl.Font = new Font("Arial", 18 * (1 / GetScreenScaleFactor()));
                lbl.AutoSize = true;
                lbl.BackColor = Color.Transparent;
                Controls.Add(lbl);
            }
        }

        private void mostraCasella(Koma koma)
        {
            shogiban.aggiungiKoma(koma);
            Tiles[koma.Posizione.Item1, koma.Posizione.Item2].BackgroundImage = koma.Icona;
            Tiles[koma.Posizione.Item1, koma.Posizione.Item2].BackgroundImageLayout = ImageLayout.Center;
        }

        private void generaPosizioneIniziale()
        {
            //pedoni
            for (int i = 0; i < 9; i++)
            {
                Fuhyo fuhyo = new Fuhyo((i, 2), false);
                mostraCasella(fuhyo);
            }
            for (int i = 0; i < 9; i++)
            {
                Fuhyo fuhyo = new Fuhyo((i, 6), true);
                mostraCasella(fuhyo);
            }

            //Re
            Osho blackKing = new Osho((4, 0), false);
            Osho whiteKing = new Osho((4, 8), true);
            mostraCasella(blackKing);
            mostraCasella(whiteKing);


            //Lancieri
            Kyosha lanciereNero1 = new Kyosha((0, 0), false);
            Kyosha lanciereNero2 = new Kyosha((8, 0), false);
            Kyosha lanciereBianco1 = new Kyosha((0, 8), true);
            Kyosha lanciereBianco2 = new Kyosha((8, 8), true);
            mostraCasella(lanciereNero1);
            mostraCasella(lanciereNero2);
            mostraCasella(lanciereBianco1);
            mostraCasella(lanciereBianco2);

            //Cavalli
            Keima cavalloNero1 = new Keima((1, 0), false);
            Keima cavalloNero2 = new Keima((7, 0), false);
            Keima cavalloBianco1 = new Keima((1, 8), true);
            Keima cavalloBianco2 = new Keima((7, 8), true);
            mostraCasella(cavalloNero1);
            mostraCasella(cavalloNero2);
            mostraCasella(cavalloBianco1);
            mostraCasella(cavalloBianco2);

            //Generali Argento
            Ginsho generaleArgentoNero1 = new Ginsho((2, 0), false);
            Ginsho generaleArgentoNero2 = new Ginsho((6, 0), false);
            Ginsho generaleArgentoBianco1 = new Ginsho((2, 8), true);
            Ginsho generaleArgentoBianco2 = new Ginsho((6, 8), true);
            mostraCasella(generaleArgentoNero1);
            mostraCasella(generaleArgentoNero2);
            mostraCasella(generaleArgentoBianco1);
            mostraCasella(generaleArgentoBianco2);

            //Generali Oro
            Kinsho generaleOroNero1 = new Kinsho((3, 0), false);
            Kinsho generaleOroNero2 = new Kinsho((5, 0), false);
            Kinsho generaleOroBianco1 = new Kinsho((3, 8), true);
            Kinsho generaleOroBianco2 = new Kinsho((5, 8), true);
            mostraCasella(generaleOroNero1);
            mostraCasella(generaleOroNero2);
            mostraCasella(generaleOroBianco1);
            mostraCasella(generaleOroBianco2);

            //Alfieri
            Kakugyo alfiereNero = new Kakugyo((7, 1), false);
            Kakugyo alfiereBianco = new Kakugyo((1, 7), true);
            mostraCasella(alfiereNero);
            mostraCasella(alfiereBianco);

            //Torri
            Hisha torreNera = new Hisha((1, 1), false);
            Hisha torreBianca = new Hisha((7, 7), true);
            mostraCasella(torreNera);
            mostraCasella(torreBianca);
        }

        private void disegnaTimer((int, int) grandezza, int min, int sec)
        {
            int width = grandezza.Item1;
            int height = grandezza.Item2;


            pbox_timer1.Size = new Size(width, height);
            pbox_timer1.Location = new Point(60, 1080 - height - (1080 - (GRIDSIZE * TILESIZE)) / 2);
            pbox_timer1.BackColor = Color.Transparent;
            pbox_timer1.Image = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/extra/timerReal.png");
            pbox_timer1.SizeMode = PictureBoxSizeMode.StretchImage;

            pbox_timer2.Size = new Size(width, height);
            pbox_timer2.Location = new Point(1920 - width - 60, height - 160);
            pbox_timer2.BackColor = Color.Transparent;
            pbox_timer2.Image = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/extra/timerReal.png");
            pbox_timer2.SizeMode = PictureBoxSizeMode.StretchImage;

            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile($@"{PERCORSOIMMAGINE}/shogiPieces/extra/numberFont.ttf");

            lbl_Min1.Text = min.ToString();           //Scalo la grandezza del font in base alla scala dello schermo  
            lbl_Min1.Font = new Font(pfc.Families[0], 80 * (1 / GetScreenScaleFactor()));
            lbl_Min1.ForeColor = Color.FromArgb(255, 38, 42);
            lbl_Min1.BackColor = Color.FromArgb(40, 40, 40);

            lbl_Sec1.Text = sec.ToString();
            lbl_Sec1.Font = new Font(pfc.Families[0], 80 * (1 / GetScreenScaleFactor()));
            lbl_Sec1.ForeColor = Color.FromArgb(255, 38, 42);
            lbl_Sec1.BackColor = Color.FromArgb(40, 40, 40);
            lbl_Sec1.Location = new Point(60 + 225, 1080 - height - (1080 - (GRIDSIZE * TILESIZE)) + 227); // x +215 e y +233 per centrare il testo

            lbl_Min2.Text = min.ToString();
            lbl_Min2.Font = new Font(pfc.Families[0], 80 * (1 / GetScreenScaleFactor()));
            lbl_Min2.ForeColor = Color.FromArgb(255, 38, 42);
            lbl_Min2.BackColor = Color.FromArgb(40, 40, 40);


            lbl_Sec2.Text = sec.ToString();
            lbl_Sec2.Font = new Font(pfc.Families[0], 80 * (1 / GetScreenScaleFactor()));
            lbl_Sec2.ForeColor = Color.FromArgb(255, 38, 42);
            lbl_Sec2.BackColor = Color.FromArgb(40, 40, 40);
            lbl_Sec2.Location = new Point(1920 - width - 60 + 223, height - 160 + 62); // x +42 e y +233 per centrare il testo

            lbl_Min1.Location = new Point(60 + 62, 1080 - height - (1080 - (GRIDSIZE * TILESIZE)) + 227); // x +62 e y +233 per centrare il testo
            lbl_Min2.Location = new Point(1920 - width - 60 + 60, height - 160 + 62); // x +60 e y +67 per centrare il testo

        }

        private void disegnaKubomawashi(int width)//quello giusto
        {
            kubomawashi1.Location = new Point(100, 162 - 40);
            kubomawashi1.BackColor = TileColor;
            kubomawashi1.Size = new Size(width, width);
            kubomawashi1.BorderStyle = BorderStyle.FixedSingle;
            TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.Dock = DockStyle.Fill; 
            tableLayoutPanel1.BackColor = Color.Transparent; 
            kubomawashi1.Controls.Add(tableLayoutPanel1); 
            kubomawashi2.Location = new Point(1920 - 100 - width, 162 - 40 + TILESIZE * GRIDSIZE - width);
            kubomawashi2.BackColor = TileColor;
            kubomawashi2.Size = new Size(width, width);
            kubomawashi2.BorderStyle = BorderStyle.FixedSingle;
            TableLayoutPanel tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel2.Dock = DockStyle.Fill; 
            tableLayoutPanel2.BackColor = Color.Transparent; 
            kubomawashi2.Controls.Add(tableLayoutPanel2); 
        }
        private void inserisciPedinaNelKubomawashi(Koma koma)
        {
            Panel panelDaUsare = null;
            List<Koma> listaPannelli = null;
            if (koma.Colore) 
            {
                if (kubomawashi_sfidante.PedinaMangiata == null)
                {
                    kubomawashi_sfidante.PedinaMangiata = koma;
                    listaPannelli = kubomawashi_sfidante.list;
                    panelDaUsare = (Panel)kubomawashi1.Controls[0]; // Ottiene il pannello interno del kubomawashi1
                }
                else
                {
                    if (kubomawashi_sfidante.list.Count == 20) { MessageBox.Show("Il kubomawashi dello sfidante è pieno!"); return; }
                    listaPannelli = kubomawashi_sfidante.list;
                    panelDaUsare = (Panel)kubomawashi1.Controls[0]; 
                }
            }
            else 
            {
                if (kubomawashi_sfidato.PedinaMangiata == null)
                {
                    kubomawashi_sfidato.PedinaMangiata = koma;
                    listaPannelli = kubomawashi_sfidato.list;
                    panelDaUsare = (Panel)kubomawashi2.Controls[0]; // Ottiene il pannello interno del kubomawashi
                }
                else
                {
                    if (kubomawashi_sfidato.list.Count == 20) { MessageBox.Show("Il kubomawashi dello sfidato è pieno!"); return; }
                    listaPannelli = kubomawashi_sfidato.list;
                    panelDaUsare = (Panel)kubomawashi2.Controls[0]; 
                }
            }
            listaPannelli.Add(koma);
            Panel panelPedina = new Panel();
            panelPedina.Size = new Size(50, 50); 
            panelPedina.BackgroundImage = koma.Icona;
            panelPedina.BackgroundImageLayout = ImageLayout.Zoom; 
            panelPedina.BorderStyle = BorderStyle.FixedSingle; 
            panelDaUsare.Controls.Add(panelPedina);
        }

        private void Tile_Click(object sender, EventArgs e)
        {
            pannelloCliccato = !pannelloCliccato;
            Panel panel = (Panel)sender;
            if (pannelloCliccato)
            {
                posizioneChiamante = getRowColFromLocation(panel.Location);
                Koma koma = null;
                try
                {
                    koma = shogiban.getKoma(posizioneChiamante);
                }
                catch
                {

                }
                if (koma != null)
                {
                    if (koma.Colore == turno)
                    {
                        Tiles[posizioneChiamante.Item1, posizioneChiamante.Item2].BackColor = Color.Red;
                        List<(int, int)> mosseRegolari = calcolaMosseRegolari(koma);
                        foreach ((int, int) mossaRegolare in mosseRegolari)
                        {
                            int casellaDaEvidenziareX = koma.Posizione.Item1 + mossaRegolare.Item1;
                            int casellaDaEvidenziareY = koma.Posizione.Item2 + mossaRegolare.Item2;

                            Tiles[casellaDaEvidenziareX, casellaDaEvidenziareY].BackColor = Color.Yellow;
                        }
                    }
                }
            }
            else
            {
                if (panel.BackColor == Color.Yellow)
                {
                    (int, int) nuovaPosizione = getRowColFromLocation(panel.Location);  
                    Koma koma = shogiban.getKoma(posizioneChiamante);
                    koma.Posizione = nuovaPosizione;
                    Koma komamangiato = shogiban.getKoma(nuovaPosizione);
                    if (komamangiato!=null)//se c'è un'altra pedina nella nuova posizione
                    {           
                        inserisciPedinaNelKubomawashi(komamangiato);                      
                    }
                    shogiban.rimuoviKoma(posizioneChiamante);   //rimuovi koma  
                    shogiban.aggiungiKoma(koma);
                    panel.BackgroundImage = koma.Icona;
                    panel.BackgroundImageLayout = ImageLayout.Center;
                    Tiles[posizioneChiamante.Item1, posizioneChiamante.Item2].BackgroundImage = null;
                    turno = !turno;
                    sound_muoviKoma.Play();
                }
                foreach (Panel tile in Tiles)
                {
                    if (tile.BackColor != TileColor)
                    {
                        tile.BackColor = TileColor;
                    }
                }
            }
        }

        private List<(int, int)> calcolaMosseRegolari(Koma koma)
        {
            List<(int, int)> mosseRegolari = new List<(int, int)>();
            for (int i = 0; i < koma.MossePossibili.GetLength(0); i++)
            {
                int mossaX = koma.MossePossibili[i, 0];
                int mossaY = koma.MossePossibili[i, 1];
                (int, int) posizioneDaControllare = (koma.Posizione.Item1 + mossaX, koma.Posizione.Item2 + mossaY);
                if (shogiban.controllaPosizioneOutOfBounds(posizioneDaControllare))
                {
                    if (koma.GetType() == typeof(Keima))
                    {
                        if (shogiban.controllaCasellaLibera(posizioneDaControllare, koma))
                        {
                            (int, int) mossaRegolare = (mossaX, mossaY);
                            mosseRegolari.Add(mossaRegolare);
                        }
                    }
                    else
                    {
                        if (!shogiban.pedinaNelMezzo(koma.Posizione, posizioneDaControllare))
                        {
                            (int, int) mossaRegolare = (mossaX, mossaY);
                            mosseRegolari.Add(mossaRegolare);
                        }
                    }
                }
            }
            return mosseRegolari;
        }

        private void timer_tick(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                int min;
                int sec;
                string player;

                if (turno) player = "SFIDANTE";
                else player = "SFIDATO";

                if (turno)
                {
                    min = int.Parse(lbl_Min1.Text);
                    sec = int.Parse(lbl_Sec1.Text);
                }
                else
                {
                    min = int.Parse(lbl_Min2.Text);
                    sec = int.Parse(lbl_Sec2.Text);
                }

                if (sec == 1)
                {
                    if (min == 0)
                    {
                        if (turno) lbl_Sec1.Text = "0";
                        else lbl_Sec2.Text = "0";
                        timer1.Stop();
                        MessageBox.Show($"LO {player} PERDE PER TEMPO");
                        sec = 0;
                    }
                    else
                    {
                        sec = 60;
                        min--;
                    }
                }
                else sec--;


                if (turno)
                {
                    lbl_Min1.Text = min.ToString();
                    lbl_Sec1.Text = sec.ToString();
                }
                else
                {
                    lbl_Min2.Text = min.ToString();
                    lbl_Sec2.Text = sec.ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            turno = !turno;
        }
    }
}