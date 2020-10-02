﻿using System;
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

namespace RentCar.UI.Registros
{
    /// <summary>
    /// Interaction logic for Vehiculo.xaml
    /// </summary>
    public partial class RegistroVehiculo : Window
    {
        public RegistroVehiculo()
        {
            InitializeComponent();
        }

        private void EliminarButton_Click(object sender , RoutedEventArgs e) {
            DialogoEliminarVehiculo dialogo = new DialogoEliminarVehiculo();
            dialogo.Owner = this;
            dialogo.ShowDialog();
        }
    }
}
