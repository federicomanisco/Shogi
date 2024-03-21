using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {
    public abstract class Koma {
        protected string nomepedina;
        protected int[,] mossePossibili;
        private (int, int) posizione;
        private bool colore;
        private bool promossa;
        private Shogiban scacchiera;
        private Image icona;


        public Shogiban Scacchiera {
            get { return Scacchiera; }
            set {
                scacchiera = value;
            }
        }

        public (int, int) Posizione {
            get { return posizione; }
            set {
                posizione = value;
            }
        }

        public bool Promossa {
            get { return promossa; }
        }

        public bool Colore {
            get { return colore; }
            set {
                colore = value;
            }
        }

        

        

        public void muovi((int, int) nuovaPosizione) {
            if (!scacchiera.controllaCasellaLibera(nuovaPosizione, this)) {
                throw new ArgumentException("Casella occupata da un'altra koma alleata");
            }
            scacchiera.rimuoviKoma(Posizione);
            Posizione = nuovaPosizione;
            scacchiera.aggiungiKoma(this, Posizione);
            
        }
        public  int controllomossa(int a, int b)
        {
            for (int i = 0; i < mossePossibili.GetLength(0); i++)
            {
                for (int j = 0; j < mossePossibili.GetLength(1) - 1; j++)
                {
                    if (mossePossibili[i, j] == a && mossePossibili[i, j + 1] == b)
                    {
                        return 0; // Mosse valide trovate
                    }
                }
            }
            return 1;//1 errori
        }

        public abstract void promuovi();


        public Koma((int, int) posizione, bool colore, Shogiban scacchiera) {
            Posizione = posizione;
            Colore = colore;
            Scacchiera = scacchiera;
        }
    }
}