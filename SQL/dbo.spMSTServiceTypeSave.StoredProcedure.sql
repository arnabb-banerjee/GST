USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spMSTServiceTypeSave]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spMSTServiceTypeSave] (
	@ServiceTypeId				BIGINT, 
	@ServiceTypeName			VARCHAR(5000),
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
	SELECT @UserID = 0

	IF(ISNULL(RTRIM(LTRIM(@UserCode)), '') <> '')
	BEGIN
		SELECT @UserID = UserId FROM [urMstUserRegisteredMaster] WHERE [UserCode] = @UserCode
	END

	select @AllowSaveDeleteData = 'Y'

	DECLARE @IsExists INT

	IF (@isOnlyDelete = 'Y')
	BEGIN 
		SELECT @IsExists = MAX(x) FROM (

			SELECT COUNT(*) x FROM [dbo].[prMAPOrganizationproduct] WHERE ServiceType = @ServiceTypeId
			UNION
			SELECT COUNT(*) x FROM [dbo].[prMAPOrganizationproductAudit] WHERE ServiceType = @ServiceTypeId)A

		IF (@IsExists > 0)
		BEGIN 
			SELECT @AllowSaveDeleteData = 'N'
			SELECT @ErrorMessage = 'Data can not be deleted as the same is assigned to another module'
		END
	END 
	ELSE 
	BEGIN 
		IF ((SELECT COUNT(ServiceTypeId) x FROM MSTServiceTypeMaster WHERE ServiceTypeName = @ServiceTypeName AND ((@ServiceTypeId < 1) OR (@ServiceTypeId > 0 AND ServiceTypeId <> @ServiceTypeId))) > 0)
		BEGIN 
			SELECT @AllowSaveDeleteData = 'N'
			SELECT @ErrorMessage = 'Data can not be saved as the same name is assigned for another record'
		END
	END 

	IF (@AllowSaveDeleteData = 'Y')
	BEGIN		
		IF @ServiceTypeId < 1
		BEGIN 
			SELECT @NewDatauniqueID = 1

			INSERT INTO MSTServiceTypeMaster (ServiceTypeName, DatauniqueID, ActivityType, IsActive, LastModifiedOn, LastModifiedBy)
			VALUES(@ServiceTypeName, @NewDatauniqueID, 'A', @IsActive, GETDATE(), @UserID)
		END
		ELSE
		BEGIN 
			SELECT @OldDatauniqueID = DatauniqueID FROM MSTServiceTypeMaster WHERE ServiceTypeId = @ServiceTypeId

			INSERT INTO MSTServiceTypeMasterAudit (ServiceTypeId, ServiceTypeName, 
				DatauniqueID, ActivityType, IsActive, LastModifiedOn, LastModifiedBy,
				AuditOperationOn, AuditOperationBy)

			SELECT ServiceTypeId, ServiceTypeName, 
				DatauniqueID, CASE WHEN(@isOnlyDelete <> 'Y') THEN ActivityType ELSE 'D' END, IsActive, LastModifiedOn, LastModifiedBy,
				GETDATE(), @UserID 
			FROM MSTServiceTypeMaster
			WHERE ServiceTypeId = @ServiceTypeId

		IF @isOnlyDelete <> 'Y'
		BEGIN 
			SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM MSTServiceTypeMaster WHERE ServiceTypeId = @ServiceTypeId

			UPDATE MSTServiceTypeMaster SET 
				ServiceTypeName = @ServiceTypeName, 
				DatauniqueID = @NewDatauniqueID, 
				ActivityType = 'M', 
				IsActive = @IsActive, 
				LastModifiedOn = GETDATE(), 
				LastModifiedBy = @UserID
					WHERE ServiceTypeId = @ServiceTypeId
			END 
			ELSE 
				DELETE FROM MSTServiceTypeMaster WHERE ServiceTypeId = @ServiceTypeId
		END 
	END
END



GO
