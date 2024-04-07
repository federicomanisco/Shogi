using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {
    public class Shogiban {
        private Koma[,] scacchiera;
        private List<(string koma, bool colore, int numeroDiKoma)> disposizioneKoma;

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
            //  Colonna           Colonna            Riga               Riga
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

            Koma altraKoma = getKoma(posizione);
            if (koma.Colore == altraKoma.Colore) {
                return false;
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
                    return true;
                }
            }
            return false;
        }

        public Shogiban() {
            scacchiera = new Koma[9, 9];
            disposizioneKoma = new List<(string koma, bool colore, int numeroDiKoma)> {
                //Bianco
                ("Osho", true, 1),
                ("Kinsho", true, 2),
                ("Ginsho", true, 2),
                ("Kakugyo", true, 1),
                ("Keima", true, 2),
                ("Hisha", true, 1),
                ("Kyosha", true, 2),
                ("Fuhyo", true, 9),
                //Neri
                ("Osho", false, 1),
                ("Kinsho", false, 2),
                ("Ginsho", false, 2),
                ("Kakugyo", false, 1),
                ("Keima", false, 2),
                ("Hisha", false, 1),
                ("Kyosha", false, 2),
                ("Fuhyo", false, 9)
            };


        }
    }
}
