using RentCar.Entidades;
using System;
using System.Collections.Generic;
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
    public partial class VistaPoliza : Window {
        public Poliza Poliza { get; set; }

        public VistaPoliza(Poliza poliza = null) {
            InitializeComponent();
            if (poliza != null) {
                Poliza = poliza;
                EstadoTetBox.Text = poliza.EstaVencida ? "Vencida" : "Vigente";
            }
            this.DataContext = this;
        }

       
    }
}
