using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace Gestion_Etudiant__WPF.GestionEtudiant
{
    /// <summary>
    /// Interaction logic for UpdatePage.xaml
    /// </summary>
    public partial class UpdatePage : Window
    {

        GestionEtudiantWPFEntities _db = new GestionEtudiantWPFEntities();
        OpenFileDialog openFileDialog1;
        int ID;
        Etudiant et = null;

        public UpdatePage(Etudiant et)
        {
            this.et = et;
            InitializeComponent();
            ID = (int)et.ID_Filiere;
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

            cneTxt.Text = et.CNE;
            nomTxt.Text = et.FirstName;
            prenomTxt.Text = et.LastName;
            genderTxt.Text = et.gender;
            adressTxt.Text = et.adresse;
            telephTxt.Text = et.tele;
            
            
            
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            if(cneTxt.Text=="" || nomTxt.Text == "" || prenomTxt.Text == "" || genderTxt.Text == "" || birthTxt.SelectedDate == null || adressTxt.Text == "" || telephTxt.Text == "" || ageTxt.Text == ""  || filierTxt.SelectedIndex==-1 )
            {
                MessageBox.Show("Veillez remplir tous les champs ", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                //Etudiant etudUpdated = (from etu in _db.Etudiants where etu.ID_Etudiant == ID select etu).Single();


                //*************************************************************//

                //Partie insertion de photo dans le repertoire 

                if (imgSiteLogo.Source == null)
                {
                    MessageBox.Show("image is required.");
                    return;
                }
                string sourceFile = (imgSiteLogo.Source as BitmapImage).UriSource.AbsolutePath;
                string targetPath = @"E:\pictures"; ;
                // for the name of student image set its name to "firstName-lastName-CNE.jpg"
                //MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory);
                string fileName = String.Format("{0}-{1}-{2}.jpg", nomTxt.Text, prenomTxt.Text, cneTxt.Text);
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
                et.CNE = cneTxt.Text;
                et.FirstName = nomTxt.Text;
                et.LastName = prenomTxt.Text;
                et.gender = genderTxt.Text;
                et.DOB = birthTxt.SelectedDate;
                et.adresse = adressTxt.Text; 
                et.tele = telephTxt.Text;
                et.age = Int32.Parse(ageTxt.Text);
                et.photo = destFile;
                et.ID_Filiere = filierTxt.SelectedIndex + 1;

                
               // _db.Etudiants.Attach(et);
                // _db.Entry(et).State = System.Data.Entity.EntityState.Modified;
               // _db.Entry(et).Property(et => et.ID_Etudiant).IsModified = true;
                _db.SaveChanges();
                this.Hide();
                MessageBox.Show("Etudiant modifié avec succè ", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                


            }
            

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString =Config.CONNECTION_STRING;
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
