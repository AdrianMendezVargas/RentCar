using RentCar.BLL;
using RentCar.Entidades;
using RentCar.UI.Registros;
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
using System.Windows.Shapes;

namespace RentCar.UI.Consulta {
    /// <summary>
    /// Interaction logic for ConsultaPoliza.xaml
    /// </summary>
    public partial class ConsultaPoliza : Window {
        public List<Poliza> poliza { get; set; } = new List<Poliza>();
        public string ButtonText { get; set; } = "";
        public bool SeleccionarPoliza { get; set; } = false;

        public ConsultaPoliza(bool selecionar = false) {
            InitializeComponent();

            SeleccionarPoliza = selecionar;
            ButtonText = selecionar ? "Seleccionar" : "Ver";

            this.DataContext = this;

            _ = InicializarPoliza();
        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            Owner.Focus();
        }

        public async Task InicializarYFiltrarPoliza() {
            await InicializarPoliza();
            Actualizar();
        }

        private async Task InicializarPoliza() {
            poliza = await PolizasBLL.GetPolizas();
            Actualizar();
        }


        public void Actualizar() //actualiza el grid
        {
            ResultadosDataGrid.ItemsSource = null;
            ResultadosDataGrid.ItemsSource = poliza;
        }


        private async void BuscarButton_Click(object sender , RoutedEventArgs e) {

            if (CriterioTextBox.Text.Trim().Length > 0) {
                switch (FiltroComboBox.SelectedIndex) {
                    case 0:
                        if (int.TryParse(CriterioTextBox.Text , out int PolizaId)) {
                            poliza = (await PolizasBLL.GetPolizas()).Where(r => r.PolizaId == PolizaId).ToList();
                        }
                        break;
                    case 1:

                       if (int.TryParse(CriterioTextBox.Text , out int VehiculoId)) {
                            poliza = (await PolizasBLL.GetPolizas()).Where(r => r.VehiculoId == VehiculoId).ToList();
                        }
                        break;

                    case 2:
                        
                        //poliza = (await PolizasBLL.GetPolizas()).Where(r => r.Estado.Contains(CriterioTextBox.Text)).ToList();

                        break;

                    case 3:

                        poliza = await PolizasBLL.GetPolizas();

                        break;

                }
            } else {
                poliza = await PolizasBLL.GetPolizas();
            }
            ResultadosDataGrid.ItemsSource = null;
            ResultadosDataGrid.ItemsSource = poliza;
        }


        private void VerButton_Click(object sender , RoutedEventArgs e) {
            PPoliza poliza = (sender as Button).DataContext as Poliza;

            VistaPoliza vista = new VistaPoliza(poliza);
            vista.Owner = this;
            vista.Show();
        }

    }
}
