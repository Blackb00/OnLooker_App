using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using OnLooker.Core;

namespace OnLooker.DataBaseAccess
{
    public class CTagGateway : ITagGateway
    {
        public Int32 Create(CTag tag)
        {
            String sqlExpression = "sp_InsertTag";
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
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
            String sqlExpression = "sp_DeleteTag";
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
                Console.WriteLine("Id удалённого из Tag объекта: {0}", id);
            }
        }
        public CTag Get(Int32 id)
        {
            String sqlExspression = "sp_GetTagById";
            CTag tag = new CTag();
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(sqlExspression, conn);

                command.CommandType = CommandType.StoredProcedure;
                SqlParameter tagIdParam = new SqlParameter
                {
                    ParameterName = "@tagId",
                    Value = id
                };
                command.Parameters.Add(tagIdParam);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tag.Value = (String)reader["Name"];
                        tag.ID = (Int32) reader["ID"];
                    }
                    reader.Close();
                }
            }

            return tag;
        }
        public Int32[] GetAllRelatedToArticle(Int32 id)
        {
            String sqlExpression = "sp_GetTagsRelatedToArticle";
            List<Int32> tags = new List<Int32>();
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(sqlExpression, conn);

                command.CommandType = CommandType.StoredProcedure;
                SqlParameter articleParam = new SqlParameter
                {
                    ParameterName = "@articleId",
                    Value = id
                };
                command.Parameters.Add(articleParam);

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tags.Add((Int32)reader["TagID"]);
                    }
                }

                reader.Close();
            }

            return tags.ToArray();
        }
        public List<CTag> GetAll()
        {
            String sqlExpression = "sp_GetTags";
            List<CTag> tags = new List<CTag>();

            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(sqlExpression, conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var tag = new CTag();
                        tag.Value = (String) reader["Name"];
                        tag.ID = (Int32)reader["ID"];
                        tags.Add(tag);
                    }
                }
            }

            return tags;
        }
        public List<string> GetAllNames()
        {
            String sqlExpression = "sp_GetTags";
            List<String> tags = new List<String>();

            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(sqlExpression, conn);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        String value = (String)reader["Name"];
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
            Int32 value = 0;                                                    //todo: come up with something more clever with value

            using (SqlConnection conn = CDbConnection.GetConnection())
            {
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
            String sqlExpression = "sp_UpdateTag";
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(sqlExpression, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter urlParam = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = tag.Value
                };
                command.Parameters.Add(urlParam);
                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = tag.ID
                };
                command.Parameters.Add(idParam);

                var result = command.ExecuteScalar();

                Console.WriteLine("Id обновлённого в Tag объекта: {0}", tag.ID);
            }
        }

    }
}
