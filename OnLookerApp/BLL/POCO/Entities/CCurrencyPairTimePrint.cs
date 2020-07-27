using System;

namespace OnLooker.Core
{
    public class CCurrencyPairTimePrint
    {
        public Int32 ID { get; set; }
        public CCurrencyPair Pair { get; set; }
        public Double Rate { get; set; }
        public DateTime Time { get; set; }

        public CCurrencyPairTimePrint(CCurrencyPair pair, Double rate, DateTime time)
        {
            Pair = pair;
            Rate = rate;
            Time = time;
        }
    }
}
