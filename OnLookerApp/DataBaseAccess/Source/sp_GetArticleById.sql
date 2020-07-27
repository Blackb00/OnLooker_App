CREATE PROCEDURE [sp_GetArticleByID]
@articleId int
AS
SELECT * From Article
WHERE Article.ID = @articleId