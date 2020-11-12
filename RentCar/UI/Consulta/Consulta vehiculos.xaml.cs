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
using System.Windows.Shapes;
using System.Linq;

namespace RentCar.UI.Consulta {
    /// <summary>
    /// Interaction logic for Consulta_vehiculos.xaml
    /// </summary>
    public partial class Consulta_vehiculos : Window {
        public List<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
        public string ButtonText { get; set; } = "";
        public bool SeleccionarVehiculo { get; set; } = false;
        public Consulta_vehiculos(bool selecionar = false) {
            InitializeComponent();

            SeleccionarVehiculo = selecionar;
            ButtonText = selecionar ? "Seleccionar" : "Ver";

            this.DataContext = this;
            _ = InicializarVehiculo();
        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            Owner.Focus();
        }

        public async Task InicializarYFiltrarVehiculo() {
            await InicializarVehiculo();
            CargarGrid();
        }
        private async Task InicializarVehiculo() {
            Vehiculos = await VehiculoBLL.GetVehiculos();
            CargarGrid();
        }
        private void AgregarVehiculoButton_Click(object sender , RoutedEventArgs e) {
            RegistroVehiculo registroVehiculo = new RegistroVehiculo();
            registroVehiculo.Owner = this;
            registroVehiculo.Show();
        }

        private void BuscarButton_Click(object sender , RoutedEventArgs e) {
            FiltrarVehiculo();
        }

        public void CargarGrid() {
            ResultadosDataGrid.ItemsSource = null;
            ResultadosDataGrid.ItemsSource = Vehiculos;
        }

        async Task FiltrarVehiculo() {
            if (FiltroComboBox.SelectedIndex == 0) {  //Todo

                Vehiculos = (await VehiculoBLL.GetVehiculos()).Where(v => true).ToList();

            } else if (FiltroComboBox.SelectedIndex == 1) {  //Vehiculo

                if (int.TryParse(CriterioTextBox.Text , out int VehiculoId)) {

                    Vehiculos = (await VehiculoBLL.GetVehiculos()).Where(v => v.VehiculoId == VehiculoId).ToList();

                }

            } else if (FiltroComboBox.SelectedIndex == 2) { //poliza

                if (int.TryParse(CriterioTextBox.Text , out int PolizaId)) {

                    Vehiculos = (await VehiculoBLL.GetVehiculos()).Where(v => v.Poliza.PolizaId == PolizaId).ToList();

                }

            }


            if (TodasRadioButton.IsChecked.Value) {
                Vehiculos = Vehiculos.Where(v => true).ToList();

            }

            CargarGrid();

        }
        private void VerButton_Click(object sender , RoutedEventArgs e) {
            Vehiculo vehiculo = (sender as Button).DataContext as Vehiculo;
            if (SeleccionarVehiculo) {
                ((RegistroRenta) Owner).RecibirVehiculoSeleccionado(vehiculo);
                Close();
            } else {
                RegistroVehiculo registroVehiculo = new RegistroVehiculo(vehiculo.VehiculoId);
                registroVehiculo.Owner = this;
                registroVehiculo.Show();
            }

        }

        private void PolizaIdButton_Click(object sender , RoutedEventArgs e) {
            Vehiculo vehiculo = (sender as Button).DataContext as Vehiculo;

            VistaPoliza vista = new VistaPoliza(vehiculo.Poliza);
            vista.Owner = this;
            vista.Show();
        }
    }
}