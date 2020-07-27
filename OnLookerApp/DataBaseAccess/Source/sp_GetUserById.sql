CREATE PROCEDURE [sp_GetUserById]
@id int
AS
SELECT * FROM UserInfo
WHERE ID = @id