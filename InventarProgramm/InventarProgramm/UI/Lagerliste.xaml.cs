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
    /// Interaktionslogik für Lagerliste.xaml
    /// </summary>
    public partial class Lagerliste : Window {
        public static Lagerliste Instance;

        public Lagerliste() {
            InitializeComponent();
            Instance = this;
            this.ReloadUI();
        }

        public void ReloadUI() {
            this.icContainer.ItemsSource = null;
            this.icContainer.ItemsSource = Database.Database.Instance.ExtractLagerViewElements();
        }

        private void btnAddLager_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            new LagerHelper.Detailed(true, new Model.LagerViewElement(-1, "", "")) { WindowStartupLocation = WindowStartupLocation.CenterOwner, Owner = this }.ShowDialog();
        }
    }
}
