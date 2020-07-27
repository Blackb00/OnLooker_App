CREATE PROCEDURE [sp_UpdateArticle]
@id int,
@url varchar(100),
@title nvarchar(256),
@content nvarchar(max),
@html varbinary(max),
@date date,
@countryid int
AS
UPDATE Article 
SET URL = @url, Title=@title, Content=  @content,  HTML=@html, Date=@date, CountryID=@countryid
WHERE ID = @id