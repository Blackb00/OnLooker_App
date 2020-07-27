using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Xml.Linq;
using System.Xml.Serialization;
using OnLooker.Core;

namespace OnLooker.DataBaseAccess
{
    public class CArticleGateway: IGateway<ArticleInfo>
    {
        public Int32 Create(ArticleInfo article)
        {
            Int32 articleId;
            String sqlExpression = "sp_InsertArticle";
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(sqlExpression, conn);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter urlParam = new SqlParameter
                {
                    ParameterName = "@url",
                    Value = article.Url
                };
                command.Parameters.Add(urlParam);
                SqlParameter titleParam = new SqlParameter
                {
                    ParameterName = "@title",
                    Value = article.Title
                };
                command.Parameters.Add(titleParam);
                SqlParameter contentParam = new SqlParameter
                {
                    ParameterName = "@content",
                    Value = article.Content
                };
                command.Parameters.Add(contentParam);
                SqlParameter htmlParam = new SqlParameter
                {
                    ParameterName = "@html",
                    Value = article.Html
                };
                command.Parameters.Add(htmlParam);
                SqlParameter dateParam = new SqlParameter
                {
                    ParameterName = "@date",
                    Value = article.Date
                };
                command.Parameters.Add(dateParam);
                SqlParameter countryParam = new SqlParameter
                {
                    ParameterName = "@countryid",
                    Value = article.Country.ID
                };
                command.Parameters.Add(countryParam);

                try
                {
                    var result = command.ExecuteScalar();
                    Console.WriteLine("Id добавленного в Article объекта: {0}", result);
                    if (Int32.TryParse(result.ToString(), out articleId))
                    {
                        
                            foreach (var tag in article.Tags)
                            {
                                PutRelation(conn, articleId, tag.ID);
                            }
                        
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Unexpected termination" + e.Message);
                    //SLogger.Log.Fatal($"CArticleGateway.Create method. Exception: {e.Message}")
                }
                return articleId;
            }
        }
        public ArticleInfo Get(Int32 id)
        {
            ArticleInfo article = new ArticleInfo();
            String sqlExpression = "sp_GetArticleById";
            CCountryGateway countryGateway = new CCountryGateway();
            CTagGateway tagGateway = new CTagGateway();
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(sqlExpression, conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@articleId",
                    Value = id
                };
                command.Parameters.Add(idParam);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Int32 countryId = (Int32)reader["CountryID"];
                            article.ID = (Int32)reader["ID"];
                            article.Url = (String)reader["URL"];
                            article.Title = (String)reader["Title"];
                            article.Content = (String)reader["Content"];
                            article.Html = (byte[])reader["HTML"];
                            article.Date = (DateTime)reader[5];
                            article.Country = countryGateway.Get(countryId);
                            article.Tags = tagGateway.GetAllRelatedToArticle(article.ID).Select(x => tagGateway.Get(x)).ToArray();

                        }
                    }
                }
            }

            return article;
        }
        public List<ArticleInfo> GetAll()
        {
            String APP_PATH = "http://localhost/WebApiTest/";
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(APP_PATH + "/api/Account/Register").Result;
                Console.WriteLine(response.StatusCode);
                XElement incomingXml = XElement.Parse(response.Content.ToString());

                var xml = @"<?xml version=""1.0"" encoding=""UTF-8""?> ...";

                var reader = new StringReader(xml);
                List<ArticleInfo> articles = (List<ArticleInfo>)new XmlSerializer(typeof(ArticleInfo)).Deserialize(reader);
                return articles;
            }
            //String sqlExpression = "sp_GetArticles";
            //CCountryGateway countryGateway = new CCountryGateway();
            //List<ArticleInfo> articles = new List<ArticleInfo>();
            //CTagGateway tagGateway = new CTagGateway();
            //using (SqlConnection conn = CDbConnection.GetConnection())
            //{
            //    SqlCommand command = new SqlCommand(sqlExpression, conn);
            //    command.CommandType = CommandType.StoredProcedure;

            //    using (var reader = command.ExecuteReader())
            //    {
            //        if (reader.HasRows)
            //        {
            //            while (reader.Read())
            //            {
            //                Int32 countryId = (Int32)reader["CountryID"];
            //                ArticleInfo article = new ArticleInfo();
            //                article.ID = (Int32)reader["ID"];
            //                article.Url = (String)reader["URL"];
            //                article.Title = (String)reader["Title"];
            //                article.Content = (String)reader["Content"];
            //                article.Html = (byte[])reader["HTML"];
            //                article.Date = (DateTime)reader[6];
            //                article.Country = countryGateway.Get(countryId);
            //                article.Tags = tagGateway.GetAllRelatedToArticle(article.ID).Select(x => tagGateway.Get(x)).ToArray();

            //                articles.Add(article);
            //            }
            //        }
            //    }
            //}

            
        }

        public List<ArticleInfo> GetByTag(CTag tag)
        {
            List<ArticleInfo> articles = new List<ArticleInfo>();
            CCountryGateway countryGateway = new CCountryGateway();
            String sqlExpression = "sp_GetArticleByTag";
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(sqlExpression, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter tagParam = new SqlParameter
                {
                    ParameterName = "@tag",
                    Value = tag.ID
                };
                cmd.Parameters.Add(tagParam);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ArticleInfo article = new ArticleInfo();
                            article.Country = countryGateway.Get((Int32)reader["CountryID"]);
                            article.Date = (DateTime) reader["Date"];
                            article.Content = (String) reader["Content"];
                            article.Html = (byte[]) reader["Html"];
                            article.Url = (String) reader["Url"];
                            article.Title = (String) reader["Title"];
                            article.ID = (Int32) reader["ID"];
                            articles.Add(article);
                        }
                    }
                }
            }

            return articles;
        }
        public void Update(ArticleInfo article)
        {
            String sqlExpression = "sp_UpdateArticle";
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(sqlExpression, conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = article.ID
                };
                command.Parameters.Add(idParam);
                SqlParameter urlParam = new SqlParameter
                {
                    ParameterName = "@url",
                    Value = article.Url
                };
                command.Parameters.Add(urlParam);
                SqlParameter titleParam = new SqlParameter
                {
                    ParameterName = "@title",
                    Value = article.Title
                };
                command.Parameters.Add(titleParam);
                SqlParameter contentParam = new SqlParameter
                {
                    ParameterName = "@content",
                    Value = article.Content
                };
                command.Parameters.Add(contentParam);
                SqlParameter htmlParam = new SqlParameter
                {
                    ParameterName = "@html",
                    Value = article.Html
                };
                command.Parameters.Add(htmlParam);
                SqlParameter dateParam = new SqlParameter
                {
                    ParameterName = "@date",
                    Value = article.Date
                };
                command.Parameters.Add(dateParam);
                SqlParameter countryParam = new SqlParameter
                {
                    ParameterName = "@countryid",
                    Value = article.Country.ID
                };
                command.Parameters.Add(countryParam);
                var result = command.ExecuteNonQuery();
                Console.WriteLine("Количество обновлённых объектов: {0}", result);
            }
        }
        public void Delete(Int32 id)
        {
            String sqlExpression = "sp_DeleteArticle";
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(sqlExpression, conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@articleId",
                    Value = id
                };
                command.Parameters.Add(idParam);
                var result = command.ExecuteNonQuery();
                Console.WriteLine("Id удалённого из Article объекта: {0}", result);
            }
        }

        private Int32 PutRelation(SqlConnection conn, Int32 articleId, Int32 tagId)
        {
            Int32 relationId;
            String sqlExpression = "sp_InsertArticleTag";
            using (SqlCommand cmd = new SqlCommand(sqlExpression, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter tagParam = new SqlParameter
                {
                    ParameterName = "@tagId",
                    Value = tagId
                };
                cmd.Parameters.Add(tagParam);
                SqlParameter articleParam = new SqlParameter
                {
                    ParameterName = "@articleId",
                    Value = articleId
                };
                cmd.Parameters.Add(articleParam);
                var result = cmd.ExecuteScalar();
                Console.WriteLine("Id добавленного ArticleTag объекта: {0}", result);
                Int32.TryParse(result.ToString(), out relationId);
            }

            return relationId;
        }


    }

    
}
