using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {
    public class Ginsho : Koma{

        public Ginsho((int, int) posizione, Giocatore colore) : base(posizione, colore) {
            Icona = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/generaleArgento.png");
            if (colore == Giocatore.Sente) {
                mossePossibili = new int[5, 2] {
                    {-1, -1},
                    {0, -1},
                    {1, -1},
                    {-1, 1},
                    {1, 1}
                };
            } else {
                mossePossibili = new int[5, 2] {
                    {-1, -1},
                    {0, 1},
                    {1, -1},
                    {-1, 1},
                    {1, 1}
                };
                Icona.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
        }

        public override void promuovi() {
            Promossa = true;
            Icona = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/Promossa/generaleArgento.png");
            if (colore == Giocatore.Sente) {
                mossePossibili = new int[6, 2] {
                    {0, 1},
                    {1, 0},
                    {-1, 0},
                    {0, -1},
                    {-1, -1},
                    {1, -1}
                };
            } else {
                mossePossibili = new int[6, 2] {
                    {0, 1},
                    {-1, 0},
                    {1, 0},
                    {1, 1},
                    {-1, 1},
                    {0, -1}
                };
                Icona.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
        }

        public override void depromuovi()
        {
            Promossa = false;
            Icona = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/generaleArgento.png");
            if (colore == Giocatore.Sente)
            {
                mossePossibili = new int[5, 2] {
                    {-1, -1},
                    {0, -1},
                    {1, -1},
                    {-1, 1},
                    {1, 1}
                };
            }
            else
            {
                mossePossibili = new int[5, 2] {
                    {-1, -1},
                    {0, 1},
                    {1, -1},
                    {-1, 1},
                    {1, 1}
                };
                Icona.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
        }

        public override void changeTeam((int, int) p)
        {
            if (colore == Giocatore.Sente)
            {
                mossePossibili = new int[5, 2] {
                    {-1, -1},
                    {0, 1},
                    {1, -1},
                    {-1, 1},
                    {1, 1}
                };
            }
            else
            {
                mossePossibili = new int[5, 2] {
                    {-1, -1},
                    {0, -1},
                    {1, -1},
                    {-1, 1},
                    {1, 1}
                };
            }
            colore = (colore == Giocatore.Sente) ? Giocatore.Gote : Giocatore.Sente;
            Posizione = p;
        }

        //public override void muovi((int, int) nuovaPosizione) { }
    }
}
