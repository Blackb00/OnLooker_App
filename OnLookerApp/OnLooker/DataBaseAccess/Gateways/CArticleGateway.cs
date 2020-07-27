using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using OnLooker.Core;
using OnLooker.Core.Infrastructure;

namespace OnLooker
{
    public class CArticleGateway: IGateway<ArticleInfo>
    {
        private Int32 id;
        public Int32 Create(ArticleInfo article)
        {
            String sqlExpression = "sp_InsertArticle";
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                conn.Open();
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
                SqlParameter countrieParam = new SqlParameter
                {
                    ParameterName = "@countryid",
                    Value = article.Country.ID
                };
                command.Parameters.Add(countrieParam);
                try
                {
                    var result = command.ExecuteScalar();
                    Console.WriteLine("Id добавленного объекта: {0}", result);
                    var value = result.ToString();
                    Int32.TryParse(value, out id);
                    return id;
                }
                catch (Exception e)
                {
                    SLogger.Log.Fatal($"CArticleGateway.Create method. Exception: {e.Message}");
                    return -1;
                }
            }

        }

        public void Delete(Int32 id)
        {
            throw new NotImplementedException();
        }

        public ArticleInfo Get(Int32 id)
        {
            throw new NotImplementedException();
        }

        public List<ArticleInfo> GetAll()
        {
            String sqlExpression = "sp_GetArticles";
            CCountryGateway countryGateway = new CCountryGateway();
            List<ArticleInfo> articles = new List<ArticleInfo>();
            CTag articleTag = new CTag();
            using (SqlConnection conn = CDbConnection.GetConnection())
            {
                conn.Open();

                SqlCommand command = new SqlCommand(sqlExpression, conn);
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            articleTag.Value = (String) reader["Tag"];
                            Int32 countryId = (Int32) reader["CountryID"];
                            ArticleInfo article = new ArticleInfo();
                            article.ID = (Int32) reader["ID"];
                            article.Url = (String) reader["URL"];
                            article.Title = (String) reader["Title"];
                            article.Content = (String) reader["Content"];
                            article.Html = (byte[]) reader["HTML"];
                            article.Date = (DateTime)reader[6];                 //todo: fix the problem with Date  -> rename column?
                            article.Country = countryGateway.Get(countryId);
                            article.Tag = articleTag;

                            articles.Add(article);
                        }
                    }
                }
            }

            return articles;
        }

        public void Update(ArticleInfo entity)
        {
            throw new NotImplementedException();
        }

    }

    
}
