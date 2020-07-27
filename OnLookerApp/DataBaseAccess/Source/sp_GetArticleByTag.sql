CREATE PROCEDURE [sp_GetArticleByTag]
@tag varchar(50)
AS
SELECT A.URL, A.Title, A.Content, A.HTML, A.Date, A.CountryID FROM Article AS A
INNER JOIN ArticleTag AS ArtTag 
ON A.ID = ArtTag.ArticleID
InNER JOIN Tag AS T
ON T.ID = ArtTag.TagID
WHERE T.ID = @tag


