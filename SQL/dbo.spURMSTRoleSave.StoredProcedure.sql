USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spURMSTRoleSave]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[spURMSTRoleSave] 1, 'dadas1', '12345', '12345', '12345', '12345', '12345', '12345', '12345', '12345', '12345', '12345', '12345', '12345', 'n', 'n', 'Y', '', 0, ''
--SELECT * FROM URMSTROLE 

CREATE PROCEDURE [dbo].[spURMSTRoleSave] (
	@RoleId							BIGINT, 
	@RoleName						VARCHAR(50),
	@CanManageCategory				VARCHAR(5) = NULL, /*1=View, 2=Add, 3= Edit, 4=Delete, 5 = Audit*/
	@CanManageProduct				VARCHAR(5) = NULL, 
	@CanManageRole					VARCHAR(5) = NULL,
	@CanManageFunction				VARCHAR(5) = NULL,
	@CanManageUserRegistered		VARCHAR(5) = NULL,
	@CanManageUserModerate			VARCHAR(5) = NULL,
	@CanManageOrganization			VARCHAR(5) = NULL,
	@CanManageBranch				VARCHAR(5) = NULL,
	@CanManageCustomer				VARCHAR(5) = NULL,
	@CanManageBill					VARCHAR(5) = NULL,
	@CanManageLanguage				VARCHAR(5) = NULL,
	@CanManageDefineLanguage		VARCHAR(5) = NULL,
	@CanManageStaticvalue			VARCHAR(5) = NULL,
	@CanManageTerms				VARCHAR(5) = NULL,
	@isOnlyForModerateUsers			VARCHAR(5) = NULL,
	@isForMembershipView			VARCHAR(5) = NULL,
	@IsActive  						VARCHAR(5) = NULL,
	@UserCode						VARCHAR(50) = NULL,
	@isOnlyDelete					VARCHAR(1) = 0,
	@NewDatauniqueID 				BIGINT = 0 OUT,
	@ErrorMessage 					VARCHAR(50) = '' OUT
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
			SELECT COUNT(RoleId) x FROM URMAPOrganizationBranchUserRole WHERE RoleId = @RoleId
			UNION
			SELECT COUNT(RoleId) x FROM URMAPOrganizationBranchUserRoleAudit WHERE RoleId = @RoleId)A

		IF (@IsExists > 0)
		BEGIN 
				SELECT @AllowSaveDeleteData = 'N'
				SELECT @ErrorMessage = 'Data can not be deleted as the same is assigned to another module'
		END
	END 
	ELSE 
	BEGIN 
		IF ((SELECT COUNT(RoleId) x FROM URMSTRole WHERE RoleName = @RoleName AND ((@RoleId < 1) OR (@RoleId > 0 AND RoleId <> @RoleId))
			) > 0)
		BEGIN 
				SELECT @AllowSaveDeleteData = 'N'
				SELECT @ErrorMessage = 'Data can not be saved as the same name is assigned for another record'
		END
	END 
	
	IF (@AllowSaveDeleteData = 'Y')
	BEGIN		
		BEGIN TRANSACTION T1;
		BEGIN TRY
		
			IF @RoleId < 1
			BEGIN 
				SELECT @NewDatauniqueID = 1
		
				INSERT INTO [dbo].[URMSTRole]
					([DatauniqueID],[ActivityType],[RoleName],
					CanManageCategory, CanManageProduct, CanManageRole, CanManageFunction, CanManageUserRegistered, CanManageUserModerate, CanManageOrganization,	
					CanManageBranch, CanManageCustomer, CanManageBill, CanManageLanguage, CanManageDefineLanguage,
					CanManageStaticvalue, CanManageTerms,
					[isOnlyForModerateUsers], [isForMembershipView], [IsActive], [LastModifiedOn], [LastModifiedBy])
				VALUES
					(@NewDatauniqueID,'A',@RoleName,
					@CanManageCategory, @CanManageProduct, @CanManageRole, @CanManageFunction, @CanManageUserRegistered, @CanManageUserModerate, @CanManageOrganization,	
					@CanManageBranch, @CanManageCustomer, @CanManageBill, @CanManageLanguage, @CanManageDefineLanguage,
					@CanManageStaticvalue, @CanManageTerms,
					@isOnlyForModerateUsers, @isForMembershipView, @IsActive, GETDATE(), @UserID)
			END
			ELSE
			BEGIN 
				SELECT @OldDatauniqueID = DatauniqueID FROM URMSTRole WHERE RoleId = @RoleId
					
				INSERT INTO URMSTRoleAudit ([DatauniqueID],[ActivityType],[RoleName],
					CanManageCategory, CanManageProduct, CanManageRole, CanManageFunction, CanManageUserRegistered, CanManageUserModerate, CanManageOrganization,	
					CanManageBranch, CanManageCustomer, CanManageBill, CanManageLanguage, CanManageDefineLanguage,
					CanManageStaticvalue, CanManageTerms,
					[isOnlyForModerateUsers], [isForMembershipView],
					[IsActive], [LastModifiedOn], [LastModifiedBy], AuditOperationOn, AuditOperationBy)
				SELECT [DatauniqueID],CASE WHEN(@isOnlyDelete <> 'Y') THEN ActivityType ELSE 'D' END,[RoleName],
					CanManageCategory, CanManageProduct, CanManageRole, CanManageFunction, CanManageUserRegistered, CanManageUserModerate, CanManageOrganization,	
					CanManageBranch, CanManageCustomer, CanManageBill, CanManageLanguage, CanManageDefineLanguage,
					CanManageStaticvalue, CanManageTerms,
					[isOnlyForModerateUsers],[isForMembershipView],
					[IsActive],[LastModifiedOn],[LastModifiedBy],
					GETDATE(), @UserID 
				FROM URMSTRole
				WHERE RoleId = @RoleId
				   				  
				IF @isOnlyDelete <> 'Y'
				BEGIN 
					SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM URMSTRole WHERE RoleId = @RoleId
		
					UPDATE URMSTRole SET 
				  			RoleName = @RoleName, 
				  			DatauniqueID = @NewDatauniqueID,
							CanManageCategory = @CanManageCategory,
							CanManageProduct = @CanManageProduct, 
							CanManageRole = @CanManageRole, 
							CanManageFunction = @CanManageFunction,
							CanManageUserRegistered = @CanManageUserRegistered, 
							CanManageUserModerate = @CanManageUserModerate, 
							CanManageOrganization = @CanManageOrganization,	
							CanManageBranch = @CanManageBranch, 
							CanManageCustomer = @CanManageCustomer, 
							CanManageBill = @CanManageBill, 
							CanManageLanguage = @CanManageLanguage, 
							CanManageDefineLanguage = @CanManageDefineLanguage,
							CanManageStaticvalue = @CanManageStaticvalue, 
							CanManageTerms = @CanManageTerms, 
							isOnlyForModerateUsers = @isOnlyForModerateUsers,
							isForMembershipView = @isForMembershipView,
				  			ActivityType = 'M', 
				  			IsActive = @IsActive, 
				  			LastModifiedOn = GETDATE(), 
				  			LastModifiedBy = @UserID
					WHERE RoleId = @RoleId
				END 
				ELSE 
					DELETE FROM URMSTRole WHERE RoleId = @RoleId

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
