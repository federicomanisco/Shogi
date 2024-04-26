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
    public partial class FormMovimenti : Form
    {

        string PERCORSOIMMAGINE = Application.StartupPath;

        public FormMovimenti()
        {
            InitializeComponent();
        }

        private void FormMovimenti_Load(object sender, EventArgs e)
        {
            this.Size = new Size(570, 500);
            DisegnaKoma();
            this.BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/extra/sfondo1.jpg");
        }

        private void DisegnaKoma()
        {
            bott_pedone.Size = new Size(60, 60);
            bott_torre.Size = new Size(60, 60);
            bott_alfiere.Size = new Size(60, 60);
            bott_genOro.Size = new Size(60, 60);
            bott_genArg.Size = new Size(60, 60);
            bott_lancia.Size = new Size(60, 60);
            bott_cavallo.Size = new Size(60, 60);
            bott_re.Size = new Size(60, 60);

            bott_pedone.BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/pedone.png");
            bott_torre.Image = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/torre.png");
            bott_alfiere.Image = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/alfiere.png");
            bott_genOro.Image = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/generaleOro.png");
            bott_genArg.Image = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/generaleArgento.png");
            bott_lancia.Image = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/lancia.png");
            bott_cavallo.Image = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/cavallo.png");
            bott_re.Image = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/re.png");

            bott_pedone.BackgroundImageLayout = ImageLayout.Stretch;
            bott_torre.BackgroundImageLayout = ImageLayout.Stretch;
            bott_alfiere.BackgroundImageLayout = ImageLayout.Stretch;
            bott_genOro.BackgroundImageLayout = ImageLayout.Stretch;
            bott_genArg.BackgroundImageLayout = ImageLayout.Stretch;
            bott_lancia.BackgroundImageLayout = ImageLayout.Stretch;
            bott_cavallo.BackgroundImageLayout = ImageLayout.Stretch;
            bott_re.BackgroundImageLayout = ImageLayout.Stretch;

            bott_pedone.BackColor = Color.Transparent;
            bott_torre.BackColor = Color.Transparent;
            bott_alfiere.BackColor = Color.Transparent;
            bott_genOro.BackColor = Color.Transparent;
            bott_genArg.BackColor = Color.Transparent;
            bott_lancia.BackColor = Color.Transparent;
            bott_cavallo.BackColor = Color.Transparent;
            bott_re.BackColor = Color.Transparent;

            bott_pedone.Text = null;
            bott_torre.Text = null;
            bott_alfiere.Text = null;
            bott_genOro.Text = null;
            bott_genArg.Text = null;
            bott_lancia.Text = null;
            bott_cavallo.Text = null;
            bott_re.Text = null;

            bott_pedone.Location = new Point( 50, 50 );
            bott_torre.Location = new Point( 180, 50);
            bott_alfiere.Location = new Point(310, 50);
            bott_genOro.Location = new Point(440, 50);
            bott_genArg.Location = new Point(50, 160);
            bott_lancia.Location = new Point(180, 160);
            bott_cavallo.Location = new Point(310, 160);
            bott_re.Location = new Point(440, 160);

        }

        private void riscala(Button b, string p)
        {
            b.Size = new Size(60, 60);
        }

        private void mouse_enter(object sender, EventArgs e)
        {

        }
    }
}
