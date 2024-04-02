using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {
    public class Kakugyo : Koma{
        private int[,] mossePossibili;

        public Kakugyo((int, int) posizione, bool colore) : base(posizione, colore) {
            Icona = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/alfiere.png");

            if (!colore) {
                Icona.RotateFlip(RotateFlipType.Rotate180FlipX);
            }

            mossePossibili = new int[32, 2] {
                //diagonale discendente (\)
                {-1, -1}, {-2, -2}, {-3, -3}, {-4, -4}, {-5, -5}, {-6, -6}, {-7, -7}, {-8, -8},  
                {1, 1}, {2, 2}, {3, 3}, {4, 4}, {5, 5}, {6, 6}, {7, 7}, {8, 8},
                //diagonale ascendente  (/)
                {1, -1}, {2, -2}, {3, -3}, {4, -4}, {5, -5}, {6, -6}, {7, -7}, {8, -8},  
                {-1, 1}, {-2, 2}, {-3, 3}, {-4, 4}, {-5, 5}, {-6, 6}, {-7, 7}, {-8, 8}
            };
        }

        public override void promuovi() {
            mossePossibili = new int[36, 2] {
                //diagonale discendente (\)
                {-1, -1}, {-2, -2}, {-3, -3}, {-4, -4}, {-5, -5}, {-6, -6}, {-7, -7}, {-8, -8},
                {1, 1}, {2, 2}, {3, 3}, {4, 4}, {5, 5}, {6, 6}, {7, 7}, {8, 8},
                //diagonale ascendente  (/)
                {1, -1}, {2, -2}, {3, -3}, {4, -4}, {5, -5}, {6, -6}, {7, -7}, {8, -8},
                {-1, 1}, {-2, 2}, {-3, 3}, {-4, 4}, {-5, 5}, {-6, 6}, {-7, 7}, {-8, 8},
                //caselle adiacenti
                {1, 0}, {-1, 0}, {0, 1}, {0, -1},
            };
            Icona = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/Promossa/alfiere.png");

            if (!colore) {
                Icona.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
        }

        //public override void muovi((int, int) nuovaPosizione) { }
    }
}
