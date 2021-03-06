USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spLanguageCountrySave]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spLanguageCountrySave](
	@Id bigint,
	@LanguageId bigint,
	@CountryId bigint,
	@Visibility varchar(1),
	@Priority int,
	@UserCode VARCHAR(50),
	@ErrorMessage varchar(500) out
)
as
begin
	select @ErrorMessage = ''

	DECLARE @UserID BIGINT
	SELECT @UserID = 0

	IF(ISNULL(RTRIM(LTRIM(@UserCode)), '') <> '')
	BEGIN
		SELECT @UserID = UserId FROM [urMstUserRegisteredMaster] WHERE [UserCode] = @UserCode
	END


	IF @Id > 0
	BEGIN

		INSERT INTO [langMAPlanguageCountryAudit]
				   ([LanguageId],[DatauniqueID],[Country],[Visibility],[Priority]
				   ,[LastModifiedOn],[LastModifiedBy],[AuditOperationOn],[AuditOperationBy]
				   ,[CreatedOn],[CreatedBy])
			 SELECT [LanguageId],[DatauniqueID],[Country],[Visibility],[Priority]
				   ,[LastModifiedOn],[LastModifiedBy],GETDATE(),@UserCode
				   ,[CreatedOn],[CreatedBy] 
				FROM [langMAPlanguageCountry]
				WHERE Id = @Id

		Declare @DatauniqueId int

		select @DatauniqueId = (max(maxid) + 1) from (
			select isnull(max(DatauniqueID), 0) maxid FROM [langMAPlanguageCountry] WHERE LanguageId = @LanguageId AND Country = @CountryId
			union all
			select isnull(max(DatauniqueID), 0) maxid FROM [langMAPlanguageCountryAudit] WHERE LanguageId = @LanguageId AND Country = @CountryId
		)a
				
		UPDATE [langMAPlanguageCountry] SET 
			[DatauniqueID] = @DatauniqueId,
			[Country] = @CountryId,
			[Visibility] = @Visibility,
			[Priority] = @Priority,
			[LastModifiedOn] = GETDATE()
		WHERE Id = @Id 
	END
	ELSE
	BEGIN


			INSERT INTO [dbo].[langMAPlanguageCountry]
				   ([LanguageId],[DatauniqueID],[Country],[Visibility],[Priority]
				   ,[LastModifiedOn],[LastModifiedBy],[CreatedOn],[CreatedBy])
			 VALUES
				   (@LanguageId, 1,@CountryId,@Visibility,@Priority
				   ,GETDATE(),@UserCode,GETDATE(),@UserCode)
	END
END

GO
