using System;
using System.Configuration;
using System.Data.SqlClient;


namespace OnLooker
{
    public static class DbConnection
    {
        static readonly String s_connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        static DbConnection()
        {
        }
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(s_connectionString);
        }
    }
}
