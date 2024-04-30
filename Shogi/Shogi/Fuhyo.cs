using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi
{
    internal class Fuhyo:Koma //Pedone
    {
        public Fuhyo((int, int) posizione, Giocatore colore) : base(posizione, colore)
        {
            Icona = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/pedone.png");
            if (colore == Giocatore.Sente) {
                mossePossibili = new int[1, 2] { { 0, -1 } };
            } else { 
                mossePossibili = new int[1, 2] { { 0, 1 } };
                Icona.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
        }

        public override void promuovi()
        {
            Promossa = true;
            if (colore == Giocatore.Sente)
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
                    {0, 1},{-1, 0},{1, 0},{1, 1},{-1, 1},{0, -1}
                };
                Icona = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/Promossa/pedone.png");
                Icona.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
        }

        public override void depromuovi()
        {
            Promossa = false;
            Icona = Image.FromFile($"{PERCORSOIMMAGINE}/shogiPieces/pedone.png");
            if (colore == Giocatore.Sente)
            {
                mossePossibili = new int[1, 2] { { 0, -1 } };
            }
            else
            {
                mossePossibili = new int[1, 2] { { 0, 1 } };
                Icona.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
        }

        public override void changeTeam((int,int) p)
        {
            if (colore == Giocatore.Sente)
            {
                mossePossibili = new int[1, 2] { { 0, 1 } };
            }
            else
            {
                mossePossibili = new int[1, 2] { { 0, -1 } };
            }
            colore = (colore == Giocatore.Sente) ? Giocatore.Gote : Giocatore.Sente;
            Posizione = p;
        }
    }
}
