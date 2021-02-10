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
        ServiceFiliere service;

        public Filiere()
        {
            InitializeComponent();
            service = new ServiceFiliere();
            dataGrid1.ItemsSource  = service.FillData().DefaultView;
        }
        int value = 0;
        string respo = "";
        string filiere = "";
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var index = dataGrid1.SelectedIndex;
            DataGridRow row = dataGrid1.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            DataRowView MyRow = (DataRowView)row.Item;
            value = int.Parse(MyRow.Row[0].ToString());
            respo = MyRow.Row[1].ToString();
            filiere = (MyRow.Row[2].ToString());
            txtResponsable.Text = respo;
            txtFiliere.Text = filiere;

        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string responsable = txtResponsable.Text;
            string filiere = txtFiliere.Text;
            if(responsable == null || filiere == null)
            {
                MessageBox.Show("Veuillez remplir le formulaire");
            }
            else
            {
                service.insert(responsable, filiere);
                txtResponsable.Text = "";
                txtFiliere.Text = "";
                dataGrid1.ItemsSource = service.FillData().DefaultView;
                MessageBox.Show("Bien Ajouté");
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem == null)
            {
                MessageBox.Show("Veuillez selectionner une ligne de grille", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                string x  = service.delete(value);
                dataGrid1.ItemsSource = service.FillData().DefaultView;
                txtResponsable.Text = "";
                txtFiliere.Text = "";
                MessageBox.Show("" + x);

            }       
        }
        
        private void btnData_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = service.FillData().DefaultView;
        }
        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem == null)
            {
                MessageBox.Show("Veuillez selectionner une ligne de grille", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                string responsableUpdate = txtResponsable.Text;
                string filiereUpdate = txtFiliere.Text;
                if (responsableUpdate == null || filiereUpdate == null)
                {
                    MessageBox.Show("Veuillez bien remplir le formulaire");
                }
                else
                {
                    service.update(value,responsableUpdate, filiereUpdate);
                    txtResponsable.Text = "";
                    txtFiliere.Text = "";
                    dataGrid1.ItemsSource = service.FillData().DefaultView;
                    MessageBox.Show("Bien Modifié");
                }
            }
        }
    }
}
