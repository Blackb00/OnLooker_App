CREATE PROCEDURE [sp_InsertUser]
@name nvarchar (15),
@email varchar(50),
@loginId int
AS
INSERT INTO UserInfo (Name, Email, LoginID)
VALUES (@name, @email, @loginId)
SELECT SCOPE_IDENTITY()