using Microsoft.VisualBasic;
using RentCar.BLL;
using RentCar.Entidades;
using RentCar.UI.Registros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RentCar.UI.Reportes {
    /// <summary>
    /// Interaction logic for ReportePolizas.xaml
    /// </summary>
    public partial class ReportePolizas : Window {
        

        public string FechaActual { get; set; } = DateTime.Now.ToShortDateString();
        public List<Poliza> Polizas { get; set; } = new List<Poliza>();

        const int DIAS_ANTICIPACION = 30;

        public ReportePolizas() {
            InitializeComponent();
            this.DataContext = this;

            BuscarPolizasARenovar();
           
        }

        public async void BuscarPolizasARenovar() {

            //Polizas.Add(new Poliza { PolizaId = 1 , VehiculoId = 1 , MontoAsegurado = 300000, FechaFinal = new DateTime(2020 , 11 , 5) });
            //Polizas.Add(new Poliza { PolizaId = 2 , VehiculoId = 2 , MontoAsegurado = 500000 , FechaFinal = new DateTime(2020 , 11 , 5) });
            //Polizas.Add(new Poliza { PolizaId = 3 , VehiculoId = 3 , MontoAsegurado = 100000000 , FechaFinal = new DateTime(2020 , 11 , 5) });
            //Polizas.Add(new Poliza { PolizaId = 1 , VehiculoId = 4 , MontoAsegurado = 300000 , FechaFinal = new DateTime(2020 , 11 , 5) });
            //Polizas.Add(new Poliza { PolizaId = 2 , VehiculoId = 5 , MontoAsegurado = 300000 , FechaFinal = new DateTime(2020 , 11 , 5) });

            Polizas = await PolizasBLL.GetPolizas();

            Polizas = Polizas.Where(p => p.FechaFinal.DayOfYear - DIAS_ANTICIPACION <= DateTime.Today.DayOfYear && p.FechaFinal.Year == DateTime.Today.Year).ToList();
            
            PolizasListBox.ItemsSource = null;
            PolizasListBox.ItemsSource = Polizas;

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
                MessageBox.Show("Finalizado","Exito",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }

        private void PolizasListBox_Selected(object sender , RoutedEventArgs e) {
            Poliza poliza = (sender as Button).DataContext as Poliza;

            VistaPoliza vista = new VistaPoliza(poliza);
            vista.Owner = this;
            vista.Show();
        }

    }
}
