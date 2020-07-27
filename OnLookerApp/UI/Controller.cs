using System;
using DataBaseAccess;
using OnLooker.Core;
using OnLooker.Core.Services;
using OnLooker.DataBaseAccess;

namespace OnLooker.UI
{
    public class Controller
    {
        private CDataBaseGates _gates;
        private QueryInfo _dataInput;
        private AParsingExpressions parsingExpressions;
        private IDataInput _queryInteractor;
        private CReport _report;
        public Controller(CCurrencyPair currencyPair, String queryText, CountryInfo country)
        {
            _gates = new CDataBaseGates();
            _dataInput = new QueryInfo();
            _dataInput.CurrencyPair = currencyPair;
            _dataInput.Country = country;
            _dataInput.Tag = new CTag(queryText);
            parsingExpressions = new RegularExpressions();
            _queryInteractor = new CUserQueryInteractor(_gates, parsingExpressions);
            _report = _queryInteractor.PutRequest(_dataInput);
        }
        public ArticleInfo[] GetArticles()
        {
            return _report.Articles;
        }

        public CCurrencyPairTimePrint[] GeTimePrints()
        {
            return _report.CurrencyGraph;
        }
    }
}
