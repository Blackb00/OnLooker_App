using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OnLooker.Core;
using OnLooker.Core.Services;
using OnLooker.DataBaseAccess;

namespace OnLooker
{
    class Program
    {
        private static AppDomain _currentDomain = AppDomain.CurrentDomain;
        static void CurrentDomain_ProcessExit(object sender, EventArgs e)     
        {
            SLogger.Log.Debug($"Application terminated {e}");
            CDbConnection.ConnectionClose();
        }

        static void Main(string[] args)
        {
            _currentDomain.ProcessExit += CurrentDomain_ProcessExit;          
            _currentDomain.UnhandledException += ExceptionsHandler;
            Console.WriteLine(@"Hello! This is OnLooker App!");

            try
            {
                 
                CDbDeploy.ConnectDataBase();
                CDbDeploy.Update();
                var t = new Thread(Go);

                t.Start();
                t.Join();
            }
            catch (Exception e)
            {
                SLogger.Log.Fatal("Unexpected termination: " + e.Message);
                Console.WriteLine($"Error: {e.Message}\nPress any key to close application");
                
            }
            finally
            {
                CDbConnection.ConnectionClose();
            }

            Console.ReadLine();
        }

        private static void ExceptionsHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            SLogger.Log.Fatal("ExceptionsHandler caught : " + e.Message);
        }

        public static void Go()
        {
            Console.WriteLine(@"Welcome to OnLooker!!!!!");
            try
            {
                /*--------------------------todo: implement DI Container ------------------------------------*/
                CCountryGateway countryGw = new CCountryGateway();
                CArticleGateway articleGw = new CArticleGateway();
                CTagGateway tagGateway = new CTagGateway();
                CArticleTagGateway artTagGateway = new CArticleTagGateway();
                /* ---------------------------------------------------------------------------------------*/
                CCountryService countryService = new CCountryService(countryGw);
                CArticleService articleService = new CArticleService(articleGw);
                CTagService tagService = new CTagService(tagGateway);
                
                Int32 tagId;

                /*create queryInfo entity storing all strings of user query */
                QueryInfo queryInfo = new QueryInfo();

                /*-----------------User interaction-------------------------*/

                Console.WriteLine("Введите строку запроса");
                String query = Console.ReadLine();                           //todo: add option with choosing  currency

                /*-----------------And of user interaction-----------------*/


                /*convert from user query to STag*/
                CTag tag = new CTag(query);
                queryInfo.Tag = tag;
                /* Crawling */                                                  //todo: put this logic to standalone class
                String searchUrl = $"https://ria.ru/search/?query=" + query;
                List<string> allUrls = CParser.ParseWebPage(searchUrl, new RegularExpressions().FindQeryUrls);


                foreach (String url in allUrls)
                {
                    ArticleInfo article = new ArticleInfo();
                    article.Tags = new []{tag};
                    article.Title = "Some Title";                           //todo: implement title parsing
                    article.Url = url;
                    article.Html = GetHtml(url);
                    article.Country = countryService.GetAllCountries().Find(x=>x.ID == 1);             

                    /*this function collect the content from webpage */
                    List<string> htmList = CParser.ParseWebPage(url, new RegularExpressions().GetContent);
                    StringBuilder content = new StringBuilder();

                    foreach (var html in htmList)
                    {
                        /*it should delete tags placed in text (like <strong> or <a>) */
                        content.AppendFormat(Regex.Replace(html, new RegularExpressions().GetAllTags, string.Empty)); //todo: fix regex, it shouldn't delete text stored between formatting tags
                    }

                    article.Content = content.ToString();
                    article.Date = DateTime.Now;                              //todo:implement parsing of url, to get the date of article

                    Console.WriteLine(url);

                    /*---------------------------------Interaction with DB--------------------------------------*/

                    /* put article into DB */
                    Int32 articleID = articleService.AddArticle(article);
                    Console.WriteLine(articleID);

                    /*checking if tag already exsists, if not - put new tag into DB*/                           //todo: should remove this operation from foreach-block after creating query validation 
                    List<string> exsistingTags = tagGateway.GetAllNames();

                    if (!exsistingTags.Contains(queryInfo.Tag.Value))
                        tagId = tagGateway.Create(queryInfo.Tag);
                    else 
                        tagId = tagGateway.GetByName(queryInfo.Tag.Value);

                    /* put article-tag relation into DB*/
                    artTagGateway.AddRelation(tagId, articleID);
                }
                Console.WriteLine("End of main procedure");

                /*------------------------------------Data output----------------------------*/
                List<ArticleInfo> articlesInDb = articleService.GetAllArticles();
                Console.SetWindowSize(160, 50);
                Console.WriteLine("|{0,-30}|{1,-5}|{2,-60}|{3,-15}|{4,-15}|{5,-20}|", "Tag", "ID", "Url", "Title", "Country", "Date");
                Console.WriteLine("|{0,30}|{1,5}|{2,60}|{3,15}|{4,15}|{5,20}|", "____ ", "_____", "_____________________", "_____", "_____", "_____");
                foreach (ArticleInfo article in articlesInDb)
                {
                    Console.WriteLine("|{0,-30}|{1,-5}|{2,-60}|{3,-15}|{4,-15}|{5,-20}|", article.Tags[0].Value, article.ID, article.Url, article.Title, article.Country.Name, article.Date);
                }
                //HelperMethodForLinqLab(articlesInDb);
            }
            catch (Exception e)
            {
                SLogger.Log.Fatal("Unexpected termination: " + e.Message);
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        public static byte[] GetHtml(String url)
        {
            using (WebClient client = new WebClient())
            {
                return client.DownloadData(url);
            }
        }
    }
}
