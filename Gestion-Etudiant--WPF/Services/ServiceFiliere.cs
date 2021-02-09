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
        public ServiceFiliere()
        {
            connection = new SqlConnection(strConn);
        }
        
        public DataTable FillData()
        {
            string sql = "SELECT * FROM Filiere";
            DataTable dt;
            connection.Open();
            using (command = new SqlCommand(sql, connection))
            {
                dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
                
            }
            connection.Close();
            return dt;
        }

        public DataTable getAll()
        {
            string query = "SELECT * FROM Filiere";
            
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
                return null;
            }

        }
    }
}
