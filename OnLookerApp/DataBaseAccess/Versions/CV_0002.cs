using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using OnLooker.Core;

namespace OnLooker.DataBaseAccess
{
    class CV_0002 : AVersion
    {
        String _path = "./../../../DataBaseAccess/Source/Currencies.json";
        String _createProcedureInsertScript = "CREATE PROCEDURE [sp_InsertCurrency]\n@code varchar(10),\n@name varchar(20)\nAS\nINSERT INTO CurrencyType (CurrencyCode, CurrencyName) VALUES (@code, @name)\nSELECT SCOPE_IDENTITY();\n";
        String _createProcedureGetScript = "CREATE PROCEDURE [sp_GetCurrencies]\nAS\nSELECT * FROM CurrencyType;\n";
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
            get { return "0002"; }
        }
        internal override String Comment
        {
            get { return "Currencies insert"; }
        }

        internal override void GetVersion()
        {
            /*this version is for putting default values in db (the names of all existing currencies)*/
            /*-- get currencies from json file --*/
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(_createProcedureInsertScript, conn);
                SqlCommand cmd2 = new SqlCommand(_createProcedureGetScript, conn);
                try
                {
                    cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                   throw new Exception("Unhandled exception " + e.Message);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    CDbConnection.IsCreating = false;
                }

                InsertDefaultValues();
            }
        }


        private void InsertDefaultValues()
        {
            Dictionary<String, String> currencies = CReadFromFileService.ReadFromFileAsDictionary(_path);
            CCurrencyGateway currencyGateway = new CCurrencyGateway();
            foreach (var currency in currencies)
            {
                CurrencyInfo curInfo = new CurrencyInfo();
                curInfo.Name = currency.Value;
                curInfo.Code = currency.Key;
                currencyGateway.Create(curInfo);
            }

            Upgrade();
        }

    }
}
