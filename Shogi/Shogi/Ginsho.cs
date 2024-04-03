using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {
    public class Ginsho : Koma{

        public Ginsho((int, int) posizione, bool colore) : base(posizione, colore) {
            Icona = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/generaleArgento.png");
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
                Icona.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
        }

        public override void promuovi() {
            Icona = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/Promossa/generaleArgento.png");
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
                Icona.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
        }

        //public override void muovi((int, int) nuovaPosizione) { }
    }
}
