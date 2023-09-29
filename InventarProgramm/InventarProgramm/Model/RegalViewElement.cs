using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarProgramm.Model {
    public class RegalViewElement {
        public RegalViewElement(int iD, string name, string lagername) {
            ID = iD;
            Name = name;
            Lagername = lagername;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Lagername { get; set; }
    }
}
