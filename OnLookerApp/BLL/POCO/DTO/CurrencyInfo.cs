using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLooker.Core
{
    [Serializable]
    public class CurrencyInfo
    {
        public Int32 ID { get; set; }
        public String Name { get; set; }
        public String Code { get; set; }
        public Decimal Bid { get; set; }
        public Decimal Ask { get; set; }
        public String FullName
        {
            get => $"{Code} {Name}";
        }
    }
}
