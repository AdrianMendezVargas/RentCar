using RentCar.BLL;
using RentCar.Entidades;
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

namespace RentCar.UI.Registros
{
    /// <summary>
    /// Interaction logic for RegistrodeImportador.xaml
    /// </summary>
    public partial class RegistroImportador : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Importador importador { get; set; } = new Importador();


        public RegistroImportador(int importadorId = 0)
        {
            InitializeComponent();

            if (importadorId > 0)
            {
                InicializarImportador(importadorId);
            }
            else
                importador = new Importador();

            this.DataContext = importador;
            MyPropertyChanged("importador");

        }

        private async void InicializarImportador(int importadorId)
        {
            var _importador = await ImportadorBLL.Buscar(importadorId);

            if (_importador != null)
            {
                importador = _importador;
            }
            else
            {
                MessageBox.Show("Este importador fue eliminado");
            }

        }

        private void MyPropertyChanged(string propiedad)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }

        private async void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool guardado = false;

            if (CamposValidos())
            {
                guardado = await ImportadorBLL.Guardar(importador);

                if (guardado)
                {
                    Limpiar();
                    MessageBox.Show("Guardado.");
                }
                else
                {
                    MessageBox.Show("Error al guardar.");
                }
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private async void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ImportadorIdTextBox.Text, out int importadorId))
            {

                if (await ImportadorBLL.Existe(importadorId))
                {
                    importador = await ImportadorBLL.Buscar(importadorId);
                    this.DataContext = importador;
                    MyPropertyChanged("importador");
                }
                else
                {
                    MessageBox.Show("Este importador no existe.");
                    Limpiar();
                }

            }
            else
            {
                MessageBox.Show("Este importador id es invalido.");
            }
        }

        private void Limpiar()
        {
            importador = new Importador();
            this.DataContext = importador;
            MyPropertyChanged("importador");
        }

        private async void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            bool eliminado = false;

            if (int.TryParse(ImportadorIdTextBox.Text, out int clienteId))
            {

                if (await ImportadorBLL.Existe(clienteId))
                {
                    MessageBoxResult opcion = MessageBox.Show("Desea eliminar este importador?.", "Confirme", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (opcion == MessageBoxResult.Yes)
                    {
                        eliminado = await ImportadorBLL.Eliminar(clienteId);

                        if (eliminado)
                        {
                            Limpiar();
                            MessageBox.Show("Eliminado.");
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar.");
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Este importador no existe.");
                }

            }
            else
            {
                MessageBox.Show("Cliente Id invalido.");
            }
        }

        private bool CamposValidos()
        {
            bool validados = true;
            string mensaje = "Errores: \n\n";

            if (!int.TryParse(ImportadorIdTextBox.Text, out int importadorId))
            {
                validados = false;
                mensaje += "\nimportador  Id invalido.";
            }
           
            if (string.IsNullOrWhiteSpace(NombreTextBox.Text))
            {
                validados = false;
                mensaje += "\nDebe introducir el nombre.";
            }
            if (string.IsNullOrWhiteSpace(PaginaWebTextBox.Text))
            {
                validados = false;
                mensaje += "\nDebe introducir la pagina web.";
            }
            
            if (string.IsNullOrWhiteSpace(TelefonoTextBox.Text))
            {
                validados = false;
                mensaje += "\nDebe introducir teléfono.";
            }
           

            if (!validados)
            {
                MessageBox.Show(mensaje);
            }

            return validados;
        }

        public void RecibirImportadorSeleccionado(Importador importador)
        {
            ImportadorIdTextBox.Text = importador.ImportadorId.ToString();
        }
    }
}
