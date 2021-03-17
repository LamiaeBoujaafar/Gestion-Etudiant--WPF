using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using System.Windows.Shapes;

namespace Gestion_Etudiant__WPF.GestionEtudiant
{
    /// <summary>
    /// Interaction logic for InsertPage.xaml
    /// </summary>
    public partial class InsertPage : Window
    {
        GestionEtudiantWPFEntities _db = new GestionEtudiantWPFEntities();

        OpenFileDialog openFileDialog1;
        private Etudiant etu;

        public InsertPage()
        {
            InitializeComponent();
            openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"E:\",
                Title = "Browse Student jpg File",

                CheckFileExists = true,
                CheckPathExists = true,

                Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif",
                FilterIndex = 2,
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
        }

        private void insererBtn_Click(object sender, RoutedEventArgs e)
        {
            if (cneTxt.Text == "" || nomTxt.Text == "" || prenomTxt.Text == "" || genderTxt.Text == "" || birthTxt.SelectedDate == null || adressTxt.Text == "" || telephTxt.Text == "" || ageTxt.Text == ""  || filierTxt.Text == "")
            {
                MessageBox.Show("Veillez remplir tous les champs ", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                //*************************************************************//

                //Partie insertion de photo dans le repertoire 

                if (imgSiteLogo.Source == null)
                {
                    MessageBox.Show("image is required.");
                    return;
                }
                string sourceFile = (imgSiteLogo.Source as BitmapImage).UriSource.AbsolutePath;
                string targetPath = @"E:\pictures";
                // for the name of student image set its name to "firstName-lastName-CNE.jpg"
                MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory);
                string fileName = String.Format("{0}-{1}-{2}.jpg",nomTxt.Text,prenomTxt.Text,cneTxt.Text);
                // Use Path class to manipulate file and directory paths.
                string destFile = System.IO.Path.Combine(targetPath, fileName);
                //MessageBox.Show("sourceFile : " + sourceFile);
                // To copy a folder's contents to a new location:
                // Create a new target folder.
                // If the directory already exists, this method does not create a new directory.
                System.IO.Directory.CreateDirectory(targetPath);
                // To copy a file to another location and
                // overwrite the destination file if it already exist
                System.IO.File.Copy(sourceFile, destFile, true); 

                // - then insert to student table imageName =  fileName or destFile
                // - to show it in datagrid view do this
                // =====> _____.source =  $"{AppDomain.CurrentDomain.BaseDirectory}pictures/{row[imageName]}"

                etu = new Etudiant()
                {
                    CNE = cneTxt.Text,
                    FirstName = nomTxt.Text,
                    LastName = prenomTxt.Text,
                    gender = genderTxt.Text,
                    DOB = birthTxt.SelectedDate,
                    adresse = adressTxt.Text,
                    tele = telephTxt.Text,
                    age = Int32.Parse(ageTxt.Text),
                    photo = destFile,
                    ID_Filiere = filierTxt.SelectedIndex + 1
                };

                _db.Etudiants.Add(etu);
                _db.SaveChanges();

                //AccueilEtudiant.datagrid.ItemsSource = _db.Etudiants.ToList();   Remplissage automatique de la base de donnees

                // Permet de fermer la fenetre d'ajout
                this.Hide();
                MessageBox.Show("Etudiant ajouté avec succès", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                // Permer de rafraichir la datagrid par la nouvelle insertion
                


                

            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString =Config.GetConnection();
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT FiliereName FROM Filiere", cn);
                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    this.filierTxt.Items.Add(rd.GetString(0));
                }
                rd.Close();


                this.filierTxt.SelectedIndex = 0;
            }
        }


        private void photBtn_Click(object sender, RoutedEventArgs e)
        {
            bool? result = openFileDialog1.ShowDialog();
            if (result.HasValue && result.Value)
            {

                var uriSource = new Uri(openFileDialog1.FileName);
                imgSiteLogo.Source = new BitmapImage(uriSource); // Image x:Name="imgSiteLogo"
            }
        }
    }
}
