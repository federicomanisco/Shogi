using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi
{
    internal class Fuhyo:Koma
    {
        public Fuhyo((int, int) posizione, bool colore, Shogiban scacchiera) : base(posizione, colore, scacchiera)
        {

        }
        public override void promuovi()
        {

        }
    }
}
