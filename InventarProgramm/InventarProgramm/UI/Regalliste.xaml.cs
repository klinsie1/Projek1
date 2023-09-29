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
    /// Interaktionslogik für Regalliste.xaml
    /// </summary>
    public partial class Regalliste : Window {
        public static Regalliste Instance;

        public Regalliste() {
            InitializeComponent();
            Instance = this;
            this.ReloadUI();
        }

        public void ReloadUI() {
            this.icContainer.ItemsSource = null;
            this.icContainer.ItemsSource = Database.Database.Instance.ExtractRegalViewElements();
        }

        private void btnAddRegal_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            new RegalHelper.Detailed(true, new Model.RegalViewElement(-1, "", "")) { WindowStartupLocation = WindowStartupLocation.CenterOwner, Owner = this }.ShowDialog();
        }
    }
}
