USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spMSTBranchSave]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spMSTBranchSave] (
	@BranchId						[BIGINT], 
	@BranchName							[VARCHAR](50), 
	@OrganizationCode				[VARCHAR](50) = '', 
	@IsMainBranch					[VARCHAR](1),
	@Street1						[varchar](500) = NULL,
	@Street2						[varchar](500) = NULL,
	@City							[VARCHAR](50) = NULL,
	@State							BIGINT = NULL,
	@Country						BIGINT = NULL,
	@PIN							[VARCHAR](10) = NULL,
	@IsActive  						[VARCHAR](1),
	@UserCode						[VARCHAR](50),
	@isOnlyDelete					[VARCHAR](1),
	@NewDatauniqueID 				[BIGINT] OUT,
	@ErrorMessage 					[VARCHAR](50) OUT
)
AS 
BEGIN 

	DECLARE @OldDatauniqueID BIGINT
	DECLARE @AllowSaveDeleteData VARCHAR(1)
	select @AllowSaveDeleteData = 'Y'
	DECLARE @IsExists INT
	DECLARE @OrganizationId BIGINT
	SELECT @OrganizationId = 0
	DECLARE @UserID BIGINT
	SELECT @UserID = 0

	IF(ISNULL(RTRIM(LTRIM(@UserCode)), '') <> '')
	BEGIN
		SELECT @UserID = UserId FROM [urMstUserRegisteredMaster] WHERE [UserCode] = @UserCode
	END


	if(isnull(rtrim(ltrim(@OrganizationCode)), '') <> '')
	begin
		SELECT @OrganizationId = OrganizationId FROM [dbo].[URMSTOrganizationMaster] WHERE [OrganizationCode] = @OrganizationCode
	end

	IF (@isOnlyDelete = 'Y')
	BEGIN 
			select 1 
	END 
	ELSE 
	BEGIN 
		IF ((SELECT COUNT(BranchId) x FROM [MSTBranch] WHERE Name = @BranchName AND ((@BranchId < 1) OR (@BranchId > 0 AND BranchId <> @BranchId AND OrganizationId = @OrganizationId))
			) > 0)
		BEGIN 
				SELECT @AllowSaveDeleteData = 'N'
				SELECT @ErrorMessage = 'Data can not be saved as the same name is assigned for another record'
		END
	END 
	
	IF (@AllowSaveDeleteData = 'Y')
	BEGIN		
		IF @BranchId < 1
		BEGIN 
				SELECT @NewDatauniqueID = 1
		
			INSERT INTO [dbo].[MSTBranch]
				([DatauniqueID],[ActivityType],[Name],[OrganizationID],IsMainBranch,[IsActive]
				,Street1, Street2, City, [State], Country, PIN
				,[LastModifiedOn],[LastModifiedBy])
				VALUES
				(@NewDatauniqueID,'A', @BranchName, @OrganizationID, @IsMainBranch, @IsActive, 
				@Street1, @Street2, @City, @State, @Country, @PIN, 
				GETDATE(), @UserID)
		END
		ELSE
		BEGIN 
				SELECT @OldDatauniqueID = DatauniqueID FROM [MSTBranch] WHERE BranchId = @BranchId
					
				INSERT INTO [dbo].[MSTBranchAudit]
					([BranchId],[DatauniqueID],[ActivityType],[Name],[OrganizationID],[IsMainBranch],[IsActive]
					,Street1, Street2, City, [State], Country, PIN
					,[LastModifiedOn],[LastModifiedBy],[AuditOperationOn],[AuditOperationBy])
				SELECT [BranchId],[DatauniqueID],(CASE WHEN(@isOnlyDelete <> 'Y') THEN ActivityType ELSE 'D' END),[Name],[OrganizationID],[IsMainBranch],[IsActive]
					,Street1, Street2, City, [State], Country, PIN
					,[LastModifiedOn],[LastModifiedBy], GETDATE(), @UserID 
				FROM [MSTBranch]
				WHERE BranchId = @BranchId
				   				  
				IF @isOnlyDelete <> 'Y'
				BEGIN 
				  	SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM [MSTBranch] WHERE BranchId = @BranchId
		
				  	UPDATE [MSTBranch] SET 
				  			DatauniqueID = @NewDatauniqueID,
							Name = @BranchName, 
							OrganizationID = @OrganizationID, 
							IsMainBranch = @IsMainBranch,
							Street1 = @Street1, 
							Street2 = @Street2, 
							City = @City, 
							[State] = @State, 
							Country = @Country, 
				  			ActivityType = 'M', 
				  			IsActive = @IsActive, 
				  			LastModifiedOn = GETDATE(), 
				  			LastModifiedBy = @UserID
					WHERE BranchId = @BranchId
				END 
				ELSE 
				  	DELETE FROM [MSTBranch] WHERE BranchId = @BranchId

		END 
	END
END



GO
