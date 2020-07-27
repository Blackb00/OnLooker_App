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
    class CCurrencyPairTimePrintGateway : IGateway<CCurrencyPairTimePrint>
    {
        public int Create(CCurrencyPairTimePrint entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Int32 id)
        {
            String sqlExpression = "sp_DeleteCurrencyPairTimePrint";
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
                Console.WriteLine("Id удалённого из CurrencyPairTimePrint объекта: {0}", id);
            }
        }

        public CCurrencyPairTimePrint Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<CCurrencyPairTimePrint> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(CCurrencyPairTimePrint entity)
        {
            throw new NotImplementedException();
        }
    }
}
