CREATE PROCEDURE [sp_DeleteCurrency]
@Id int
AS
DELETE FROM CurrencyType
WHERE ID = @Id