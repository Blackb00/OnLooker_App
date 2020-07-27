CREATE PROCEDURE [sp_InsertCurrencyPair]
@base int,
@quoted int
AS
INSERT INTO CurrencyPair (BaseCurrencyID, QuotedCurrencyID) VALUES (@base, @quoted)
SELECT SCOPE_IDENTITY()