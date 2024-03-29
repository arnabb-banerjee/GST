USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spURMSTFunctionsSave]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spURMSTFunctionsSave] (
	@FunctionId						[BIGINT], 
	@FunctionName					[VARCHAR](50), 
	@IsForModerate					[VARCHAR](1),
	@IsForMembership				[VARCHAR](1),
	@IsDesignation					[VARCHAR](1),
	@IsDefaultForModerateUser		[VARCHAR](1),
	@IsDefaultForRegisteredUser		[VARCHAR](1),
	@Roles							[VARCHAR](5000),
	@IsActive  						[VARCHAR](1),
	@UserCode						[VARCHAR](50) = '',
	@isOnlyDelete					[VARCHAR](1) = '',
	@NewDatauniqueID 				[BIGINT] OUT,
	@ErrorMessage 					[VARCHAR](50) OUT
)
AS 
BEGIN 

	DECLARE @OldDatauniqueID BIGINT
	DECLARE @AllowSaveDeleteData VARCHAR(1)
	select @AllowSaveDeleteData = 'Y'
	DECLARE @IsExists INT
	DECLARE @UserID BIGINT
	SELECT @UserID = 0

	IF(ISNULL(RTRIM(LTRIM(@UserCode)), '') <> '')
	BEGIN
		SELECT @UserID = UserId FROM [urMstUserRegisteredMaster] WHERE [UserCode] = @UserCode
	END


	IF (@isOnlyDelete = 'Y')
	BEGIN 
			select 1 
	END 
	ELSE 
	BEGIN 
			IF ((SELECT COUNT(FunctionId) x FROM URMSTFunctions WHERE FunctionName = @FunctionName AND ((@FunctionId < 1) OR (@FunctionId > 0 AND FunctionId <> @FunctionId))
				) > 0)
			BEGIN 
					SELECT @AllowSaveDeleteData = 'N'
					SELECT @ErrorMessage = 'Data can not be saved as the same FunctionName is assigned for another record'
			END
	END 
	
	IF (@AllowSaveDeleteData = 'Y')
	BEGIN		
		BEGIN TRANSACTION T1;
		BEGIN TRY

			IF @FunctionId < 1
			BEGIN 
					SELECT @NewDatauniqueID = 1
		
			  	INSERT INTO [dbo].URMSTFunctions
				   ([DatauniqueID],[ActivityType],[FunctionName],[IsActive]
				   ,IsForModerate,IsForMembership,IsDesignation,ApplicableRoles
				   ,IsDefaultForModerateUser, IsDefaultForRegisteredUser
				   ,[LastModifiedOn],[LastModifiedBy])
				 VALUES
					(@NewDatauniqueID,'A', @FunctionName, @IsActive, 
					@IsForModerate, @IsForMembership, @IsDesignation, @Roles, 
					@IsDefaultForModerateUser, @IsDefaultForRegisteredUser, GETDATE(), @UserID)


			END
			ELSE
			BEGIN 
					SELECT @OldDatauniqueID = DatauniqueID FROM URMSTFunctions WHERE FunctionId = @FunctionId
					
					INSERT INTO [dbo].[URMSTFunctionsAudit]
					   ([FunctionId],[DatauniqueID],[ActivityType],[FunctionName],[IsActive]
					   ,IsForModerate,IsForMembership,IsDesignation,ApplicableRoles, IsDefaultForModerateUser, IsDefaultForRegisteredUser 
					   ,[LastModifiedOn],[LastModifiedBy],[AuditOperationOn],[AuditOperationBy])
					SELECT [FunctionId],[DatauniqueID],CASE WHEN(@isOnlyDelete <> 'Y') THEN ActivityType ELSE 'D' END,[FunctionName],[IsActive]
				   ,IsForModerate,IsForMembership,IsDesignation,ApplicableRoles, IsDefaultForModerateUser, IsDefaultForRegisteredUser
				   ,[LastModifiedOn],[LastModifiedBy],GETDATE(), @UserID 
					FROM URMSTFunctions
				   WHERE FunctionId = @FunctionId
				   				  
				  IF @isOnlyDelete <> 'Y'
				  BEGIN 
				  		SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM URMSTFunctions WHERE FunctionId = @FunctionId
		
				  		UPDATE URMSTFunctions SET 
				  				DatauniqueID = @NewDatauniqueID,
								FunctionName = @FunctionName, 
								IsForModerate = @IsForModerate,
								IsForMembership = @IsForMembership,
								IsDesignation = @IsDesignation,
								ApplicableRoles = @Roles,
								IsDefaultForModerateUser = @IsDefaultForModerateUser, 
								IsDefaultForRegisteredUser = @IsDefaultForRegisteredUser,
				  				ActivityType = 'M', 
				  				IsActive = @IsActive, 
				  				LastModifiedOn = GETDATE(), 
				  				LastModifiedBy = @UserID
						WHERE FunctionId = @FunctionId
				  END 
				  ELSE 
				  		DELETE FROM URMSTFunctions WHERE FunctionId = @FunctionId

			END 

			COMMIT;
		END TRY
		BEGIN CATCH
			ROLLBACK;
			SELECT @ErrorMessage = ERROR_MESSAGE()
			SELECT @ErrorMessage
		END CATCH;
		RETURN
	END
END




GO
