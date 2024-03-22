using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {
    public class Ginsho : Koma{
        private int[,] mossePossibili;

        public Ginsho((int, int) posizione, bool colore, Shogiban scacchiera) : base(posizione, colore, scacchiera) {
            if (colore) {
                mossePossibili = new int[5, 2] {
                    {-1, -1},
                    {0, -1},
                    {1, -1},
                    {-1, 1},
                    {1, 1}
                };
            } else {
                mossePossibili = new int[5, 2] {
                    {-1, -1},
                    {0, 1},
                    {1, -1},
                    {-1, 1},
                    {1, 1}
                };
            }
        }

        public override void promuovi() {
            if (colore) {
                mossePossibili = new int[6, 2] {
                    {0, 1},
                    {1, 0},
                    {-1, 0},
                    {0, -1},
                    {-1, -1},
                    {1, -1}
                };
            } else {
                mossePossibili = new int[6, 2] {
                    {0, -1},
                    {1, 0},
                    {-1, 0},
                    {0, 1},
                    {1, -1},
                    {1, 1}
                };
            }
        }

        //public override void muovi((int, int) nuovaPosizione) { }
    }
}
