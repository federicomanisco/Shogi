namespace Shogi {
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Panel[,] Tiles;
        //84x84
        const int TILESIZE = 84;
        const int GRIDSIZE = 9;
        Color TileColor = Color.FromArgb(238, 182, 115);

        private void Form1_Load(object sender, EventArgs e)
        { 
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
        }

        private void disegnaZonaPromozione(Color colore)
        {
            Image sfondo = new Bitmap(10, 10);
            Graphics g = Graphics.FromImage(sfondo);
            Brush brush = new SolidBrush(Color.FromArgb(60,60,60));
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
                lbl.Size = new Size(30, 30);
                lbl.Font = new Font("Arial", 18);
                Controls.Add(lbl);
            }

            for (int i = 0; i < 9; i++)
            {
                Label lbl = new Label();
                lbl.Location = new Point(puntoPartenza.Item1 + (TILESIZE*9) - 30, puntoPartenza.Item2 + (i * TILESIZE) + 10 + 55);
                lbl.Text = Math.Abs((i - 9)).ToString();
                lbl.Size = new Size(30, 30);
                lbl.Font = new Font("Arial", 18);
                Controls.Add(lbl);
            }
        }

        private void generaPosizioneIniziale()
        {

        }        

        private void Tile_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ciao");
        }

    }
}