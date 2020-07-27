using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using SearchingNews.WebService.Models;

namespace SearchingNews.WebService.Controller
{
    [RoutePrefix("api/news")]
    public class NewsController: ApiController
    {
        [Route("search")]
        [HttpGet]
        public HttpResponseMessage SearchNews(string tag, string country)
        {
            HttpResponseMessage response = Request.CreateResponse();
            List<CArticle>articleList = new List<CArticle>();
            var article = new CArticle
            {
                Content = "Test content",
                Country = new CCountry {Name = country},
                Date = DateTime.Now,
                Html = new byte[10],
                Tags = new CTag[] {new CTag {Value = tag}},
                Url = "Http://testurl.com"
            };
            var count = 0;
            for (var i = 0; i < 50; i++)                                       
            {
                String q = (count++).ToString();                    // это всё равно не помогло =)  отложенная операция - почему?
                article.Title = $"Test Title {q}";
                articleList.Add(article);
            }

            CArticle[] articles = articleList.ToArray();
            try
            {
                response.Headers.AcceptRanges.Add("articles");
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new ObjectContent(typeof(CArticle[]),articles, new XmlMediaTypeFormatter());
               // response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("");
               // response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
               

            }
            catch(Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message, new MediaTypeHeaderValue("application/xml"));
            }

            return response;
        }

        [Route("searchasync")]
        [HttpGet]
        public async Task<HttpResponseMessage> SarchNewsAsync(string tag, string country)
        {
            return await new TaskFactory().StartNew(() => SearchNews(tag, country));
        }
    }
}
