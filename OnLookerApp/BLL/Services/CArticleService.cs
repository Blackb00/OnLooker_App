using System;
using System.Collections.Generic;


namespace OnLooker.Core
{
    public class CArticleService
    {
        private readonly IGateway<ArticleInfo> dbService;
        public CArticleService(IGateway<ArticleInfo> gateway)
        {
            this.dbService = gateway;
        }
        
        public Int32 AddArticle(ArticleInfo entity)
        {
            return dbService.Create(entity);
        }
        public void DeleteArticle(Int32 id)
        {
            dbService.Delete(id);
        }
        public ArticleInfo GetArticleById(Int32 id)
        {
            return dbService.Get(id);
        }
        public List<ArticleInfo> GetAllArticles()
        {
            return dbService.GetAll();
        }
        public void UpdateArticle(ArticleInfo entity)
        {
            dbService.Update(entity);
        }
    }
}
