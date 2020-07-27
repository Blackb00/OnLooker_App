CREATE PROCEDURE [sp_DeleteLogin]
@id int
AS
DELETE FROM UserLogin
WHERE ID = @id