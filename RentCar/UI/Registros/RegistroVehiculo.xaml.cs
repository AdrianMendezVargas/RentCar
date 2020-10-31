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

namespace RentCar.UI.Registros
{
    /// <summary>
    /// Interaction logic for Vehiculo.xaml
    /// </summary>
    public partial class RegistroVehiculo : Window, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        public Vehiculo vehiculo { get; set; } = new Vehiculo();

        public RegistroVehiculo(int vehiculoId = 0)
        {
            InitializeComponent();

            if (vehiculoId > 0) {
                InicializarVehiculo(vehiculoId);
            }
       
            this.DataContext = this;
            MyPropertyChanged("vehiculo");

            
        }

        private async void InicializarVehiculo(int vehiculoId) {
            var _vehiculo = await VehiculoBLL.Buscar(vehiculoId);

            if (_vehiculo != null) {
                vehiculo = _vehiculo;
            } else {
                MessageBox.Show("Este cliente fue eliminado");
            }

        }

        private void MyPropertyChanged(string propiedad)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }

        private async void BuscarButton_Click(object sender, RoutedEventArgs e)
        {

            if (int.TryParse(VehiculoIdTextBox.Text, out int vehiculoId))
            {

                if (await VehiculoBLL.Existe(vehiculoId))
                {
                    vehiculo = await VehiculoBLL.Buscar(vehiculoId);
                    MyPropertyChanged("vehiculo");
                }
                else
                {
                    MessageBox.Show("Este vehiculo no existe.");
                    Limpiar();
                }

            }
            else
            {
                MessageBox.Show("Este vehiculo id es invalido.");
            }

        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {

            Limpiar();
        }

        private async void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool guardado = false;

            if (CamposValidos())
            {
                guardado = await VehiculoBLL.Guardar(vehiculo);

                if (guardado)
                {
                    Limpiar();
                    MessageBox.Show("Guardado.");
                }
                else
                {
                    MessageBox.Show("Error al guardar.");
                }
            }

        }

        private void Limpiar()
        {
            vehiculo = new Vehiculo();
            MyPropertyChanged("vehiculo");
        }

        private async void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            bool eliminado = false;

            if (int.TryParse(VehiculoIdTextBox.Text, out int vehiculoId))
            {

                if (await VehiculoBLL.Existe(vehiculoId))
                {

                    RegistroSalidasVehiculo.ID = vehiculoId;

                   // MessageBoxResult opcion = MessageBox.Show("Desea eliminar este vehiculo?.", "Confirme", MessageBoxButton.YesNo, MessageBoxImage.Question);

                  //  if (opcion == MessageBoxResult.Yes)
                    {
                        eliminado = await VehiculoBLL.Eliminar(vehiculoId);

                        if (eliminado)
                        {
                            Limpiar();
                            MessageBox.Show("Eliminado.");
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar.");
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Este vehiculo no existe.");
                }

            }
            else
            {
                MessageBox.Show("vehiculo Id invalido.");
            }

        }

        private bool CamposValidos()
        {
            bool validados = true;
            string mensaje = "Errores: \n\n";

            if (!int.TryParse(VehiculoIdTextBox.Text, out int vehiculoId))
            {
                validados = false;
                mensaje += "\nVehiculo Id invalido.";
            }
            if (!int.TryParse(PolizaIdTextBox.Text, out int PolizaId))
            {
                validados = false;
                mensaje += "\nPoliza Id invalido.";
            }
            if (string.IsNullOrWhiteSpace(MatriculaTextBox.Text))
            {
                validados = false;
                mensaje += "\nDebe introducir una Matricula.";
            }
            if (string.IsNullOrWhiteSpace(PlacaTextBox.Text))
            {
                validados = false;
                mensaje += "\nDebe introducir la placa.";
            }
            if (string.IsNullOrWhiteSpace(MarcaTextBox.Text))
            {
                validados = false;
                mensaje += "\nDebe introducir la marca.";
            }
            if (string.IsNullOrWhiteSpace(ModeloTextBox.Text))
            {
                validados = false;
                mensaje += "\nDebe introducir el modelo.";
            }
            if (string.IsNullOrWhiteSpace(EstadoTextBox.Text))
            {
                validados = false;
                mensaje += "\nDebe introducir el estado del vehiculo.";
            }
            if (string.IsNullOrWhiteSpace(PrecioTextBox.Text))
            {
                validados = false;
                mensaje += "\nDebe introducir el precio.";
            }
            if (AñoFabricacionDatePicker.SelectedDate > DateTime.Now)
            {
                validados = false;
                mensaje += "\nDebe introducir una fecha de fabricacion menor que la actual.";
            }

            if (string.IsNullOrWhiteSpace(ChassisTextBox.Text))
            {
                validados = false;
                mensaje += "\nDebe introducir la cantidad de chassis.";
            }
            if (string.IsNullOrWhiteSpace(PasajerosTextBox.Text))
            {
                validados = false;
                mensaje += "\nDebe introducir la cantidad de pasajeros.";
            }

            if (string.IsNullOrWhiteSpace(PuertasTextBox.Text))
            {
                validados = false;
                mensaje += "\nDebe introducir la cantidad de puertas.";
            }
            if (string.IsNullOrWhiteSpace(TraccionTextBox.Text))
            {
                validados = false;
                mensaje += "\nDebe introducir la traccion del vehiculo.";
            }
            if (string.IsNullOrWhiteSpace(ComentarioTextBox.Text))
            {
                validados = false;
                mensaje += "\nDebe introducir un comentario del vehiculo.";
            }
            if (string.IsNullOrWhiteSpace(ValorTextBox.Text))
            {
                validados = false;
                mensaje += "\nDebe introducir el valor del vehiculo.";
            }
            if (string.IsNullOrWhiteSpace(TipoTextBox.Text))
            {
                validados = false;
                mensaje += "\nDebe introducir el tipo de vehiculo.";
            }
            if (!validados)
            {
                MessageBox.Show(mensaje);
            }

            return validados;
        }

    }


}
