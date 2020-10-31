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
    /// Interaction logic for ConsultaVehiculos.xaml
    /// </summary>
    public partial class ConsultaClientes : Window {

        public List<Cliente> cliente { get; set; } = new List<Cliente>();

        public ConsultaClientes() 
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e) 
        {
            base.OnClosed(e);
            Owner.Focus();
        }

        public async Task InicializarYFiltrarClientes() 
        {
            await InicializarClientes();
            Actualizar();
        }

        private async Task InicializarClientes() {
            cliente = await ClientesBLL.GetClientes();
            Actualizar();
        }


        public void Actualizar() //actualiza el grid
        {
            ResultadosDataGrid.ItemsSource = null;
            ResultadosDataGrid.ItemsSource = cliente;
        }


        private void AgregarClienteButton_Click(object sender , RoutedEventArgs e) 
        {
            RegistroCliente registroCliente = new RegistroCliente();
            registroCliente.Owner = this;
            registroCliente.Show();
        }

        private async void BuscarButton_Click(object sender , RoutedEventArgs e)
        {
            var buscar = new List<Cliente>();

            if(CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0: 
                    if (int.TryParse(CriterioTextBox.Text, out int clienteId))
                    {                        
                        buscar = (await ClientesBLL.GetClientes()).Where(r => r.ClienteId == clienteId).ToList();
                    }
                        break;
                    case 1:
                    
                        buscar = (await ClientesBLL.GetClientes()).Where(r => r.Nombres == CriterioTextBox.Text).ToList();

                        break;
                    case 2:
                  
                        buscar = (await ClientesBLL.GetClientes()).Where(r => r.Apellidos == CriterioTextBox.Text).ToList();
                        break; 
                    
                    case 3:
                     
                        buscar = (await ClientesBLL.GetClientes()).Where(r => r.Cedula == CriterioTextBox.Text).ToList();
                        break;
                    
                }
            }
            else
            {
                buscar = buscar.Where(c=>true).ToList();
            }
            ResultadosDataGrid.ItemsSource = null;
            ResultadosDataGrid.ItemsSource = buscar;
        }


        private void EditarButton_Click(object sender , RoutedEventArgs e) 
        {
            Cliente cliente = (sender as Button).DataContext as Cliente;
            RegistroCliente registroCliente = new RegistroCliente(cliente.ClienteId);
            registroCliente.Owner = this;
            registroCliente.Show();
        }


    }
}