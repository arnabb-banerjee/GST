USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spUrMstUserModerateFunctionMappingSave]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spUrMstUserModerateFunctionMappingSave]
(
	@UserId bigint,
	@FunctionId bigint,
	@ErrorMessage varchar(500) out
)
as
begin
	insert into [urMapModerateUserFunctionAudit] (FunctionId, UserId)
	select FunctionId, UserId from [urMapModerateUserFunction] where UserId = @UserId

	delete from [urMapModerateUserFunction] where UserId = @UserId
	
	insert into [urMapModerateUserFunction] (FunctionId, UserId)
	values (@FunctionId, @UserId)
end
GO
