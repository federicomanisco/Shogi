using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {
    public class Osho: Koma {

        private int[,] mossePossibili; 

        public Osho((int, int) posizione, bool colore, Shogiban scacchiera) : base(posizione, colore, scacchiera) {
            mossePossibili = new int[8, 2] { 
                {0, 1}, 
                {1, 0}, 
                {1, 1}, 
                {0, -1}, 
                {-1, 0}, 
                {-1, -1}, 
                {1, -1}, 
                {-1, 1} 
            };
        }

        public int[,] MossePossibili {
            get { return mossePossibili; }
        }

        public override void promuovi() {
            throw new ArgumentException("Il re non può essere promosso.");
        }

        //public override void muovi((int, int) nuovaPosizione) { }
    }
}
