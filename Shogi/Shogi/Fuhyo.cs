﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi
{
    internal class Fuhyo:Koma //Pedone
    {
        private int[,] mossePossibili;//prima della virgola ci sono le mosse possibili
        public Fuhyo((int, int) posizione, bool colore) : base(posizione, colore)
        {
            Icona = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/pedone.png");
            if (colore) {
                mossePossibili = new int[1, 2] { { 0, -1 } };
            } else { 
                mossePossibili = new int[1, 2] { { 0, 1 } };
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
                Icona = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/Promossa/pedone.png");
            }
            else
            {
                mossePossibili = new int[6, 2]
                {
                    {0, -1},{1, 0},{-1, 0},{0, 1},{1, -1},{1, 1}
                };
                Icona = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/Promossa/pedone.png");
                Icona.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
        }
    }
}
