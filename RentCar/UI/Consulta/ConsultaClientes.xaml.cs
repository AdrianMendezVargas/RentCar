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
    /// Interaction logic for ConsultaVehiculos.xaml
    /// </summary>
    public partial class ConsultaClientes : Window {
        public List<Cliente> clientes { get; set; } = new List<Cliente>();

        public ConsultaClientes() {
            InitializeComponent();

            this.DataContext = this;

            _ = InicializarClientes();
        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            Owner.Focus();
        }

        public async Task InicializarYFiltrarClientes() {
            await InicializarClientes();
            Actualizar();
        }

        private async Task InicializarClientes() {
            clientes = await ClientesBLL.GetClientes();
            Actualizar();
        }


        public void Actualizar() //actualiza el grid
        {
            ResultadosDataGrid.ItemsSource = null;
            ResultadosDataGrid.ItemsSource = clientes;
        }


        private void AgregarClienteButton_Click(object sender , RoutedEventArgs e) {
            RegistroCliente registroCliente = new RegistroCliente();
            registroCliente.Owner = this;
            registroCliente.Show();
        }

        private async void BuscarButton_Click(object sender , RoutedEventArgs e) {

            if (CriterioTextBox.Text.Trim().Length > 0) {
                switch (FiltroComboBox.SelectedIndex) {
                    case 0:
                        if (int.TryParse(CriterioTextBox.Text , out int clienteId)) {
                            clientes = (await ClientesBLL.GetClientes()).Where(r => r.ClienteId == clienteId).ToList();
                        }
                        break;
                    case 1:

                        clientes = (await ClientesBLL.GetClientes()).Where(r => r.Nombres.Contains(CriterioTextBox.Text)).ToList();

                        break;
                    case 2:

                        clientes = (await ClientesBLL.GetClientes()).Where(r => r.Apellidos.Contains(CriterioTextBox.Text)).ToList();
                        break;

                    case 3:

                        clientes = (await ClientesBLL.GetClientes()).Where(r => r.Cedula.Contains(CriterioTextBox.Text)).ToList();
                        break;

                    case 4:
                        clientes = await ClientesBLL.GetClientes();

                        break;

                }
            } else {
                clientes = await ClientesBLL.GetClientes();
            }
            ResultadosDataGrid.ItemsSource = null;
            ResultadosDataGrid.ItemsSource = clientes;
        }


        private void EditarButton_Click(object sender , RoutedEventArgs e) {
            Cliente cliente = (sender as Button).DataContext as Cliente;
            RegistroCliente registroCliente = new RegistroCliente(cliente.ClienteId);
            registroCliente.Owner = this;
            registroCliente.Show();
        }

    }
}
