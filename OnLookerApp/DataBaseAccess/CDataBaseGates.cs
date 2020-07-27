using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnLooker.Core;
using OnLooker.Core.Infrastructure;
using OnLooker.DataBaseAccess;

namespace DataBaseAccess
{
    public class CDataBaseGates: IDataBaseIO
    {
        public IGateway<ArticleInfo> ArticleGateway { get; set; }
        public IGateway<CountryInfo> CountryGateway { get; set; }
        public IGateway<CurrencyInfo> CurrencyGateway { get; set; }
        public IJobGateway JobGateway { get; set; }
        public IGateway<CReport> ReportGateway { get; set; }
        public ITagGateway TagGateway { get; set; }

        public CDataBaseGates()
        {
            ArticleGateway = new CArticleGateway();
            CountryGateway = new CCountryGateway();
            CurrencyGateway = new CCurrencyGateway();
            JobGateway = new CJobGateway();
            ReportGateway = new CReportGateway();
            TagGateway = new CTagGateway();
        }
    }
}


