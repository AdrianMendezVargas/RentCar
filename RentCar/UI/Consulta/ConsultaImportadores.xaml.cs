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
using System.Windows.Shapes;

namespace RentCar.UI.Consulta
{
    /// <summary>
    /// Interaction logic for ConsultaImportadores.xaml
    /// </summary>
    public partial class ConsultaImportadores : Window
    {

        public List<Importador> Importadores { get; set; } = new List<Importador>();
        public string ButtonText { get; set; } = "";
        public bool SeleccionarImportador{ get; set; } = false;
        
        public ConsultaImportadores(bool selecionar = false)
        {
            InitializeComponent();
            SeleccionarImportador = selecionar;
            ButtonText = selecionar ? "Seleccionar" : "Ver";

            this.DataContext = this;
            _ = InicializarImportador();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Owner.Focus();
        }

        public async Task InicializarYFiltrarImportador()
        {
            await InicializarImportador();
            CargarGrid();
        }
        private async Task InicializarImportador()
        {
            Importadores = await ImportadorBLL.GetImportadores(e=>true);
            CargarGrid();
        }

        public void CargarGrid()
        {
            ResultadosDataGrid.ItemsSource = null;
            ResultadosDataGrid.ItemsSource = Importadores;
        }

        private void AgregarImportadorButton_Click(object sender, RoutedEventArgs e)
        {
            RegistroImportador registroImportador = new RegistroImportador();
            registroImportador.Owner = this;
            registroImportador.Show();
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            FiltrarImportador();
        }

        public void Actualizar() //actualiza el grid
        {
            ResultadosDataGrid.ItemsSource = null;
            ResultadosDataGrid.ItemsSource = Importadores;
        }

        async Task FiltrarImportador()
        {
            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        Importadores = Task.Run(() => ImportadorBLL.GetImportadores(e => true)).Result;
                        break;


                    case 1:
                        int num = 0;
                        Importadores = Task.Run(() => ImportadorBLL.GetImportadores(e => e.ImportadorId == Convert.ToInt32(CriterioTextBox.Text))).Result;
                        break;
                    case 2:

                        Importadores = Task.Run(() => ImportadorBLL.GetImportadores(e => e.Nombre.Contains(CriterioTextBox.Text))).Result;
                        break;
                    case 3:

                        Importadores = Task.Run(() => ImportadorBLL.GetImportadores(e => e.PaginaWeb.Contains(CriterioTextBox.Text))).Result;
                        break;

                    case 4:

                        Importadores = Task.Run(() => ImportadorBLL.GetImportadores(e => e.Telefono.Contains(CriterioTextBox.Text))).Result;
                        break;


                }
            }
            else
            {
                Importadores = Task.Run(() => ImportadorBLL.GetImportadores(e => true)).Result;
            }
            ResultadosDataGrid.ItemsSource = null;
            ResultadosDataGrid.ItemsSource = Importadores;

            CargarGrid();

        }

        private void VerButton_Click(object sender, RoutedEventArgs e)
        {
            Importador importador = (sender as Button).DataContext as Importador;
            if (SeleccionarImportador)
            {
                ((RegistroImportador)Owner).RecibirImportadorSeleccionado(importador);
                Close();
            }
            else
            {
                RegistroImportador registroImportador = new RegistroImportador(importador.ImportadorId);
                registroImportador.Owner = this;
                registroImportador.Show();
            }
        }
    }
}
