using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {
    public class Salvataggio {
        public Koma[,] ShogibanState { get; set; }
        public ((int, int), (int, int)) TempiGiocatori { get; set; }
        public Koma.Giocatore Turno { get; set; }
        public Kubomawashi KubomawashiSente { get; set; }
        public Kubomawashi KubomawashiGote { get; set; }

        public Salvataggio(Koma[,] shogibanState, Koma.Giocatore turno, ((int, int), (int, int)) tempiGiocatori, Kubomawashi kubomawashiGote, Kubomawashi kubomawashiSente) {
            ShogibanState = shogibanState;
            Turno = turno;
            TempiGiocatori = tempiGiocatori;
            KubomawashiGote = kubomawashiGote;
            KubomawashiSente = kubomawashiSente;
        }
    }
}
