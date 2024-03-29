USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spURMSTOrganizationSave]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spURMSTOrganizationSave] (
	@OrganizationCode			VARCHAR(50), 
	@OrganizationName			VARCHAR(5000),
	@IsActive  					VARCHAR(1),
	@UserID						BIGINT,
	@isOnlyDelete				VARCHAR(1),
	@NewDatauniqueID 			BIGINT OUT,
	@ErrorMessage 				VARCHAR(50) OUT
)
AS 
BEGIN 

	DECLARE @OldDatauniqueID BIGINT
	DECLARE @AllowSaveDeleteData VARCHAR(1)
	select @AllowSaveDeleteData = 'Y'
	DECLARE @IsExists INT
	DECLARE @OrganizationId BIGINT 
	
	if(isnull(rtrim(ltrim(@OrganizationCode)), '') <> '')
	begin
		SELECT @OrganizationId = OrganizationId FROM [dbo].[URMSTOrganizationMaster] WHERE [OrganizationCode] = @OrganizationCode
	end

	IF (@isOnlyDelete = 'Y')
	BEGIN 
		SELECT @IsExists = MAX(x) FROM (
			SELECT COUNT(OrganizationId) x FROM URMSTOrganizationBranch WHERE OrganizationId = @OrganizationId
			UNION
			SELECT COUNT(OrganizationId) x FROM URMSTOrganizationBranchAudit WHERE OrganizationId = @OrganizationId)A

		IF (@IsExists > 0)
		BEGIN 
			SELECT @AllowSaveDeleteData = 'N'
			SELECT @ErrorMessage = 'Data can not be deleted as the same is assigned to another module'
		END
	END 
	ELSE 
	BEGIN 
		IF ((
			SELECT COUNT(OrganizationId) x FROM URMSTOrganizationMaster 
			WHERE OrganizationName = @OrganizationName 
			AND ((@OrganizationId < 1) OR (@OrganizationId > 0 AND OrganizationId <> @OrganizationId))
			) > 0)
		BEGIN 
			SELECT @AllowSaveDeleteData = 'N'
			SELECT @ErrorMessage = 'Data can not be saved as the same name is assigned for another record'
		END
	END 
	
	IF (@AllowSaveDeleteData = 'Y')
	BEGIN			
		IF @OrganizationId = ''
		BEGIN 
			SELECT @OrganizationCode = NEWID()
			SELECT @NewDatauniqueID = 1
				
			INSERT INTO URMSTOrganizationMaster (OrganizationCode, OrganizationName, 
				DatauniqueID, ActivityType, IsActive, LastModifiedOn, LastModifiedBy)
			VALUES(@OrganizationCode, @OrganizationName, 
				@NewDatauniqueID, 'A', @IsActive, GETDATE(), @UserID)
		END
		ELSE
		BEGIN 
			SELECT @OldDatauniqueID = DatauniqueID FROM URMSTOrganizationMaster WHERE OrganizationId = @OrganizationId
					
			INSERT INTO URMSTOrganizationMasterAudit (OrganizationId, OrganizationName, 
				DatauniqueID, ActivityType, IsActive, LastModifiedOn, LastModifiedBy,
				AuditOperationOn, AuditOperationBy)
			SELECT OrganizationId, OrganizationName, 
				DatauniqueID, CASE WHEN(@isOnlyDelete <> 'Y') THEN ActivityType ELSE 'D' END, IsActive, LastModifiedOn, LastModifiedBy,
				GETDATE(), @UserID 
			FROM URMSTOrganizationMaster
			WHERE OrganizationId = @OrganizationId
				   				  
			IF @isOnlyDelete <> 'Y'
			BEGIN 
				SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM URMSTOrganizationMaster WHERE OrganizationId = @OrganizationId
		
				UPDATE URMSTOrganizationMaster SET 
					OrganizationName = @OrganizationName, 
					DatauniqueID = @NewDatauniqueID, 
					ActivityType = 'M', 
					IsActive = @IsActive, 
					LastModifiedOn = GETDATE(), 
					LastModifiedBy = @UserID
				WHERE OrganizationId = @OrganizationId
			END 
			ELSE 
				DELETE FROM URMSTOrganizationMaster WHERE OrganizationId = @OrganizationId
		END 
	END
END



GO
