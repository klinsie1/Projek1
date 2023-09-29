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

namespace InventarProgramm.UI.LagerHelper {
    /// <summary>
    /// Interaktionslogik für Detailed.xaml
    /// </summary>
    public partial class Detailed : Window {
        private bool isCreationMode;
        private int id;

        public Detailed(bool isCreationMode, LagerViewElement lve) {
            InitializeComponent();
            this.isCreationMode = isCreationMode;
            this.id = lve.ID;
            this.tbLagername.Text = lve.Name;
            this.tbOrt.Text = lve.Ort;
        }

        private void btnCancel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            this.Hide();
        }

        private void btnSave_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            if (this.isCreationMode) {
                Database.Database.Instance.Insert(new Lager(-1, this.tbLagername.Text, this.tbOrt.Text));
            } else {
                Database.Database.Instance.Update(new Lager(this.id, this.tbLagername.Text, this.tbOrt.Text));
            }
            Lagerliste.Instance.ReloadUI();
            this.Hide();
        }

        private void tbOrt_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter)
                this.btnSave_MouseLeftButtonUp(null, null);
        }
    }
}
