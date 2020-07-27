CREATE PROCEDURE [sp_InsertUserLogin]
@login varchar(15),
@password varchar(8)
AS
INSERT INTO UserLogin (Login, Password)
VALUES (@login, @password)
SELECT SCOPE_IDENTITY()