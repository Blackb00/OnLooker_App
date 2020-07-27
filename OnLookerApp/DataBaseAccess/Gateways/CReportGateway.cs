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
    public class CReportGateway : IGateway<CReport>
    {
        public int Create(CReport entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Int32 id)
        {
            String sqlExpression = "sp_DeleteReport";
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
                Console.WriteLine("Id удалённого из Report объекта: {0}", id);
            }
        }

        public CReport Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<CReport> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(CReport entity)
        {
            throw new NotImplementedException();
        }
    }
}
