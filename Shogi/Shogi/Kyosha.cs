using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi
{
    internal class Kyosha:Koma //Lanciere
    {
        private int[,] mossePossibili;//prima della virgola ci sono le mosse possibili
        public Kyosha((int, int) posizione, bool colore, Shogiban scacchiera) : base(posizione, colore, scacchiera)
        {
            if (colore) { mossePossibili = new int[8, 2] { { 0, -1 }, { 0, -2 }, {0, -3 }, {   0, -4 }, {0, -5 }, { 0, -6 }, { 0, -7 }, {0, -8 } }; }
            else { mossePossibili = new int[8, 2] { { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 } }; }
            
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
