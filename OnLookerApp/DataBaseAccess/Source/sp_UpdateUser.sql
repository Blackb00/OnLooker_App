CREATE PROCEDURE [sp_UpdateUser]
@name nvarchar (15),
@email varchar(50),
@id int
AS
UPDATE UserInfo  
SET Name=@name, Email=@email
WHERE ID = @id