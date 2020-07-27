using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using OnLooker.Core;

namespace OnLooker.DataBaseAccess
{
    public class CCurrencyGateway : IGateway<CurrencyInfo>
    {
        public Int32 Create(CurrencyInfo entity)
        {
            Int32 id;
            var insertCurrency = "sp_InsertCurrency";
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(insertCurrency, conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter name = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = entity.Name
                };
                command.Parameters.Add(name);
                SqlParameter code = new SqlParameter
                {
                    ParameterName = "@code",
                    Value = entity.Code
                };
                command.Parameters.Add(code);
                try
                {
                    var result = command.ExecuteScalar();
                    Console.WriteLine("Id добавленного объекта: {0}", result);
                    var value = result.ToString();
                    Int32.TryParse(value, out id);
                }
                catch (Exception e)
                {
                    //SLogger.Log.Fatal($"CCurrencyGateway.Create method. Exception: {e.Message}");
                    throw  new Exception("Unhandeled exception"+e.Message);
                }
                return id;
            }
        }
        public void Delete(Int32 id)
        {
            String sqlExpression = "sp_DeleteCurrency";
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(sqlExpression, conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = id
                };
                command.Parameters.Add(idParam);
                var result = command.ExecuteNonQuery();
                Console.WriteLine("Id удалённого из CurrencyType объекта: {0}", result);
            }
        }

        public CurrencyInfo Get(Int32 id)
        {
            String sqlExpression = "sp_GetCurrencyById";
            CurrencyInfo currency = new CurrencyInfo();

            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(sqlExpression, conn);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter urlParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = id
                };
                command.Parameters.Add(urlParam);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        currency.Code = (String) reader["CurrencyCode"];
                        currency.Name = (String) reader["CurrencyName"];
                        currency.ID = (Int32) reader["ID"];
                    }
                }

                reader.Close();
            }

            return currency;
        }

        public List<CurrencyInfo> GetAll()
        {
            var sqlExpression = "sp_GetCurrencies";
            var currencies = new List<CurrencyInfo>();

            using (var conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(sqlExpression, conn);
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var currencyInfo = new CurrencyInfo();
                            currencyInfo.Name = (String) reader["CurrencyName"];
                            currencyInfo.Code = (String) reader["CurrencyCode"];
                            currencies.Add(currencyInfo);
                        }
                    }
                }
            }

            return currencies;
        }

        public void Update(CurrencyInfo entity)
        {
            throw new NotImplementedException();
        }
    }
}
