using RentCar;
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

namespace Rudy_BuyOrRentACar {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window {

        public Login() {
            InitializeComponent();
            usuarioTextBox.Focus();
        }

        private void RegistrarseButton_Click(object sender , RoutedEventArgs e) {


          /*  RegistroUsuario registroUsuario = new RegistroUsuario(0);  //Le paso 0 porque se registra a través del login
            registroUsuario.Owner = this;

            this.Hide();

            registroUsuario.Show();     */

        }

        private void IniciarSesionButton_Click(object sender , RoutedEventArgs e) {

            if (string.IsNullOrWhiteSpace(usuarioTextBox.Text) || string.IsNullOrWhiteSpace(contrasenaTextBox.Password)) {
                MessageBox.Show("Ingrese sus datos de login");
                return;
            } else {
                this.Hide();
                
                MainWindow menuPrincipal = new MainWindow();
                menuPrincipal.Owner = this;
                menuPrincipal.Show();
            }

           /* List<Usuario> usuariosList = new List<Usuario>();
            usuariosList = UsuariosBLL.GetList(u => u.NombreUsuario == usuarioTextBox.Text);
            if(usuariosList.Count == 1) {

                if (usuariosList[0].Clave == contrasenaTextBox.Password) {

                    

                    this.Hide();
                                                                    //Asignando el UsuarioId de esta sesión
                    MenuPrincipal menuPrincipal = new MenuPrincipal(usuariosList[0].UsuarioId);
                    menuPrincipal.Owner = this;
                    menuPrincipal.Show();

                } else {
                    MessageBox.Show("Contraseña incorrecta.");
                }

            } else {
                MessageBox.Show("Este usuario no existe.");
            }     */
        }
    }
}
