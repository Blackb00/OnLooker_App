using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLooker.Core
{
    public interface IJobGateway:IGateway<CJob>
    {
        CJob GetByQueryInfo(QueryInfo info);
    }
}
