using Gestion_Etudiant__WPF.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gestion_Etudiant__WPF.GestionFiliere
{
    /// <summary>
    /// Logique d'interaction pour Filiere.xaml
    /// </summary>
    public partial class Filiere : UserControl
    {
        ServiceEtudiant service;

        public Filiere()
        {
            InitializeComponent();
            service = new ServiceEtudiant();
            dataGrid1.ItemsSource  = service.FillData().DefaultView;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        private void btnData_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = service.FillData().DefaultView;
        }
        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
