CREATE PROCEDURE [sp_DeleteCurrencyPair]
@Id int
AS
DELETE FROM CurrencyPair
WHERE ID = @Id