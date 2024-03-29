USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spSocialMediaLogin]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- SPLOGIN 'R','btecharnab@gmail.com','Arnab#123', ''

CREATE PROCEDURE [dbo].[spSocialMediaLogin]
(
	@UserType VARCHAR(50),
	@UserCode VARCHAR(50),
	@ErrorMessage VARCHAR(250) = '' OUT
)
AS
BEGIN
	DECLARE @UserID bigint

	SELECT @UserID = USERID from URMSTUserRegisteredEmail WHERE EmailID = @UserCode 
		
	IF (ISNULL(@UserID, '') = '') 
	BEGIN
		SELECT @UserID = USERID from URMSTUserRegisteredMobile WHERE Mobile = @UserCode 
	END

	IF (ISNULL(@UserID, '') = '') 
	BEGIN
		SELECT @ErrorMessage = 'User not found'
	END	

	IF len(rtrim(ltrim((@ErrorMessage)))) = 0
	BEGIN
		SELECT R.UserCode, R.AccessAllowed, R.FirstName, R.LastName, R.IsApproved, R.IsActive, R.Sex, R.Title, 
			E.EmailID, E.IsEmailVerified, e.IsEmailConfirmationSend, e.EmailConfirmationSentOn, e.EmailVerifiedOn, 
			M.Mobile, M.IsMobileVerified, m.IsMobileConfirmationSent, m.MobileConfirmationSentOn, m.MobileVerifiedOn, 
			R.UserType, 
			(SELECT OrganizationCode FROM URMSTOrganizationMaster WHERE OrganizationID IN (
				SELECT TOP 1 OrganizationID FROM [urMapRegisteredUserFunction] WHERE UserID = R.UserID)) OrganizationCode,
			(SELECT OrganizationName FROM URMSTOrganizationMaster WHERE OrganizationID IN (
				SELECT TOP 1 OrganizationID FROM [urMapRegisteredUserFunction] WHERE UserID = R.UserID)) OrganizationName  
		FROM URMSTUserRegisteredMaster R
		LEFT JOIN URMSTUserRegisteredEmail E ON R.UserID = E.UserID
		LEFT JOIN URMSTUserRegisteredMobile M ON R.UserID = M.UserID
		WHERE R.UserID = @UserID
	END
END

GO
