namespace Shogi
{
    public partial class FormMovimenti : Form
    {

        string PERCORSOIMMAGINE = Application.StartupPath;
        const int PANELSIZE = 80;
        
        public FormMovimenti()
        {
            InitializeComponent();
        }
        public FormMovimenti(Image i)
        {
            InitializeComponent();
            BackgroundImageLayout = ImageLayout.Stretch;
            BackgroundImage = i;
        }

        private void FormMovimenti_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Size = Screen.PrimaryScreen.WorkingArea.Size;
            WindowState = FormWindowState.Maximized;
            

            Utilities.ScriviLabel(this, PERCORSOIMMAGINE);
            Utilities.DisegnaKoma(new EventHandler(chiudi), new EventHandler(provaKoma), PERCORSOIMMAGINE, this, Width / 2 - 610 / 2 - 39, PANELSIZE);

        }



        private void chiudi(object sender, EventArgs e)
        {
            Close();
        }

        private void provaKoma(object sender, EventArgs e)
        {
            Hide();
            FormMovimentiShogiban movimenti = new FormMovimentiShogiban(BackgroundImage);
            movimenti.ShowDialog();
            Close();
        }
    }
}
