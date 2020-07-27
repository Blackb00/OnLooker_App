using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using OnLooker.Core;
using OnLooker.Core.Interfaces;

namespace OnLooker
{
    public class CArticleGateway: IGateway<ArticleInfo>
    {
        public int Create(ArticleInfo article)
        {
            string sqlExpression = "sp_InsertArticle";
            using (SqlConnection conn = DBConnection.Instance.GetConnection())
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sqlExpression, conn); 
                //int number = command.ExecuteNonQuery();
                command.CommandType = System.Data.CommandType.StoredProcedure;

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

                var result = command.ExecuteScalar();
                
                Console.WriteLine("Id добавленного объекта: {0}", result);
                string value = result.ToString();
                int id = int.Parse(value);
                return id;
            }

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ArticleInfo Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<ArticleInfo> GetAll()
        {
            string sqlExpression = "sp_GetArticles";
            CCountryGateway countryGateway = new CCountryGateway();
            List<ArticleInfo> articles = new List<ArticleInfo>();
            STag articleTag = new STag();
            using (SqlConnection conn = DBConnection.Instance.GetConnection())
            {
                conn.Open();

                SqlCommand command = new SqlCommand(sqlExpression, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            articleTag.Value = (string) reader["Tag"];
                            int countryId = (int) reader["CountryID"];
                            ArticleInfo article = new ArticleInfo();
                            article.ID = (int) reader["ID"];
                            article.Url = (string) reader["URL"];
                            article.Title = (string) reader["Title"];
                            article.Content = (string) reader["Content"];
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
