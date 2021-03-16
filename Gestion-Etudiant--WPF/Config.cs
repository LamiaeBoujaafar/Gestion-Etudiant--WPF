using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Etudiant__WPF
{
    class Config
    {

    //public static string GetConnection() = @"Data Source=DESKTOP-GK7J0IJ\SQLEXPRESS;Initial Catalog=GestionEtudiantWPF;Integrated Security=True";
    public static string GetConnection()
    {
      SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
      builder.DataSource = "40.87.141.70";
      builder.UserID = "SA";
      builder.Password = "GI2020wpf";
      builder.InitialCatalog = "GestionEtudiantWPF";
      return builder.ConnectionString;
    }
    public static string CONNECTION_STRING = @"Data Source=DESKTOP-LB4BGFM\SQLEXPRESS;Initial Catalog=GestionEtudiantWPF;Integrated Security=True";
        
    }
}
