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

        bool pannelloCliccato = false;
        Panel[,] Tiles;
        Color TileColor = Color.FromArgb(238, 182, 115);
        const int TILESIZE = 84;
        const int GRIDSIZE = 9;
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
            this.Size = new Size(GRIDSIZE * TILESIZE + 500, GRIDSIZE * TILESIZE + 39);
            Tiles = new Panel[GRIDSIZE, GRIDSIZE];
            this.BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/extra/sfondo1.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            fontCollection.AddFontFile($@"{PERCORSOIMMAGINE}/shogiPieces/extra/movimentiFont.ttf");
            customFont = new Font(fontCollection.Families[0], 30 * (1 / GetScreenScaleFactor()), FontStyle.Bold);

            generaShogiban();
            generaKoma();
            preparaSpiegazione();
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
            this.Controls.Add(pbox);

            //nome koma selezionata
            nomeKoma = new Label();
            nomeKoma.Text = String.Empty;
            nomeKoma.Size = new Size(300, 50);
            nomeKoma.Location = new Point(TILESIZE * GRIDSIZE + 30, 100);
            nomeKoma.Font = customFont;
            nomeKoma.BackColor = Color.Transparent;
            this.Controls.Add(nomeKoma);

        }

        private void generaShogiban()
        {
            //genera shogiban
            for (int c = 0; c < GRIDSIZE; c++)
            {
                for (int r = 0; r < GRIDSIZE; r++)
                {
                    Panel Tile = new Panel
                    {
                        Size = new Size(TILESIZE, TILESIZE),
                        Location = new Point(TILESIZE * c, TILESIZE * r),
                        BackColor = TileColor,
                        BorderStyle = BorderStyle.FixedSingle,
                        BackgroundImageLayout = ImageLayout.Stretch
                    };
                Tile.Click += new EventHandler(Tile_Click);
                this.Controls.Add(Tile);
                Tiles[c, r] = Tile;
                }
            } //fine generazione shogiban
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

        public (int, int) getRowColFromLocation(Point point)
        {
            return ((point.X) / TILESIZE, (point.Y) / TILESIZE);
            float fattoreScalaSchermo = GetScreenScaleFactor();
            float fattoreScalaSchermoInverso = 1 / fattoreScalaSchermo;

            Scale(new SizeF(fattoreScalaSchermoInverso, fattoreScalaSchermoInverso));
        }
        float GetScreenScaleFactor()
        { //restituisce la scala dello schermo (100%, 125%, 150%, 175%) sapendo che 96DPI = 100%
            Graphics graphics = CreateGraphics();
            float dpiX = graphics.DpiX;
            graphics.Dispose();

            return dpiX / 96f;
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
                    catch { }

                    if (koma != null)
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
            else
            {

                if (panel.BackColor == Color.Yellow)
                {
                    (int, int) nuovaPosizione = getRowColFromLocation(panel.Location);
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

        private void generaKoma()
        {

            Panel bott_INDIETRO = new Panel();
            bott_INDIETRO.Click += new EventHandler(chiudi);

            Panel bott_pedone = new Panel { Tag = new Fuhyo((4, 6), true) };
            Panel bott_torre = new Panel() { Tag = new Hisha((4, 6), true) };
            Panel bott_alfiere = new Panel() { Tag = new Kakugyo((4, 6), true) };
            Panel bott_genOro = new Panel() { Tag = new Kinsho((4, 6), true) };
            Panel bott_genArg = new Panel() { Tag = new Ginsho((4, 6), true) };
            Panel bott_cavallo = new Panel() { Tag = new Keima((4, 6), true) };
            Panel bott_lancia = new Panel() { Tag = new Kyosha((4, 6), true) };
            Panel bott_re = new Panel() { Tag = new Osho((4, 6), true) };
            Panel bott_PROMtorre = new Panel() { Tag = new Hisha((4, 6), true) };
            Panel bott_PROMalfiere = new Panel() { Tag = new Kakugyo((4, 6), true) };
            Panel bott_PROMpedone = new Panel() { Tag = new Fuhyo((4, 6), true) };
            Panel bott_PROMgenArg = new Panel() { Tag = new Ginsho((4, 6), true) };
            Panel bott_PROMcavallo = new Panel() { Tag = new Keima((4, 6), true) };
            Panel bott_PROMlancia = new Panel() { Tag = new Kyosha((4, 6), true) };

            bott_PROMpedone.Name = "p";
            bott_PROMtorre.Name = "p";
            bott_PROMalfiere.Name = "p";
            bott_PROMgenArg.Name = "p";
            bott_PROMlancia.Name = "p";
            bott_PROMcavallo.Name = "p";

            bott_INDIETRO.Size = new Size(50, 50);
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

            bott_INDIETRO.BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/extra/turnBack.png");
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

            bott_INDIETRO.Location = new Point(this.Width - bott_INDIETRO.Width - 40, this.Height - bott_INDIETRO.Height - 55);
            bott_pedone.Location = new Point(820, 265);
            bott_torre.Location = new Point(920, 265);
            bott_alfiere.Location = new Point(1020, 265);
            bott_genOro.Location = new Point(1120, 265);
            bott_genArg.Location = new Point(820, 375);
            bott_lancia.Location = new Point(920, 375);
            bott_cavallo.Location = new Point(1020, 375);
            bott_re.Location = new Point(1120, 375);
            bott_PROMpedone.Location = new Point(900 - 30, 485);
            bott_PROMtorre.Location = new Point(1000 - 30, 485);
            bott_PROMalfiere.Location = new Point(1100 - 30, 485);
            bott_PROMgenArg.Location = new Point(900 - 30, 595);
            bott_PROMlancia.Location = new Point(1000 - 30, 595);
            bott_PROMcavallo.Location = new Point(1100 - 30, 595);

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

            this.Controls.Add(bott_INDIETRO);
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
        private void chiudi(object sender, EventArgs e)
        {
            this.Close();
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

        private string immagineMovimenti(Koma koma)
        {
            string nome = koma.GetType().Name;
            if (!koma.Promossa) { 
                switch (nome)
                {
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
            }else
            {
                switch (nome)
                {
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


    }

}
