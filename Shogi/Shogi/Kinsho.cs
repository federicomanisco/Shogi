﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {
    public class Kinsho : Koma{

        public Kinsho((int, int) posizione, Giocatore colore) : base(posizione, colore) {
            Icona = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/generaleOro.png");
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
                    {0, -1}, 
                    {1, 0}, 
                    {-1, 0}, 
                    {0, 1}, 
                    {-1, 1}, 
                    {1, 1} 
                };
                Icona.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
        }

        public override void promuovi() {
            throw new ArgumentException("Il generale d'oro non può essere promosso.");
        }
        public override void depromuovi()
        {
            throw new ArgumentException("Il generale d'oro non può essere depromosso.");
        }
        public override void changeTeam((int, int) p)
        {
            if (colore == Giocatore.Sente)
            {
                mossePossibili = new int[6, 2] {
                    {0, -1},
                    {1, 0},
                    {-1, 0},
                    {0, 1},
                    {-1, 1},
                    {1, 1}
                };
            }
            else
            {
                mossePossibili = new int[6, 2] {
                    {0, 1},
                    {1, 0},
                    {-1, 0},
                    {0, -1},
                    {-1, -1},
                    {1, -1}
                };
            }
            colore = (colore == Giocatore.Sente) ? Giocatore.Gote : Giocatore.Sente;
            Posizione = p;
        }
        //public override void muovi((int, int) nuovaPosizione) { }
    }
}
