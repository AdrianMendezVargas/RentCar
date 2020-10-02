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
    /// Interaction logic for DialogoEliminarVehiculo.xaml
    /// </summary>
    public partial class DialogoEliminarVehiculo : Window {
        public DialogoEliminarVehiculo() {
            InitializeComponent();
        }

        private void cancelarButton_Click(object sender , RoutedEventArgs e) {
            Close();
        }

        private void eliminarButton_Click(object sender , RoutedEventArgs e) {

        }
    }
}
