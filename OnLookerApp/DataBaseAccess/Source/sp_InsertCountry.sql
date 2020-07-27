CREATE PROCEDURE [sp_InsertCountry]
@code varchar(10),
@name varchar(20)
AS
INSERT INTO Country (Code, Name) VALUES (@code, @name)
SELECT SCOPE_IDENTITY()