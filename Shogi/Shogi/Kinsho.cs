using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {
    public class Kinsho : Koma{
        public Kinsho((int, int) posizione, bool colore, Shogiban scacchiera) : base(posizione, colore, scacchiera) {
        }

        public override void promuovi() {
            throw new NotImplementedException();
        }

        //public override void muovi((int, int) nuovaPosizione) { }
    }
}
