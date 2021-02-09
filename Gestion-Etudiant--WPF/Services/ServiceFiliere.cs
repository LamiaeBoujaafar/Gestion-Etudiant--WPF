using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Etudiant__WPF.Services
{
    class ServiceFiliere
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        String strConn = Config.CONNECTION_STRING;
        public ServiceFiliere()
        {
            con = new SqlConnection(strConn);
        }
        public bool delete(object id)
        {
            con.Open();
            string requete = "DELETE FROM Filiere WHERE ID_Filiere = @id ";
            SqlCommand cmd = new SqlCommand(requete, con);
            cmd.Parameters.AddWithValue("@id", id);
            int a = cmd.ExecuteNonQuery();
            con.Close();
            return a > 0;
        }
        public DataTable FillData()
        {
            string sql = "SELECT * FROM Filiere";
            DataTable dt;
            con.Open();
            using (com = new SqlCommand(sql, con))
            {
                dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dt);
                
            }
            con.Close();
            return dt;
        }
    }
}
