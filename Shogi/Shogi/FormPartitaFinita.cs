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
            mettiLabel();
            this.BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/extra/starman.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private string PrendiVincitore(bool v)
        {
            if (v) return "SENTE";
            return "GOTE";
        }

        private void mettiLabel()
        {
            label1.Font = new Font("Arial", 60 * (1 / GetScreenScaleFactor()));
            label1.BackColor = Color.Transparent;
            label1.Text = PrendiVincitore(vittorioso);
            label1.Location = new Point(this.Width / 2 - (75 * (label1.Text.Length) / 2), 10);

            label2.Font = new Font("Arial", 60 * (1 / GetScreenScaleFactor()));
            label2.BackColor = Color.Transparent;
            label2.Text = "VINCE";
            label2.Location = new Point((this.Width / 2 - 150), 100);

        }

        float GetScreenScaleFactor()
        { //restituisce la scala dello schermo (100%, 125%, 150%, 175%) sapendo che 96DPI = 100%
            Graphics graphics = CreateGraphics();
            float dpiX = graphics.DpiX;
            graphics.Dispose();

            return dpiX / 96f;
        }

        private void button1_Click(object sender, EventArgs e)
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
