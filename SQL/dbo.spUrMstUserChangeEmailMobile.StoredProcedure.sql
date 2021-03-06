USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spUrMstUserChangeEmailMobile]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	[dbo].[spUrMstUserChangePassword] 
	
*/
CREATE PROCEDURE [dbo].[spUrMstUserChangeEmailMobile]
(
	@ChangeEmailMobileOption	VARCHAR(500), 
	@NewEmailMobile				VARCHAR(500),
	@Password					VARCHAR(500),

	@DONEBY						VARCHAR(50),

	@NewDatauniqueID 			BIGINT OUT,
	@ErrorMessage 				VARCHAR(max) OUT
)
AS 
BEGIN 
	DECLARE @UserID bigint
	DECLARE @OrganizationID bigint
	
	IF (ISNULL(RTRIM(LTRIM(@ChangeEmailMobileOption)), '') = '')
		select @ErrorMessage += 'Interal error change option is invalid'
	ELSE IF (ISNULL(RTRIM(LTRIM(@Password)), '') = '')
		select @ErrorMessage += 'Password number is mandatrory'
	ELSE IF (ISNULL(RTRIM(LTRIM(@ChangeEmailMobileOption)), 'E') = '' AND ISNULL(RTRIM(LTRIM(@NewEmailMobile)), '') = '')
		select @ErrorMessage += 'Please enter emailid'
	ELSE IF (ISNULL(RTRIM(LTRIM(@ChangeEmailMobileOption)), 'M') = '' AND ISNULL(RTRIM(LTRIM(@NewEmailMobile)), '') = '')
		select @ErrorMessage += 'Please enter mobile number'
	ELSE
	BEGIN
		DECLARE @NewUserCode VARCHAR(50)
		SELECT @NewUserCode = NEWID()
		SELECT @NewDatauniqueID = 1
		SELECT UserID FROM urMstUserRegisteredPassword WHERE Password = @Password

		BEGIN TRANSACTION T1;
		BEGIN TRY
			IF ISNULL(RTRIM(lTRIM(@ChangeEmailMobileOption)), 0) = 'E'
			BEGIN
				INSERT INTO urMstUserRegisteredEmailAudit
					(UserID, ActivityType, DatauniqueID, 
					IsEmailConfirmationSend, EmailConfirmationSentOn, 
					IsEmailVerified, EmailVerifiedOn, 
					EmailID, IsActive,  
					LastModifiedBy, LastModifiedOn, AuditOperationBy, AuditOperationOn)

					SELECT  UserID, ActivityType, DatauniqueID, 
						IsEmailConfirmationSend, EmailConfirmationSentOn, 
						IsEmailVerified, EmailVerifiedOn, 
						EmailID, IsActive,  
						LastModifiedBy, LastModifiedOn, @DONEBY, GETDATE()
							FROM urMstUserRegisteredEmail
							WHERE UserID = @UserID

				SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM urMstUserRegisteredEmail WHERE UserID = @UserID

				DELETE FROM urMstUserRegisteredEmail WHERE UserID = @UserID
				
				INSERT INTO urMstUserRegisteredEmail
					(UserID, ActivityType, DatauniqueID, 
					IsEmailConfirmationSend, EmailConfirmationSentOn, 
					IsEmailVerified, EmailVerifiedOn, 
					EmailID, IsActive,  
					LastModifiedBy, LastModifiedOn)
	
					SELECT @UserID, 'Y', @NewDatauniqueID, 
						NULL, NULL,  
						NULL, NULL, 
						RTRIM(LTRIM(@NewEmailMobile)), NULL,
						@DONEBY, GETDATE()  
			END
			IF ISNULL(RTRIM(lTRIM(@ChangeEmailMobileOption)), 0) = 'M'
			BEGIN
				INSERT INTO urMstUserRegisteredMobileAudit
					(UserID, ActivityType, DatauniqueID, 
					IsMobileConfirmationSent, MobileConfirmationSentOn, 
					IsMobileVerified, MobileVerifiedOn, 
					Mobile, IsActive,  
					LastModifiedBy, LastModifiedOn, AuditOperationBy, AuditOperationOn)

					SELECT  UserID, ActivityType, DatauniqueID, 
						IsMobileConfirmationSent, MobileConfirmationSentOn, 
						IsMobileVerified, MobileVerifiedOn, 
						Mobile, IsActive,  
						LastModifiedBy, LastModifiedOn, @DONEBY, GETDATE()
							FROM urMstUserRegisteredMobile
							WHERE UserID = @UserID

				SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM urMstUserRegisteredMobile WHERE UserID = @UserID

				DELETE FROM urMstUserRegisteredMobile WHERE UserID = @UserID
				
				INSERT INTO urMstUserRegisteredMobile
					(UserID, ActivityType, DatauniqueID, 
						IsMobileConfirmationSent, MobileConfirmationSentOn, 
						IsMobileVerified, MobileVerifiedOn, 
						Mobile, IsActive,  
						LastModifiedBy, LastModifiedOn)
	
					SELECT @UserID, 'Y', @NewDatauniqueID, 
						NULL, NULL,  
						NULL, NULL, 
						RTRIM(LTRIM(@NewEmailMobile)), NULL,
						@DONEBY, GETDATE()  
			END
				
			COMMIT;
		END TRY
		BEGIN CATCH
			ROLLBACK;
			SELECT @ErrorMessage = ERROR_MESSAGE()
			SELECT @ErrorMessage
		END CATCH;
		RETURN
	END -- End of Insertion
END
GO
