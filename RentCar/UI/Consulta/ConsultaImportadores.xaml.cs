using RentCar.BLL;
using RentCar.Entidades;
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
        private readonly Importador importador = new Importador();
        public ConsultaImportadores()
        {
            InitializeComponent();
            importador = new Importador();
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado =  new  List<Importador>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = Task.Run(() => ImportadorBLL.GetList(e => true)).Result;
                        break;
                        

                    case 1:
                        int num = 0;
                        listado = Task.Run(() => ImportadorBLL.GetList(e => e.ImportadorId == Convert.ToInt32(CriterioTextBox.Text))).Result;
                        break;
                    case 2:
                        
                        listado = Task.Run(() => ImportadorBLL.GetList(e => e.Nombre.Contains(CriterioTextBox.Text))).Result;
                        break;
                    case 3:
                        
                        listado = Task.Run(() => ImportadorBLL.GetList(e => e.PaginaWeb.Contains(CriterioTextBox.Text))).Result;
                        break;

                    case 4:
                        
                        listado = Task.Run(() => ImportadorBLL.GetList(e => e.Telefono.Contains(CriterioTextBox.Text))).Result;
                        break;


                }
            }
            else
            {
                listado = Task.Run(() => ImportadorBLL.GetList(e => true)).Result;
            }

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}
