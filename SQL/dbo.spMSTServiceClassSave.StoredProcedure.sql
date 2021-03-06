USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spMSTServiceClassSave]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spMSTServiceClassSave] (
	@ServiceClassId				BIGINT, 
	@ServiceClassName			VARCHAR(5000),
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
		SELECT @IsExists = MAX(x) FROM (

			SELECT COUNT(*) x FROM [dbo].[prMAPOrganizationproduct] WHERE Class = @ServiceClassId
			UNION
			SELECT COUNT(*) x FROM [dbo].[prMAPOrganizationproductAudit] WHERE Class = @ServiceClassId)A

		IF (@IsExists > 0)
		BEGIN 
			SELECT @AllowSaveDeleteData = 'N'
			SELECT @ErrorMessage = 'Data can not be deleted as the same is assigned to another module'
		END
	END 
	ELSE 
	BEGIN 
		IF ((SELECT COUNT(ServiceClassId) x FROM MSTServiceClassMaster WHERE ServiceClassName = @ServiceClassName AND ((@ServiceClassId < 1) OR (@ServiceClassId > 0 AND ServiceClassId <> @ServiceClassId))) > 0)
		BEGIN 
			SELECT @AllowSaveDeleteData = 'N'
			SELECT @ErrorMessage = 'Data can not be saved as the same name is assigned for another record'
		END
	END 

	IF (@AllowSaveDeleteData = 'Y')
	BEGIN		
		IF @ServiceClassId < 1
		BEGIN 
			SELECT @NewDatauniqueID = 1

			INSERT INTO MSTServiceClassMaster (ServiceClassName, DatauniqueID, ActivityType, IsActive, LastModifiedOn, LastModifiedBy)
			VALUES(@ServiceClassName, @NewDatauniqueID, 'A', @IsActive, GETDATE(), @UserID)
		END
		ELSE
		BEGIN 
			SELECT @OldDatauniqueID = DatauniqueID FROM MSTServiceClassMaster WHERE ServiceClassId = @ServiceClassId

			INSERT INTO MSTServiceClassMasterAudit (ServiceClassId, ServiceClassName, 
				DatauniqueID, ActivityType, IsActive, LastModifiedOn, LastModifiedBy,
				AuditOperationOn, AuditOperationBy)

			SELECT ServiceClassId, ServiceClassName, 
				DatauniqueID, CASE WHEN(@isOnlyDelete <> 'Y') THEN ActivityType ELSE 'D' END, IsActive, LastModifiedOn, LastModifiedBy,
				GETDATE(), @UserID 
			FROM MSTServiceClassMaster
			WHERE ServiceClassId = @ServiceClassId

		IF @isOnlyDelete <> 'Y'
		BEGIN 
			SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM MSTServiceClassMaster WHERE ServiceClassId = @ServiceClassId

			UPDATE MSTServiceClassMaster SET 
				ServiceClassName = @ServiceClassName, 
				DatauniqueID = @NewDatauniqueID, 
				ActivityType = 'M', 
				IsActive = @IsActive, 
				LastModifiedOn = GETDATE(), 
				LastModifiedBy = @UserID
					WHERE ServiceClassId = @ServiceClassId
			END 
			ELSE 
				DELETE FROM MSTServiceClassMaster WHERE ServiceClassId = @ServiceClassId
		END 
	END
END



GO
