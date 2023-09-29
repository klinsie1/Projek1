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

namespace InventarProgramm.UI.LagerHelper {
    /// <summary>
    /// Interaktionslogik für Row.xaml
    /// </summary>
    public partial class Row : UserControl {
        public Row() {
            InitializeComponent();
        }

        private void btnEdit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            new Detailed(false, (this.DataContext as LagerViewElement)) { WindowStartupLocation = WindowStartupLocation.CenterOwner, Owner = (Window.GetWindow(this) as Lagerliste) }.ShowDialog();
        }

        private void btnRemove_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            int myLagerId = (this.DataContext as LagerViewElement).ID;
            bool isRegalInLagerStillExisting = false;
            Database.Database.Instance.Regals.Values.ToList().ForEach(regal => { if (regal.Lager_id == myLagerId) isRegalInLagerStillExisting = true; });
            if (isRegalInLagerStillExisting) {
                MessageBox.Show("Lager konnte nicht gelöscht werden da noch ein Regal darin existiert.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            } else {
                Database.Database.Instance.Delete(Database.ModelTyp.lager, myLagerId);
                this.ReloadUI();
            }
        }

        private void ReloadUI() {
            (Window.GetWindow(this) as Lagerliste).ReloadUI();
        }
    }
}
