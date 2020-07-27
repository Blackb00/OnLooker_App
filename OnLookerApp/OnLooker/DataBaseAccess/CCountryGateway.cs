using OnLooker.Core;
using OnLooker.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace OnLooker
{
    public class CCountryGateway : IGateway<CCountry>
    {
        public int Create(CCountry entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<CCountry> GetAll()
        {
            throw new NotImplementedException();
        }

        public CCountry Get(int id)
        {
            string sqlExpression = "sp_GetCountryById";
            CCountry country = new CCountry();

            using (SqlConnection conn = DBConnection.Instance.GetConnection())
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

                //var result = command.ExecuteScalar();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        country.Name = (string) reader["Name"];
                        country.Code = (string) reader["Code"];
                        country.ID = (int) reader["ID"];
                    }
                }

                reader.Close();
            }

            return country;
        }

        public void Update(CCountry entity)
        {
            throw new NotImplementedException();
        }
    }
}
