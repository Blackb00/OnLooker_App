CREATE PROCEDURE [sp_GetCurrencyById]
@id int
AS
SELECT * FROM CurrencyType
WHERE ID = @id