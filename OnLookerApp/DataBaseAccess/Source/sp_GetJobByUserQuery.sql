CREATE PROCEDURE [sp_GetJobByUserQuery]
@base varchar (3),
@quoted varchar (3),
@country varchar (20),
@tag varchar (50)
AS
WITH JobTable AS (SELECT Job.ID AS JobID, CurPairNamed.BaseCurrency, CurPairNamed.QuotedCurrency,Country.Name AS Country, Tag.Name AS Tag
  FROM [ONLOOKER].[dbo].[Job] AS Job 
  JOIN [ONLOOKER].[dbo].[Country] AS Country 
  ON Job.[CountryID] = Country.ID
  Join [ONLOOKER].[dbo].[Tag] AS Tag
  ON Job.TagID = Tag.ID
  JOIN(
	SELECT CurPair.ID, CurTypeBase.CurrencyCode AS BaseCurrency, CurTypeQuoted.CurrencyCode AS QuotedCurrency
	FROM [ONLOOKER].[dbo].[CurrencyPair] AS CurPair
	JOIN [ONLOOKER].[dbo].[CurrencyType] AS CurTypeBase
	ON CurPair.BaseCurrencyID = CurTypeBase.ID
	JOIN [ONLOOKER].[dbo].[CurrencyType] AS CurTypeQuoted
	ON CurPair.BaseCurrencyID = CurTypeQuoted.ID
	) AS CurPairNamed
	ON  CurPairNamed.ID = Job.CurrencyPairID)
	SELECT * FROM JobTable
	WHERE BaseCurrency = @base AND QuotedCurrency = @quoted AND Country = @country AND Tag = @tag