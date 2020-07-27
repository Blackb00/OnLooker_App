using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace OnLooker.Core
{
    public class CCurrencyPair
    {
        public Int32 ID {get;set; }
        private Decimal _rate;
        public CurrencyInfo BaseCurrency { get; set; }
        private String _pairName;
        public CurrencyInfo QuotedCurrency { get; set; }

        public String PairName
        {
            get => _pairName;
            set => _pairName = $"{BaseCurrency.Code}{QuotedCurrency.Code}";
        }

        public Decimal Rate
        {
            get => _rate;
            set => GetRate();
        }
        public void GetRate()
        {
            _rate = BaseCurrency.Bid / QuotedCurrency.Ask;
        }
    }
}
