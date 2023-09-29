using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarProgramm.Model {
    public class ArtikelViewElement {
        public ArtikelViewElement(int artikelnummer, string artikelname, int bestand, string beschreibung, string regalname) {
            Artikelnummer = artikelnummer;
            Artikelname = artikelname;
            Bestand = bestand;
            Beschreibung = beschreibung;
            Regalname = regalname;
        }

        public int Artikelnummer { get; set; }
        public string Artikelname { get; set; }
        public int Bestand { get; set; }
        public string Beschreibung { get; set; }
        public string Regalname { get; set; }
    }
}
