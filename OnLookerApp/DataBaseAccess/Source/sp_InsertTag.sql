﻿CREATE PROCEDURE [sp_InsertTag]
@name nvarchar(50)
AS
INSERT INTO Tag (Name)
VALUES (@name)
SELECT SCOPE_IDENTITY()