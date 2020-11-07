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

namespace RentCar.UI.Consulta
{
    /// <summary>
    /// Interaction logic for Consulta_vehiculos.xaml
    /// </summary>
    public partial class Consulta_vehiculos : Window
    {
        public List<Vehiculo> Vehiculo { get; set; } = new List<Vehiculo>();
        public Consulta_vehiculos()
        {
            InitializeComponent();
            _ = InicializarVehiculo();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Owner.Focus();
        }

        public async Task InicializarYFiltrarVehiculo()
        {
            await InicializarVehiculo();
            CargarGrid();
        }
        private async Task InicializarVehiculo()
        {
            Vehiculo = await VehiculoBLL.GetVehiculo();
            CargarGrid();
        }
        private void AgregarRentaButton_Click(object sender, RoutedEventArgs e)
        {
            RegistroVehiculo registroVehiculo = new RegistroVehiculo();
            registroVehiculo.Owner = this;
            registroVehiculo.Show();
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            FiltrarVehiculo();
        }

        public void CargarGrid()
        {
            ResultadosDataGrid.ItemsSource = null;
            ResultadosDataGrid.ItemsSource = Vehiculo;
        }

        async Task FiltrarVehiculo()
        {
            if (FiltroComboBox.SelectedIndex == 0)
            {  //Todo

                Vehiculo = (await VehiculoBLL.GetVehiculo()).Where(v => true).ToList();

            }
            else if (FiltroComboBox.SelectedIndex == 1)
            {  //Vehiculo

                if (int.TryParse(CriterioTextBox.Text, out int VehiculoId))
                {

                    Vehiculo = (await VehiculoBLL.GetVehiculo()).Where(v => v.VehiculoId == VehiculoId).ToList();

                }

            }
            else if (FiltroComboBox.SelectedIndex == 2)
            { //poliza

                if (int.TryParse(CriterioTextBox.Text, out int PolizaId))
                {

                    Vehiculo = (await VehiculoBLL.GetVehiculo()).Where(v => v.PolizaId == PolizaId).ToList();

                }

            }


            if (TodasRadioButton.IsChecked.Value)
            {
                Vehiculo = Vehiculo.Where(v => true).ToList();

            }

            CargarGrid();

        }
        private void EditarButton_Click(object sender, RoutedEventArgs e)
        {
            Vehiculo vehiculo = (sender as Button).DataContext as Vehiculo;

            RegistroVehiculo registroVehiculo = new RegistroVehiculo(vehiculo.VehiculoId);
            registroVehiculo.Owner = this;
            registroVehiculo.Show();

        }




    }
}