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
    public class CCurrencyPairGateway : IGateway<CCurrencyPair>
    {
        public int Create(CCurrencyPair entity)
        {
            Int32 id;
            var sqlExspression = "sp_InsertCurrencyPair";
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(sqlExspression, conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter baseCur = new SqlParameter
                {
                    ParameterName = "@base",
                    Value = entity.BaseCurrency.ID
                };
                command.Parameters.Add(baseCur);
                SqlParameter quotedCur = new SqlParameter
                {
                    ParameterName = "@quoted",
                    Value = entity.QuotedCurrency.ID
                };
                command.Parameters.Add(quotedCur);
                try
                {
                    var result = command.ExecuteScalar();
                    Console.WriteLine("Id добавленного объекта: {0}", result);
                    Int32.TryParse(result.ToString(), out id);
                }
                catch (Exception e)
                {
                    throw new Exception("Unhandeled exception" + e.Message);
                }

                return id;
            }
        }

        public void Delete(Int32 id)
        {
            String sqlExpression = "sp_DeleteCurrencyPair";
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
                Console.WriteLine("Id удалённого из CurrencyPair объекта: {0}", id);
            }
        }

        public CCurrencyPair Get(Int32 id)
        {
            String sqlExpression = "sp_GetCurrencyPairById";
            CCurrencyPair pair = new CCurrencyPair();
            CCurrencyGateway curInfoGateway = new CCurrencyGateway();

            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(sqlExpression, conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

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
                            pair.BaseCurrency = curInfoGateway.Get((Int32) reader["BaseCurrencyID"]);
                            pair.QuotedCurrency = curInfoGateway.Get((Int32) reader["QuotedCurrencyID"]);
                            pair.ID = (Int32) reader["ID"];
                        }
                    }
                    else
                    {
                        pair = null;
                    }

                    reader.Close();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message + e.StackTrace);
                }
            }
            return pair;
        }

        public List<CCurrencyPair> GetAll()
        {
            String sqlExpression = "sp_GetAllCurrencyPairs";
            List<CCurrencyPair> pairs = new List<CCurrencyPair>();
            CCurrencyGateway curInfoGateway = new CCurrencyGateway();

            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(sqlExpression, conn);
                command.CommandType = CommandType.StoredProcedure;

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CCurrencyPair pair = new CCurrencyPair();
                        pair.BaseCurrency = curInfoGateway.Get((Int32)reader["BaseCurrencyID"]);
                        pair.QuotedCurrency = curInfoGateway.Get((Int32)reader["QuotedCurrencyID"]);
                        pair.ID = (Int32)reader["ID"];
                        pairs.Add(pair);
                    }
                }

                reader.Close();
            }

            return pairs;
        }

        public void Update(CCurrencyPair entity)
        {
            var sqlExspression = "sp_UpdateCurrencyPair";
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(sqlExspression, conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = entity.ID
                };
                command.Parameters.Add(idParam);
                SqlParameter baseCur = new SqlParameter
                {
                    ParameterName = "@base",
                    Value = entity.BaseCurrency.ID
                };
                command.Parameters.Add(baseCur);
                SqlParameter quotedCur = new SqlParameter
                {
                    ParameterName = "@quoted",
                    Value = entity.QuotedCurrency.ID
                };
                command.Parameters.Add(quotedCur);
                try
                {
                    var result = command.ExecuteNonQuery();
                    Console.WriteLine("Id обновлённого объекта: {0}", entity.ID);
                }
                catch (Exception e)
                {
                    throw new Exception("Unhandeled exception" + e.Message);
                }
            }
        }
    }
}
