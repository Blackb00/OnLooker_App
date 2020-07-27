using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using OnLooker.DataBaseAccess;
using OnLooker.Core;

namespace OnLooker
{
    public class COutputPort : IDataOutput
    {
        private CCurrencyGateway _currencyGateway = new CCurrencyGateway();
        private CCountryGateway _countryGateway = new CCountryGateway();
        private CArticleGateway _articleGateway = new CArticleGateway();
        public ArticleInfo[] Articles { get; set; }
        public CCurrencyPairTimePrint[] CurrencyTimePrints { get; set; }
        public CountryInfo[] Countries => _countryGateway.GetAll().ToArray();
        public CurrencyInfo[] Currencies => _currencyGateway.GetAll().ToArray();
        public bool IsReadyToShowReport { get; set; }

        public ArticleInfo[] GetArticles(CTag tag)
        {
            return _articleGateway.GetByTag(tag).ToArray();
        }
            
    }
}
