CREATE PROCEDURE [sp_GetTagByName]
@name nvarchar(50)
AS
SELECT * FROM Tag
WHERE Name = @name