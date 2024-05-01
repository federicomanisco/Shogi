using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {
    public class Shogiban {
        private Koma[,] scacchiera;
        public Koma[,] Scacchiera { get { return scacchiera; } }

        /* NERI
           1 2 3 4 5 6 7 8 9
                             9
                             8
                             7
                             6
                             5
                             4
                             3
                             2
                             1
           BIANCHI
        */

        public bool controllaPosizioneOutOfBounds((int, int) posizione) {
            //  Colonna                    Colonna                  Riga                  Riga
            if (posizione.Item1 < 0 || posizione.Item1 > 8 || posizione.Item2 < 0 || posizione.Item2 > 8)
                return false;
            return true;
        }

        public Koma getKoma((int, int) posizione) {
            if (!controllaPosizioneOutOfBounds(posizione)) {
                throw new ArgumentException("Posizione fuori dai limiti della scacchiera");
            }
            return scacchiera[posizione.Item1, posizione.Item2];
        }

        public bool controllaCasellaLibera((int, int) posizione, Koma koma) {
            if (!controllaPosizioneOutOfBounds(posizione)) {
                throw new ArgumentException("La posizione è fuori dai limiti della scacchiera.");
            }

            Koma altraKoma = null;
            try {
                altraKoma = getKoma(posizione);
            } catch {

            }

            if (altraKoma != null) {
                if (koma.Colore == altraKoma.Colore) {
                    return false;
                }
            }
            return true;
        }


        public void rimuoviKoma((int, int) posizione) {
            if (controllaPosizioneOutOfBounds(posizione)) {
                scacchiera[posizione.Item1, posizione.Item2] = null;
            } else {
                throw new ArgumentException("Posizione fuori dai limiti della scacchiera");
            }
        }

        public void aggiungiKoma(Koma koma) {
            (int, int) posizione = koma.Posizione;
            if (controllaPosizioneOutOfBounds(posizione)) {
                scacchiera[posizione.Item1, posizione.Item2] = koma;
            }
        }

        private void makeMove(Koma koma, (int, int) nuovaPosizione) {
            rimuoviKoma(koma.Posizione);
            koma.Posizione = nuovaPosizione;
            aggiungiKoma(koma);
        }

        public bool pedinaNelMezzo((int, int) posizioneIniziale, (int, int) posizioneFinale) {
            if (!controllaPosizioneOutOfBounds(posizioneIniziale)) {
                throw new ArgumentException("Posizione iniziale fuori dai limiti della scacchiera.");
            }

            if (!controllaPosizioneOutOfBounds(posizioneFinale)) {
                throw new ArgumentException("Posizione finale fuori dai limiti della scacchiera.");
            }

            if (posizioneIniziale == posizioneFinale) {
                throw new ArgumentException("Posizioni iniziale e finale coincidenti.");
            }

            int Xiniziale = posizioneIniziale.Item1;
            int Yiniziale = posizioneIniziale.Item2;
            int Xfinale = posizioneFinale.Item1;
            int Yfinale = posizioneFinale.Item2;
            int contaPedineNemiche = 0;

            if (Xiniziale != Xfinale && Yiniziale != Yfinale && Math.Abs(Yfinale - Yiniziale) != Math.Abs(Xfinale - Xiniziale)) {
                throw new ArgumentException("Le caselle non sono allineate.");
            }

            int direzioneRiga = (Xiniziale == Xfinale ? 0 : (Xfinale > Xiniziale ? 1 : -1));
            int direzioneColonna = (Yiniziale == Yfinale ? 0 : (Yfinale > Yiniziale ? 1 : -1));
            int deltaRiga = Math.Abs(Xfinale - Xiniziale);
            int deltaColonna = Math.Abs(Yfinale - Yiniziale);
            int numeroPassi = Math.Max(deltaRiga, deltaColonna);
            for (int passo = 1; passo <= numeroPassi; passo++) {
                int riga = Xiniziale + (direzioneRiga * passo);
                int colonna = Yiniziale + (direzioneColonna * passo);
                if (scacchiera[riga, colonna] != null) {
                    if (scacchiera[riga, colonna].Colore == getKoma(posizioneIniziale).Colore) {
                        return true;
                    } else {
                        if (contaPedineNemiche == 1) return true;
                        contaPedineNemiche++;
                    }
                } else {
                    if (contaPedineNemiche == 1) return true;
                }
            }
            return false;
        }

        public List<(int, int)> calcolaMosseLegali(Koma koma) {
            List<(int, int)> mosseLegali = new List<(int, int)>();
            List<(int, int)> mosseRegolari = calcolaMosseRegolari(koma);
            Shogiban copia = Copy();
            foreach ((int, int) mossa in mosseRegolari) {
                Koma komaPrecedente = null;
                (int, int) vecchiaPosizione = koma.Posizione;
                (int, int) nuovaPosizione = (koma.Posizione.Item1 + mossa.Item1, koma.Posizione.Item2 + mossa.Item2);
                if (Scacchiera[nuovaPosizione.Item1, nuovaPosizione.Item2] != null) {
                    komaPrecedente = Scacchiera[nuovaPosizione.Item1, nuovaPosizione.Item2];
                    copia.makeMove(koma, nuovaPosizione);
                } else {
                    copia.makeMove(koma, nuovaPosizione);
                }
                
                if (!ReSottoScacco(copia, koma.Colore)) {
                    mosseLegali.Add(mossa);
                }
                copia.makeMove(koma, vecchiaPosizione);
                if (komaPrecedente != null) {
                    copia.aggiungiKoma(komaPrecedente);
                }
            }
            return mosseLegali;
        }

        public List<(int, int)> calcolaMosseRegolari(Koma koma) {
            List<(int, int)> mosseRegolari = new List<(int, int)>();
            for (int i = 0; i < koma.MossePossibili.GetLength(0); i++) {
                int mossaX = koma.MossePossibili[i, 0];
                int mossaY = koma.MossePossibili[i, 1];
                (int, int) posizioneDaControllare = (koma.Posizione.Item1 + mossaX, koma.Posizione.Item2 + mossaY);
                if (controllaPosizioneOutOfBounds(posizioneDaControllare)) {
                    if (koma.GetType() == typeof(Keima)) {
                        if (controllaCasellaLibera(posizioneDaControllare, koma)) {
                            (int, int) mossaRegolare = (mossaX, mossaY);
                            mosseRegolari.Add(mossaRegolare);
                        }
                    } else {
                        if (!pedinaNelMezzo(koma.Posizione, posizioneDaControllare)) {
                            (int, int) mossaRegolare = (mossaX, mossaY);
                            mosseRegolari.Add(mossaRegolare);
                        }
                    }
                }
            }
            return mosseRegolari;
        }

        public (int, int) trovaReNemico(Koma.Giocatore colore) {
            foreach (Koma koma in Scacchiera) {
                if (koma != null) {
                    if (koma.GetType() == typeof(Osho)) {
                        if (koma.Colore != colore)
                            return koma.Posizione;
                    }
                }
            }
            throw new ArgumentException("Nessun Re trovato");
        }

        public bool ScaccoMatto(Koma[,] scacchiera, Koma.Giocatore colore) {
            int numeroMosseLegali = 0;
            foreach(Koma koma in scacchiera) {
                if (koma != null && koma.Colore == colore) {
                    numeroMosseLegali += calcolaMosseLegali(koma).Count;
                }
            }

            if (numeroMosseLegali > 0) {
                return false;
            } else {
                return true;
            }
        }

        public bool ReSottoScacco(Shogiban copia, Koma.Giocatore colore) {
            foreach (Koma koma in copia.Scacchiera) {
                if (koma != null) {
                    if (koma.Colore != colore && koma.CanCaptureKing(copia)) {
                        return true;
                    }
                }
            }
            return false;
        }

        public Shogiban() {
            scacchiera = new Koma[9, 9];
        }

        public Shogiban Copy() {
            Shogiban copia = new Shogiban();

            foreach (Koma koma in scacchiera) {
                if (koma != null)
                    copia.Scacchiera[koma.Posizione.Item1, koma.Posizione.Item2] = koma;
            }

            return copia;
        }
    }
}
