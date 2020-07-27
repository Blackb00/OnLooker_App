CREATE PROCEDURE [sp_GetTagById]
@tagId int
AS
SELECT * FROM Tag
WHERE ID =  @tagId 