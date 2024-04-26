using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shogi
{
    public partial class PaginaIniziale : Form
    {
        static protected string PERCORSOIMMAGINE = Application.StartupPath;
        (int, int) grandezzaPulsante = (400, 70);

        public PaginaIniziale()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(235, 235, 235);
            /*pictureBox1.Image = Image.FromFile($@"{PERCORSOIMMAGINE}/shogiPieces/pedone.png"); // Replace with the path to your logo
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Location = new Point(12, 12);*/
            pictureBox1.Visible = false;
        }


        private void PaginaIniziale_Load(object sender, EventArgs e)
        {
            foreach (Control elemento in this.Controls)
            {
                if (elemento is Label)
                {
                    PrivateFontCollection fontCollection = new PrivateFontCollection();
                    fontCollection.AddFontFile($@"{PERCORSOIMMAGINE}/shogiPieces/extra/MISTV___.ttf");
                    Font customFont = new Font(fontCollection.Families[0], 34, FontStyle.Bold);
                    elemento.Font = customFont;
                }
            }
            label1.Location = new Point(345, 50);
            label1.BackColor = Color.Transparent;

            caricaPulsanti();

        }

        private void caricaPulsanti()
        {
            button1.Size = new Size(grandezzaPulsante.Item1, grandezzaPulsante.Item2);
            button1.Location = new Point(this.Width / 2 - button1.Width / 2, this.Height / 2 - 100);
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.ImageAlign = ContentAlignment.MiddleRight;

            button2.Size = new Size(grandezzaPulsante.Item1, grandezzaPulsante.Item2);
            button2.Location = new Point(this.Width / 2 - button1.Width / 2, this.Height / 2);
            button2.TextImageRelation = TextImageRelation.ImageBeforeText;
            button2.ImageAlign = ContentAlignment.MiddleRight;

            button3.Size = new Size(grandezzaPulsante.Item1, grandezzaPulsante.Item2);
            button3.Location = new Point(this.Width / 2 - button1.Width / 2, this.Height / 2 + 100);
            button3.TextImageRelation = TextImageRelation.ImageBeforeText;
            button3.ImageAlign = ContentAlignment.MiddleRight;
        }

        private void button1_Click(object sender, EventArgs e)//Nasconde la Pagina Iniziale e fa partire Form1.cs
        {
            Form1 form1 = new Form1();
            form1.ShowDialog();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)//deve essere centrata al cento della schermata
        {


        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.Image = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/extra/frecciaDestra.png");
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Image = null;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.Image = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/extra/frecciaDestra.png");
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.Image = null;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.Image = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/extra/frecciaDestra.png");
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.Image = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormMovimenti movimenti = new FormMovimenti();
            movimenti.ShowDialog();
        }
    }
}
