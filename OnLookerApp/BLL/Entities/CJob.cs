using System;

namespace OnLooker.Core
{
    public class CJob
    {
        public int ID { get; set; }
       // public int UserID { get; private set; }
        public ECurrencyType CurrencyPair { get; private set; }
        public STag Tag { get; private set; }
        public CReport Report { get; private set; }
       // public List<CReport> Reports { get; set; }                next sprint task
        public DateTime LastUpdate { get; set; }

        public CJob(QueryInfo query)
        {
            this.CurrencyPair = query.CurrencyPair;
            this.Tag = query.tag;
            this.Report = this.CreateReport(query);
        }

        private CReport CreateReport(QueryInfo query)
        {
            CReport report = new CReport();

            return report;
        }
        //public void UpdateReports()                                  next sprint task
        //{

        //}
    }
}
