using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using OnLooker.Core;

namespace OnLooker.DataBaseAccess
{
    class CMigrationGateway : IGateway<AVersion>
    {
        public Int32 Create(AVersion entity)
        {
            Int32 id;
            String insertVersion = "sp_InsertVersion";
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(insertVersion, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter major = new SqlParameter
                {
                    ParameterName = "@major",
                    Value = entity.Major
                };
                command.Parameters.Add(major);
                SqlParameter minor = new SqlParameter
                {
                    ParameterName = "@minor",
                    Value = entity.Minor
                };
                command.Parameters.Add(minor);
                SqlParameter filenumber = new SqlParameter
                {
                    ParameterName = "@filenumber",
                    Value = entity.FileNumber
                };
                command.Parameters.Add(filenumber);
                SqlParameter comment = new SqlParameter
                {
                    ParameterName = "@comment",
                    Value = entity.Comment
                };
                command.Parameters.Add(comment);
                SqlParameter date = new SqlParameter
                {
                    ParameterName = "@date",
                    Value = DateTime.Now
                };
                command.Parameters.Add(date);

                try
                {
                    var result = command.ExecuteScalar();
                    Console.WriteLine("Id добавленного объекта: {0}", result);
                    var value = result.ToString();
                    Int32.TryParse(value, out id);
                    id =1;
                }
                catch (Exception e)
                {
                    //SLogger.Log.Fatal($"CMigrationGateway.Create method. Exception: {e.Message}");
                    id= -1;
                    throw new Exception("Unhendeled exception"+e.Message);
                }

                return id;
            }
        }

        public void Delete(Int32 id)
        {
            String sqlExpression = "sp_DeleteVersion";
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
                Console.WriteLine("Id удалённого из MigrationHistory объекта: {0}", id);
            }
        }

        public AVersion Get(Int32 id)
        {
            throw new NotImplementedException();
        }

        public List<AVersion> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(AVersion entity)
        {
            throw new NotImplementedException();
        }

        public CVersion GetLastVersion()
        {
            CVersion lastVersion = null; 
            var sqlExpression = "SELECT * FROM MigrationHistory WHERE DateApplied = (SELECT MAX(DateApplied)  FROM MigrationHistory)";
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                var cmd = new SqlCommand(sqlExpression, conn);
                try
                {
                    cmd.CommandType = CommandType.Text;
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var major = (String) reader["MajorVersion"];
                                var minor = (String) reader["MinorVersion"];
                                var fileNumber = (String) reader["FileNumber"];
                                var comment = (String) reader["Comment"];

                                lastVersion = new CVersion(major, minor, fileNumber, comment);
                            }

                        }
                        else
                            Console.WriteLine(
                                "Migration history is empty or some error occured in SqlServer while reading it");
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Unhendled exception" +e.Message);
                }
            }
            return lastVersion;
        }
    }
}
