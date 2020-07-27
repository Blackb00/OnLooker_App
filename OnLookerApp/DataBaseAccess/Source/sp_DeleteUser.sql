CREATE PROCEDURE [sp_DeleteUser]
@id int
AS
DELETE FROM UserInfo
WHERE ID = @id