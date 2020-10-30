using RentCar.UI.Consulta;
using RentCar.UI.Registros;
using RentCar.UI.Reportes;
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

namespace RentCar {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        protected override void OnClosed(EventArgs e) {
            this.Owner.Show();
        }
        public MainWindow() {
            InitializeComponent();
        }

        private void RegistroRentaMenuItem_Click(object sender , RoutedEventArgs e) {
            RegistroRenta registro = new RegistroRenta();
            registro.Owner = this;
            registro.Show();
        }

        private void RegistroVehiculoMenuItem_Click(object sender , RoutedEventArgs e) {
            RegistroVehiculo registro = new RegistroVehiculo();
            registro.Owner = this;
            registro.Show();
        }

        private void RegistroImportadorMenuItem_Click(object sender , RoutedEventArgs e) {
            RegistroImportador registro = new RegistroImportador();
            registro.Owner = this;
            registro.Show();
        }

        private void RegistroClienteMenuItem_Click(object sender , RoutedEventArgs e) {
            RegistroCliente registro = new RegistroCliente();
            registro.Owner = this;
            registro.Show();
        }
        private void ConsultaVehiculoMenuItem_Click(object sender , RoutedEventArgs e) {

        }

        private void ConsultaClientesMenuItem_Click(object sender , RoutedEventArgs e) {
            ConsultaClientes consulta = new ConsultaClientes();
            consulta.Owner = this;
            consulta.Show();
        }

        private void ConsultaRentasMenuItem_Click(object sender , RoutedEventArgs e) {
            ConsultaRentas consulta = new ConsultaRentas();
            consulta.Owner = this;
            consulta.Show();
        }

        private void ConsultaImportadoresMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ConsultaImportadores consulta = new ConsultaImportadores();
            consulta.Owner = this;
            consulta.Show();
        }

        private void ReportePolizas_Click(object sender , RoutedEventArgs e) {
            ReportePolizas reporte = new ReportePolizas();
            reporte.Owner = this;
            reporte.ShowDialog();
        }

        private void ReporteClientesMenuItem_Click(object sender , RoutedEventArgs e) {
            ReporteClientes reporte = new ReporteClientes();
            reporte.Owner = this;
            reporte.ShowDialog();
        }

        private void RegistroSalidasVehiculoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            RegistroSalidasVehiculo registro = new RegistroSalidasVehiculo();
            registro.Owner = this;
            registro.Show();
        }
    }
}
