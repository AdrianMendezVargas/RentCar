using RentCar.BLL;
using RentCar.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
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
    /// Interaction logic for RegistroSalidasVehiculo.xaml
    /// </summary>
    public partial class RegistroSalidasVehiculo : Window, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        public SalidaVehiculo salida { get; set; } = new SalidaVehiculo();
        public static int ID { get; set; }
        public RegistroSalidasVehiculo(int SalidaId = 0)
        {
            InitializeComponent();

            if (SalidaId > 0)
            {
                InicializarSalidaVehiculo(SalidaId);
            }


            
            
            this.DataContext = this;
            MyPropertyChanged("Salidas");
            
        }


        private async void InicializarSalidaVehiculo(int SalidaId)
        {
            var vehiculo = await VehiculoBLL.Buscar(ID);

            var _salida = await SalidasVehiculoBLL.Buscar(SalidaId);

            if (_salida != null)
            {
               salida = _salida;
            }
            else
            {
                MessageBox.Show("Esta salida fue eliminado");
            }

        }

        private void MyPropertyChanged(string propiedad)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }

        private async void BuscarButton_Click(object sender, RoutedEventArgs e)
        {

            if (int.TryParse(SalidaIdTextBox.Text, out int salidaId))
            {

                if (await SalidasVehiculoBLL.Existe(salidaId))
                {
                    salida= await SalidasVehiculoBLL.Buscar(salidaId);
                    MyPropertyChanged("salida");
                }
                else
                {
                    MessageBox.Show("Esta salida no existe.");
                    Limpiar();
                }

            }
            else
            {
                MessageBox.Show("Esta salida Id es invalido.");
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
                guardado = await SalidasVehiculoBLL.Guardar(salida);

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
            salida = new SalidaVehiculo();
            MyPropertyChanged("salida");
        }

        private async void EliminarButton_Click(object sender, RoutedEventArgs e)
        {

            bool eliminado = false;

            if (int.TryParse(SalidaIdTextBox.Text, out int salidaId))
            {

                if (await SalidasVehiculoBLL.Existe(salidaId))
                {
                   
                    MessageBoxResult opcion = MessageBox.Show("Desea eliminar esta salida?.", "Confirme", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (opcion == MessageBoxResult.Yes)
                    {
                        eliminado = await SalidasVehiculoBLL.Eliminar(salidaId);

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
                    MessageBox.Show("Esta salida no existe.");
                }

            }
            else
            {
                MessageBox.Show("salida Id invalido.");
            }

        }

        private bool CamposValidos()
        {
            bool validados = true;
            string mensaje = "Errores: \n\n";

            if (!int.TryParse(SalidaIdTextBox.Text, out int salidaId))
            {
                validados = false;
                mensaje += "\nSalida Id invalido.";
            }
            if (!int.TryParse(VehiculoIdTextBox.Text, out int VehiculoId))
            {
                validados = false;
                mensaje += "\nVehiculo Id invalido.";
            }
            if (!int.TryParse(ClienteIdTextBox.Text, out int ClienteId))
            {
                validados = false;
                mensaje += "\nVehiculo Id invalido.";
            }
            if (string.IsNullOrWhiteSpace(MarcaTextBox.Text))
            {
                validados = false;
                mensaje += "\nDebe introducir una Marca.";
            }
            if (string.IsNullOrWhiteSpace(ModeloTextBox.Text))
            {
                validados = false;
                mensaje += "\nDebe introducir un modelo.";
            }
            if (string.IsNullOrWhiteSpace(KilometrajeTextBox.Text))
            {
                validados = false;
                mensaje += "\nDebe introducir su kilometraje.";
            }

            if (string.IsNullOrWhiteSpace(NombresTextBox.Text))
            {
                validados = false;
                mensaje += "\nDebe introducir su Nombre.";
            }
            if (string.IsNullOrWhiteSpace(PrecioTextBox.Text))
            {
                validados = false;
                mensaje += "\nDebe introducir el precio.";
            }
            if (string.IsNullOrWhiteSpace(ComentarioTextBox.Text))
            {
                validados = false;
                mensaje += "\nDebe introducir un comentario con respecto a la salida del vehiculo";
            }
            if (FechaDatePicker.SelectedDate > DateTime.Now)
            {
                validados = false;
                mensaje += "\nDebe introducir una fecha de salida del vehiculo.";
            }
            if (!validados)
            {
                MessageBox.Show(mensaje);
            }

            return validados;
        }
    }

    
}
