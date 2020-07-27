using System;

namespace OnLooker.Core
{
    public interface IDataOutput
    {
        ArticleInfo[] Articles { get; set; }
        CCurrencyPairTimePrint[] CurrencyTimePrints { get; set; }
        CountryInfo[] Countries { get;}
        CurrencyInfo[] Currencies { get;}
        Boolean IsReadyToShowReport { get; set; }

    }
}
