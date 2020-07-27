using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using OnLooker.Core;

namespace OnLooker.DataBaseAccess
{
    class CV_0000 : AVersion
    {
        internal override String Major
        {
            get { return "01"; }
        }
        internal override String Minor
        {
            get { return "01"; }
        }
        internal override String FileNumber
        {
            get { return "0000"; }
        }
        internal override String Comment
        {
            get { return "Db creation from scratch"; }
        }
        internal override void GetVersion()
        {
            var storedProcedures = Directory.GetFiles("./../../../DataBaseAccess/Source/", "sp_*.sql");
            String createDbScript = File.ReadAllText("./../../../DataBaseAccess/Source/CreateDataBase.sql");

            using (SqlConnection conn = CDbConnection.GetServerConnection())
            {
                try
                {
                    conn.Open();
                    
                    SqlCommand cmd0 = new SqlCommand("CREATE DATABASE ONLOOKER", conn);
                    cmd0.ExecuteNonQuery();
                    cmd0.Dispose();

                    SqlCommand cmd = new SqlCommand(createDbScript, conn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    foreach (var sqlScriptFile in storedProcedures)
                    {
                        String sqlExpression = File.ReadAllText(sqlScriptFile);
                        SqlCommand cmd2 = new SqlCommand(sqlExpression, conn);
                        cmd2.ExecuteNonQuery();
                        cmd2.Dispose();
                    }
                    
                }
                catch (Exception e)
                {
                    //SLogger.Log.Fatal("Unexpected termination: " + e.Message);
                    throw new Exception("Unhendled exception" + e.Message);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
                InsertDefaultValues();

            }
        }

        private void InsertDefaultValues()
        {
            CountryInfo country = new CountryInfo();
            country.Name = "International";
            country.Code = "None";
            CCountryGateway countryGateway = new CCountryGateway();
            countryGateway.Create(country);
            Upgrade();
        }
    }
}
