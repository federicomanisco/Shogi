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
    public partial class FormPartitaFinita : Form
    {

        Koma.Giocatore perdente;
        string PERCORSOIMMAGINE = Application.StartupPath;

        public FormPartitaFinita(Koma.Giocatore p) //true vince Sente (sotto), false vince Gote (sopra)
        {
            InitializeComponent();
            perdente = p;
        }



        private void FormPartitaFinita_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.Fixed3D;
            mettiLabel();
            mettiImmagini();
            mettiBottone();

            BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/extra/sfondo1.jpg");
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        private string PrendiVincitore(Koma.Giocatore v)
        {
            if (v == Koma.Giocatore.Sente) return "SENTE";
            return "GOTE";
        }

        private void mettiBottone()
        {
            button1.Size = new Size(300, 60);
            button1.Location = new Point(150, 255);
        }

        private void mettiLabel()
        {
            label1.Font = new Font("Arial", 40 * (1 / Utilities.GetScreenScaleFactor(this)));
            label1.BackColor = Color.Transparent;
            label1.Text = $"VINCE {PrendiVincitore(perdente)}";
            label1.Location = new Point(120, 25);
        }

        private void mettiImmagini()
        {
            pbox_sente.Image = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/re.png");
            pbox_sente.SizeMode = PictureBoxSizeMode.StretchImage;
            pbox_sente.BackColor = Color.Black;
            pbox_sente.Size = new Size(60, 60);
            pbox_sente.Location = new Point(this.Width / 2 - 3 * pbox_sente.Width, 150);
            pbox_sente.BorderStyle = BorderStyle.Fixed3D;

            pbox_gote.Image = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/re.png");
            pbox_gote.Image.RotateFlip(RotateFlipType.Rotate180FlipX);
            pbox_gote.SizeMode = PictureBoxSizeMode.StretchImage;
            pbox_gote.BackColor = Color.Black;
            pbox_gote.Size = new Size(60, 60);
            pbox_gote.Location = new Point(this.Width / 2 + 3 * pbox_gote.Width - pbox_gote.Width, 150);
            pbox_gote.BorderStyle = BorderStyle.Fixed3D;

            pbox_crown.Image = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/extra/crown.png");
            pbox_crown.SizeMode = PictureBoxSizeMode.StretchImage;
            pbox_crown.BackColor = Color.Transparent;
            pbox_crown.Size = new Size(60, 40);

            if (perdente != Koma.Giocatore.Sente)
                pbox_crown.Location = new Point(this.Width / 2 - 3 * pbox_sente.Width, 150 - pbox_crown.Height);
            else
                pbox_crown.Location = new Point(this.Width / 2 + 3 * pbox_gote.Width - pbox_gote.Width, 150 - pbox_crown.Height);

        }

        private void customButton1_Click_1(object sender, EventArgs e)
        {
            PaginaIniziale formSchermataIniziale = new PaginaIniziale();
            Close();
            formSchermataIniziale.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            PaginaIniziale formSchermataIniziale = new PaginaIniziale();
            Hide();
            formSchermataIniziale.Show();
        }
    }
}
