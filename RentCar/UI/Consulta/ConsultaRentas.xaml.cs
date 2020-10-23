using RentCar.BLL;
using RentCar.Entidades;
using RentCar.UI.Registros;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Linq;

namespace RentCar.UI.Consulta {
    /// <summary>
    /// Interaction logic for ConsultaRentas.xaml
    /// </summary>
    public partial class ConsultaRentas : Window {

        public List<Renta> Rentas { get; set; } = new List<Renta>();

        public ConsultaRentas() {
            InitializeComponent();
            _ = InicializarRentas();
        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            Owner.Focus();
        }

        public async Task InicializarYFiltrarRentas() {
            await InicializarRentas();
            CargarGrid();
        }

        private async Task InicializarRentas() {
            Rentas = await RentasBLL.GetRentas();
            CargarGrid();
        }

        private void AgregarRentaButton_Click(object sender , RoutedEventArgs e) {
            RegistroRenta registroRenta = new RegistroRenta();
            registroRenta.Owner = this;
            registroRenta.Show();
        }

        private void BuscarButton_Click(object sender , RoutedEventArgs e) {
            FiltrarRentas();
        }

        public void CargarGrid() {
            ResultadosDataGrid.ItemsSource = null;
            ResultadosDataGrid.ItemsSource = Rentas;
        }

        async Task FiltrarRentas() {
            if (FiltroComboBox.SelectedIndex == 0) {  //Todo

                Rentas = (await RentasBLL.GetRentas()).Where(r => true).ToList();

            } else if (FiltroComboBox.SelectedIndex == 1) {  //RentaId

                if (int.TryParse(CriterioTextBox.Text, out int rentaId)) {

                    Rentas = (await RentasBLL.GetRentas()).Where(r => r.RentaId == rentaId).ToList();

                }

            } else if (FiltroComboBox.SelectedIndex == 2) { //ClienteId

                if (int.TryParse(CriterioTextBox.Text , out int clienteId)) {

                    Rentas = (await RentasBLL.GetRentas()).Where(r => r.ClienteId == clienteId).ToList();

                }

            } else if (FiltroComboBox.SelectedIndex == 3) { //VehiculoId

                if (int.TryParse(CriterioTextBox.Text , out int vehiculoId)) {

                    Rentas = (await RentasBLL.GetRentas()).Where(r => r.VehiculoId == vehiculoId).ToList();

                }

            }

            if (TodasRadioButton.IsChecked.Value) {
                Rentas = Rentas.Where(r => true).ToList();

            } else if (ActualesRadioButton.IsChecked.Value) {
                Rentas = Rentas.Where(r => r.Activa).ToList();

            } else if (AntiguasRadioButton.IsChecked.Value) {
                Rentas = Rentas.Where(r => !r.Activa).ToList();
            }

            CargarGrid();

        }

        private void EditarButton_Click(object sender , RoutedEventArgs e) {
            Renta renta = (sender as Button).DataContext as Renta;

            RegistroRenta registroRenta = new RegistroRenta(renta.RentaId);
            registroRenta.Owner = this;
            registroRenta.Show();

        }

        private void VehiculoIdButton_Click(object sender , RoutedEventArgs e) {
            Renta renta = (sender as Button).DataContext as Renta;

            RegistroVehiculo registroVehiculo = new RegistroVehiculo(renta.VehiculoId);
            registroVehiculo.Owner = this;
            registroVehiculo.Show();
        }

        private void ClienteIdButton_Click(object sender , RoutedEventArgs e) {
            Renta renta = (sender as Button).DataContext as Renta;

            RegistroCliente registroCliente = new RegistroCliente(renta.ClienteId);
            registroCliente.Owner = this;
            registroCliente.Show();

        }
    }
}
