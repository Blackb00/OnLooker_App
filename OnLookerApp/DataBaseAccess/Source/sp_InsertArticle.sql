CREATE PROCEDURE [sp_InsertArticle]
@url varchar(100),
@title nvarchar(256),
@content nvarchar(max),
@html varbinary(max),
@date date,
@countryid int
AS
INSERT INTO Article (URL, Title, Content, HTML, Date, CountryID)
VALUES (@url, @title, @content, @html, @date, @countryid)
SELECT SCOPE_IDENTITY()