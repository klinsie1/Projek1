using InventarProgramm.Database.Model;
using InventarProgramm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InventarProgramm.UI.InventarlisteHelper {
    /// <summary>
    /// Interaktionslogik für Detailed.xaml
    /// </summary>
    public partial class Detailed : Window {
        private bool isCreationMode;
        private int id;

        public Detailed(bool isCreationMode, ArtikelViewElement ave) {
            InitializeComponent();
            this.isCreationMode = isCreationMode;
            this.id = ave.Artikelnummer;
            this.tbArtikelname.Text = ave.Artikelname;
            this.tbBestand.Text = ave.Bestand.ToString();
            this.tbBeschreibung.Text = ave.Beschreibung;
            this.cbRegal.ItemsSource = Database.Database.Instance.ExtractAllChoosableRegalElements();
            this.cbRegal.SelectedItem = ave.Regalname;
        }

        private void btnCancel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            this.Hide();
        }

        private void btnSave_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            int regalid = this.ParseRegalId();
            if (this.isCreationMode) {
                Database.Database.Instance.Insert(new Artikel(-1, regalid, this.tbArtikelname.Text, this.tbBeschreibung.Text, Convert.ToInt32(this.tbBestand.Text), "null"));
            } else {
                Database.Database.Instance.Update(new Artikel(this.id, regalid, this.tbArtikelname.Text, this.tbBeschreibung.Text, Convert.ToInt32(this.tbBestand.Text), "null"));
            }
            Inventarliste.Instance.ReloadUI();
            this.Hide();
        }

        private int ParseRegalId() {
            return Convert.ToInt32(this.cbRegal.SelectedItem.ToString().Replace("]", "").Split(' ').ToList().Last());
        }
    }
}
