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
using System.Data.SqlClient;
using System.Configuration;


namespace Gestion_Etudiant__WPF
{
    /// <summary>
    /// Logique d'interaction pour loginWindow.xaml
    /// </summary>
    public partial class loginWindow : Window
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        public loginWindow()
        {
            InitializeComponent();
            String strConn = Config.GetConnection();
            con.ConnectionString = strConn;
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            if (VerifyUser(txtUsername.Text, txtPassword.Password))
            {
                MainWindow dashbord = new MainWindow();
                dashbord.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Email or password is incorrect", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private bool VerifyUser(string email, string password)
        {
            con.Open();
            com.Connection = con;
            com.CommandText = "select COUNT(1) from Admin where Email='" + email + "' and Mdp='" + password + "'";
            int count = Convert.ToInt16(com.ExecuteScalar());
            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
