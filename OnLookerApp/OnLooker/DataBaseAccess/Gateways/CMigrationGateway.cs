using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using OnLooker.Core.Infrastructure;

namespace OnLooker.DataBaseAccess.Gateways
{
    class CMigrationGateway : IGateway<AVersion>
    {
        public Int32 Create(AVersion entity)
        {
            Int32 id;
            String insertVersion = "sp_InsertVersion";
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                conn.Open();
                
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
                    return id;
                }
                catch (Exception e)
                {
                    SLogger.Log.Fatal($"CMigrationGateway.Create method. Exception: {e.Message}");
                    return -1;
                }
            }
        }

        public void Delete(Int32 id)
        {
            throw new NotImplementedException();
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
    }
}
