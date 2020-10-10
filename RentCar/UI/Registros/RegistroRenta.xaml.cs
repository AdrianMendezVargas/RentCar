using RentCar.BLL;
using RentCar.Entidades;
using RentCar.Entidades.Enums;
using RentCar.UI.Consulta;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        bool BuscarButtonPresionado = false;

        public RegistroRenta(int rentaId = 0) {
            InitializeComponent();
            this.DataContext = this;

            if (rentaId > 0) {
                InicializarRenta(rentaId);
                BuscarButtonPresionado = true;
            }
        }

        async Task InicializarRenta(int rentaId) {
            Renta = await RentasBLL.Buscar(rentaId);
        }

        protected override async void OnClosed(EventArgs e) {
            base.OnClosed(e);

            if (Owner.GetType() == typeof(ConsultaRentas)) {
                await ((ConsultaRentas) Owner).InicializarYFiltrarRentas();
            }

            Owner.Focus();
        }

        private void MyPropertyChanged(string propiedad) {
            CalcularBalance();
            PropertyChanged?.Invoke(this , new PropertyChangedEventArgs(propiedad));
        }

        private async void BuscarButton_Click(object sender , RoutedEventArgs e) {

            BuscarButtonPresionado = true;

            if (int.TryParse(RentaIdTextBox.Text , out int RentaId)) {

                if (await ExisteEnBaseDatos()) {
                    Renta = await RentasBLL.Buscar(RentaId);

                    NotificarCambioVehiculoId(BuscarButtonPresionado);

                    MyPropertyChanged("Renta");
                } else {
                    MessageBox.Show("Este Renta no existe.");
                    Limpiar();
                }

            } else {
                MessageBox.Show("Este Renta id es invalido.");
            }
            BuscarButtonPresionado = false;

        }

        private void NuevoButton_Click(object sender , RoutedEventArgs e) {

            Limpiar();
        }

        private async void GuardarButton_Click(object sender , RoutedEventArgs e) {
            if (! await Validar()) {
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

                if (await ExisteEnBaseDatos()) {
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

        private async Task<bool> ExisteEnBaseDatos() {
            Renta Renta = await RentasBLL.Buscar(this.Renta.RentaId);
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
                }
            }
        }

        private void CalcularBalance() {
            //Renta.MontoTotal = 0;
            //foreach (RentaCuotas cuota in Renta.CuotasDetalle) {
            //    if (!cuota.Pagada) {
            //        Renta.Balance += cuota.Balance;
            //    }
            //}

        }

        private async void NotificarCambioVehiculoId(bool fueBuscarButton) {
            if (fueBuscarButton) {

                Vehiculo vehiculo = await VehiculoBLL.Buscar(Renta.VehiculoId);       //TODO: Revisar

                MarcaTextBox.Text = vehiculo.Marca;
                ModeloTextBox.Text = vehiculo.Modelo;
                PrecioTextBox.Text = string.Format( "{0:c}",vehiculo.PrecioDia);
                ChassisTextBox.Text = vehiculo.Chassis;
                KilometrajeTextBox.Text = vehiculo.Kilometraje.ToString();
                AnoTextBox.Text = vehiculo.Ano.ToString();

                MyPropertyChanged("Renta");

            } else if (!int.TryParse(VehiculoIdTextBox.Text , out int vehiculoId)) {

                ModeloTextBox.Text = "Id invalido.";
                MarcaTextBox.Text = "";
                PrecioTextBox.Text = "";
                ChassisTextBox.Text = "";
                KilometrajeTextBox.Text = "";
                AnoTextBox.Text = "";

                Renta.MontoTotal = 0.0m;
                MyPropertyChanged("Renta");

            } else {
                Vehiculo vehiculo = await VehiculoBLL.Buscar(vehiculoId);      

                if (vehiculo == null) {

                    ModeloTextBox.Text = "No existe.";
                    MarcaTextBox.Text = "";
                    PrecioTextBox.Text = "";
                    ChassisTextBox.Text = "";
                    KilometrajeTextBox.Text = "";
                    AnoTextBox.Text = "";

                    Renta.MontoTotal = 0.0m;
                    MyPropertyChanged("Renta");
                } else {
                    if (vehiculo.Estado == VehiculoEstado.Disponible) {
                        MarcaTextBox.Text = vehiculo.Marca;
                        ModeloTextBox.Text = vehiculo.Modelo;
                        PrecioTextBox.Text = string.Format("{0:c}" , vehiculo.PrecioDia);
                        ChassisTextBox.Text = vehiculo.Chassis.ToString();
                        KilometrajeTextBox.Text = vehiculo.Kilometraje.ToString();
                        AnoTextBox.Text = vehiculo.Ano.ToString();

                        Renta.MontoTotal = vehiculo.PrecioDia;
                        MyPropertyChanged("Renta");

                    } else if (vehiculo.Estado == VehiculoEstado.Rentado) {

                        if (!BuscarButtonPresionado) {
                            ModeloTextBox.Text = "Rentado!.";
                        }

                    } else {
                        if (!BuscarButtonPresionado) {
                            ModeloTextBox.Text = "Eliminado!.";
                        }
                    }

                }
            }

        }

        private void VehiculoIdTextBox_TextChanged(object sender , TextChangedEventArgs e) {

            NotificarCambioVehiculoId(BuscarButtonPresionado);

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

            MyPropertyChanged("Renta");

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

    }
}
