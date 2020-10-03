using RentCar.BLL;
using RentCar.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace RentCar.UI.Registros {
    /// <summary>
    /// Interaction logic for Cliente.xaml
    /// </summary>
    public partial class RegistroCliente : Window, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        public Cliente cliente { get; set; }

        public RegistroCliente() {
            InitializeComponent();

            cliente = new Cliente();
            this.DataContext = this;
            MyPropertyChanged("cliente");
        }

        private void MyPropertyChanged(string propiedad) {
            PropertyChanged?.Invoke(this , new PropertyChangedEventArgs(propiedad));
        }

        private async void BuscarButton_Click(object sender , RoutedEventArgs e) {

            if (int.TryParse(ClienteIdTextBox.Text , out int clienteId)) {

                if (await ClientesBLL.Existe(clienteId)) {
                    cliente = await ClientesBLL.Buscar(clienteId);
                    MyPropertyChanged("cliente");
                } else {
                    MessageBox.Show("Este cliente no existe.");
                    Limpiar();
                }

            } else {
                MessageBox.Show("Este cliente id es invalido.");
            }

        }

        private void NuevoButton_Click(object sender , RoutedEventArgs e) {

            Limpiar();
        }

        private async void GuardarButton_Click(object sender , RoutedEventArgs e) {
            bool guardado = false;

            if (CamposValidos()) {
                guardado = await ClientesBLL.Guardar(cliente);

                if (guardado) {
                    Limpiar();
                    MessageBox.Show("Guardado.");
                } else {
                    MessageBox.Show("Error al guardar.");
                }
            }

        }

        private async void EliminarButton_Click(object sender , RoutedEventArgs e) {

            bool eliminado = false;

            if (int.TryParse(ClienteIdTextBox.Text , out int clienteId)) {

                if (await ClientesBLL.Existe(clienteId)) {
                    MessageBoxResult opcion = MessageBox.Show("Desea eliminar este cliente?." , "Confirme" , MessageBoxButton.YesNo , MessageBoxImage.Question);

                    if (opcion == MessageBoxResult.Yes) {
                        eliminado = await ClientesBLL.Eliminar(clienteId);

                        if (eliminado) {
                            Limpiar();
                            MessageBox.Show("Eliminado.");
                        } else {
                            MessageBox.Show("Error al eliminar.");
                        }
                    }

                } else {
                    MessageBox.Show("Este cliente no existe.");
                }

            } else {
                MessageBox.Show("Cliente Id invalido.");
            }

        }

        private void Limpiar() {
            cliente = new Cliente();
            MyPropertyChanged("cliente");
        }

        private bool CamposValidos() {
            bool validados = true;
            string mensaje = "Errores: \n\n";

            if (!int.TryParse(ClienteIdTextBox.Text , out int clienteId)) {
                validados = false;
                mensaje += "\nCliente Id invalido.";
            }
            if (string.IsNullOrWhiteSpace(EmailTextBox.Text)) {
                validados = false;
                mensaje += "\nDebe introducir un e-mail.";
            }
            if (string.IsNullOrWhiteSpace(NombresTextBox.Text)) {
                validados = false;
                mensaje += "\nDebe introducir el nombre.";
            }
            if (string.IsNullOrWhiteSpace(ApellidosTextBox.Text)) {
                validados = false;
                mensaje += "\nDebe introducir el apellido.";
            }
            if (string.IsNullOrWhiteSpace(CedulaTextBox.Text)) {
                validados = false;
                mensaje += "\nDebe introducir la cédula.";
            }
            if (string.IsNullOrWhiteSpace(TelefonoTextBox.Text)) {
                validados = false;
                mensaje += "\nDebe introducir teléfono.";
            }
            if (string.IsNullOrWhiteSpace(DireccionTextBox.Text)) {
                validados = false;
                mensaje += "\nDebe introducir la dirección.";
            }
            if (FechaNacimientoDatePicker.SelectedDate > DateTime.Now) {
                validados = false;
                mensaje += "\nDebe introducir una fecha de nacimiento menor que la actual.";
            }

            if (!validados) {
                MessageBox.Show(mensaje);
            }

            return validados;
        }
    }
}
