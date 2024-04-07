using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Shogi {
    public class Kubomawashi 
    {

        string PERCORSOIMMAGINE = Application.StartupPath;

        List<Koma> list = new List<Koma>();
        public Kubomawashi()
        {
            
        }  

        public void AddKoma(Koma pedina)
        {
            list.Add(pedina);
        }
        public void RemoveKoma(Koma pedina)
        {
            list.Remove(pedina);
        }





    }
}
