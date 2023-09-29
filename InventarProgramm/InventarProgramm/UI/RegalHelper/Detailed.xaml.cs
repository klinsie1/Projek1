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

namespace InventarProgramm.UI.RegalHelper {
    /// <summary>
    /// Interaktionslogik für Detailed.xaml
    /// </summary>
    public partial class Detailed : Window {
        private bool isCreationMode;
        private int id;

        public Detailed(bool isCreationMode, RegalViewElement rve) {
            InitializeComponent();
            this.isCreationMode = isCreationMode;
            this.id = rve.ID;
            this.tbRegalname.Text = rve.Name;
            this.cbLager.ItemsSource = Database.Database.Instance.ExtractAllChoosableLagerElements();
            this.cbLager.SelectedItem = rve.Lagername;
        }

        private void btnCancel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            this.Hide();
        }

        private void btnSave_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            int lagerid = this.ParseLagerId();
            if (this.isCreationMode) {
                Database.Database.Instance.Insert(new Regal(-1, lagerid, this.tbRegalname.Text));
            } else {
                Database.Database.Instance.Update(new Regal(this.id, lagerid, this.tbRegalname.Text));
            }
            Regalliste.Instance.ReloadUI();
            this.Hide();
        }

        private int ParseLagerId() {
            return Convert.ToInt32(this.cbLager.SelectedItem.ToString().Replace(")", "").Split(' ').ToList().Last());
        }
    }
}
