USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[splangValuSave]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
splangValuSave 1, 'CA', 12, 'asfisafsdfshf', 'Y', 'Y', 1
*/

CREATE procedure [dbo].[splangValuSave]
(
	@MasterIDField			varchar(50) = NULL,
	@MasterTablePrefix		varchar(50) = NULL,
	@LanguageId				bigint = -1,
	@Value					nvarchar(max)  = NULL,
	@IsActive				varchar(1)  = NULL,
	@isOnlyDelete			varchar(1)  = NULL,
	@UserCode				varchar(50) = '',
	@ErrorMessage			varchar(500) out
)
AS 
BEGIN
	DECLARE @DatauniqueID bigint, @ActivityType VARCHAR(1)
	DECLARE @UserID BIGINT
	SELECT @UserID = 0

	if(isnull(rtrim(ltrim(@UserCode)), '') <> '')
	begin
		SELECT @UserID = UserId FROM [urMstUserRegisteredMaster] WHERE [UserCode] = @UserCode
	end

	SELECT @ActivityType = 'A'
		
	IF(SELECT COUNT(*) FROM [langMstMasterDataMultilanguage] WHERE MasterIDField = @MasterIDField AND MasterTablePrefix = @MasterTablePrefix AND LanguageId = @LanguageId) > 0
	BEGIN
		insert into [langMstMasterDataMultilanguageAudit] 
			([MasterIDField],[MasterTablePrefix],[LanguageId],[DatauniqueID],[ActivityType],[Value]
			,[IsActive],[LastModifiedOn],[LastModifiedBy],[CreatedOn],[CreatedBy])
		SELECT [MasterIDField],[MasterTablePrefix],[LanguageId],[DatauniqueID],[ActivityType],[Value]
			,CASE WHEN(@isOnlyDelete = 'Y') THEN 'D' ELSE [IsActive] END,[LastModifiedOn],[LastModifiedBy],[CreatedOn],[CreatedBy]
			FROM [langMstMasterDataMultilanguage]
			WHERE MasterIDField = @MasterIDField 
			AND MasterTablePrefix = @MasterTablePrefix 
			AND LanguageId = @LanguageId

		if(@isOnlyDelete <> 'Y')
		BEGIN
			SELECT @DatauniqueID = CAST(ISNULL(MAX(DatauniqueID), 1) AS INT) + 1 FROM [langMstMasterDataMultilanguageAudit] 
				WHERE [MasterIDField] = @MasterIDField
				AND [MasterTablePrefix] = @MasterTablePrefix
				AND [LanguageId] = @LanguageId

			UPDATE [langMstMasterDataMultilanguage] 
				SET [DatauniqueID] = @DatauniqueID,
					[ActivityType] = 'M',
					[Value] = @Value,
					[IsActive] = @IsActive,
					[LastModifiedOn] = GETDATE(),
					[LastModifiedBy] = @UserID
					
				WHERE [MasterIDField] = @MasterIDField
					AND [MasterTablePrefix] = @MasterTablePrefix
					AND [LanguageId] = @LanguageId
		END
		ELSE 
			DELETE FROM [langMstMasterDataMultilanguage] 
			WHERE [MasterIDField] = @MasterIDField
					AND [MasterTablePrefix] = @MasterTablePrefix
					AND [LanguageId] = @LanguageId
	END
	ELSE 
	BEGIN
		SELECT @DatauniqueID = 1

		insert into [langMstMasterDataMultilanguage] 
			([MasterIDField],[MasterTablePrefix],[LanguageId],[DatauniqueID],[ActivityType],[Value]
			,[IsActive],[CreatedOn],[CreatedBy])
		SELECT @MasterIDField, @MasterTablePrefix, @LanguageId, @DatauniqueID, 'A', @Value, @IsActive 
			,GETDATE(), @UserID
	END	   	
END			   			

GO
