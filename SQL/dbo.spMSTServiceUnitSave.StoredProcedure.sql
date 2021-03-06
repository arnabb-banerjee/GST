USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spMSTServiceUnitSave]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spMSTServiceUnitSave] (
	@ServiceUnitId				BIGINT, 
	@ServiceUnitName			VARCHAR(5000),
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

			SELECT COUNT(*) x FROM [dbo].[prMAPOrganizationproduct] WHERE Unit = @ServiceUnitId
			UNION
			SELECT COUNT(*) x FROM [dbo].[prMAPOrganizationproductAudit] WHERE Unit = @ServiceUnitId)A

		IF (@IsExists > 0)
		BEGIN 
			SELECT @AllowSaveDeleteData = 'N'
			SELECT @ErrorMessage = 'Data can not be deleted as the same is assigned to another module'
		END
	END 
	ELSE 
	BEGIN 
		IF ((SELECT COUNT(ServiceUnitId) x FROM MSTServiceUnitMaster WHERE ServiceUnitName = @ServiceUnitName AND ((@ServiceUnitId < 1) OR (@ServiceUnitId > 0 AND ServiceUnitId <> @ServiceUnitId))) > 0)
		BEGIN 
			SELECT @AllowSaveDeleteData = 'N'
			SELECT @ErrorMessage = 'Data can not be saved as the same name is assigned for another record'
		END
	END 

	IF (@AllowSaveDeleteData = 'Y')
	BEGIN		
		IF @ServiceUnitId < 1
		BEGIN 
			SELECT @NewDatauniqueID = 1

			INSERT INTO MSTServiceUnitMaster (ServiceUnitName, DatauniqueID, ActivityType, IsActive, LastModifiedOn, LastModifiedBy)
			VALUES(@ServiceUnitName, @NewDatauniqueID, 'A', @IsActive, GETDATE(), @UserID)
		END
		ELSE
		BEGIN 
			SELECT @OldDatauniqueID = DatauniqueID FROM MSTServiceUnitMaster WHERE ServiceUnitId = @ServiceUnitId

			INSERT INTO MSTServiceUnitMasterAudit (ServiceUnitId, ServiceUnitName, 
				DatauniqueID, ActivityType, IsActive, LastModifiedOn, LastModifiedBy,
				AuditOperationOn, AuditOperationBy)

			SELECT ServiceUnitId, ServiceUnitName, 
				DatauniqueID, CASE WHEN(@isOnlyDelete <> 'Y') THEN ActivityType ELSE 'D' END, IsActive, LastModifiedOn, LastModifiedBy,
				GETDATE(), @UserID 
			FROM MSTServiceUnitMaster
			WHERE ServiceUnitId = @ServiceUnitId

		IF @isOnlyDelete <> 'Y'
		BEGIN 
			SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM MSTServiceUnitMaster WHERE ServiceUnitId = @ServiceUnitId

			UPDATE MSTServiceUnitMaster SET 
				ServiceUnitName = @ServiceUnitName, 
				DatauniqueID = @NewDatauniqueID, 
				ActivityType = 'M', 
				IsActive = @IsActive, 
				LastModifiedOn = GETDATE(), 
				LastModifiedBy = @UserID
					WHERE ServiceUnitId = @ServiceUnitId
			END 
			ELSE 
				DELETE FROM MSTServiceUnitMaster WHERE ServiceUnitId = @ServiceUnitId
		END 
	END
END


GO
