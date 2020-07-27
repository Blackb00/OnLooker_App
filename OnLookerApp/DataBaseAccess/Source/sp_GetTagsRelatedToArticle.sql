CREATE PROCEDURE [sp_GetTagsRelatedToArticle]
@articleId int
AS
SELECT TagID FROM [dbo].[ArticleTag]
WHERE ArticleID= @articleId