using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace OnLooker.DataBaseAccess
{
    public static class CDbConnection
    {
        public static Boolean IsLocalDb;
        internal static Boolean IsCreating;
        private static readonly String s_connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public static SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(s_connectionString);
            if(conn.State == ConnectionState.Open)
                conn.ChangeDatabase("ONLOOKER");
            else
            {
                conn.Open();
                conn.ChangeDatabase("ONLOOKER");
            }
            return conn;
        }
        public static SqlConnection GetServerConnection()
        {
            return new SqlConnection(s_connectionString);
        }

        public static void ConnectionClose()
        {
            SqlConnection conn = new SqlConnection(s_connectionString);
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }
}
