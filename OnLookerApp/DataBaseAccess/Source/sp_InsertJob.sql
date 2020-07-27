CREATE PROCEDURE [sp_InsertJob]
@base varchar(3),
@quoted varchar(3),
@countryid int,
@tagid int,
@CurPairID int
AS
DECLARE @tablevariable TABLE ( ID int NOT NULL PRIMARY KEY, BaseCurrency varchar(3) NOT NULL, QuotedCurrency varchar(3) NOT NULL)  
INSERT INTO @tablevariable
SELECT CurPair.ID, CurTypeBase.CurrencyCode AS BaseCurrency, CurTypeQuoted.CurrencyCode AS QuotedCurrency
FROM[ONLOOKER].[dbo].[CurrencyPair] AS CurPair
JOIN[ONLOOKER].[dbo].[CurrencyType] AS CurTypeBase
ON CurPair.BaseCurrencyID = CurTypeBase.ID
JOIN[ONLOOKER].[dbo].[CurrencyType] AS CurTypeQuoted
ON CurPair.BaseCurrencyID = CurTypeQuoted.ID;
SET @CurPairID = (SELECT ID FROM @tablevariable
WHERE BaseCurrency = @base AND QuotedCurrency = @quoted)
INSERT INTO Job (CurrencyPairID, CountryID, TagID) values (@CurPairID ,@countryid,@tagid) SELECT SCOPE_IDENTITY()