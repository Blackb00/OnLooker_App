CREATE PROCEDURE [sp_GetUserLoginByUserInfoId]
@id int
AS
SELECT UserLogin.ID AS LoginID, UserLogin.Login, UserLogin.Password, UserInfo.ID AS UserID, UserInfo.Email, UserInfo.Name FROM UserLogin
INNER JOIN UserInfo
ON UserLogin.ID = UserInfo.LoginID
WHERE UserInfo.ID = @id
