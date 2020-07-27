using System;
using System.Configuration;
using System.Data.SqlClient;


namespace OnLooker
{
    public static class CDbConnection
    {
        public static Boolean IsLocalDb;
        internal static Boolean IsCreating;
        private static String s_connectionString=ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        static CDbConnection()
        {}
        public static SqlConnection GetConnection()
        {
            if (IsLocalDb)
            {
                if (IsCreating)
                    s_connectionString = ConfigurationManager.ConnectionStrings["CreateConnection"].ConnectionString;
                else
                    s_connectionString = ConfigurationManager.ConnectionStrings["DebugConnection"].ConnectionString;
            }
            return new SqlConnection(s_connectionString);
        }

        public static void ChangeDefaultConnection(String conn)
        {
            s_connectionString = conn;
        }
    }
}
