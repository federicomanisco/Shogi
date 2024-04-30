using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {
    public abstract class Koma {
        protected string nomepedina;
        private (int, int) posizione;
        protected Giocatore colore; //Sente (sfidante), Gote (sfidato)
        private bool promossa;
        protected int[,] mossePossibili;//prima della virgola ci sono le mosse possibili
        [JsonIgnore]
        public int[,] MossePossibili {
            get { return mossePossibili; }
        }
        protected string PERCORSOIMMAGINE = Application.StartupPath;
        private Image icona;

        public (int, int) Posizione {
            get { return posizione; }
            set {
                posizione = value;
            }
        }

        public enum Giocatore {
            Sente,
            Gote
        }

        [JsonIgnore]
        public Image Icona
        {
            get { return icona; }
            set
            {
                icona = value;
            }
        }

        public bool Promossa {
            get { return promossa; }
            set { promossa = value; }
        }

        public Giocatore Colore {
            get { return colore; }
            set {
                colore = value;
            }
        }

        public bool CanCaptureKing(Shogiban shogiban) {
            List<(int, int)> mosseRegolari = shogiban.calcolaMosseRegolari(this);
            foreach ((int, int) mossaPossibile in mosseRegolari) {
                (int, int) nuovaPosizione = (Posizione.Item1 + mossaPossibile.Item1, Posizione.Item2 + mossaPossibile.Item2);
                if (nuovaPosizione == shogiban.trovaReNemico(Colore)) {
                    return true;
                }
            }
            return false;
        }

        public abstract void promuovi();

        public abstract void depromuovi();

        public abstract void changeTeam((int,int)p); //quando una koma viene mangiata e cambia "team"


        public Koma((int, int) posizione, Giocatore colore) {
            Posizione = posizione;
            Colore = colore; 
        }

    }

}