using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using OnLooker.Core;
using OnLooker.Core.Interfaces;

namespace OnLooker
{
    public class CTagGateway : ITagGateway
    {
        public int Create(STag tag)
        {
            string sqlExpression = "sp_InsertTag";
            using (SqlConnection conn = DBConnection.Instance.GetConnection())
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
                string value = result.ToString();
                int id = int.Parse(value);
                return id;
            }

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public STag Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<STag> GetAll()
        {
            throw new NotImplementedException();

        }

        public List<string> GetAllNames()
        {
            string sqlExpression = "sp_GetTags";
            List<string> tags = new List<string>();

            using (SqlConnection conn = DBConnection.Instance.GetConnection())
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sqlExpression, conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string value = (string) reader["Name"];
                        tags.Add(value);
                    }
                }

                reader.Close();
            }

            return tags;
        }


        public int GetByName(string name)
        {
            string sqlExpression = "sp_GetTagByName";
            List<string> tags = new List<string>();
            int value=0;                                                    //todo: come up with something more clever with value

            using (SqlConnection conn = DBConnection.Instance.GetConnection())
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

        public void Update(STag tag)
        {
            throw new NotImplementedException();
        }

    }
}
