using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLooker.Core.Infrastructure
{
    public interface IDataPort
    {
        void GetArticles(String tag);
        Int32 DataTransfer(ECurrencyType currencyPair, String queryText, CountryInfo country);

    }
}
