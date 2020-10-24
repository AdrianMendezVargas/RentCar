using RentCar.BLL;
using RentCar.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RentCar.UI.Reportes {
    /// <summary>
    /// Interaction logic for ReporteClientes.xaml
    /// </summary>
    public partial class ReporteClientes : Window {
        private const int DIAS_DE_REGISTRO = 30;

        public string FechaActual { get; set; } = DateTime.Now.ToShortDateString();

        public List<Cliente> ClientesNuevos { get; set; } = new List<Cliente>();

        public ReporteClientes() {
            InitializeComponent();

            this.DataContext = this;
            CargarClientesNuevos();
        }

        private async void CargarClientesNuevos() {
            ClientesNuevos = (await ClientesBLL.GetClientes())
                .Where(c => c.FechaRegistro > DateTime.Today.Subtract(new TimeSpan (DIAS_DE_REGISTRO , 0,0,0)))
                .ToList();

            ClientesNuevosListBox.ItemsSource = null;
            ClientesNuevosListBox.ItemsSource = ClientesNuevos;
        }

        private void ImprimirButton_Click(object sender , RoutedEventArgs e) {
            try {
                this.ImprimirButton.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true) {
                    printDialog.PrintVisual(print , "Reporte");
                }
            } finally {
                this.ImprimirButton.IsEnabled = true;
                MessageBox.Show("Finalizado" , "Exito" , MessageBoxButton.OK , MessageBoxImage.Information);
            }
        }

    }
}
