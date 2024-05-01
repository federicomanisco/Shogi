using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {
    public class Kubomawashi 
    {
        private Koma ultimaPedinaMangiata;
        public Koma UltimaPedinaMangiata {
            get { return ultimaPedinaMangiata; }
            set { ultimaPedinaMangiata = value; }
        }

        private List<Koma> pedine = new List<Koma>();
        public List<Koma> Pedine { get { return pedine; } } 
        public Kubomawashi()
        {
            
        }  

        public void AddKoma(Koma pedina)
        {
            pedine.Add(pedina);
        }

        public void RemoveKoma(Koma pedina)
        {
            pedine.Remove(pedina);
        }
    }
}
