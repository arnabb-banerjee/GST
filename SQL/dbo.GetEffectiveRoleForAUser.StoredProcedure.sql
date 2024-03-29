USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[GetEffectiveRoleForAUser]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
[GetEffectiveRoleForAUser] '1C0E80ED-A137-49EC-9152-2D3FBAF0C42A', ''

*/


CREATE procedure [dbo].[GetEffectiveRoleForAUser] 
(
	@UserCode VARCHAR(50),
	@BranchId BIGINT = 0
)
as
begin
	DECLARE @UserID BIGINT
	
	SELECT @UserID = USERID FROM urMstUserRegisteredMaster WHERE UserCode = @UserCode

	select [CanManageCategory],[CanManageRole],[CanManageUserRegistered],[CanManageUserModerate],[CanManageOrganization]
		  ,[CanManageBranch],[CanManageCustomer],[CanManageBill],[CanManageLanguage],[CanManageDefineLanguage],[CanManageStaticvalue]
		  ,[CanManageTerms],[isOnlyForModerateUsers],[isForMembershipView],[CanManageProduct],[CanManageFunction]
	from urMstRole 
	where RoleID in (
			select distinct roleid
			from 
			urMstRole R, urMstFunctions f, urMapRegisteredUserFunction uf, urMstUserRegisteredMaster u

			where charindex(cast(r.RoleID as varchar(50)), f.ApplicableRoles) > 0
			and f.FunctionId = uf.FunctionID 
			and uf.UserID = u.UserID
			and uf.UserID = @UserID
			--and isnull(uf.BranchID, '') = case when(ISNULL(@BranchId, '') <> '') then ISNULL(@BranchId, '') else uf.BranchID end 
			and (
					(u.UserType = 'M') or
					(u.UserType = 'R' and isnull(f.IsForModerate, '') <> 'Y')
				 )
	)
end
GO
