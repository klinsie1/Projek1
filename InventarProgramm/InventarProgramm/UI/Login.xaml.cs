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
    /// Interaktionslogik für Login.xaml
    /// </summary>
    public partial class Login : Window {
        public Login() {
            InitializeComponent();
        }

        private void btnLogin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            this.login();
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                this.login();
            }
        }

        private void login() {
            bool succeeded = false;
            Database.Database.Instance.Users.Values.ToList().ForEach(user => {
                if (user.Name == this.tbUsername.Text) {
                    succeeded = user.password == this.tbPassword.Password;
                }
            });
            if (succeeded) {
                this.Hide();
                new Inventarliste() { WindowStartupLocation = WindowStartupLocation.CenterOwner, Owner = this }.Show();
            } else {
                MessageBox.Show("Benutzername oder Passwort falsch.", "Fehlerhafte Anmeldedaten", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}