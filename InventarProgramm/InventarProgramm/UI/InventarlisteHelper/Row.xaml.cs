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

namespace InventarProgramm.UI.InventarlisteHelper {
    /// <summary>
    /// Interaktionslogik für Row.xaml
    /// </summary>
    public partial class Row : UserControl {
        public Row() {
            InitializeComponent();
        }

        private void btnEdit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            new Detailed(false, (this.DataContext as ArtikelViewElement)) { WindowStartupLocation = WindowStartupLocation.CenterOwner, Owner = (Window.GetWindow(this) as Inventarliste) }.ShowDialog();
        }

        private void btnRemove_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            Database.Database.Instance.Delete(Database.ModelTyp.artikel, (this.DataContext as ArtikelViewElement).Artikelnummer);
            this.ReloadUI();
        }

        private void ReloadUI() {
            (Window.GetWindow(this) as Inventarliste).ReloadUI();
        }
    }
}
