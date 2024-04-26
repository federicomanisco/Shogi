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

        bool vittorioso;
        string PERCORSOIMMAGINE = Application.StartupPath;

        public FormPartitaFinita(bool v) //true vince Sente (sotto), false vince Gote (sopra)
        {
            InitializeComponent();
            vittorioso = v;
        }



        private void FormPartitaFinita_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            mettiLabel();
            mettiImmagini();
            MettiBottone();

            this.BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/extra/sfondo1.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private string PrendiVincitore(bool v)
        {
            if (v) return "SENTE";
            return "GOTE";
        }

        private void MettiBottone()
        {
            button1.Size = new Size(300, 60);
            button1.Location = new Point(150, 255);
        }

        private void mettiLabel()
        {
            label1.Font = new Font("Arial", 40 * (1 / GetScreenScaleFactor()));
            label1.BackColor = Color.Transparent;
            label1.Text = $"VINCE {PrendiVincitore(vittorioso)}";
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

            if (vittorioso)
                pbox_crown.Location = new Point(this.Width / 2 - 3 * pbox_sente.Width, 150 - pbox_crown.Height);
            else
                pbox_crown.Location = new Point(this.Width / 2 + 3 * pbox_gote.Width - pbox_gote.Width, 150 - pbox_crown.Height);

        }

        float GetScreenScaleFactor()
        { //restituisce la scala dello schermo (100%, 125%, 150%, 175%) sapendo che 96DPI = 100%
            Graphics graphics = CreateGraphics();
            float dpiX = graphics.DpiX;
            graphics.Dispose();

            return dpiX / 96f;
        }

        private void customButton1_Click_1(object sender, EventArgs e)
        {
            Form1 formSchermataIniziale = new Form1(); //TODO sostituire il form della schermata iniziale a quello del form1 (appena viene finito)
            this.Close();
            formSchermataIniziale.Show();
        }
        /*
FormPartitaFinita formFinale = new FormPartitaFinita(true);
timer1.Stop();
formFinale.ShowDialog();
this.Close();
*/
    }
}
