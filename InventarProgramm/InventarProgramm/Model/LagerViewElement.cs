using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarProgramm.Model {
    public class LagerViewElement {
        public LagerViewElement(int iD, string name, string ort) {
            ID = iD;
            Name = name;
            Ort = ort;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Ort { get; set; }
    }
}
