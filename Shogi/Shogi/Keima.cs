﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Shogi
{
    internal class Keima:Koma //cavallo
    {
        private int[,] mossePossibili;//prima della virgola ci sono le mosse possibili
        public Keima((int, int) posizione, bool colore) : base(posizione, colore)
        {
            Icona = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/cavallo.png");
            if (colore) { 
                mossePossibili = new int[2, 2] { { 1, 2 }, { -1, 2 } }; 
            } else { 
                mossePossibili = new int[2, 2] { { 1, -2 }, { -1, -2 } };
                Icona.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
        }
        public override void promuovi()
        {
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
            }
            Icona = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/Promossa/cavallo.png");
        }
        
    }
}
