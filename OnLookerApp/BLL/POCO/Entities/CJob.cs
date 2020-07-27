using System;
using System.Threading;
using OnLooker.Core.Infrastructure;

namespace OnLooker.Core
{
    public class CJob
    {
        
        public Int32 ID { get; set; }
       // public Int32 UserID { get; private set; }
        public CCurrencyPair CurrencyPair { get; set; }
        public CTag Tag { get; set; }
        public CountryInfo Country { get; set; }
        public DateTime LastUpdate { get; set; }
        public CReport Report { get; set; }
        private IDataBaseIO _gates { get; set; }
        private AParsingExpressions _parsingExpressions { get; set; }
        public CJob()
        {

        }
        public CJob(QueryInfo query, AParsingExpressions parsingExpressions, IDataBaseIO gates)
        {
            CurrencyPair = query.CurrencyPair;
            Tag = query.Tag;
            Country = query.Country;
            _gates = gates;
            _parsingExpressions = parsingExpressions;
        }

        public CReport CreateReport(QueryInfo query)
        {
            query.Tag.ID = _gates.TagGateway.Create(query.Tag);
             
            CReport report = new CReport();
            SearchingMachine searchingArticles  = new SearchingMachine(query, _gates);
            report.Articles = searchingArticles.GetArticles();
            report.CurrencyGraph = searchingArticles.GetCurrencyGraph();
            return report;
        }
        public void UpdateReports()                                  
        {

        }
}
}
