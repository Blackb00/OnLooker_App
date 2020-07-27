using System;

namespace OnLooker.Core
{
    public struct SCurrencyPairTimePrint
    {
        public int ID;
        public ECurrencyType Pair;
        public decimal Rate;
        public DateTime Time;

        //public SCurrencyPairTimePrint(int id, ECurrencyType pair, decimal rate, DateTime time)
        //{
        //    this.ID = id;
        //    this.Pair = pair;
        //    this.Rate = rate;
        //    this.Time = time;

        //}

    }
}
