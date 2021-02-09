﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace Gestion_Etudiant__WPF.Services
{
    class ServiceEtudiant
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        String strConn = Config.CONNECTION_STRING;

        public ServiceEtudiant()
        {
            connection = new SqlConnection(strConn);
        }

        public DataTable getAll()
        {
            string query = "SELECT * FROM Etudiant";
            
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
