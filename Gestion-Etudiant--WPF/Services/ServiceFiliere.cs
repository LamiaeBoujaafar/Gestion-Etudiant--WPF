using System;
using System.Data;
using System.Data.SqlClient;

namespace Gestion_Etudiant__WPF.Services
{
    class ServiceFiliere
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        String strConn = Config.CONNECTION_STRING;
<<<<<<<< HEAD:Gestion-Etudiant--WPF/Services/ServiceFiliere.cs
        public ServiceFiliere()
========

        public ServiceEtudiant()
>>>>>>>> f5d3f4d6e0f86fcfe7c74843d634c938717e7b5a:Gestion-Etudiant--WPF/Services/ServiceEtudiant.cs
        {
            connection = new SqlConnection(strConn);
        }
        {
        }
        public DataTable FillData()
========

        public DataTable getAll()
>>>>>>>> f5d3f4d6e0f86fcfe7c74843d634c938717e7b5a:Gestion-Etudiant--WPF/Services/ServiceEtudiant.cs
        {
            
            SqlCommand cmd = new SqlCommand(query, connection);
            DataTable dt = new DataTable("fil");
            connection.Open();
            SqlDataReader red = cmd.ExecuteReader();

            if (red.HasRows)
            {
                dt.Load(red);
                connection.Close();
                return dt;
            }
            else
            {
                connection.Close();
                return null;
            }

        }
    }
}
