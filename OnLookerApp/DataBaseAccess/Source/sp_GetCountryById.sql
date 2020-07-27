CREATE PROCEDURE [sp_GetCountryById]
@id int
AS
SELECT * FROM Country
WHERE ID = @id