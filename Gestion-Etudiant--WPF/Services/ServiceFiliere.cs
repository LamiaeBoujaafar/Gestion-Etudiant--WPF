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
        public DataTable get(String filiere)
        {
            SqlCommand cmd = new SqlCommand(
                "SELECT * from Filiere where Responsable like @filiere;",
                connection);
            cmd.Parameters.AddWithValue("@filiere", $"%{filiere}%");
   
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
            return null;

        }
        public string delete(int id)
        {
            connection.Open();
            string requete = "DELETE FROM Filiere WHERE ID_Filiere = @id; ";
            SqlCommand cmd = new SqlCommand(requete, connection);
            cmd.Parameters.AddWithValue("@id", id);
            bool a = false;
            string x = "";
            try
            {
                cmd.ExecuteNonQuery();
                a = true;
                x = "bien supprimé";
            }
            catch (SqlException ex)
            {
                x = "vous ne pouvez pas supprimer cette filière";
            }
            connection.Close();
            return x ;
        }
        public int insert(String responsable,string filiere)
        {
            string query = "INSERT INTO Filiere(Responsable,FiliereName) VALUES (@responsable,@filiere);";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@responsable", responsable);
            cmd.Parameters.AddWithValue("@filiere", filiere);
            connection.Open();
            int a = cmd.ExecuteNonQuery();
            connection.Close();
            return a;
        }
        public int update(int id,String responsable, string filiere)
        {
            string query = "UPDATE Filiere SET Responsable = @responsable, FiliereName = @filiere WHERE ID_Filiere = @id; ";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@responsable", responsable);
            cmd.Parameters.AddWithValue("@filiere", filiere);
            connection.Open();
            int a = cmd.ExecuteNonQuery();
            connection.Close();
            return a;
        }
        public DataTable FillData()
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
                connection.Close();
                return null;
            }

        }
    }
}
