using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shogi
{
    public partial class FormMovimentiShogiban : Form
    {
        public FormMovimentiShogiban()
        {
            InitializeComponent();
        }
        public FormMovimentiShogiban(Image i)
        {
            InitializeComponent();
            BackgroundImageLayout = ImageLayout.Stretch;
            BackgroundImage = i;
        }

        bool pannelloCliccato = false;
        Panel[,] Tiles = new Panel[9, 9];
        Color TileColor = Color.FromArgb(238, 182, 115);
        int TILESIZE = 0;
        const int GRIDSIZE = 9;
        const int GRANDKOMA = 80;
        static protected string PERCORSOIMMAGINE = Application.StartupPath;
        System.Media.SoundPlayer sound_muoviKoma = new System.Media.SoundPlayer($"{PERCORSOIMMAGINE}/shogiPieces/extra/movingPiece.wav");
        (int, int) posizioneChiamante;
        Shogiban shogiban = new Shogiban();
        PrivateFontCollection fontCollection = new PrivateFontCollection();
        Font customFont;
        PictureBox pbox;
        Label nomeKoma;

        private void FormMovimentiShogiban_Load(object sender, EventArgs e)
        {
            TILESIZE = this.Height / GRIDSIZE + 3;
            Size = Screen.PrimaryScreen.WorkingArea.Size;
            WindowState = FormWindowState.Maximized;
            Tiles = new Panel[GRIDSIZE, GRIDSIZE];
            
            fontCollection.AddFontFile($@"{PERCORSOIMMAGINE}/shogiPieces/extra/movimentiFont.ttf");
            customFont = new Font(fontCollection.Families[0], 30 * (1 / Utilities.GetScreenScaleFactor(this)), FontStyle.Bold);


            generaShogiban();
            Utilities.generaKoma(this, new EventHandler(chiudi), new EventHandler(pedinaMezzo), GRANDKOMA, PERCORSOIMMAGINE, GRIDSIZE, TILESIZE);
            preparaSpiegazione();
        }

        private void generaShogiban() {
            for (int c = 0; c < GRIDSIZE; c++) {
                for (int r = 0; r < GRIDSIZE; r++) {
                    Panel Tile = new Panel {
                        Size = new Size(TILESIZE, TILESIZE),
                        Location = new Point(TILESIZE * c, TILESIZE * r),
                        BackColor = TileColor,
                        BorderStyle = BorderStyle.FixedSingle,
                        BackgroundImageLayout = ImageLayout.Stretch
                    };
                    Tile.Click += new EventHandler(Tile_Click);
                    Controls.Add(Tile);
                    Tiles[c, r] = Tile;
                }
            }
        }

        private void preparaSpiegazione()
        {
            //immagine di movimento
            pbox = new PictureBox();
            pbox.Size = new Size(190, 190);
            pbox.BorderStyle = BorderStyle.FixedSingle;
            pbox.BackgroundImageLayout = ImageLayout.Stretch;
            pbox.Location = new Point(this.Width - pbox.Width - 39, 30);
            pbox.BackColor = TileColor;
            Controls.Add(pbox);

            //nome koma selezionata
            nomeKoma = new Label();
            nomeKoma.Text = "Seleziona la Koma";
            nomeKoma.Size = new Size(500, 50);
            nomeKoma.Location = new Point(TILESIZE * GRIDSIZE + 30, 100);
            nomeKoma.Font = customFont;
            nomeKoma.BackColor = Color.Transparent;
            Controls.Add(nomeKoma);
        }

        private void Tile_Click(object sender, EventArgs e)
        {

            pannelloCliccato = !pannelloCliccato;
            Panel panel = (Panel)sender;

            if (pannelloCliccato)
            {
                    posizioneChiamante = Utilities.getRowColFromLocation(panel.Location, TILESIZE, 0, 0);
                    Koma koma = null;
                    try
                    {
                        koma = shogiban.getKoma(posizioneChiamante);
                    }
                    catch { }

                    if (koma != null)
                    {
                        Tiles[posizioneChiamante.Item1, posizioneChiamante.Item2].BackColor = Color.Red;
                        List<(int, int)> mosseRegolari = shogiban.calcolaMosseRegolari(koma);
                        foreach ((int, int) mossaRegolare in mosseRegolari)
                        {
                            int casellaDaEvidenziareX = koma.Posizione.Item1 + mossaRegolare.Item1;
                            int casellaDaEvidenziareY = koma.Posizione.Item2 + mossaRegolare.Item2;

                            Tiles[casellaDaEvidenziareX, casellaDaEvidenziareY].BackColor = Color.Yellow;
                        }
                    }

                }
            else
            {

                if (panel.BackColor == Color.Yellow)
                {
                    (int, int) nuovaPosizione = Utilities.getRowColFromLocation(panel.Location, TILESIZE, 0, 0);
                    Koma koma = shogiban.getKoma(posizioneChiamante);
                    koma.Posizione = nuovaPosizione;
                    shogiban.rimuoviKoma(posizioneChiamante);   //rimuovi koma  
                    shogiban.aggiungiKoma(koma);
                    panel.BackgroundImage = koma.Icona;
                    panel.BackgroundImageLayout = ImageLayout.Center;
                    Tiles[posizioneChiamante.Item1, posizioneChiamante.Item2].BackgroundImage = null;
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

        private void mostraCasella(Koma koma)
        {
            shogiban.aggiungiKoma(koma);
            Tiles[koma.Posizione.Item1, koma.Posizione.Item2].BackgroundImage = koma.Icona;
            Tiles[koma.Posizione.Item1, koma.Posizione.Item2].BackgroundImageLayout = ImageLayout.Center;
        }

        private void chiudi(object sender, EventArgs e)
        {
            Close();
        }

        private void pedinaMezzo(object sender, EventArgs e)
        {
            pannelloCliccato = false;
            ripulisciShogiban();
            Panel panel = (Panel)sender;
            Koma koma = (Koma)panel.Tag;
            if (panel.Name == "p") koma.promuovi();

            mostraCasella(koma);
            sound_muoviKoma.Play();
            string typeKoma = koma.GetType().Name;
            nomeKoma.Text = typeKoma;
            pbox.BackgroundImage = null;
            pbox.BackgroundImage = Image.FromFile(immagineMovimenti(koma));
        }

        private void ripulisciShogiban()
        {
            for(int i = 0; i<9 ; i++)
            {
                for (int j = 0; j < 9 ; j++)
                {
                    shogiban.rimuoviKoma((j, i));
                    Tiles[j,i].BackColor = TileColor;
                    Tiles[j, i].BackgroundImage = null;
                }
            }
        }

        private string immagineMovimenti(Koma koma) {
            string nome = koma.GetType().Name;
            if (!koma.Promossa) {
                switch (nome) {
                    case "Fuhyo":
                        return $"{PERCORSOIMMAGINE}/shogiPieces/movimenti/pedone.png";
                    case "Ginsho":
                        return $"{PERCORSOIMMAGINE}/shogiPieces/movimenti/genArg.png";
                    case "Kinsho":
                        return $"{PERCORSOIMMAGINE}/shogiPieces/movimenti/genOro.png";
                    case "Kakugyo":
                        return $"{PERCORSOIMMAGINE}/shogiPieces/movimenti/alfiere.png";
                    case "Hisha":
                        return $"{PERCORSOIMMAGINE}/shogiPieces/movimenti/torre.png";
                    case "Kyosha":
                        return $"{PERCORSOIMMAGINE}/shogiPieces/movimenti/lancia.png";
                    case "Keima":
                        return $"{PERCORSOIMMAGINE}/shogiPieces/movimenti/cavallo.png";
                    case "Osho":
                        return $"{PERCORSOIMMAGINE}/shogiPieces/movimenti/re.png";
                }
            } else {
                switch (nome) {
                    case "Fuhyo":
                        return $"{PERCORSOIMMAGINE}/shogiPieces/movimenti/genOro.png";
                    case "Ginsho":
                        return $"{PERCORSOIMMAGINE}/shogiPieces/movimenti/genOro.png";
                    case "Kakugyo":
                        return $"{PERCORSOIMMAGINE}/shogiPieces/movimenti/Palfiere.png";
                    case "Hisha":
                        return $"{PERCORSOIMMAGINE}/shogiPieces/movimenti/Ptorre.png";
                    case "Kyosha":
                        return $"{PERCORSOIMMAGINE}/shogiPieces/movimenti/genOro.png";
                    case "Keima":
                        return $"{PERCORSOIMMAGINE}/shogiPieces/movimenti/genOro.png";
                }
            }


            return "";
        }
    }
}
