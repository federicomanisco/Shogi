using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {
    public class Osho: Koma {

        public Osho((int, int) posizione, Giocatore colore) : base(posizione, colore) {
            mossePossibili = new int[8, 2] { 
                {0, 1}, 
                {1, 0}, 
                {1, 1}, 
                {0, -1}, 
                {-1, 0}, 
                {-1, -1}, 
                {1, -1}, 
                {-1, 1} 
            };
            Icona = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/re.png");

            if (colore == Giocatore.Gote) {
                Icona.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
        }

        public override void promuovi() {
            throw new ArgumentException("Il re non può essere promosso.");
        }
        public override void depromuovi()
        {
            throw new ArgumentException("Il re non può essere depromosso.");
        }
        public override void changeTeam((int, int) p)
        {
            throw new ArgumentException("Il re non può essere rimesso sulla shogiban.");
        }
    }
}
