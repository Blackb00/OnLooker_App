using System;
using OnLooker.Core;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace OnLooker.DataBaseAccess
{
    public class CJobGateway : IJobGateway
    {
        public Int32 Create(CJob entity)
        {
            Int32 id = 0;
            Int32 CurrencyPairID = 0;
            String getCurrencyPairID =
                $@"WITH CurPairNamed AS (SELECT CurPair.ID, CurTypeBase.CurrencyCode AS BaseCurrency, CurTypeQuoted.CurrencyCode AS QuotedCurrency

            FROM[ONLOOKER].[dbo].[CurrencyPair] AS CurPair

            JOIN[ONLOOKER].[dbo].[CurrencyType] AS CurTypeBase

            ON CurPair.BaseCurrencyID = CurTypeBase.ID

            JOIN[ONLOOKER].[dbo].[CurrencyType] AS CurTypeQuoted

            ON CurPair.BaseCurrencyID = CurTypeQuoted.ID)
            SELECT ID FROM CurPairNamed

            WHERE BaseCurrency = '{entity.CurrencyPair.BaseCurrency.Code}' AND QuotedCurrency = '{entity.CurrencyPair.QuotedCurrency.Code}'";
            String insertJob =
                $@"INSERT INTO Job (CurrencyPairID, CountryID, TagID) values ({CurrencyPairID},{entity.Country.ID},{entity.Tag.ID}) SELECT SCOPE_IDENTITY()";

            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand cmdGetCurPairId = new SqlCommand(getCurrencyPairID, conn);
                cmdGetCurPairId.CommandType = CommandType.Text;

                using (var reader = cmdGetCurPairId.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CurrencyPairID = (Int32)reader["ID"];
                        }
                    }
                }


                SqlCommand cmdInsertJob = new SqlCommand(insertJob, conn);
                cmdInsertJob.CommandType = CommandType.Text;

                //SqlParameter curPairParam = new SqlParameter
                //{
                //    ParameterName = "@curPair",
                //    Value = CurrencyPairID
                //};
                //cmdInsertJob.Parameters.Add(curPairParam);
                //SqlParameter countryParam = new SqlParameter
                //{
                //    ParameterName = "@country",
                //    Value = entity.Country.ID
                //};
                //cmdInsertJob.Parameters.Add(countryParam);
                //SqlParameter tagParam = new SqlParameter
                //{
                //    ParameterName = "@tag",
                //    Value = entity.Tag.ID
                //};
                //cmdInsertJob.Parameters.Add(tagParam);
                try
                {
                    var result = cmdInsertJob.ExecuteScalar();
                    Console.WriteLine("Id добавленного объекта: {0}", result);
                    id = 1;
                }
                catch (Exception e)
                {
                    throw new Exception("Unexpected termination" + e.Message);
                    //SLogger.Log.Fatal($"CArticleGateway.Create method. Exception: {e.Message}")
                }

                return id;
            }
        }

        public void Delete(Int32 id)
        {
            String sqlExpression = "sp_DeleteJob";
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(sqlExpression, conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@aId",
                    Value = id
                };
                command.Parameters.Add(idParam);
                var result = command.ExecuteNonQuery();
                Console.WriteLine("Id удалённого из Job объекта: {0}", id);
            }
        }

        public CJob Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public CJob GetByQueryInfo(QueryInfo info)
        {
            CCountryGateway countryGateway = new CCountryGateway();
            CJob job = new CJob();
            String sqlExpression = "sp_GetJobByUserQuery";
 //               $@" WITH JobTable AS (SELECT Job.ID AS JobID, CurPairNamed.BaseCurrency, CurPairNamed.QuotedCurrency,Country.Name AS Country, Tag.Name AS Tag
 // FROM [ONLOOKER].[dbo].[Job] AS Job 
 // JOIN [ONLOOKER].[dbo].[Country] AS Country 
 // ON Job.[CountryID] = Country.ID
 // Join [ONLOOKER].[dbo].[Tag] AS Tag
 // ON Job.TagID = Tag.ID
 // JOIN(
	//SELECT CurPair.ID, CurTypeBase.CurrencyCode AS BaseCurrency, CurTypeQuoted.CurrencyCode AS QuotedCurrency
	//FROM [ONLOOKER].[dbo].[CurrencyPair] AS CurPair
	//JOIN [ONLOOKER].[dbo].[CurrencyType] AS CurTypeBase
	//ON CurPair.BaseCurrencyID = CurTypeBase.ID
	//JOIN [ONLOOKER].[dbo].[CurrencyType] AS CurTypeQuoted
	//ON CurPair.BaseCurrencyID = CurTypeQuoted.ID
	//) AS CurPairNamed
	//ON  CurPairNamed.ID = Job.CurrencyPairID)
	//SELECT * FROM JobTable
	//WHERE BaseCurrency = '{info.CurrencyPair.BaseCurrency.Code}' AND QuotedCurrency = '{info.CurrencyPair.QuotedCurrency.Code}' AND Country = '{info.Country.Name}' AND Tag = '{info.Tag.Value}'";
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(sqlExpression, conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter baseParam = new SqlParameter
                {
                    ParameterName = "@base",
                    Value = info.CurrencyPair.BaseCurrency.Code

                };
                command.Parameters.Add(baseParam);
                SqlParameter quotedParam = new SqlParameter
                {
                    ParameterName = "@quoted",
                    Value = info.CurrencyPair.QuotedCurrency.Code
                };
                command.Parameters.Add(quotedParam);
                SqlParameter countryParam = new SqlParameter
                {
                    ParameterName = "@country",
                    Value = info.Country.Name
                };
                command.Parameters.Add(countryParam);
                SqlParameter tagParam = new SqlParameter
                {
                    ParameterName = "@tag",
                    Value = info.Tag.Value
                };
                command.Parameters.Add(tagParam);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            job.CurrencyPair = new CCurrencyPair() { BaseCurrency = (CurrencyInfo)reader["BaseCurrency"], QuotedCurrency = (CurrencyInfo)reader["QuotedCurrency"] };
                            job.ID = (Int32)reader["JobID"];
                            job.Tag = new CTag((String)reader["Tag"]);
                            job.Country = countryGateway.GetAll().FirstOrDefault(x => String.Compare(x.Name, (String)reader["Country"], StringComparison.InvariantCulture) == 0);
                        }
                    }
                    else
                    {
                        job = null;
                    }
                }
            }
            return job;

        }
        public List<CJob> GetAll()
        {
            List<CJob> jobs = new List<CJob>();

            String sqlExpression = "sp_GetAllJobs";
                
            //    @"SELECT Job.ID AS JobID, CurPairNamed.BaseCurrency, CurPairNamed.QuotedCurrency,Country.Name AS Country, Tag.Name AS Tag
            //FROM[ONLOOKER].[dbo].[Job] AS Job
            //JOIN[ONLOOKER].[dbo].[Country] AS Country
            //ON Job.[CountryID] = Country.ID
            //Join[ONLOOKER].[dbo].[Tag] AS Tag
            //ON Job.TagID = Tag.ID
            //JOIN(
            //    SELECT CurPair.ID, CurTypeBase.CurrencyName AS BaseCurrency, CurTypeQuoted.CurrencyName AS QuotedCurrency

            //FROM[ONLOOKER].[dbo].[CurrencyPair] AS CurPair

            //JOIN[ONLOOKER].[dbo].[CurrencyType] AS CurTypeBase

            //ON CurPair.BaseCurrencyID = CurTypeBase.ID

            //JOIN[ONLOOKER].[dbo].[CurrencyType] AS CurTypeQuoted

            //ON CurPair.BaseCurrencyID = CurTypeQuoted.ID
            //    ) AS CurPairNamed

            //ON CurPairNamed.ID = Job.CurrencyPairID
            //";
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
             
                SqlCommand command = new SqlCommand(sqlExpression, conn);
                command.CommandType = CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CJob job = new CJob();
                            job.CurrencyPair = new CCurrencyPair() { BaseCurrency = (CurrencyInfo)reader["BaseCurrency"], QuotedCurrency = (CurrencyInfo)reader["QuotedCurrency"] };
                            job.ID = (Int32)reader["JobID"];
                            job.Tag = new CTag((String)reader["Tag"]);
                            jobs.Add(job);
                        }
                    }
                    else
                    {
                        jobs = null;
                    }
                }
            }
            return jobs;
        }

        public void Update(CJob entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
