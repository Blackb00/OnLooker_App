using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using OnLooker.Core;

namespace OnLooker.DataBaseAccess.Versions
{
    class CV_0000: AVersion
    {
        [Serializable]
        private class SqlScripts
        {
            internal String DbCreate;
            internal String DbUse;
            internal TablesCreateScripts TablesCreate;
            internal StoredProceduresCreate StoredProceduresCreate;
        }

        [Serializable]
        private class TablesCreateScripts
        {
            internal String UserLogin;
            internal String UserInfo;
            internal String Country;
            internal String Tag;
            internal String CurrencyType;
            internal String CurrencyPair;
            internal String Job;
            internal String UserJobs;
            internal String Article;
            internal String ArticleTag;
            internal String Report;
            internal String CurrencyPairTimePrint;
            internal String ReportCurPairTomePrints;
            internal String MigrationHistory;
            internal String Logs;

            internal String GetAll()
            {
                return
                    $"{UserLogin} {UserInfo} {Country} {Tag} {CurrencyType} {CurrencyPair} {Job} {UserJobs} {Article} {ArticleTag} {Report} {CurrencyPairTimePrint} {ReportCurPairTomePrints} {MigrationHistory} {Logs}";
            }
        }

        [Serializable]
        private class StoredProceduresCreate
        {
            internal String GetArticles;
            internal String GetArticleTagRelations;
            internal String GetCountryById;
            internal String GetTagByName;
            internal String GetTags;
            internal String InsertArticle;
            internal String InsertArticleTag;
            internal String InsertTag;
            internal String InsertCountry;
            internal String InsertVersion;
            internal String GetAllCountries;

        internal String GetAll()
            {
                return
                    $"{GetArticles} {GetArticleTagRelations} {GetCountryById} {GetTagByName} {GetTags} {InsertArticle} {InsertArticleTag} {InsertTag} {InsertCountry} {InsertVersion} {GetAllCountries}";
            }
        }

        static readonly String s_jsonString = File.ReadAllText("./../../Source/SqlScripts.json");
        readonly SqlScripts s_scripts = CJsonService.Deserialize<SqlScripts>(s_jsonString);

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
            CDbConnection.IsLocalDb = true;
            CDbConnection.IsCreating = true;
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                var dbCreate = s_scripts.DbCreate;
                var dbUse = s_scripts.DbUse;
                var tablesCreate = s_scripts.TablesCreate.GetAll();
                var StoredProceduresCreate = s_scripts.StoredProceduresCreate.GetArticles;
                var StoredProceduresCreate2 = s_scripts.StoredProceduresCreate.GetArticleTagRelations;
                var StoredProceduresCreate3 = s_scripts.StoredProceduresCreate.GetCountryById;
                var StoredProceduresCreate4 = s_scripts.StoredProceduresCreate.GetTagByName;
                var StoredProceduresCreate5 = s_scripts.StoredProceduresCreate.GetTags;
                var StoredProceduresCreate6 = s_scripts.StoredProceduresCreate.InsertArticle;
                var StoredProceduresCreate7 = s_scripts.StoredProceduresCreate.InsertArticleTag;
                var StoredProceduresCreate8 = s_scripts.StoredProceduresCreate.InsertTag;
                var StoredProceduresCreate9 = s_scripts.StoredProceduresCreate.InsertCountry;
                var StoredProceduresCreate10 = s_scripts.StoredProceduresCreate.InsertVersion;
                var StoredProceduresCreate11 = s_scripts.StoredProceduresCreate.GetAllCountries;


                SqlCommand cmd = new SqlCommand(dbCreate, conn);
                SqlCommand cmd2 = new SqlCommand(dbUse, conn);
                SqlCommand cmd3 = new SqlCommand(tablesCreate, conn);
                SqlCommand
                    cmd4 = new SqlCommand(StoredProceduresCreate,
                        conn); //:todo resolve the problem with sql query with creating all procedures in one (like with tables)
                SqlCommand cmd5 = new SqlCommand(StoredProceduresCreate2, conn);
                SqlCommand cmd6 = new SqlCommand(StoredProceduresCreate3, conn);
                SqlCommand cmd7 = new SqlCommand(StoredProceduresCreate4, conn);
                SqlCommand cmd8 = new SqlCommand(StoredProceduresCreate5, conn);
                SqlCommand cmd9 = new SqlCommand(StoredProceduresCreate6, conn);
                SqlCommand cmd10 = new SqlCommand(StoredProceduresCreate7, conn);
                SqlCommand cmd11 = new SqlCommand(StoredProceduresCreate8, conn);
                SqlCommand cmd12 = new SqlCommand(StoredProceduresCreate9, conn);
                SqlCommand cmd13 = new SqlCommand(StoredProceduresCreate10, conn);
                SqlCommand cmd14 = new SqlCommand(StoredProceduresCreate11, conn);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    cmd3.ExecuteNonQuery();
                    cmd4.ExecuteNonQuery();
                    cmd5.ExecuteNonQuery();
                    cmd6.ExecuteNonQuery();
                    cmd7.ExecuteNonQuery();
                    cmd8.ExecuteNonQuery();
                    cmd9.ExecuteNonQuery();
                    cmd10.ExecuteNonQuery();
                    cmd11.ExecuteNonQuery();
                    cmd12.ExecuteNonQuery();
                    cmd13.ExecuteNonQuery();
                    cmd14.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    SLogger.Log.Fatal("Unexpected termination: " + e.Message);
                    Console.WriteLine(e.Message);
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
            CountryInfo country = new CountryInfo();
            country.Name = "International";
            country.Code = "None";
            CCountryGateway countryGateway = new CCountryGateway();
            countryGateway.Create(country);
            Upgrade();
        }
    }
}
