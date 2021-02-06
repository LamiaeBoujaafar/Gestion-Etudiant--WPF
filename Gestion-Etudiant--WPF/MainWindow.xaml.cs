using Gestion_Etudiant__WPF.GestionEtudiant;
using Gestion_Etudiant__WPF.GestionFiliere;
using Gestion_Etudiant__WPF.GestionStatistiques;
using System;
using System.Collections.Generic;
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

namespace Gestion_Etudiant__WPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserControl usc = null;
        public MainWindow()
        {
            InitializeComponent();
            usc = new AccueilEtudiant();
            GridMain.Children.Add(usc);
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemStudent":
                    usc = new AccueilEtudiant();
                    GridMain.Children.Add(usc);
                    Title.Text = "Gestion des Etudiants";
                    break;
                case "ItemFiliere":
                    usc = new Filiere();
                    GridMain.Children.Add(usc);
                    Title.Text = "Gestion des Filières";
                    break;
                case "ItemStatistiques":
                    usc = new Statistiques();
                    GridMain.Children.Add(usc);
                    Title.Text = "Statistiques";
                    break; 
                 /*case "ItemLogOut":
                    loginWindow dashbord = new loginWindow();
                    dashbord.Show();
                    this.Close();
                    break;*/
                default:
                    break;
            }
        }
    }
}
