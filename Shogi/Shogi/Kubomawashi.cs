using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {
    public class Kubomawashi 
    {
        List<Koma> list;
        public Kubomawashi(Koma pedina)
        {
            list = new List<Koma>();
        }   
        public void Add(Koma pedina)
        {
            list.Add(pedina);
        }
    }
}
