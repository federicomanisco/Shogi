using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi
{
    internal class Hisha:Koma //Torre
    {
        private int[,] mossePossibili;//prima della virgola ci sono le mosse possibili
        public Hisha((int, int) posizione, bool colore, Shogiban scacchiera) : base(posizione, colore, scacchiera)
        {    
            mossePossibili = new int[32, 2]
            {
                { 0, 1 }, { 0, 2 }, {0, 3}, {0, 4}, {0, 5}, {0, 6}, {0, 7}, {0, 8},//alto
                { 0, -1 }, { 0,-2  }, { 0, -3 }, { 0, -4 }, { 0, -5 }, { 0, -6 }, {0, -7}, {0, -8},//destra
                { 0, 1 }, { 0,2   }, { 0,3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, {0, 7}, {0, 8},//sinistra
                { 0, -1 }, { 0, -2 }, {0, -3}, {0, -4}, {0, -5}, {0, -6}, {0, -7}, {0,-8}//in basso
            };     
        }  
        public override void promuovi()
        {
            mossePossibili = new int[36, 2]
            {
                { 0, 1 }, { 0, 2 }, {0, 3}, {0, 4}, {0, 5}, {0, 6}, {0, 7}, {0, 8},//alto
                { 0, -1 }, { 0,-2  }, { 0, -3 }, { 0, -4 }, { 0, -5 }, { 0, -6 }, {0, -7}, {0, -8},//destra
                { 0, 1 }, { 0,2   }, { 0,3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, {0, 7}, {0, 8},//sinistra
                { 0, -1 }, { 0, -2 }, {0, -3}, {0, -4}, {0, -5}, {0, -6}, {0, -7}, {0,-8},//in basso
                {1,1 },{1,-1 },{-1,-1 },{-1,1 }
            };
        }
    }
}
