using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi
{
    internal class Hisha:Koma //Torre
    {//prima della virgola ci sono le mosse possibili
        public Hisha((int, int) posizione, bool colore) : base(posizione, colore)
        {    
            mossePossibili = new int[32, 2]
            {
                { 0, 1 }, { 0, 2 }, {0, 3}, {0, 4}, {0, 5}, {0, 6}, {0, 7}, {0, 8},//alto
                { -1, 0 }, { -2, 0  }, {-3, 0}, {-4, 0}, {-5, 0}, {-6, 0}, {-7, 0}, {-8, 0},//destra
                { 1, 0 }, {2, 0}, {3, 0}, {4, 0}, {5, 0}, {6, 0}, {7, 0}, {8, 0},//sinistra
                { 0, -1 }, { 0, -2 }, {0, -3}, {0, -4}, {0, -5}, {0, -6}, {0, -7}, {0,-8}//in basso
            };
            Icona = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/torre.png");

            if (!colore) {
                Icona.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
        }
        public override void promuovi()
        {
            Promossa = true;
            mossePossibili = new int[36, 2]
            {
                { 0, 1 }, { 0, 2 }, {0, 3}, {0, 4}, {0, 5}, {0, 6}, {0, 7}, {0, 8},//alto
                { -1, 0 }, {-2, 0}, {-3, 0}, {-4, 0}, {-5, 0}, {-6, 0}, {-7, 0}, {-8, 0},//destra
                {1, 0}, {2, 0}, {3, 0}, {4, 0}, {5, 0}, {6, 0}, {7, 0}, {8, 0},//sinistra
                { 0, -1 }, { 0, -2 }, {0, -3}, {0, -4}, {0, -5}, {0, -6}, {0, -7}, {0,-8},//in basso
                {1,1 },{1,-1 },{-1,-1 },{-1,1 }
            };
            Icona = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/Promossa/torre.png");

            if (!colore) {
                Icona.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
        }

        public override void depromuovi()
        {
            Promossa = false;
            mossePossibili = new int[32, 2]
{
                { 0, 1 }, { 0, 2 }, {0, 3}, {0, 4}, {0, 5}, {0, 6}, {0, 7}, {0, 8},//alto
                { -1, 0 }, { -2, 0  }, {-3, 0}, {-4, 0}, {-5, 0}, {-6, 0}, {-7, 0}, {-8, 0},//destra
                { 1, 0 }, {2, 0}, {3, 0}, {4, 0}, {5, 0}, {6, 0}, {7, 0}, {8, 0},//sinistra
                { 0, -1 }, { 0, -2 }, {0, -3}, {0, -4}, {0, -5}, {0, -6}, {0, -7}, {0,-8}//in basso
};
            Icona = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/torre.png");

            if (!colore)
            {
                Icona.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
        }
    }
}
