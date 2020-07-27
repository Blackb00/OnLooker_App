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
    public class CUserLoginGateway : IGateway<UserAuthInfo>
    {
        public int Create(UserAuthInfo entity)
        {
            Int32 id;
            var insertCredentials = "sp_InsertUserLogin";
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(insertCredentials, conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter loginParam = new SqlParameter
                {
                    ParameterName = "@login",
                    Value = entity.Login
                };
                command.Parameters.Add(loginParam);
                SqlParameter passParam = new SqlParameter
                {
                    ParameterName = "@password",
                    Value = entity.Password
                };
                command.Parameters.Add(passParam);
                try
                {
                    var result = command.ExecuteScalar();
                    Console.WriteLine("Id добавленного объекта: {0}", result);
                    Int32.TryParse(result.ToString(), out id);
                }
                catch (Exception e)
                {
                    //SLogger.Log.Fatal($"CCountryGateway.Create method. Exception: {e.Message}");
                    throw new Exception("Unhendeled exception" + e.Message);
                }

                return id;
            }
        }

        public void Delete(Int32 id)
        {
            CUserInfoGateway userInfoGateway = new CUserInfoGateway();
            var userId = userInfoGateway.GetByLoginId(id);
            using (SqlConnection connection = CDbConnection.GetConnection())
            {
                SqlTransaction transaction = connection.BeginTransaction();

                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;

                try
                {
                    // выполняем две отдельные команды
                    command.CommandText = "sp_DeleteLogin";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@id",
                        Value = id
                    };
                    command.Parameters.Add(idParam);

                    var result = command.ExecuteNonQuery();


                    command.CommandText = "sp_DeleteUser";
                    command.Parameters.Clear();
                    SqlParameter idParameter = new SqlParameter
                    {
                        ParameterName = "@id",
                        Value = userId
                    };
                    command.Parameters.Add(idParameter);
                    var result2 = command.ExecuteNonQuery();

                    transaction.Commit();
                    Console.WriteLine("Количество удалённых из UserLogin объектов: {0}", result);
                    Console.WriteLine("Количество удалённых из UserLogin объектов: {0}", result2);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
            }
        }

        public UserAuthInfo Get(Int32 id)
        {
            throw new NotImplementedException();
        }

        public List<UserAuthInfo> GetAll()
        {
            throw new NotImplementedException();
        }

        public Int32 GetByUserId(Int32 id)
        {
            Int32 loginId = 0;
            var insertCredentials = "sp_GetUserLoginByUserInfoId";
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
                            loginId = (Int32) reader["LoginID"];
                        }
                    }
                }
                catch (Exception e)
                {
                    //SLogger.Log.Fatal($"CCountryGateway.Create method. Exception: {e.Message}");
                    throw new Exception("Unhendeled exception" + e.Message);
                }

                return loginId;
            }
        }

        public void Update(UserAuthInfo entity)
        {
            throw new NotImplementedException();
        }
    }
}
