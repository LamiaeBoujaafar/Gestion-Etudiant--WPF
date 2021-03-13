using System;
using System.Collections.Generic;
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
using System.Data;
using System.Collections.ObjectModel;


namespace Gestion_Etudiant__WPF.GestionEtudiant
{
    /// <summary>
    /// Logique d'interaction pour AccueilEtudiant.xaml
    /// </summary>
    public partial class AccueilEtudiant : UserControl
    {
        GestionEtudiantWPFEntities _db = new GestionEtudiantWPFEntities();
        ObservableCollection<Etudiant> obse;
        


        public static DataGrid datagrid;
        public AccueilEtudiant()
        {
            InitializeComponent();
            Load();
        }

        private void AfficherBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if(this.filiereComboBox.SelectedIndex==-1)
            {
                MessageBox.Show("Veuillez sélectionner une filiere", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                String fili = this.filiereComboBox.SelectedItem.ToString();
                var comboFili = _db.Filieres.Where<Filiere>(fi => fi.FiliereName  == fili).Single();
                var etudiants = _db.Etudiants.Where<Etudiant>(et => et.ID_Filiere == comboFili.ID_Filiere);
                obse = new ObservableCollection<Etudiant>(etudiants.ToList());
                dataGridEtud.ItemsSource = obse;

            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = Config.CONNECTION_STRING;
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT FiliereName FROM Filiere", cn);
                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    this.filiereComboBox.Items.Add(rd.GetString(0));
                }
                rd.Close();
                
               
                this.filiereComboBox.SelectedIndex=0;
            }
            
        }

        public void dataGridLoad()
        {
            /* SqlConnection con = new SqlConnection();
             con.ConnectionString = @"Data Source=DESKTOP-GFRQLGV\SQLEXPRESS;Initial Catalog=GestionEtudiantWPF;Integrated Security=True";
             if (this.filiereComboBox.SelectedIndex == -1)
             {
                 MessageBox.Show("Veuillez sélectionner une filiere", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
             }
             else
             {
                 SqlCommand cmd = new SqlCommand("SELECT e.*,f.FiliereName FROM Etudiant e,Filiere f WHERE e.ID_Filiere=f.ID_Filiere  AND f.FiliereName=@filie;", con);
                 cmd.Parameters.AddWithValue("@filie", this.filiereComboBox.SelectedItem);
                 try
                 {

                     con.Open();
                     // cmd.ExecuteReader();
                     DataTable dt = new DataTable("Etudiant");
                     SqlDataAdapter da = new SqlDataAdapter(cmd);
                     da.Fill(dt);
                     dataGridEtud.ItemsSource = dt.DefaultView;

                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                 }
                 finally
                 {
                     con.Close();

                 }
             }
            */

            
        }
        

        private void filiereComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString =Config.CONNECTION_STRING;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Responsable FROM Filiere  WHERE FiliereName=@filie";
            cmd.Connection = cn;
            cn.Open();
            cmd.Parameters.AddWithValue("@filie", this.filiereComboBox.SelectedItem);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                this.respoTxtBox.Text = rd.GetString(0);
            }
            cn.Close();
        }

        /*Je me suis arreté à la fonctionnalité qie permet 
         * d'afficher les etudiants selon leur filiere
         */

        public void Load()
        {
            
        }

        private void ajoutBtn_Click(object sender, RoutedEventArgs e)
        {
            
                InsertPage insPage = new InsertPage();
                insPage.ShowDialog();

        }

        private void modifBtn_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridEtud.SelectedItem==null)
            {
                MessageBox.Show("Veuillez selectionner une ligne de grille pour modifier", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {

                Etudiant etu = (Etudiant)dataGridEtud.SelectedItem;
                UpdatePage updPage = new UpdatePage(etu);
                updPage.ShowDialog();

            }

        }

        private void suppBtn_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridEtud.SelectedItem == null)
            {
                MessageBox.Show("Veuillez selectionner une ligne de grille pour supprimer", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBoxResult result2 = MessageBox.Show("Voulez vraiment supprimer cet étudiant","Message Important",MessageBoxButton.YesNoCancel,MessageBoxImage.Question);
                
                if(result2 == MessageBoxResult.Yes)
                {
                    //var index = dataGridEtud.SelectedIndex;
                    ////here we get the actual row at selected index
                    //DataGridRow row = dataGridEtud.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                    //DataRowView MyRow = (DataRowView)row.Item ;


                    Etudiant etu = (Etudiant)dataGridEtud.SelectedItem;

                    var deletedEtud = _db.Etudiants.Where<Etudiant>(et => et.ID_Etudiant == etu.ID_Etudiant).Single();
                    // dataGridEtud.Items.RemoveAt(dataGridEtud.SelectedIndex);
                    _db.Etudiants.Remove(deletedEtud);
                    _db.SaveChanges();
                    MessageBox.Show("Etudiant supprimé avec succè ", "Message", MessageBoxButton.OK, MessageBoxImage.Information);

                    //Refresh the datagrid after deleting
                    String fili = this.filiereComboBox.SelectedItem.ToString();
                    var comboFili = _db.Filieres.Where<Filiere>(fi => fi.FiliereName == fili).Single();
                    var etudiants = _db.Etudiants.Where<Etudiant>(et => et.ID_Filiere == comboFili.ID_Filiere);
                    obse = new ObservableCollection<Etudiant>(etudiants.ToList());
                    dataGridEtud.ItemsSource = obse;
                }
                
                
            }

        }
    } 
}
