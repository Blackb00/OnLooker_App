
CREATE PROCEDURE [sp_GetArticles]
AS
SELECT  Article.ID,
Article.URL,
Article.Title,
Article.Content,
Article.HTML,
Article.CountryID,
Article.Date as Article,
Tag.Name as Tag
FROM ArticleTag
INNER JOIN Article
ON ArticleTag.ArticleID = Article.ID
Inner JOIN Tag
ON ArticleTag.TagID = Tag.ID
