using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi
{
    internal class Fuhyo:Koma //Pedone
    {
        private int[,] mossePossibili;//prima della virgola ci sono le mosse possibili
        public Fuhyo((int, int) posizione, bool colore, Shogiban scacchiera) : base(posizione, colore, scacchiera)
        {
            if(colore) {mossePossibili = new int[1, 2] { { 0, -1 } };}
            else { mossePossibili = new int[1, 2] { { 0, 1 } }; }
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
        }
    }
}
