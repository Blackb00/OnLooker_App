CREATE PROCEDURE [sp_InsertArticleTag]
@tagId int,
@articleId int
AS
INSERT INTO ArticleTag (ArticleID, TagID) VALUES (@articleId,@tagId)
SELECT SCOPE_IDENTITY()