using MaterialDesignThemes.Wpf;
using RentCar.BLL;
using RentCar.Entidades;
using RentCar.Entidades.Enums;
using RentCar.UI.Consulta;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RentCar.UI.Registros {
    /// <summary>
    /// Interaction logic for RegistroRenta.xaml
    /// </summary>
    public partial class RegistroRenta : Window, INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;

        public Renta Renta { get; set; } = new Renta();
        public Vehiculo Vehiculo { get; set; }

        bool EsUnaBusqueda = false;
        bool OmitirVehiculoTextChanged = false;

        public RegistroRenta(int rentaId = 0) {
            InitializeComponent();
            this.DataContext = this;

            if (rentaId > 0) {
                InicializarRenta(rentaId);
            }
        }

        void InicializarRenta(int rentaId) {
            RentaIdTextBox.Text = rentaId.ToString();
            OmitirVehiculoTextChanged = true;
            BuscarRenta();
        }

        protected override async void OnClosed(EventArgs e) {
            base.OnClosed(e);

            if (Owner.GetType() == typeof(ConsultaRentas)) {
                await ((ConsultaRentas) Owner).InicializarYFiltrarRentas();
            }

            Owner.Focus();
        }

        private void MyPropertyChanged(string propiedad) {
            PropertyChanged?.Invoke(this , new PropertyChangedEventArgs(propiedad));
        }

        private async void BuscarButton_Click(object sender , RoutedEventArgs e) {

            await BuscarRenta();
        }

        private void VerTodasButton_Click(object sender , RoutedEventArgs e) {
            ConsultaRentas consultaRentas = new ConsultaRentas();
            consultaRentas.Owner = this;
            consultaRentas.Show();
        }

        private async Task BuscarRenta() {
            EsUnaBusqueda = true;

            if (int.TryParse(RentaIdTextBox.Text , out int RentaId)) {

                if (await ExisteEnBaseDatos(RentaId)) {
                    Renta = await RentasBLL.Buscar(RentaId);
                    MyPropertyChanged("Renta");

                    await NotificarCambioVehiculoId();

                } else {
                    MessageBox.Show("Este Renta no existe.");
                    Limpiar();
                }

            } else {
                MessageBox.Show("Este Renta id es invalido.");
            }

            EsUnaBusqueda = false;
        }

        private void NuevoButton_Click(object sender , RoutedEventArgs e) {

            Limpiar();
        }

        private async void GuardarButton_Click(object sender , RoutedEventArgs e) {
            if (!await Validar()) {
                return;
            }

            bool guardado = false;

            if (string.IsNullOrWhiteSpace(RentaIdTextBox.Text) || Renta.RentaId == 0) {
                guardado = await RentasBLL.Guardar(Renta);
            } else {

                MessageBox.Show("Luego de que se realiza una Renta, esta no se puede modificar, solo eliminar.");
                return;
            }

            if (guardado) {
                Limpiar();

                if (Owner.GetType() == typeof(ConsultaRentas)) {
                    await ((ConsultaRentas) Owner).InicializarYFiltrarRentas();
                }

                MessageBox.Show("Guardado.");
            } else {
                MessageBox.Show("Error al guardar.");
            }

        }

        private async void EliminarButton_Click(object sender , RoutedEventArgs e) {

            bool eliminado = false;

            if (int.TryParse(RentaIdTextBox.Text , out int RentaId)) {

                if (await ExisteEnBaseDatos(RentaId)) {
                    MessageBoxResult opcion = MessageBox.Show("Desea eliminar este Renta?." , "Confirme" , MessageBoxButton.YesNo , MessageBoxImage.Question);

                    if (opcion == MessageBoxResult.Yes) {
                        eliminado = await RentasBLL.Eliminar(RentaId);
                    } else {
                        return;
                    }
                } else {
                    MessageBox.Show("Este Renta no existe.");
                    return;
                }

            } else {
                MessageBox.Show("Renta Id invalido.");
                return;
            }

            if (eliminado) {
                Limpiar();
                MessageBox.Show("Eliminado.");
            } else {
                MessageBox.Show("Error al eliminar.");
            }

        }

        private async Task<bool> ExisteEnBaseDatos(int rentaId) {
            Renta Renta = await RentasBLL.Buscar(rentaId);
            return (Renta != null);
        }

        private void Limpiar() {
            Renta = new Renta();
            MyPropertyChanged("Renta");
        }

        private async Task<bool> Validar() {
            bool validados = true;
            string mensaje = "Errores: \n\n";

            if (!int.TryParse(RentaIdTextBox.Text , out int RentaId)) {
                validados = false;
                mensaje += "\nRenta Id invalido.";
            } else {
                if (RentaId < 0) {
                    validados = false;
                    mensaje += "\nRenta Id invalido.";
                }
            }

            if (!int.TryParse(ClienteIdTextBox.Text , out int clienteId)) {
                validados = false;
                mensaje += "\nCliente Id invalido.";
            } else {
                Cliente cliente = await ClientesBLL.Buscar(clienteId);
                if (cliente == null) {
                    validados = false;
                    mensaje += "\nEste cliente no existe.";
                }
            }


            if (!int.TryParse(VehiculoIdTextBox.Text , out int vehiculoId)) {
                validados = false;
                mensaje += "\nVehículo Id invalido.";
            } else {                                                              //  TODO: Revisar
                Vehiculo vehiculo = await VehiculoBLL.Buscar(vehiculoId);
                if (vehiculo == null) {
                    validados = false;
                    mensaje += "\nEste vehículo no existe.";
                } else {
                    if (vehiculo.Estado == VehiculoEstado.Rentado) {
                        validados = false;
                        mensaje += "\nEste vehículo fue rentado.";
                    } else if (vehiculo.Estado == VehiculoEstado.Rentado) {
                        validados = false;
                        mensaje += "\nEste vehículo fue Rentado.";
                    }
                }
            }

            if (Renta.FechaInicial.Date < DateTime.Now.Date) {
                validados = false;
                mensaje += "\nLa fecha inicial debe ser mayor que la actual";
            }

            if (Renta.FechaFinal.Date <= Renta.FechaInicial.Date) {
                validados = false;
                mensaje += "\nLa fecha final debe ser mayor que la inical";
            }

            if (!validados) {
                MessageBox.Show(mensaje);
            }

            return validados;

        }

        private async void ClienteIdTextBox_TextChanged(object sender , TextChangedEventArgs e) {

            if (!int.TryParse(ClienteIdTextBox.Text , out int clienteId)) {

                NombreClienteTextBox.Text = "Cliente Id invalido.";
            } else {
                Cliente cliente = await ClientesBLL.Buscar(clienteId);
                if (cliente == null) {

                    NombreClienteTextBox.Text = "Este cliente no existe.";
                } else {
                    NombreClienteTextBox.Text = cliente.Nombres;
                    CedulaClienteTextBox.Text = cliente.Cedula;
                }
            }
        }

        private void CalcularMontoRenta() {
            if (Vehiculo != null) {
                Renta.MontoTotal = Vehiculo.PrecioDia;
            } else {
                Renta.MontoTotal = 0;
            }
            //MyPropertyChanged("Renta");
            MontoTotalTextBox.Text = string.Format("{0:c}", Renta.MontoTotal);
        }

        private async Task NotificarCambioVehiculoId() {

            Vehiculo = await VehiculoBLL.Buscar(Renta.VehiculoId);
            LlenarCamposVehiculo();
            CalcularMontoRenta();
        }

        private void LlenarCamposVehiculo() {

            if (Vehiculo != null) {

                if (Vehiculo.Estado == VehiculoEstado.Disponible || (Vehiculo.Estado == VehiculoEstado.Rentado && EsUnaBusqueda)) {
                    MarcaTextBox.Text = Vehiculo.Marca;
                    ModeloTextBox.Text = Vehiculo.Modelo;
                    PrecioTextBox.Text = string.Format("{0:c}" , Vehiculo.PrecioDia);
                    ChassisTextBox.Text = Vehiculo.Chassis;
                    KilometrajeTextBox.Text = Vehiculo.Kilometraje.ToString();
                    AnoTextBox.Text = Vehiculo.Ano.ToString();

                } else if (Vehiculo.Estado == VehiculoEstado.Rentado) {

                    LimpiarCamposVehiculo();
                    ModeloTextBox.Text = "Rentado.";

                } else if (Vehiculo.Estado == VehiculoEstado.Eliminado) {

                    LimpiarCamposVehiculo();
                    ModeloTextBox.Text = "Eliminado.";
                }

            } else {
                LimpiarCamposVehiculo();
                ModeloTextBox.Text = "No existe.";
            }

        }

        private void LimpiarCamposVehiculo() {
            ModeloTextBox.Text = "";
            MarcaTextBox.Text = "";
            PrecioTextBox.Text = "";
            ChassisTextBox.Text = "";
            KilometrajeTextBox.Text = "";
            AnoTextBox.Text = "";
        }

        private async void VehiculoIdTextBox_TextChanged(object sender , TextChangedEventArgs e) {

            if (!OmitirVehiculoTextChanged) {   //Si esta ventana fue abierta desde la consulta, se omitira el primer textChange
                await NotificarCambioVehiculoId();
            }

            OmitirVehiculoTextChanged = false;
        }

        private void RentaIdTextBox_TextChanged(object sender , TextChangedEventArgs e) {
            if (int.TryParse(RentaIdTextBox.Text , out int RentaId)) {


                if (RentaId > 0) {
                    PuedeModificar(false);
                } else {
                    PuedeModificar(true);
                }

            } else {
                PuedeModificar(true);
            }

        }

        private void PuedeModificar(bool valor) {

            ClienteIdTextBox.IsEnabled = valor;
            DesdeDatePicker.IsEnabled = valor;
            HastaDatePicker.IsEnabled = valor;
            VehiculoIdTextBox.IsEnabled = valor;
            if (valor) {
                Limpiar();
            }
        }

        private void ColorZone_MouseLeftButtonDown(object sender , System.Windows.Input.MouseButtonEventArgs e) {
            DragMove();
        }

        private void WindowCloseButton_Click(object sender , RoutedEventArgs e) {
            Close();
        }

        
    }
}
