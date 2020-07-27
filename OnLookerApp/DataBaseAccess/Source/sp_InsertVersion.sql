CREATE PROCEDURE [sp_InsertVersion]
@major varchar(2),
@minor varchar(2),
@filenumber varchar(4),
@comment varchar(25),
@date datetime
AS
INSERT INTO MigrationHistory (MajorVersion, MinorVersion, FileNumber, Comment, DateApplied) VALUES (@major, @minor, @filenumber, @comment, @date)
SELECT SCOPE_IDENTITY()