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

namespace RentCar.UI.Registros {
    /// <summary>
    /// Interaction logic for RegistroRenta.xaml
    /// </summary>
    public partial class RegistroRenta : Window, INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;

        //public Venta venta { get; set; }
        public int UsuarioId { get; set; }

        bool BuscarButtonPresionado = false;

        public RegistroRenta() {
            InitializeComponent();
            this.DataContext = this;
        }

        private void MyPropertyChanged(string propiedad) {
            CargarGrid();
            CalcularBalance();

            PropertyChanged?.Invoke(this , new PropertyChangedEventArgs(propiedad));
        }

        private void BuscarButton_Click(object sender , RoutedEventArgs e) {

            //BuscarButtonPresionado = true;

            //if (int.TryParse(VentaIdTextBox.Text , out int ventaId)) {

            //    if (ExisteEnBaseDatos()) {
            //        venta = VentasBLL.Buscar(ventaId);

            //        NotificarCambioVehiculoId(BuscarButtonPresionado);

            //        MyPropertyChanged("venta");
            //    } else {
            //        MessageBox.Show("Este venta no existe.");
            //        Limpiar();
            //    }

            //} else {
            //    MessageBox.Show("Este venta id es invalido.");
            //}
            //BuscarButtonPresionado = false;

        }

        private void NuevoButton_Click(object sender , RoutedEventArgs e) {

            //Limpiar();
        }

        private void GuardarButton_Click(object sender , RoutedEventArgs e) {
            //if (!Validar()) {
            //    return;
            //}

            //bool guardado = false;

            //if (string.IsNullOrWhiteSpace(VentaIdTextBox.Text) || venta.VentaId == 0) {
            //    guardado = VentasBLL.Guardar(venta);
            //} else {

            //    MessageBox.Show("Luego de que se realiza una venta, esta no se puede modificar, solo eliminar.");
            //    return;

            //}

            //if (guardado) {
            //    Limpiar();
            //    MessageBox.Show("Guardado.");
            //} else {
            //    MessageBox.Show("Error al guardar.");
            //}

        }

        private void EliminarButton_Click(object sender , RoutedEventArgs e) {

            bool eliminado = false;

            //if (int.TryParse(VentaIdTextBox.Text , out int ventaId)) {

            //    if (ExisteEnBaseDatos()) {
            //        MessageBoxResult opcion = MessageBox.Show("Desea eliminar este venta?." , "Confirme" , MessageBoxButton.YesNo , MessageBoxImage.Question);

            //        if (opcion == MessageBoxResult.Yes) {
            //            eliminado = VentasBLL.Eliminar(ventaId);
            //        } else {
            //            return;
            //        }
            //    } else {
            //        MessageBox.Show("Este venta no existe.");
            //        return;
            //    }

            //} else {
            //    MessageBox.Show("Venta Id invalido.");
            //    return;
            //}

            //if (eliminado) {
            //    Limpiar();
            //    MessageBox.Show("Eliminado.");
            //} else {
            //    MessageBox.Show("Error al eliminar.");
            //}

        }

        private bool ExisteEnBaseDatos() {
            //Venta venta = VentasBLL.Buscar(this.venta.VentaId);
            //return (venta != null);
            return false;
        }

        private void Limpiar() {
            //venta = new Venta(UsuarioId);
            //MyPropertyChanged("venta");
        }

        private bool Validar() {
            //bool validados = true;
            //string mensaje = "Errores: \n\n";

            //if (!int.TryParse(VentaIdTextBox.Text , out int ventaId)) {
            //    validados = false;
            //    mensaje += "\nVenta Id invalido.";
            //} else {
            //    if (ventaId < 0) {
            //        validados = false;
            //        mensaje += "\nVenta Id invalido.";
            //    }
            //}

            //if (!int.TryParse(ClienteIdTextBox.Text , out int clienteId)) {
            //    validados = false;
            //    mensaje += "\nCliente Id invalido.";
            //} else {
            //    Cliente cliente = ClientesBLL.Buscar(clienteId);
            //    if (cliente == null) {
            //        validados = false;
            //        mensaje += "\nEste cliente no existe.";
            //    }
            //}


            //if (!int.TryParse(VehiculoIdTextBox.Text , out int vehiculoId)) {
            //    validados = false;
            //    mensaje += "\nVehículo Id invalido.";
            //} else {
            //    Vehiculo vehiculo = VehiculosBLL.Buscar(vehiculoId);
            //    if (vehiculo == null) {
            //        validados = false;
            //        mensaje += "\nEste vehículo no existe.";
            //    } else {
            //        if (vehiculo.Estado == VehiculoEstado.Vendido) {
            //            validados = false;
            //            mensaje += "\nEste vehículo fue vendido.";
            //        } else if (vehiculo.Estado == VehiculoEstado.Rentado) {
            //            validados = false;
            //            mensaje += "\nEste vehículo fue rentado.";
            //        }
            //    }
            //}

            //if (!int.TryParse(CuotasTextBox.Text , out int cuotas)) {
            //    validados = false;
            //    mensaje += "\nCuotas invalidas.";
            //} else {

            //    if (cuotas <= 0) {
            //        validados = false;
            //        mensaje += "\nDebe haber por lo menos una cuota.";
            //    }
            //}

            //if (!validados) {
            //    MessageBox.Show(mensaje);
            //}

            //return validados;
            return false;//borrar esta linea
        }

        private void ClienteIdTextBox_TextChanged(object sender , TextChangedEventArgs e) {

            //if (!int.TryParse(ClienteIdTextBox.Text , out int clienteId)) {

            //    NombreClienteTextBox.Text = "Cliente Id invalido.";
            //} else {
            //    Cliente cliente = ClientesBLL.Buscar(clienteId);
            //    if (cliente == null) {

            //        NombreClienteTextBox.Text = "Este cliente no existe.";
            //    } else {
            //        NombreClienteTextBox.Text = cliente.Nombres;
            //    }
            //}
        }

        private void CuotasTextBox_TextChanged(object sender , TextChangedEventArgs e) {

            //if (BuscarButtonPresionado == false) {
            //    if (int.TryParse(CuotasTextBox.Text , out int NumeroCuotas)) {

            //        if (venta.MontoTotal > 0) {

            //            if (NumeroCuotas > 0) {
            //                venta.CuotasDetalle.Clear();
            //                for (int i = 0 ; i < NumeroCuotas ; i++) {
            //                    VentaCuotas cuota = new VentaCuotas(UsuarioId);
            //                    cuota.VentaId = venta.VentaId;
            //                    cuota.Monto = (venta.MontoTotal / NumeroCuotas);
            //                    cuota.Balance = (venta.MontoTotal / NumeroCuotas);
            //                    cuota.Numero = (1 + i);

            //                    venta.CuotasDetalle.Add(cuota);

            //                }
            //                if (venta.CuotasDetalle.Count > 0) {
            //                    venta.MontoCuotas = venta.CuotasDetalle[0].Monto;
            //                }

            //            } else {
            //                venta.CuotasDetalle.Clear();
            //            }
            //        } else {
            //            venta.CuotasDetalle.Clear();
            //        }

            //    } else {
            //        venta.CuotasDetalle.Clear();
            //    }
            //}

            //MyPropertyChanged("venta");


        }

        private void CargarGrid() {
            //CuotasDataGrid.ItemsSource = null;
            //CuotasDataGrid.ItemsSource = venta.CuotasDetalle;
        }
        private void CalcularBalance() {
            //venta.Balance = 0;
            //foreach (VentaCuotas cuota in venta.CuotasDetalle) {
            //    if (!cuota.Pagada) {
            //        venta.Balance += cuota.Balance;
            //    }
            //}

        }

        private void NotificarCambioVehiculoId(bool fueBuscarButton) {
            //if (fueBuscarButton) {

            //    Vehiculo vehiculo = VehiculosBLL.Buscar(venta.VehiculoId);

            //    MarcaTextBox.Text = vehiculo.Marca;
            //    ModeloTextBox.Text = vehiculo.Modelo;
            //    PrecioTextBox.Text = vehiculo.PrecioVenta.ToString();
            //    VinTextBox.Text = vehiculo.Vin.ToString();
            //    KilometrajeTextBox.Text = vehiculo.Kilometraje.ToString();
            //    AnoTextBox.Text = vehiculo.Ano.ToString();

            //    MyPropertyChanged("venta");

            //} else if (!int.TryParse(VehiculoIdTextBox.Text , out int vehiculoId)) {

            //    ModeloTextBox.Text = "Id invalido.";
            //    MarcaTextBox.Text = "";
            //    PrecioTextBox.Text = "";
            //    VinTextBox.Text = "";
            //    KilometrajeTextBox.Text = "";
            //    AnoTextBox.Text = "";

            //    CuotasTextBox.Text = "0";

            //    venta.MontoTotal = 0.0m;
            //    venta.CuotasDetalle.Clear();
            //    MyPropertyChanged("venta");

            //} else {
            //    Vehiculo vehiculo = VehiculosBLL.Buscar(vehiculoId);
            //    if (vehiculo == null) {

            //        ModeloTextBox.Text = "No existe.";
            //        MarcaTextBox.Text = "";
            //        PrecioTextBox.Text = "";
            //        VinTextBox.Text = "";
            //        KilometrajeTextBox.Text = "";
            //        AnoTextBox.Text = "";

            //        CuotasTextBox.Text = "0";   //Reiniciando cuotas, monto y re-calculando balance

            //        venta.MontoTotal = 0.0m;
            //        venta.CuotasDetalle.Clear();
            //        MyPropertyChanged("venta");
            //    } else {
            //        if (vehiculo.Estado == VehiculoEstado.Disponible) {
            //            MarcaTextBox.Text = vehiculo.Marca;
            //            ModeloTextBox.Text = vehiculo.Modelo;
            //            PrecioTextBox.Text = vehiculo.PrecioVenta.ToString();
            //            VinTextBox.Text = vehiculo.Vin.ToString();
            //            KilometrajeTextBox.Text = vehiculo.Kilometraje.ToString();
            //            AnoTextBox.Text = vehiculo.Ano.ToString();

            //            venta.MontoTotal = vehiculo.PrecioVenta;
            //            MyPropertyChanged("venta");

            //        } else if (vehiculo.Estado == VehiculoEstado.Rentado) {

            //            if (!BuscarButtonPresionado) {
            //                ModeloTextBox.Text = "Rentado!.";
            //            }

            //        } else {
            //            if (!BuscarButtonPresionado) {
            //                ModeloTextBox.Text = "Vendido!.";
            //            }
            //        }

            //    }
            //}
           
        }

        private void VehiculoIdTextBox_TextChanged(object sender , TextChangedEventArgs e) {

            //NotificarCambioVehiculoId(BuscarButtonPresionado);

        }

        private void VentaIdTextBox_TextChanged(object sender , TextChangedEventArgs e) {
            //if (int.TryParse(VentaIdTextBox.Text , out int ventaId)) {


            //    if (ventaId > 0) {
            //        PuedeModificar(false);
            //    } else {
            //        PuedeModificar(true);
            //    }

            //} else {
            //    PuedeModificar(true);
            //}

        }

        private void PuedeModificar(bool valor) {

            //ClienteIdTextBox.IsEnabled = valor;
            //CuotasTextBox.IsEnabled = valor;
            //VehiculoIdTextBox.IsEnabled = valor;
            //if (valor) {
            //    Limpiar();
            //}
        }

    }
}
