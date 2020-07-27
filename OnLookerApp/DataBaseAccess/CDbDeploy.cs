using System;
using System.Data;
using System.Data.SqlClient;


namespace OnLooker.DataBaseAccess
{
    public class CDbDeploy
    {
        private  static Boolean CheckDatabaseExists(SqlConnection conn, String databaseName)
        {
            String sqlCreateDBQuery;
            Boolean result = false;

            try
            {
                sqlCreateDBQuery = string.Format("SELECT database_id FROM sys.databases WHERE Name = '{0}'",
                    databaseName);
                using (SqlCommand sqlCmd = new SqlCommand(sqlCreateDBQuery, conn))
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    Object resultObj = sqlCmd.ExecuteScalar();
                    Int32 databaseID = -1;
                    if (resultObj != null)
                        Int32.TryParse(resultObj.ToString(), out databaseID);

                    result = databaseID > 0;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw new Exception(e.Message);
            }

            return result;
        }

        public static void ConnectDataBase()
        {
            String databaseName = "ONLOOKER";
            var conn = CDbConnection.GetServerConnection();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            try
            {
                if(CheckDatabaseExists(conn, databaseName))
                    conn.ChangeDatabase(databaseName);
                else
                    CDbDeploy.Create();
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw new Exception(e.Message);
            }
        }
        public static void Create()
        {
            CV_0000 version = new CV_0000();
            version.GetVersion();
            Update();
        }

        public static void Update() 
        {
            Action upgradeToLastVersion= new Action((() => { Console.WriteLine("Updating Database..."); })); 
            
            CV_0001 version1 = new CV_0001();
            CV_0002 version2 = new CV_0002();

            if (version1.IsLastVersion())
                upgradeToLastVersion += version2.GetVersion;

            else if (version2.IsLastVersion())
                Console.WriteLine("Version of DB is up to date");

            else
            {
                upgradeToLastVersion += version1.GetVersion;
                upgradeToLastVersion += version2.GetVersion;
            }
                
            

            upgradeToLastVersion.Invoke();
        }
    }
}
