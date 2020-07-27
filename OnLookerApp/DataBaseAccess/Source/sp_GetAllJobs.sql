CREATE PROCEDURE [sp_GetAllJobs]
AS
SELECT Job.ID AS JobID, CurPairNamed.BaseCurrency, CurPairNamed.QuotedCurrency,Country.Name AS Country, Tag.Name AS Tag
            FROM[ONLOOKER].[dbo].[Job] AS Job
            JOIN[ONLOOKER].[dbo].[Country] AS Country
            ON Job.[CountryID] = Country.ID
            Join[ONLOOKER].[dbo].[Tag] AS Tag
            ON Job.TagID = Tag.ID
            JOIN(
                SELECT CurPair.ID, CurTypeBase.CurrencyName AS BaseCurrency, CurTypeQuoted.CurrencyName AS QuotedCurrency

            FROM[ONLOOKER].[dbo].[CurrencyPair] AS CurPair

            JOIN[ONLOOKER].[dbo].[CurrencyType] AS CurTypeBase

            ON CurPair.BaseCurrencyID = CurTypeBase.ID

            JOIN[ONLOOKER].[dbo].[CurrencyType] AS CurTypeQuoted

            ON CurPair.BaseCurrencyID = CurTypeQuoted.ID
                ) AS CurPairNamed

            ON CurPairNamed.ID = Job.CurrencyPairID