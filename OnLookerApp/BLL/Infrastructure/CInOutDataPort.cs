using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnLooker.Core.Services;

namespace OnLooker.Core.Infrastructure
{
    public class CInOutDataPort : IDataPort
    {
        CUserQueryInteractor _userQueryInteractor;
        QueryInfo userQuery = new QueryInfo();
        

        public Int32 DataTransfer(ECurrencyType currencyPair, String queryText, CountryInfo country)
        {
            
            userQuery.CurrencyPair = currencyPair;
            _userQueryInteractor = new CUserQueryInteractor(userQuery);
            Int32 result = 0;
            return result;
        }

        public void GetArticles(String tag)
        {
            throw new NotImplementedException();
        }
    }
}
