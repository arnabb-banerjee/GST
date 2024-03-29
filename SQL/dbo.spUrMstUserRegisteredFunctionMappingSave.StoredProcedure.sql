USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spUrMstUserRegisteredFunctionMappingSave]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE procedure [dbo].[spUrMstUserRegisteredFunctionMappingSave]
(
	@UserCode varchar(50),
	@DatauniqueID bigint,
	@FunctionId bigint,
	@BranchId bigint,
	@OrganizationId bigint,
	@DoneBy bigint,
	@ErrorMessage varchar(500) out
)
as
begin
	DECLARE @UserId BIGINT

	SELECT @UserId = UserId FROM urMstUserRegisteredMaster WHERE UserCode  = @UserCode

	IF ISNULL(RTRIM(LTRIM(@DoneBy)), 0) = 0
		SELECT @DoneBy = @UserId

	INSERT INTO [urMapRegisteredUserFunctionAudit] (DataUniqueID, FunctionId, UserId, BranchID, OrganizationID, LastModifiedBy, LastModifiedOn)
	SELECT DatauniqueID, FunctionId, UserId, BranchID, OrganizationID, LastModifiedBy, LastModifiedOn
	FROM [urMapModerateUserFunction] where UserId = @UserId

	DELETE FROM [urMapModerateUserFunction] where UserId = @UserId
	
	INSERT INTO [urMapModerateUserFunction] (DataUniqueID, FunctionId, UserId, BranchID, OrganizationID, LastModifiedBy, LastModifiedOn)
	VALUES (@DatauniqueID, @FunctionId, @UserId, nullif(@BranchId, ''), nullif(@OrganizationId, ''), @DoneBy, GETDATE() )
end


GO
