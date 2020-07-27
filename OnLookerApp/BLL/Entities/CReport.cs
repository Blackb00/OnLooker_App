using System;

namespace OnLooker.Core
{
    public class CReport
    {
        public int ID { get; private set; }
        public CJob Job { get; private set; }
        public string Description { get; set; }
        private SCurrencyPairTimePrint[] CurrencyGraph { get; set; }
        public ArticleInfo[] Articles { get; set; }
        public DateTime LastUpdate { get; private set; }

        public void Update()
        {
            this.LastUpdate = DateTime.UtcNow;
            //todo create method for updating report 
        }

    }
}
