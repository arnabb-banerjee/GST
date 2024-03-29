USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spURMAPOrganizationBranchUserRoleSave]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spURMAPOrganizationBranchUserRoleSave] (
	@RoleId							BIGINT, 
	@BranchId						BIGINT,
	@OrganisationCode				VARCHAR(50) = NULL, 
	@UserCode						VARCHAR(50) = NULL, 
	@IsActive  						VARCHAR(5) = NULL,
	@DoneBy							VARCHAR(50) = NULL,
	@isOnlyDelete					VARCHAR(1) = '',
	@NewDatauniqueID 				BIGINT = 0 OUT,
	@ErrorMessage 					VARCHAR(50) = '' OUT
)
AS 
BEGIN 

	DECLARE @OldDatauniqueID BIGINT
	DECLARE @OrganizationId BIGINT
	DECLARE @UserID BIGINT
	DECLARE @AllowSaveDeleteData VARCHAR(1)
	select @AllowSaveDeleteData = 'Y'
	DECLARE @IsExists INT

	SELECT @OrganizationId = 0
	SELECT @UserID = 0

	SELECT @UserID = UserID from URMSTUserRegisteredMaster where UserCode = @UserCode
	
	IF(ISNULL(@OrganisationCode, '') <> '')
	BEGIN
		select @OrganizationId = OrganizationID from URMSTOrganizationMaster where OrganizationCode = @OrganisationCode
	END
	
	--IF (@isOnlyDelete = 'Y')
	--BEGIN 
	--	SELECT @IsExists = MAX(x) FROM (
	--		SELECT COUNT(RoleId) x FROM URMAPOrganizationBranchUserRole WHERE RoleId = @RoleId
	--		UNION
	--		SELECT COUNT(RoleId) x FROM URMAPOrganizationBranchUserRoleAudit WHERE RoleId = @RoleId)A

	--	IF (@IsExists > 0)
	--	BEGIN 
	--			SELECT @AllowSaveDeleteData = 'N'
	--			SELECT @ErrorMessage = 'Data can not be deleted as the same is assigned to another module'
	--	END
	--END 
	--ELSE 
	--BEGIN 
		IF ((SELECT COUNT(*) x 
			 FROM URMAPOrganizationBranchUserRole 
			 WHERE RoleID = @RoleId 
				 AND ISNULL(BranchID, 0) = ISNULL(@BranchId, 0) 
				 AND ISNULL(OrganizationID, 0) = ISNULL(@OrganizationId, 0) 
				 AND UserID = @UserID) > 0)
		BEGIN 
				SELECT @AllowSaveDeleteData = 'N'
				SELECT @ErrorMessage = 'Data can not be saved as the same combination is already available'
		END
	--END 
	
	IF (@AllowSaveDeleteData = 'Y')
	BEGIN		
		BEGIN TRANSACTION T1;
		BEGIN TRY
		
			IF @RoleId < 1
			BEGIN 
				SELECT @NewDatauniqueID = 1
		
				INSERT INTO [dbo].URMAPOrganizationBranchUserRole
					([DatauniqueID],RoleID, BranchID, OrganizationID, UserID, [LastModifiedOn], [LastModifiedBy])
				VALUES
					(@NewDatauniqueID, @RoleId, NULLIF(@BranchId, 0), NULLIF(@OrganizationId, 0), @UserID, GETDATE(), @DoneBy)
			END
			ELSE IF(@RoleId > 0 AND @isOnlyDelete = 'Y')
			BEGIN
				INSERT INTO [dbo].URMAPOrganizationBranchUserRoleAudit
					([DatauniqueID],RoleID, BranchID, OrganizationID, UserID, [LastModifiedOn], [LastModifiedBy])
				SELECT [DatauniqueID],RoleID, BranchID, OrganizationID, UserID, [LastModifiedOn], [LastModifiedBy] 
				FROM URMAPOrganizationBranchUserRole
				WHERE RoleID = @RoleId 
				 AND ISNULL(BranchID, 0) = ISNULL(@BranchId, 0) 
				 AND ISNULL(OrganizationID, 0) = ISNULL(@OrganizationId, 0) 
				 AND UserID = @UserID

				DELETE FROM URMAPOrganizationBranchUserRole 
				WHERE RoleID = @RoleId 
				 AND ISNULL(BranchID, 0) = ISNULL(@BranchId, 0) 
				 AND ISNULL(OrganizationID, 0) = ISNULL(@OrganizationId, 0) 
				 AND UserID = @UserID
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
