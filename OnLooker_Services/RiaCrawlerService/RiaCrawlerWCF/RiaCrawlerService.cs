using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace RiaCrawlerWCF
{
    public class RiaCrawlerService : IRiaCrawlerService
    {

        private CParsingKeys _keys = new CParsingKeys();
        public CArticle[] GetLocalNews(string tag, string country)
        {
            throw new NotImplementedException();
        }

        public CArticle[] GetWorldNews(string tag)
        {
            String _searchUrl = $"https://ria.ru/search/?query=" + tag;
            var allUrls = CParser.ParseWebPage(_searchUrl, _keys.FindQeryUrls);
            List<CArticle> articles = new List<CArticle>();
            foreach (String url in allUrls.Take(10))
            {
                CArticle article = new CArticle();
                article.Tags = new[] { new CTag { Value = tag } };
                article.Title = CParser.ParseWebPage(url, _keys.GetTitle).FirstOrDefault();
                article.Url = url;
                //article.Html = GetHtml(url);
                article.Country = new CCountry { Name = "International", Code = "None" };

                /*this function collect the content from webpage */
                List<string> htmList = CParser.ParseWebPage(url, _keys.GetContent);
                StringBuilder content = new StringBuilder();

                foreach (var html in htmList)
                {
                    /*it should delete tags placed in text (like <strong> or <a>) */
                    content.AppendFormat(Regex.Replace(html, _keys.GetAllTags,
                        string.Empty)); //todo: fix regex, it shouldn't delete text stored between formatting tags
                }
                article.Content = content.ToString();
                
                articles.Add(article);
            }
           

            return articles.ToArray();
        }
        public byte[] GetHtml(String url)
        {
            using (WebClient client = new WebClient())
            {
                return client.DownloadData(url);
            }
        }

        public Object GetData(String url)
        {
            using (WebClient client = new WebClient())
            {
                return client.OpenRead(url);
            }
        }
    }
}
