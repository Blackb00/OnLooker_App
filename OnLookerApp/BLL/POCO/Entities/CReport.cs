using System;

namespace OnLooker.Core
{
    public class CReport
    {
        public Int32 ID { get; set; }
        public CJob Job { get; }
        public String Description { get; set; }
        public CCurrencyPairTimePrint[] CurrencyGraph { get; set; }
        public ArticleInfo[] Articles { get; set; }
        public DateTime LastUpdate { get; private set; }

        public CReport()   //CJob job
        {
            //CCurrencyPairTimePrint[] curGraph= new CCurrencyPairTimePrint[0]; //todo: implement method GetCurGraph
            //ArticleInfo[] articles = new ArticleInfo[0];                      // todo: implement method GetArticles
            //Job = job;
            //CurrencyGraph = curGraph;
            //Articles = articles;
        }
        public void Update()
        {
            LastUpdate = DateTime.UtcNow;
            
            //todo: create method for updating report --> update Articles and CurrencyGraph
        }

        //public CCurrencyPairTimePrint[] DrawCurrencyGraph()
        //{
        //    return CurrencyGraph;
        //}
    }
}
