using RentCar.BLL;
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
            List<Importador> listado = new List<Importador>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = ImportadorBLL.GetList(e => true);
                        break;
                        

                    case 1:
                        int num = 0;
                        listado = ImportadorBLL.GetList(e => e.ImportadorId == Convert.ToInt32(CriterioTextBox.Text));
                        break;
                        


                }
            }
            else
            {
                listado = ImportadorBLL.GetList(e=>true);
            }
        }
    }
}
