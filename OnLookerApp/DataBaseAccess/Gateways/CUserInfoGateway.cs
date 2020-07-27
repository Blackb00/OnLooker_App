using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnLooker.Core;

namespace OnLooker.DataBaseAccess
{
    public class CUserInfoGateway : IGateway<UserInfo>
    {
        public Int32 Create(UserInfo entity, UserAuthInfo login )
        {
            Int32 userId=0;
            CUserLoginGateway loginGateway = new CUserLoginGateway();
            using (SqlConnection connection = CDbConnection.GetConnection())
            {
                SqlTransaction transaction = connection.BeginTransaction();

                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    command.CommandText = "sp_InsertUserLogin";
                    SqlParameter loginParam = new SqlParameter
                    {
                        ParameterName = "@login",
                        Value = login.Login
                    };
                    command.Parameters.Add(loginParam);
                    SqlParameter passParam = new SqlParameter
                    {
                        ParameterName = "@password",
                        Value = login.Password
                    };
                    command.Parameters.Add(passParam);
                    var result= command.ExecuteScalar();
                    Int32.TryParse(result.ToString(), out Int32 loginId);
                    command.CommandText = "sp_InsertUser";
                    command.Parameters.Clear();
                    SqlParameter name = new SqlParameter
                    {
                        ParameterName = "@name",
                        Value = entity.Name
                    };
                    command.Parameters.Add(name);
                    SqlParameter emailParam = new SqlParameter
                    {
                        ParameterName = "@email",
                        Value = entity.Email
                    };
                    command.Parameters.Add(emailParam);
                    SqlParameter loginIdParam = new SqlParameter
                    {
                        ParameterName = "@loginId",
                        Value = loginId
                    };
                    command.Parameters.Add(loginIdParam);

                    result = command.ExecuteScalar();
                    Int32.TryParse(result.ToString(), out userId);

                    transaction.Commit();
                    Console.WriteLine($"Id добавленного объекта: {loginId}");
                    Console.WriteLine($"Id добавленного объекта: {userId} ");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }

                return userId;
            }

        }

        public int Create(UserInfo entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            String sqlExpression = "sp_DeleteUser";
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(sqlExpression, conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = id
                };
                command.Parameters.Add(idParam);
                var result = command.ExecuteNonQuery();
                Console.WriteLine("Id удалённого из UserInfo объекта: {0}", result);
            }
        }

        public UserInfo Get(Int32 id)
        {
            UserInfo user = new UserInfo();
            String sqlExpression = "sp_GetUserById";
            
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
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            user.Name = (String) reader["Name"];
                            user.Email = (String) reader["Email"];
                            user.ID = (Int32) reader["ID"];
                        }
                    }
                    else
                        user = null;
                }
            }

            return user;
        }

        public Int32 GetByLoginId(Int32 id)
        {
            Int32 userId = 0;
            var insertCredentials = "sp_GetUserByLoginId";
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(insertCredentials, conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = id
                };
                command.Parameters.Add(idParam);
                try
                {
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            userId = (Int32)reader["UserID"];
                        }
                    }
                }
                catch (Exception e)
                {
                    //SLogger.Log.Fatal($"CCountryGateway.Create method. Exception: {e.Message}");
                    throw new Exception("Unhendeled exception" + e.Message);
                }

                return userId;
            }
        }
        public List<UserInfo> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(UserInfo entity)
        {
            var insertUser = "sp_UpdateUser";

            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(insertUser, conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter name = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = entity.Name
                };
                command.Parameters.Add(name);
                SqlParameter id = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = entity.ID
                };
                command.Parameters.Add(id);
                SqlParameter emailParam = new SqlParameter
                {
                    ParameterName = "@email",
                    Value = entity.Email
                };
                command.Parameters.Add(emailParam);
                try
                {
                    var result = command.ExecuteScalar();
                    Console.WriteLine("Количество обновлённых объектов в UserInfo: {0}", result);
                }
                catch (Exception e)
                {
                    //SLogger.Log.Fatal($"CCountryGateway.Create method. Exception: {e.Message}");
                    throw new Exception("Unhendeled exception" + e.Message);
                }

            }
        }
    }
}
