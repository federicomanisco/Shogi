﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {
    public abstract class Koma {
        protected string nomepedina;
        private (int, int) posizione;
        protected bool colore;
        private bool promossa;
        protected string PERCORSOIMMAGINE = Application.StartupPath;
        private Image icona;


        public (int, int) Posizione {
            get { return posizione; }
            set {
                posizione = value;
            }
        }

        public Image Icona
        {
            get { return icona; }
            set
            {
                icona = value;
            }
        }

        public bool Promossa {
            get { return promossa; }
        }

        public bool Colore {
            get { return colore; }
            set {
                colore = value;
            }
        }

        

        

        public void muovi((int, int) nuovaPosizione) {
            /*if (!scacchiera.controllaCasellaLibera(nuovaPosizione, this)) {
                throw new ArgumentException("Casella occupata da un'altra koma alleata");
            }
            scacchiera.rimuoviKoma(Posizione);
            Posizione = nuovaPosizione;
            scacchiera.aggiungiKoma(this, Posizione);         
            */
        }
        

        public abstract void promuovi();


        public Koma((int, int) posizione, bool colore) {
            Posizione = posizione;
            Colore = colore;
        }
    }
}