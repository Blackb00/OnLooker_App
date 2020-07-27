using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLooker.Core.Infrastructure
{
    public interface IDataBaseIO
    {
        IGateway<ArticleInfo> ArticleGateway { get; set; }
        IGateway<CountryInfo> CountryGateway { get; set; }
        IGateway<CurrencyInfo> CurrencyGateway { get; set; }
        IJobGateway JobGateway { get; set; }
        IGateway<CReport> ReportGateway { get; set; }
        ITagGateway TagGateway { get; set; }

    }
}
