using Newtonsoft.Json;
using System.Windows.Forms.VisualStyles;

//TODO re sotto minaccia (scacco)

namespace Shogi {
    public partial class Form1: Form {
        public Form1() {
            InitializeComponent();
            float fattoreScalaSchermo = Utilities.GetScreenScaleFactor(this);
            float fattoreScalaSchermoInverso = 1 / fattoreScalaSchermo;

            Scale(new SizeF(fattoreScalaSchermoInverso, fattoreScalaSchermoInverso)); // scala i componenti della form in base alla scala dello schermo
        }

        bool pannelloCliccato = false;
        Panel[,] Tiles = new Panel[9, 9];
        //84x84
        const int TILESIZE = 84;
        const int GRIDSIZE = 9;
        Color TileColor = Color.FromArgb(238, 182, 115);
        Shogiban shogiban = new Shogiban();
        Kubomawashi kubomawashi_sfidante = new Kubomawashi(); //lo sfidante inizia sotto
        Kubomawashi kubomawashi_sfidato = new Kubomawashi();  //lo sfidato inizia sopra
        int kubomawashi_width = 283; //lunghezza lato kubomawashi, quadrato
        static protected string PERCORSOIMMAGINE = Application.StartupPath;
        const int TIMERWIDTH = 400;
        const int TIMERHEIGHT = 240;
        int tempoMin = 10; //tempo di gioco per giocatore, minuti
        int tempoSec = 30; //tempo di gioco per giocatore, secondi
        bool partitaFinita = false;
        const int MARGINEX = 582;
        const int MARGINEY = 122;
        Koma.Giocatore turno = Koma.Giocatore.Sente;  //true Sente (muove x primo, generalmente lo sfidante), false Gote (lo sfidato)

        bool reinserimento = false; // true-> il giocatore sta tentando di reinserire una koma nella shogiban
        Panel komaReinserimento;

        List<string> komaNonPromovibili = new List<string>
        {
            "Osho", //re
            "Kinsho", //generale d'oro
        };

        (int, int) posizioneChiamante;

        System.Media.SoundPlayer sound_muoviKoma = new System.Media.SoundPlayer($"{PERCORSOIMMAGINE}/shogiPieces/extra/movingPiece.wav");


        private void Form1_Load(object sender, EventArgs e) {
            Location = new Point(0, 0);
            Size = Screen.PrimaryScreen.WorkingArea.Size;
            WindowState = FormWindowState.Maximized;
            BackgroundImage = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/extra/woodenTable.jpg");

            Utilities.disegnaCaselle(Tiles, Controls, GRIDSIZE, TILESIZE, MARGINEX, MARGINEY, TileColor, new EventHandler(Tile_Click));
            Utilities.disegnaZonaPromozione(TileColor, pictureBox1, pictureBox2, pictureBox3, pictureBox4, TILESIZE);
            Utilities.scriviNumeriCaselle(TILESIZE, Utilities.GetScreenScaleFactor(this), Controls);
            Utilities.generaPosizioneIniziale(shogiban, Tiles);
            Utilities.disegnaKubomawashi(kubomawashi_width, kubomawashi1, kubomawashi2, TileColor, TILESIZE, GRIDSIZE);
            Utilities.disegnaTimer((TIMERWIDTH, TIMERHEIGHT), tempoMin, tempoSec, TILESIZE, GRIDSIZE, pbox_timer1, pbox_timer2, PERCORSOIMMAGINE, lbl_Min1, lbl_Min2, lbl_Sec1, lbl_Sec2, Utilities.GetScreenScaleFactor(this));
            controllaPartiteSalvate();
        }

        void controllaPartiteSalvate() {
            StreamReader sr = new StreamReader("Salvataggio.json");
            string json = sr.ReadToEnd();
            sr.Close();
            if (!string.IsNullOrEmpty(json)) {
                DialogResult caricamento;
                caricamento = MessageBox.Show("Caricare la scorsa partita non finita?", "", MessageBoxButtons.YesNo);
                if (caricamento == DialogResult.Yes) {
                    CaricaPartita();
                }
            }
        }

        private void inserisciPedinaNelKubomawashi(Koma koma) {
            if (!komaNonPromovibili.Contains(koma.GetType().Name)) //se la koma non è depromovibile allora non lo farà
                koma.depromuovi();

            koma.Icona.RotateFlip(RotateFlipType.Rotate180FlipX);
            TableLayoutPanel panelDaUsare = null;
            List<Koma> listaPannelli = null;
            if (koma.Colore == Koma.Giocatore.Sente) {
                if (kubomawashi_sfidante.PedinaMangiata == null) {
                    kubomawashi_sfidante.PedinaMangiata = koma;
                    listaPannelli = kubomawashi_sfidante.list;
                    panelDaUsare = (TableLayoutPanel)kubomawashi1.Controls[0]; // Ottiene il pannello interno del kubomawashi1
                } else {
                    if (kubomawashi_sfidante.list.Count == 20) { MessageBox.Show("Il kubomawashi dello sfidante è pieno!"); return; }
                    listaPannelli = kubomawashi_sfidante.list;
                    panelDaUsare = (TableLayoutPanel)kubomawashi1.Controls[0];
                }
            } else {
                if (kubomawashi_sfidato.PedinaMangiata == null) {
                    kubomawashi_sfidato.PedinaMangiata = koma;
                    listaPannelli = kubomawashi_sfidato.list;
                    panelDaUsare = (TableLayoutPanel)kubomawashi2.Controls[0]; // Ottiene il pannello interno del kubomawashi
                } else {
                    if (kubomawashi_sfidato.list.Count == 20) { MessageBox.Show("Il kubomawashi dello sfidato è pieno!"); return; }
                    listaPannelli = kubomawashi_sfidato.list;
                    panelDaUsare = (TableLayoutPanel)kubomawashi2.Controls[0];
                }
            }
            listaPannelli.Add(koma);
            koma.changeTeam(posizioneChiamante);
            Panel panelPedina = new Panel {
                Size = new Size(50, 50),
                BackgroundImage = koma.Icona,
                BackgroundImageLayout = ImageLayout.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                Name = "prova",
                Tag = koma
            };

            panelPedina.Click += new EventHandler(ReinserimentoKoma);

            panelDaUsare.Controls.Add(panelPedina);


        }

        private void ReinserimentoKoma(object sender, EventArgs e) {

            Panel panel = (Panel)sender;
            Koma koma = (Koma)panel.Tag;
            string nomeKoma = koma.GetType().Name;
            if (pannelloCliccato)  //controllo che l'utente non stesse cercando di spostare una koma nel campo
            {
                reimpostaCaselle();
            } else if (!reinserimento && turno == koma.Colore) {
                reinserimento = true;
                komaReinserimento = panel;

                for (int c = 0; c <= 8; c++) {
                    for (int r = 0; r <= 8; r++) {
                        if (
                            !((nomeKoma == "Kyosha" || nomeKoma == "Fuhyo") && koma.Colore == Koma.Giocatore.Sente && r == 0) &&   //pedoni e lance non possono essere inseriti nell'ultima casella della shogiban (non potrebbero muoversi)
                            !((nomeKoma == "Kyosha" || nomeKoma == "Fuhyo") && koma.Colore == Koma.Giocatore.Gote && r == 8) &&
                            !(nomeKoma == "Keima" && koma.Colore == Koma.Giocatore.Sente && r < 2) &&     //i cavalli non possono essere inseriti nelle ultime due caselle della shogiban (non potrebbero muoversi)
                            !(nomeKoma == "Keima" && koma.Colore == Koma.Giocatore.Gote && r > 6) &&
                            Tiles[c, r].BackgroundImage == null
                            ) {
                            Tiles[c, r].BackColor = Color.Yellow;
                        }


                    }
                }

            } else {
                reinserimento = false;
                reimpostaCaselle();
                komaReinserimento = null;
            }

        }

        private void Tile_Click(object sender, EventArgs e) {
            pannelloCliccato = !pannelloCliccato;
            Panel panel = (Panel)sender;
            if (!reinserimento) {
                if (pannelloCliccato) {
                    posizioneChiamante = Utilities.getRowColFromLocation(panel.Location, TILESIZE, MARGINEX, MARGINEY);
                    Koma koma = null;
                    try {
                        koma = shogiban.getKoma(posizioneChiamante);
                    } catch { }

                    if (koma != null) {
                        if (koma.Colore == turno) {
                            Tiles[posizioneChiamante.Item1, posizioneChiamante.Item2].BackColor = Color.Red;
                            List<(int, int)> mosseLegali = shogiban.calcolaMosseLegali(koma);
                            foreach ((int, int) mossaLegale in mosseLegali) {
                                int casellaDaEvidenziareX = koma.Posizione.Item1 + mossaLegale.Item1;
                                int casellaDaEvidenziareY = koma.Posizione.Item2 + mossaLegale.Item2;


                                Tiles[casellaDaEvidenziareX, casellaDaEvidenziareY].BackColor = Color.Yellow;
                            }
                        }
                    }
                } else {

                    if (panel.BackColor == Color.Yellow) {
                        (int, int) nuovaPosizione = Utilities.getRowColFromLocation(panel.Location, TILESIZE, MARGINEX, MARGINEY);
                        Koma koma = shogiban.getKoma(posizioneChiamante);
                        koma.Posizione = nuovaPosizione;
                        Koma komamangiato = shogiban.getKoma(nuovaPosizione);

                        if (komamangiato != null)//se c'è un'altra pedina nella nuova posizione
                        {
                            inserisciPedinaNelKubomawashi(komamangiato);
                        }
                        shogiban.rimuoviKoma(posizioneChiamante);
                        shogiban.aggiungiKoma(koma);
                        panel.BackgroundImage = koma.Icona;
                        panel.BackgroundImageLayout = ImageLayout.Center;
                        Tiles[posizioneChiamante.Item1, posizioneChiamante.Item2].BackgroundImage = null;

                        //promozione
                        if (((nuovaPosizione.Item2 <= 2 && koma.Colore == Koma.Giocatore.Sente && !koma.Promossa) || (nuovaPosizione.Item2 >= 6 && koma.Colore == Koma.Giocatore.Gote && !koma.Promossa)) && !komaNonPromovibili.Contains(koma.GetType().Name)) {
                            if ((nuovaPosizione.Item2 != 0 && koma.Colore == Koma.Giocatore.Sente) || (nuovaPosizione.Item2 != 8 && koma.Colore == Koma.Giocatore.Gote)) {
                                DialogResult chiamata;
                                chiamata = MessageBox.Show("Vuoi promuovere la pedina?", "", MessageBoxButtons.YesNo);
                                if (chiamata == DialogResult.Yes) {
                                    koma.promuovi();
                                    Tiles[nuovaPosizione.Item1, nuovaPosizione.Item2].BackgroundImage = koma.Icona;
                                }
                            } else {
                                koma.promuovi();
                                Tiles[nuovaPosizione.Item1, nuovaPosizione.Item2].BackgroundImage = koma.Icona;
                            }
                        } //fine promozione


                        turno = (turno == Koma.Giocatore.Sente) ? Koma.Giocatore.Gote : Koma.Giocatore.Sente;
                        if (shogiban.ScaccoMatto(shogiban.Scacchiera, turno)) {
                            finisciPartita(new Osho((0, 0), turno));
                        }
                        sound_muoviKoma.Play();
                    }
                    foreach (Panel tile in Tiles) {
                        if (tile.BackColor != TileColor) {
                            tile.BackColor = TileColor;
                        }
                    }
                }
            } else if (reinserimento) {
                posizioneChiamante = Utilities.getRowColFromLocation(panel.Location, TILESIZE, MARGINEX, MARGINEY);

                if (Tiles[posizioneChiamante.Item1, posizioneChiamante.Item2].BackColor == Color.Yellow)  //se non c'è nessuna koma nella nuova posizione
                {

                    Koma koma = (Koma)komaReinserimento.Tag;
                    Tiles[posizioneChiamante.Item1, posizioneChiamante.Item2].BackgroundImage = koma.Icona;
                    Tiles[posizioneChiamante.Item1, posizioneChiamante.Item2].BackgroundImageLayout = ImageLayout.Center;
                    komaReinserimento.Parent.Controls.Remove(komaReinserimento);
                    //koma.changeTeam(posizioneChiamante);

                    koma.Posizione = posizioneChiamante;
                    shogiban.aggiungiKoma(koma);

                    turno = (turno == Koma.Giocatore.Sente) ? Koma.Giocatore.Gote : Koma.Giocatore.Sente;
                    sound_muoviKoma.Play();
                }

                reinserimento = false;
                reimpostaCaselle();
                komaReinserimento = null;
                pannelloCliccato = false;

            }
        }
        private void reimpostaCaselle() {
            for (int c = 0; c <= 8; c++) {
                for (int r = 0; r <= 8; r++) {
                    Tiles[c, r].BackColor = TileColor;
                }
            }
        }

        private void timer_tick(object sender, EventArgs e) {
            if (timer1.Enabled) {
                int min;
                int sec;
                string player;

                if (turno == Koma.Giocatore.Sente) player = "SFIDANTE";
                else player = "SFIDATO";

                if (turno == Koma.Giocatore.Sente) {
                    min = int.Parse(lbl_Min1.Text);
                    sec = int.Parse(lbl_Sec1.Text);
                } else {
                    min = int.Parse(lbl_Min2.Text);
                    sec = int.Parse(lbl_Sec2.Text);
                }

                if (sec == 1) {
                    if (min == 0) {
                        if (turno == Koma.Giocatore.Sente) lbl_Sec1.Text = "0";
                        else lbl_Sec2.Text = "0";
                        timer1.Stop();
                        MessageBox.Show($"LO {player} PERDE PER TEMPO");
                        sec = 0;
                    } else {
                        sec = 60;
                        min--;
                    }
                } else sec--;


                if (turno == Koma.Giocatore.Sente) {
                    lbl_Min1.Text = min.ToString("D2");
                    lbl_Sec1.Text = sec.ToString("D2");
                } else {
                    lbl_Min2.Text = min.ToString("D2");
                    lbl_Sec2.Text = sec.ToString("D2");
                }
            }
        }

        private void Salva() {
            ((int, int), (int, int)) tempiGiocatori = ((int.Parse(lbl_Min1.Text), int.Parse(lbl_Sec1.Text)), (int.Parse(lbl_Min2.Text), int.Parse(lbl_Sec2.Text)));
            Salvataggio salvataggio = new Salvataggio(shogiban.Scacchiera, turno, tempiGiocatori, kubomawashi_sfidato, kubomawashi_sfidante);
            StreamWriter sw = new StreamWriter("Salvataggio.json");
            string salvataggioTesto = JsonConvert.SerializeObject(salvataggio, Formatting.Indented, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
            sw.Write(salvataggioTesto);
            sw.Close();
        }

        private void CaricaPartita() {
            StreamReader sr = new StreamReader("Salvataggio.json");
            string salvataggioTesto = sr.ReadToEnd();
            sr.Close();
            bool successo = false;
            Salvataggio salvataggio = null;
            try {
                salvataggio = JsonConvert.DeserializeObject<Salvataggio>(salvataggioTesto, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                successo = true;
            } catch {
                MessageBox.Show("Caricamento fallito.");
            }

            if (successo) {
                turno = salvataggio.Turno;
                lbl_Min1.Text = salvataggio.TempiGiocatori.Item1.Item1.ToString("D2");
                lbl_Sec1.Text = salvataggio.TempiGiocatori.Item1.Item2.ToString("D2");
                lbl_Min2.Text = salvataggio.TempiGiocatori.Item2.Item1.ToString("D2");
                lbl_Sec2.Text = salvataggio.TempiGiocatori.Item2.Item2.ToString("D2");
                for (int i = 0; i < salvataggio.ShogibanState.GetLength(0); i++) {
                    for (int j = 0; j < salvataggio.ShogibanState.GetLength(1); j++) {
                        shogiban.rimuoviKoma((i, j));
                        Tiles[i, j].BackgroundImage = null;
                        Koma koma = salvataggio.ShogibanState[i, j];
                        if (koma != null) {
                            //Utilities.mostraCasella(shogiban, Tiles, koma);
                        }
                    }
                }
                kubomawashi_sfidato = salvataggio.KubomawashiGote;
                kubomawashi_sfidante = salvataggio.KubomawashiSente;
            }
        }

        private void SvuotaSalvataggio() {
            StreamWriter sw = new StreamWriter("Salvataggio.json");
            sw.Write(string.Empty);
        }

        private void finisciPartita(Koma koma) {
            FormPartitaFinita formFinale = new FormPartitaFinita(koma.Colore); //passo il colore opposto del re mangiato
            timer1.Stop();
            formFinale.ShowDialog();
            partitaFinita = true;
            SvuotaSalvataggio();
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            if (!partitaFinita) {
                DialogResult risposta;
                risposta = MessageBox.Show("Vuoi salvare la partita?", "", MessageBoxButtons.YesNo);
                if (risposta == DialogResult.Yes) {
                    Salva();
                    MessageBox.Show("Partita salvata.");
                }
            }
        }
    }
}