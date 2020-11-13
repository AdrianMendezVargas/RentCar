using RentCar.BLL;
using RentCar.Entidades;
using RentCar.UI.Reportes;
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
    /// Interaction logic for VistaPoliza.xaml
    /// </summary>
    public partial class VistaPoliza : Window, INotifyPropertyChanged {
       

        private void MyPropertyChanged(string propiedad) {
            PropertyChanged?.Invoke(this , new PropertyChangedEventArgs(propiedad));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public Poliza Poliza { get; set; }

        public VistaPoliza(Poliza poliza = null) {
            InitializeComponent();
            if (poliza != null) {
                SetPoliza(poliza);
            }
            this.DataContext = this;
        }

        private async void SetPoliza(Poliza poliza) {
            var _poliza = await PolizasBLL.Buscar(poliza.PolizaId);
            if (_poliza != null) {
                Poliza = _poliza;
                MyPropertyChanged("Poliza");
            }
            EstadoTetBox.Text = Poliza.EstaVencida ? "Vencida" : "Vigente";
        }

        private async void RenovarButton_Click(object sender , RoutedEventArgs e) {
            if (Poliza.FechaFinal.DayOfYear - 30 <= DateTime.Today.DayOfYear && Poliza.FechaFinal.Year == DateTime.Today.Year) {
                Poliza.FechaFinal = DateTime.Today.AddDays(365);
                bool renovada = await PolizasBLL.Modificar(Poliza);
                if (renovada) {
                    SetPoliza(Poliza);
                    MessageBox.Show("La poliza fue renovada" , "Exito" , MessageBoxButton.OK , MessageBoxImage.Exclamation);
                }
            } else {
                MessageBox.Show("Aun le queda mas de un mes de vigencia" , "Error" , MessageBoxButton.OK , MessageBoxImage.Warning);
            }
        }

        protected override void OnClosed(EventArgs e) {
            if (Owner.GetType() == typeof(ReportePolizas)) {
               ((ReportePolizas) Owner).BuscarPolizasARenovar();
            }
            Owner.Focus();
        }
    }
}
