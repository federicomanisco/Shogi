using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void FormMovimentiShogiban_Load(object sender, EventArgs e)
        {

            Size = Screen.PrimaryScreen.WorkingArea.Size;
            WindowState = FormWindowState.Maximized;

            //genera shogiban
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
            } //fine generazione shogiban



        }

        public (int, int) getRowColFromLocation(Point point)
        {
            return ((point.X - 582) / TILESIZE, (point.Y - 162 + 40) / TILESIZE);
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
                //MessageBox.Show(koma.Colore.ToString());

            }
            else
            {

                if (panel.BackColor == Color.Yellow)
                {
                    (int, int) nuovaPosizione = getRowColFromLocation(panel.Location);
                    Koma koma = shogiban.getKoma(posizioneChiamante);
                    koma.Posizione = nuovaPosizione;
                    Koma komamangiato = shogiban.getKoma(nuovaPosizione);

                    if (komamangiato != null)//se c'è un'altra pedina nella nuova posizione
                    {
                        if (komamangiato.GetType().Name == "Osho") finisciPartita(komamangiato);  // se viene mangiato il re deve finire la partita
                        else inserisciPedinaNelKubomawashi(komamangiato);

                    }
                    shogiban.rimuoviKoma(posizioneChiamante);   //rimuovi koma  
                    shogiban.aggiungiKoma(koma);
                    panel.BackgroundImage = koma.Icona;
                    panel.BackgroundImageLayout = ImageLayout.Center;
                    Tiles[posizioneChiamante.Item1, posizioneChiamante.Item2].BackgroundImage = null;

                    //promozione
                    if (((nuovaPosizione.Item2 <= 2 && koma.Colore && !koma.Promossa) || (nuovaPosizione.Item2 >= 6 && !koma.Colore && !koma.Promossa)) && !komaNonPromovibili.Contains(koma.GetType().Name))
                    {
                        if ((nuovaPosizione.Item2 != 0 && koma.Colore) || (nuovaPosizione.Item2 != 8 && !koma.Colore))
                        {
                            DialogResult chiamata;
                            chiamata = MessageBox.Show("Vuoi promuovere la pedina?", "", MessageBoxButtons.YesNo);
                            if (chiamata == DialogResult.Yes)
                            {
                                koma.promuovi();
                                Tiles[nuovaPosizione.Item1, nuovaPosizione.Item2].BackgroundImage = koma.Icona;
                            }
                        }
                        else
                        {
                            koma.promuovi();
                            Tiles[nuovaPosizione.Item1, nuovaPosizione.Item2].BackgroundImage = koma.Icona;
                        }
                    } //fine promozione


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
    }
}
