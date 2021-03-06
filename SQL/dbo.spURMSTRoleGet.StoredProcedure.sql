USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spURMSTRoleGet]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- [dbo].[spURMSTRoleGet] 0, 0, '', ''

CREATE PROCEDURE [dbo].[spURMSTRoleGet] (
	@RoleId					BIGINT,
	@BranchId 				BIGINT,
	@IsActive  				VARCHAR(1),
	@UserID					VARCHAR(50)
)
AS 
BEGIN 

	SELECT B.RoleId, B.RoleName,
		CanManageCategory, CanManageRole, CanManageFunction, CanManageUserRegistered, CanManageUserModerate, CanManageOrganization,	
		CanManageBranch, CanManageCustomer, CanManageBill, CanManageLanguage, CanManageDefineLanguage,
		CanManageStaticvalue, CanManageTerms, CanManageProduct,
		[isOnlyForModerateUsers],[isForMembershipView], 
		B.LastModifiedOn, B.LastModifiedBy,[IsActive], ActivityType, DatauniqueID
	FROM URMSTRole B 
	WHERE RoleId = CASE WHEN(ISNULL(@RoleId, 0) = 0) Then B.RoleId Else @RoleId End
	AND IsActive = CASE WHEN(ISNULL(@IsActive, '') = '') Then B.IsActive Else @IsActive End
	AND (
			(@UserID < 1) OR 
			(@UserID > 0 AND B.RoleId IN (
								SELECT RoleId 
								FROM URMAPOrganizationBranchUserRole
								WHERE UserID = @UserID
			))
	)
	AND (
			(@BranchId < 1) OR 
			(@BranchId > 0 AND B.RoleId IN (
								SELECT RoleId 
								FROM URMAPOrganizationBranchUserRole
								WHERE BranchId = @BranchId
			))
	)
	
END


GO
