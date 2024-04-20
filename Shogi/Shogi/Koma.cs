using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {
    public abstract class Koma {
        protected string nomepedina;
        private (int, int) posizione;
        protected bool colore; //true=Sente, false=Gote
        private bool promossa;
        protected int[,] mossePossibili;//prima della virgola ci sono le mosse possibili
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

        public bool Colore {
            get { return colore; }
            set {
                colore = value;
            }
        }

        

        

        public void muovi((int, int) spostamento) {
            bool spostamentoValido = false;

            for (int i = 0; i < MossePossibili.GetLength(0); i++) {
                int spostamentoX = spostamento.Item1;
                int spostamentoY = spostamento.Item2;
                int mossaPossibileX = MossePossibili[i, 0];
                int mossaPossibileY = MossePossibili[i, 1];
                if (spostamentoX == mossaPossibileX && spostamentoY == mossaPossibileY) {
                    spostamentoValido = true;
                }
            }

            if (!spostamentoValido) {
                throw new ArgumentException("La mossa inserita non è valida.");
            } else {
                (int, int) nuovaPosizione = (Posizione.Item1 + spostamento.Item1, Posizione.Item2 + spostamento.Item2);
                Posizione = nuovaPosizione;
            }
        }
        

        public abstract void promuovi();


        public Koma((int, int) posizione, bool colore) {
            Posizione = posizione;
            Colore = colore;        }
    }
}