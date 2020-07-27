using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using OnLooker.Core;
using OnLooker.Core.Infrastructure;
using OnLooker.Core.Services;

namespace OnLooker.Core
{
    public class SearchingMachine
    {
        private QueryInfo _query;
        private static IDataBaseIO _dataBaseGates;   
        private List<string> _allUrls;


        public SearchingMachine(QueryInfo query, IDataBaseIO gates)
        {
            _query = query;
            _dataBaseGates = gates;
        }

        public CCurrencyPairTimePrint[] GetCurrencyGraph()
        {
            var a = new CCurrencyPairTimePrint[522];
            //String uri = "https://currate.ru/api/";
            //String apiKey = "c604adcb82fa205658af7fbe299b4c38";
            //String resultUri = $"{uri}?get=rates&pairs={_query.CurrencyPair.BaseCurrency.Code}{_query.CurrencyPair.QuotedCurrency.Code}&key={apiKey}";
            //Object result = GetHtml(resultUri);
            var r = new Random();
            DateTime start = new DateTime(2000,01,01);
            for (var i = 0; i < 522; i++)
            {
                Double q = r.Next(10000, 200000)*0.00001;
                var b = new CCurrencyPairTimePrint(_query.CurrencyPair,q, start);
                a[i] = b;
                start = start.AddDays(7);
            }
            
            return a;
        }


        public ArticleInfo[] GetArticles()
        {
            try
            {
                String APP_PATH = "http://localhost/SearchingNewsWebApiHost2/";
                using (var client = new HttpClient())
                {
                    var response = client
                        .GetAsync(APP_PATH + $"/api/news/search?tag={_query.Tag.Value}&country={_query.Country.Name}")
                        .Result;
                    Console.WriteLine(response.StatusCode);
                    var result = response.Content.ReadAsStringAsync();
                    var strResult = result.Result;

                    var articles = CJsonService.Deserialize<List<ArticleInfo>>(strResult);
                    foreach (var article in articles)
                    {
                      
                            var dateString = Regex.Replace(article.Url, @"[^\d]+", "").ToCharArray();
                            var yearStr = new Char[4];
                            var monthStr = new Char[2];
                            var dayStr = new Char[2];
                            for (var i=0; i<dateString.Length; i++)
                            {
                                if (i < 4)
                                    yearStr[i] = dateString[i];
                                else if(i < 6)
                                    monthStr[i - 4] = dateString[i];
                                else if (i < 8)
                                    dayStr[i - 6] = dateString[i];
                            }

                             Boolean yearBool= Int32.TryParse(new String(yearStr), out Int32 year);
                             Boolean monthBool = Int32.TryParse(new String(monthStr), out Int32 month);
                             Boolean dayBool = Int32.TryParse(new String(dayStr), out Int32 day);
                             var date = new DateTime(year, month, day);
                             article.Date = date;
                    }
                    return articles.ToArray();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


    }
}
