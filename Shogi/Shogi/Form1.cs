using System.CodeDom;

namespace Shogi {
    public partial class Form1: Form {
        public Form1() {
            InitializeComponent();
        }

        bool pannelloCliccato = false;
        Panel[,] Tiles;
        //84x84
        const int TILESIZE = 84;
        const int GRIDSIZE = 9;
        Color TileColor = Color.FromArgb(238, 182, 115);
        Shogiban shogiban = new Shogiban();

        private void Form1_Load(object sender, EventArgs e) {
            Location = new Point(0, 0);
            Size = Screen.PrimaryScreen.WorkingArea.Size;
            WindowState = FormWindowState.Maximized;

            Tiles = new Panel[GRIDSIZE, GRIDSIZE];
            for (int c = 0; c < GRIDSIZE; c++) {
                for (int r = 0; r < GRIDSIZE; r++) {
                    Panel Tile = new Panel {
                        Size = new Size(TILESIZE, TILESIZE),
                        //582 = margine orizzontale, 162 = margine verticale, 40 = altezza taskbar windows
                        Location = new Point(TILESIZE * c + 582, TILESIZE * r + 162 - 40),
                        BackColor = TileColor,
                        BorderStyle = BorderStyle.FixedSingle,
                    };
                    Tile.Controls.Add(new ListBox {
                        Enabled = false,
                        Visible = false
                    });
                    Tile.Click += new EventHandler(Tile_Click);
                    Controls.Add(Tile);
                    Tiles[c, r] = Tile;
                }
            }

            disegnaZonaPromozione(TileColor);
            scriviNumeriCaselle();
            generaPosizioneIniziale();
        }

        public (int, int) getRowColFromLocation(Point point) {
            return ((point.X - 582) / TILESIZE, (point.Y - 162 + 40) / TILESIZE);
        }

        private void disegnaZonaPromozione(Color colore) {
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

        private void scriviNumeriCaselle() {
            // 582+38 = Posizione orizzontale della scritta rispetto al bordo sinistro della scacchiera, 82 = altezza della scritta
            (int, int) puntoPartenza = (582 + 38, 82);

            for (int i = 0; i < 9; i++) {
                Label lbl = new Label();
                lbl.Location = new Point(puntoPartenza.Item1 + (i * TILESIZE) - 10, puntoPartenza.Item2);
                lbl.Text = (i + 1).ToString();
                lbl.Size = new Size(30, 30);
                lbl.Font = new Font("Arial", 18);
                lbl.AutoSize = true;
                Controls.Add(lbl);
            }

            for (int i = 0; i < 9; i++) {
                Label lbl = new Label();
                lbl.Location = new Point(puntoPartenza.Item1 + (TILESIZE * 9) - 30, puntoPartenza.Item2 + (i * TILESIZE) + 10 + 55);
                lbl.Text = Math.Abs((i - 9)).ToString();
                lbl.Size = new Size(30, 30);
                lbl.Font = new Font("Arial", 18);
                lbl.AutoSize = true;
                Controls.Add(lbl);
            }
        }

        private void mostraCasella(Koma koma) {
            shogiban.aggiungiKoma(koma);
            Tiles[koma.Posizione.Item1, koma.Posizione.Item2].BackgroundImage = koma.Icona;
            Tiles[koma.Posizione.Item1, koma.Posizione.Item2].BackgroundImageLayout = ImageLayout.Center;
            foreach (Control control in Tiles[koma.Posizione.Item1, koma.Posizione.Item2].Controls) {
                if (control.GetType() == typeof(ListBox)) {
                    ListBox lista = (ListBox) control;
                    lista.Items.Add(koma);
                }
            }
        }

        private void generaPosizioneIniziale() {
            //pedoni
            for (int i = 0; i < 9; i++) {
                Fuhyo fuhyo = new Fuhyo((i, 2), false);
                mostraCasella(fuhyo);
            }
            for (int i = 0; i < 9; i++) {
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

        private void Tile_Click(object sender, EventArgs e) {
            pannelloCliccato = !pannelloCliccato;

            if (pannelloCliccato) {
                Panel panel = (Panel)sender;
                (int, int) posizioneChiamante = getRowColFromLocation(panel.Location);
                Koma koma = null;
                foreach (Control control in Tiles[posizioneChiamante.Item1, posizioneChiamante.Item2].Controls) {
                    if (control.GetType() == typeof(ListBox)) {
                        try {
                            ListBox lista = (ListBox)control;
                            koma = (Koma)lista.Items[0];
                            Tiles[posizioneChiamante.Item1, posizioneChiamante.Item2].BackColor = Color.Red;
                        } catch {

                        }
                    }
                }

                if (koma != null) {
                    List<(int, int)> mosseRegolari = calcolaMosseRegolari(koma);
                    foreach ((int, int) mossaRegolare in mosseRegolari) {
                        int casellaDaEvidenziareX = koma.Posizione.Item1 + mossaRegolare.Item1;
                        int casellaDaEvidenziareY = koma.Posizione.Item2 + mossaRegolare.Item2;
                        
                        Tiles[casellaDaEvidenziareX, casellaDaEvidenziareY].BackColor = Color.Yellow;
                    }
                }
            } else {
                foreach (Panel tile in Tiles) {
                    if (tile.BackColor != TileColor) {
                        tile.BackColor = TileColor;
                    }
                }
            }
            
        }

        private List<(int, int)> calcolaMosseRegolari(Koma koma) {
            List<(int, int)> mosseRegolari = new List<(int, int)>();
            for (int i = 0; i < koma.MossePossibili.GetLength(0); i++) {
                int mossaX = koma.MossePossibili[i, 0];
                int mossaY = koma.MossePossibili[i, 1];
                (int, int) posizioneDaControllare = (koma.Posizione.Item1 + mossaX, koma.Posizione.Item2 + mossaY);
                if (shogiban.controllaPosizioneOutOfBounds(posizioneDaControllare)) {
                    if (koma.GetType() == typeof(Keima)) {
                        if (shogiban.controllaCasellaLibera(posizioneDaControllare, koma)) {
                            (int, int) mossaRegolare = (mossaX, mossaY);
                            mosseRegolari.Add(mossaRegolare);
                        }
                    } else {
                        if (!shogiban.pedinaNelMezzo(koma.Posizione, posizioneDaControllare)) {
                            (int, int) mossaRegolare = (mossaX, mossaY);
                            mosseRegolari.Add(mossaRegolare);
                        }
                    }
                }
            }
            return mosseRegolari;
        }
    }
}