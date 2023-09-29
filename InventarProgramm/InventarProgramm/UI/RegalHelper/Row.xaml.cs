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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventarProgramm.UI.RegalHelper {
    /// <summary>
    /// Interaktionslogik für Row.xaml
    /// </summary>
    public partial class Row : UserControl {
        public Row() {
            InitializeComponent();
        }

        private void btnEdit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            new Detailed(false, (this.DataContext as RegalViewElement)) { WindowStartupLocation = WindowStartupLocation.CenterOwner, Owner = (Window.GetWindow(this) as Regalliste) }.ShowDialog();
        }

        private void btnRemove_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            int myRegalId = (this.DataContext as RegalViewElement).ID;
            bool isArtikelInRegalStillExisting = false;
            Database.Database.Instance.Artikels.Values.ToList().ForEach(artikel => { if (artikel.RegalId == myRegalId) isArtikelInRegalStillExisting = true; });
            if (isArtikelInRegalStillExisting) {
                MessageBox.Show("Regal konnte nicht gelöscht werden da noch ein Artikel darin existiert.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            } else {
                Database.Database.Instance.Delete(Database.ModelTyp.regal, myRegalId);
                this.ReloadUI();
            }
        }

        private void ReloadUI() {
            (Window.GetWindow(this) as Regalliste).ReloadUI();
        }
    }
}
