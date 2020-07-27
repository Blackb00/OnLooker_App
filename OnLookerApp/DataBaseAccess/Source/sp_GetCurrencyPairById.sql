CREATE PROCEDURE [sp_GetCurrencyPairById]
@id int
AS
SELECT * FROM CurrencyPair
WHERE ID = @id