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
    public partial class FormMovimenti : Form
    {

        string PERCORSOIMMAGINE = Application.StartupPath;
        int disBordo;
        
        public FormMovimenti()
        {
            InitializeComponent();
        }
        public FormMovimenti(Image i)
        {
            InitializeComponent();
            BackgroundImageLayout = ImageLayout.Stretch;
            this.BackgroundImage = i;
        }

        private void FormMovimenti_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Size = Screen.PrimaryScreen.WorkingArea.Size;
            WindowState = FormWindowState.Maximized;
            

            ScriviLabel();
            DisegnaKoma();

           }

        private void DisegnaKoma()
        {
            Panel bott_INDIETRO = new Panel();
            bott_INDIETRO.Click += new EventHandler(chiudi);
            bott_INDIETRO.Name = "indietro";

            Panel bott_pedone = new Panel();
            Panel bott_torre = new Panel();
            Panel bott_alfiere = new Panel();
            Panel bott_genOro = new Panel();
            Panel bott_genArg = new Panel();
            Panel bott_cavallo = new Panel();
            Panel bott_lancia = new Panel();
            Panel bott_re = new Panel();
            Panel bott_PROMtorre = new Panel(); 
            Panel bott_PROMalfiere = new Panel();
            Panel bott_PROMpedone = new Panel();
            Panel bott_PROMgenArg = new Panel();
            Panel bott_PROMcavallo = new Panel();
            Panel bott_PROMlancia = new Panel();

            bott_INDIETRO.Size = new Size(50,50);
            bott_pedone.Size = new Size(80, 80);
            bott_torre.Size = new Size(80, 80);
            bott_alfiere.Size = new Size(80, 80);
            bott_genOro.Size = new Size(80, 80);
            bott_genArg.Size = new Size(80, 80);
            bott_lancia.Size = new Size(80, 80);
            bott_cavallo.Size = new Size(80, 80);
            bott_re.Size = new Size(80, 80);
            bott_PROMpedone.Size = new Size(80, 80);
            bott_PROMtorre.Size = new Size(80, 80);
            bott_PROMalfiere.Size = new Size(80, 80);
            bott_PROMgenArg.Size = new Size(80, 80);
            bott_PROMlancia.Size = new Size(80, 80);
            bott_PROMcavallo.Size = new Size(80, 80);

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

            
            bott_INDIETRO.Location = new Point(this.Width - bott_INDIETRO.Width - 70, 15);
            bott_pedone.Location = new Point(50 + disBordo, 105 + 50);
            bott_torre.Location = new Point(220 + disBordo, 105 + 50);
            bott_alfiere.Location = new Point(390 + disBordo, 105 + 50);
            bott_genOro.Location = new Point(560 + disBordo, 105 + 50);
            bott_genArg.Location = new Point(50 + disBordo, 215 + 100);
            bott_lancia.Location = new Point(220 + disBordo, 215 + 100);
            bott_cavallo.Location = new Point(390 + disBordo, 215 + 100);
            bott_re.Location = new Point(560 + disBordo, 215 + 100);
            bott_PROMpedone.Location = new Point(110 + disBordo, 400 + 50 + 130);
            bott_PROMtorre.Location = new Point(300 + disBordo, 400 + 50 + 130);
            bott_PROMalfiere.Location = new Point(490 + disBordo, 400 + 50 + 130);
            bott_PROMgenArg.Location = new Point(110 + disBordo, 510 + 50 + 180);
            bott_PROMlancia.Location = new Point(300 + disBordo, 510 + 50 + 180);
            bott_PROMcavallo.Location = new Point(490 + disBordo, 510 + 50 + 180);

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

            foreach (Control panel in this.Controls)
            {
                if (panel is Panel && panel.Name != "indietro")
                {
                    panel.Click += new EventHandler(provaKoma);
                }
            }
        }

        private void ScriviLabel()
        {
            Label lbl_MOVIMENTI = new Label();
            Label lbl_pedone = new Label();
            Label lbl_torre = new Label();
            Label lbl_alfiere = new Label();
            Label lbl_genOro = new Label();
            Label lbl_genArg = new Label();
            Label lbl_cavallo = new Label();
            Label lbl_lancia = new Label();
            Label lbl_re = new Label();
            Label lbl_PROMOSSE = new Label();
            Label lbl_Ppedone = new Label();
            Label lbl_Ptorre = new Label();
            Label lbl_Palfiere = new Label();
            Label lbl_PgenArg = new Label();
            Label lbl_Pcavallo = new Label();
            Label lbl_Plancia = new Label();

            lbl_MOVIMENTI.Text = "KOMA";
            lbl_pedone.Text = "FUHYO";
            lbl_torre.Text = "HISHA";
            lbl_alfiere.Text = "KAKUGYO";
            lbl_genOro.Text = "KINSHO";
            lbl_genArg.Text = "GINSHO";
            lbl_cavallo.Text = "KEIMA";
            lbl_lancia.Text = "KYOSHA";
            lbl_re.Text = "OSHO";
            lbl_PROMOSSE.Text = "PROMOSSE";
            lbl_Ppedone.Text = "FUHYO";
            lbl_Ptorre.Text = "HISHA";
            lbl_Palfiere.Text = "KAKUGYO";
            lbl_PgenArg.Text = "GINSHO";
            lbl_Pcavallo.Text = "KEIMA";
            lbl_Plancia.Text = "KYOSHA";

            disBordo = this.Width / 2 - 610 / 2 - 39;
            lbl_MOVIMENTI.Location = new Point(this.Width / 2 - 110, 30);
            lbl_pedone.Location = new Point(55 + disBordo - 20, 85 + 40); //+5
            lbl_torre.Location = new Point(225 + disBordo - 15, 85 + 40); //+5
            lbl_alfiere.Location = new Point(388 + disBordo - 30, 85 + 40); //-2
            lbl_genOro.Location = new Point(563 + disBordo - 20, 85 + 40); //+3
            lbl_genArg.Location = new Point(53 + disBordo - 20, 195 + 90); //+3
            lbl_cavallo.Location = new Point(394 + disBordo - 10, 195 + 90); //+4
            lbl_lancia.Location = new Point(222 + disBordo - 20, 195 + 90); //+2
            lbl_re.Location = new Point(560 + disBordo - 7, 195 + 90);
            lbl_PROMOSSE.Location = new Point(this.Width / 2 - 150, 305 + 50 + 100);
            lbl_Ppedone.Location = new Point(115 + disBordo - 20, 380 + 50 + 120); 
            lbl_Ptorre.Location = new Point(305 + disBordo - 15, 380 + 50 + 120);   
            lbl_Palfiere.Location = new Point(488 + disBordo - 30, 380 + 50 + 120);
            lbl_PgenArg.Location = new Point(110 + disBordo - 20, 490 + 50 + 170);
            lbl_Pcavallo.Location = new Point(494 + disBordo - 10, 490 + 50 + 170); 
            lbl_Plancia.Location = new Point(302 + disBordo - 20, 490 + 50 + 170); 

            this.Controls.Add(lbl_MOVIMENTI);
            this.Controls.Add(lbl_pedone);
            this.Controls.Add(lbl_torre);
            this.Controls.Add(lbl_alfiere);
            this.Controls.Add(lbl_genOro);
            this.Controls.Add(lbl_genArg);
            this.Controls.Add(lbl_cavallo);
            this.Controls.Add(lbl_lancia);
            this.Controls.Add(lbl_re);
            this.Controls.Add(lbl_PROMOSSE);
            this.Controls.Add(lbl_Ppedone);
            this.Controls.Add(lbl_Ptorre);
            this.Controls.Add(lbl_Palfiere);
            this.Controls.Add(lbl_PgenArg);
            this.Controls.Add(lbl_Pcavallo);
            this.Controls.Add(lbl_Plancia);

            convertiFont();
            PrivateFontCollection fontCollection = new PrivateFontCollection();
            fontCollection.AddFontFile($@"{PERCORSOIMMAGINE}/shogiPieces/extra/MISTV___.ttf");
            lbl_MOVIMENTI.Font = new Font(fontCollection.Families[0], 40 * (1 / GetScreenScaleFactor()), FontStyle.Bold);
            lbl_PROMOSSE.Font = new Font(fontCollection.Families[0], 40 * (1 / GetScreenScaleFactor()), FontStyle.Bold);
            lbl_MOVIMENTI.Size = new Size(400, 60);
            lbl_PROMOSSE.Size = new Size(400, 60);

        }


        private void chiudi(object sender, EventArgs e)
        {
            this.Close();
        }

        float GetScreenScaleFactor()
        { //restituisce la scala dello schermo (100%, 125%, 150%, 175%) sapendo che 96DPI = 100%
            Graphics graphics = CreateGraphics();
            float dpiX = graphics.DpiX;
            graphics.Dispose();

            return dpiX / 96f;
        }

        private void convertiFont()
        {
            foreach (Control elemento in this.Controls)
            {
                if (elemento is Label)
                {
                    PrivateFontCollection fontCollection = new PrivateFontCollection();
                    fontCollection.AddFontFile($@"{PERCORSOIMMAGINE}/shogiPieces/extra/movimentiFont.ttf");
                    Font customFont = new Font(fontCollection.Families[0], 20 * (1 / GetScreenScaleFactor()), FontStyle.Bold);
                    elemento.BackColor = Color.Transparent;
                    elemento.Font = customFont;
                    elemento.Size = new Size(150, 30);
                }
            }
        }

        private void provaKoma(object sender, EventArgs e)
        {
            this.Hide();
            FormMovimentiShogiban movimenti = new FormMovimentiShogiban(BackgroundImage);
            movimenti.ShowDialog();
            this.Close();
        }

        private void mouse_enter(object sender, EventArgs e)
        {

        }
    }
}
