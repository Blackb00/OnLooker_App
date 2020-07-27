using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnLooker.Core;
using OnLooker.DataBaseAccess;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Tests
{
    [TestClass]
    public class CRUDArticleTests
    {
        CArticleGateway _articleGateway = new CArticleGateway();
        CTagGateway _tagGateway = new CTagGateway();
        CArticleTagGateway _articleTagGateway = new CArticleTagGateway();
        private CTag _tag;
        private CTag _tag2;
        private ArticleInfo _article;
        private ArticleInfo _articleChanged;

        [TestInitialize]
        public void Initialize()
        {
            CDbDeploy.ConnectDataBase();
            _tag = new CTag()
            {
                Value = "Test tag"
            };
            _tag2 = new CTag()
            {
                Value = "Test2 tag"
            };
            _tag.ID = _tagGateway.Create(_tag);
            _tag2.ID = _tagGateway.Create(_tag2);

            _article = new ArticleInfo()
            {
                Content = "test string",
                Country = new CountryInfo() { Code = "ALA", Name = "AALAND ISLANDS ", ID = 2 },
                Date = DateTime.Now,
                Html = new byte[0],
                Tags = new[] { _tag, _tag2 },
                Title = "Test Title",
                Url = "http://testurl"
            };
            _articleChanged = new ArticleInfo()
            {
                Content = _article.Content,
                Country = _article.Country,
                Date = _article.Date,
                Html = _article.Html,
                Tags = _article.Tags,
                Title = "New Test Title",
                Url = _article.Url
            };
        }
        [TestMethod]
        public void ShouldCreateArticle()
        {
            //Act
            _article.ID = _articleGateway.Create(_article);
            var articleFromDb = _articleGateway.Get(_article.ID);

            //Assert

            IsTrue(articleFromDb.Country.ID == _article.Country.ID
                   && articleFromDb.Tags[0].ID == _article.Tags[0].ID
                   && articleFromDb.Tags[1].ID == _article.Tags[1].ID
                   && articleFromDb.Title == _article.Title
                   && articleFromDb.Url == _article.Url
                   && articleFromDb.Content == _article.Content);
        }

        [TestCleanup]
        public void DeleteArticle()
        { 
            _articleGateway.Delete(_article.ID);
        }
        [TestMethod]
        public void ShouldReadArticle()
        {
            //Arrange
            _article.ID = _articleGateway.Create(_article);
            //Act
            var articleFromDb = _articleGateway.Get(_article.ID);
            
            //Assert
            IsTrue(articleFromDb.Country.ID == _article.Country.ID
                   && articleFromDb.Tags[0].ID == _article.Tags[0].ID
                   && articleFromDb.Tags[1].ID == _article.Tags[1].ID
                   && articleFromDb.Title == _article.Title
                   && articleFromDb.Url == _article.Url
                   && articleFromDb.Content == _article.Content);
        }

        [TestMethod]
        public void ShouldUpdateArticle()
        {
            //Arrange
            _article.ID = _articleGateway.Create(_article);
            _articleChanged.ID = _article.ID;

            //Act 
            _articleGateway.Update(_articleChanged);
            var articleFromDbChanged = _articleGateway.Get(_article.ID);

            //Assert
            IsTrue(articleFromDbChanged.Country.ID == _article.Country.ID);
            IsTrue(articleFromDbChanged.Tags[0].ID == _article.Tags[0].ID);
            IsTrue(articleFromDbChanged.Tags[1].ID == _article.Tags[1].ID);
            IsTrue(articleFromDbChanged.Content == _article.Content);
            IsTrue(articleFromDbChanged.Url == _article.Url);

            IsFalse(articleFromDbChanged.Title == _article.Title);
        }

        [TestMethod]
        public void ShouldDeleteArticle()
        {
            //Arrange
            _article.ID = _articleGateway.Create(_article);

            //Act
            _articleGateway.Delete(_article.ID);
            Int32[] tags = _tagGateway.GetAllRelatedToArticle(_article.ID);

            //Assert
            IsTrue(tags.Length == 0);
        }
    }

}
