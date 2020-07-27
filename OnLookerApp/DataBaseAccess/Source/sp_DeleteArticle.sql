CREATE PROCEDURE [sp_DeleteArticle]
@articleId int
AS
DELETE FROM Article
WHERE ID = @articleId
