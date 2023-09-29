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

namespace InventarProgramm.UI {
    /// <summary>
    /// Interaktionslogik für Inventarliste.xaml
    /// </summary>
    public partial class Inventarliste : Window {
        public static Inventarliste Instance;

        public Inventarliste() {
            InitializeComponent();
            Instance = this;
            this.ReloadUI();
        }

        public void ReloadUI() {
            this.icContainer.ItemsSource = null;
            var filteredText = this.tbFiltertext.Text.ToLower();
            if (this.tbFiltertext.Text == "") {
                this.icContainer.ItemsSource = Database.Database.Instance.ExtractArtikelViewElements();
            } else {
                var filteredList = new List<ArtikelViewElement>();
                foreach (var ave in Database.Database.Instance.ExtractArtikelViewElements()) {
                    if (ave.Artikelnummer.ToString().ToLower().Contains(filteredText) ||
                        ave.Artikelname.ToString().ToLower().Contains(filteredText) ||
                        ave.Beschreibung.ToString().ToLower().Contains(filteredText) ||
                        ave.Regalname.ToString().ToLower().Contains(filteredText))
                        filteredList.Add(ave);
                }
                this.icContainer.ItemsSource = filteredList;
            }
        }

        private void btnAddArtikel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            new InventarlisteHelper.Detailed(true, new Model.ArtikelViewElement(-1, "", 0, "", "")) { WindowStartupLocation = WindowStartupLocation.CenterOwner, Owner = this }.ShowDialog();
        }

        private void btnAddRegal_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            new Regalliste() { WindowStartupLocation = WindowStartupLocation.CenterOwner, Owner = this }.ShowDialog();
        }

        private void btnAddLager_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            new Lagerliste() { WindowStartupLocation = WindowStartupLocation.CenterOwner, Owner = this }.ShowDialog();
        }

        private void tbFiltertext_TextChanged(object sender, TextChangedEventArgs e) {
            this.ReloadUI();
        }
    }
}
