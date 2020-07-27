CREATE PROCEDURE [sp_UpdateCurrencyPair]
@base int,
@quoted int,
@id int
AS
UPDATE CurrencyPair
SET BaseCurrencyID=@base, QuotedCurrencyID=@quoted
WHERE ID=@id