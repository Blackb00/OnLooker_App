using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using OnLooker.Core;
using OnLooker.Core.Infrastructure;

namespace OnLooker
{
    public class CTagGateway : ITagGateway
    {
        public Int32 Create(CTag tag)
        {
            String sqlExpression = "sp_InsertTag";
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sqlExpression, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter urlParam = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = tag.Value
                };
                command.Parameters.Add(urlParam);
                
                var result = command.ExecuteScalar();

                Console.WriteLine("Id добавленного объекта: {0}", result);
                String value = result.ToString();
                Int32 id = int.Parse(value);
                return id;
            }

        }

        public void Delete(Int32 id)
        {
            throw new NotImplementedException();
        }

        public CTag Get(Int32 id)
        {
            throw new NotImplementedException();
        }

        public List<CTag> GetAll()
        {
            throw new NotImplementedException();

        }

        public List<string> GetAllNames()
        {
            String sqlExpression = "sp_GetTags";
            List<string> tags = new List<string>();

            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sqlExpression, conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        String value = (string) reader["Name"];
                        tags.Add(value);
                    }
                }

                reader.Close();
            }

            return tags;
        }


        public Int32 GetByName(String name)
        {
            String sqlExpression = "sp_GetTagByName";
            List<string> tags = new List<string>();
            Int32 value=0;                                                    //todo: come up with something more clever with value

            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sqlExpression, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = name
                };
                command.Parameters.Add(nameParam);

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        value = (int)reader["ID"];
                    }
                }

                reader.Close();
            }

            return value;
        }

        public void Update(CTag tag)
        {
            throw new NotImplementedException();
        }

    }
}
