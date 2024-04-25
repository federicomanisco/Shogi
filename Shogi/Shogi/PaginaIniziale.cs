using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            label1.Location = new Point(345,50);
            label1.BackColor = Color.Transparent;
            
        }

        private void button1_Click(object sender, EventArgs e)//Nasconde la Pagina Iniziale e fa partire Form1.cs
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)//deve essere centrata al cento della schermata
        {


        }
    }
}
