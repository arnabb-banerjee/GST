USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[splangMSTLanguageSave]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[splangMSTLanguageSave] (
	@LanguageId					BIGINT, 
	@LanguageName				VARCHAR(5000),
	@IsActive  					VARCHAR(1),
	@UserCode					VARCHAR(50),
	@isOnlyDelete				VARCHAR(1),
	@NewDatauniqueID 			BIGINT OUT,
	@ErrorMessage 				VARCHAR(50) OUT
)

AS 
BEGIN 
	DECLARE @OldDatauniqueID BIGINT
	DECLARE @AllowSaveDeleteData VARCHAR(1)
	DECLARE @UserID BIGINT
	SELECT	@UserID = 0

	IF(ISNULL(RTRIM(LTRIM(@UserCode)), '') <> '')
	BEGIN
		SELECT @UserID = UserId FROM [urMstUserRegisteredMaster] WHERE [UserCode] = @UserCode
	END

	select @AllowSaveDeleteData = 'Y'

	DECLARE @IsExists INT

	IF (@isOnlyDelete = 'Y')
	BEGIN 
		SELECT @IsExists = MAX(x) FROM (

			SELECT COUNT(LanguageId) x FROM LangMSTMasterDataMultiLanguage WHERE LanguageId = @LanguageId
			UNION
			SELECT COUNT(LanguageId) x FROM LangMSTMasterDataMultiLanguageAudit WHERE LanguageId = @LanguageId)A

		IF (@IsExists > 0)
		BEGIN 
			SELECT @AllowSaveDeleteData = 'N'
			SELECT @ErrorMessage = 'Data can not be deleted as the same is assigned to another module'
		END
	END 
	ELSE 
	BEGIN 
		IF ((SELECT COUNT(LanguageId) x FROM LangMSTLanguageMaster WHERE LanguageName = @LanguageName AND ((@LanguageId < 1) OR (@LanguageId > 0 AND LanguageId <> @LanguageId))) > 0)
		BEGIN 
			SELECT @AllowSaveDeleteData = 'N'
			SELECT @ErrorMessage = 'Data can not be saved as the same name is assigned for another record'
		END
	END 

	IF (@AllowSaveDeleteData = 'Y')
	BEGIN		
		IF @LanguageId < 1
		BEGIN 
			SELECT @NewDatauniqueID = 1

			INSERT INTO LangMSTLanguageMaster (LanguageName, DatauniqueID, ActivityType, IsActive, LastModifiedOn, LastModifiedBy)
			VALUES(@LanguageName, @NewDatauniqueID, 'A', @IsActive, GETDATE(), @UserID)
		END
		ELSE
		BEGIN 
			SELECT @OldDatauniqueID = DatauniqueID FROM LangMSTLanguageMaster WHERE LanguageId = @LanguageId

			INSERT INTO LangMSTLanguageMasterAudit (LanguageId, LanguageName, 
				DatauniqueID, ActivityType, IsActive, LastModifiedOn, LastModifiedBy,
				AuditOperationOn, AuditOperationBy)

			SELECT LanguageId, LanguageName, 
				DatauniqueID, CASE WHEN(@isOnlyDelete <> 'Y') THEN ActivityType ELSE 'D' END, IsActive, LastModifiedOn, LastModifiedBy,
				GETDATE(), @UserID 
			FROM LangMSTLanguageMaster
			WHERE LanguageId = @LanguageId

		IF @isOnlyDelete <> 'Y'
		BEGIN 
			SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM LangMSTLanguageMaster WHERE LanguageId = @LanguageId

			UPDATE LangMSTLanguageMaster SET 
				LanguageName = @LanguageName, 
				DatauniqueID = @NewDatauniqueID, 
				ActivityType = 'M', 
				IsActive = @IsActive, 
				LastModifiedOn = GETDATE(), 
				LastModifiedBy = @UserID
					WHERE LanguageId = @LanguageId
			END 
			ELSE 
				DELETE FROM LangMSTLanguageMaster WHERE LanguageId = @LanguageId
		END 
	END
END



GO
