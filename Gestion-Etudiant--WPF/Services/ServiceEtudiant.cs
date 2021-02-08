using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Etudiant__WPF.Services
{
    class ServiceEtudiant
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        String strConn = Config.CONNECTION_STRING;
        public ServiceEtudiant()
        {
            con = new SqlConnection(strConn);
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
