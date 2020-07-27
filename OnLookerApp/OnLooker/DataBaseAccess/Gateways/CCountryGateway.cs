﻿using OnLooker.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using OnLooker.Core.Infrastructure;


namespace OnLooker
{
    public class CCountryGateway : IGateway<CountryInfo>
    {
        public Int32 Create(CountryInfo entity)
        {
            Int32 id;
            var insertCountry = "sp_InsertCountry";
            CountryInfo country = new CountryInfo();
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand command = new SqlCommand(insertCountry, conn);
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
                    return id;
                }
                catch (Exception e)
                {
                    SLogger.Log.Fatal($"CCountryGateway.Create method. Exception: {e.Message}");
                    return -1;
                }
            }
        }

        public void Delete(Int32 id)
        {
            throw new NotImplementedException();
        }

        public List<CountryInfo> GetAll()
        {
            String sqlExpression = "sp_GetAllCountries";
            List<CountryInfo> countryList = new List<CountryInfo>();
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sqlExpression, conn);
                command.CommandType = CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CountryInfo country = new CountryInfo();
                            country.Name = (String) reader["Name"];
                            country.ID = (Int32) reader["ID"];
                            country.Code = (String) reader["Code"];
                            countryList.Add(country);
                        }
                    }
                }
            }

            return countryList;
        }

        public CountryInfo Get(Int32 id)
        {
            String sqlExpression = "sp_GetCountryById";
            CountryInfo country = new CountryInfo();

            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                conn.Open();

                SqlCommand command = new SqlCommand(sqlExpression, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

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
                        country.Name = (string)reader["Name"];
                        country.Code = (string)reader["Code"];
                        country.ID = (int)reader["ID"];
                    }
                }

                reader.Close();
            }

            return country;
        }

        public void Update(CountryInfo entity)
        {
            throw new NotImplementedException();
        }
    }
}
