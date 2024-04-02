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
            if (posizione.Item1 < 1 || posizione.Item1 > 9 || posizione.Item2 < 1 || posizione.Item2 > 9)
                return false;
            return true;
        }

        public Koma getKoma((int, int) posizione) {
            if (!controllaPosizioneOutOfBounds(posizione)) {
                throw new ArgumentException("Posizione fuori dai limiti della scacchiera");
            }
            return scacchiera[posizione.Item1 - 1, posizione.Item2 - 1];
        }

        public bool controllaCasellaLibera((int, int) posizione, Koma koma) {
            if (controllaPosizioneOutOfBounds(posizione)) {
                return false;
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
