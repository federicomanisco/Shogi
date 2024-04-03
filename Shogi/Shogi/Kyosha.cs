using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi
{
    internal class Kyosha:Koma //Lanciere
    {
        public Kyosha((int, int) posizione, bool colore) : base(posizione, colore)
        {
            Icona = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/lancia.png");
            if (colore) { 
                mossePossibili = new int[8, 2] { { 0, -1 }, { 0, -2 }, {0, -3 }, {   0, -4 }, {0, -5 }, { 0, -6 }, { 0, -7 }, {0, -8 } };
            }
            else { 
                mossePossibili = new int[8, 2] { { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 } };
                Icona.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
            
        }
        public override void promuovi()
        {
            Icona = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/Promossa/lancia.png");
            if (colore)
            {
                mossePossibili = new int[6, 2]
                {
                    {0, 1},{1, 0},{-1, 0},{0, -1},{-1, -1},{1, -1}
                };
            }
            else
            {
                mossePossibili = new int[6, 2]
                {
                    {0, -1},{1, 0},{-1, 0},{0, 1},{1, -1},{1, 1}
                };
                Icona.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
        }
    }
}
