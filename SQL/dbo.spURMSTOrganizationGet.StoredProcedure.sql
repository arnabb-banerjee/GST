USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spURMSTOrganizationGet]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[dbo].[spURMSTOrganizationGet] '', '', ''

CREATE PROCEDURE [dbo].[spURMSTOrganizationGet] (
	@Mode				INT = 0,/*0 = MASTER, 1 =DROPDOWN*/
	@OrganizationCode	VARCHAR(50), 
	@IsActive  			VARCHAR(1),
	@UserCode			VARCHAR(50)
)
AS 
BEGIN 
	
	DECLARE @OrganizationId BIGINT
	DECLARE @UserId BIGINT

	if(isnull(rtrim(ltrim(@OrganizationCode)), '') <> '')
	begin
		SELECT @OrganizationId = OrganizationId FROM [URMSTOrganizationMaster] WHERE [OrganizationCode] = @OrganizationCode
	end

	if(isnull(rtrim(ltrim(@UserCode)), '') <> '')
	begin
		SELECT @UserId = UserId FROM [urMstUserRegisteredMaster] WHERE [UserCode] = @UserCode
	end

	IF @Mode = 0
	BEGIN 
		SELECT B.OrganizationCode, OrganizationName, 
			B.DatauniqueID, B.ActivityType, B.IsActive, B.LastModifiedOn, B.LastModifiedBy
		FROM URMSTOrganizationMaster B 

		WHERE ISNULL(B.OrganizationID, 0) = CASE WHEN(ISNULL(@OrganizationID, 0) = 0) Then ISNULL(B.OrganizationID, 0) Else @OrganizationID End
		AND ISNULL(B.IsActive, '') = CASE WHEN(ISNULL(@IsActive, '') = '') Then ISNULL(B.IsActive, '') Else @IsActive End
		AND (
				(ISNULL(@UserID, 0) = 0) OR 
				(ISNULL(@UserID, 0) <> 0 AND B.OrganizationID IN (
									SELECT OrganizationID 
									FROM [urMapRegisteredUserFunction]
									WHERE ISNULL(UserID, 0) = ISNULL(@UserID, 0)
				))
			)

		order by OrganizationName asc
	END
	ELSE IF @Mode = 1
	BEGIN
		SELECT B.OrganizationCode, OrganizationName
		FROM URMSTOrganizationMaster B 

		WHERE ISNULL(B.OrganizationID, 0) = CASE WHEN(ISNULL(@OrganizationID, 0) = 0) Then ISNULL(B.OrganizationID, 0) Else @OrganizationID End
		AND ISNULL(B.IsActive, '') = CASE WHEN(ISNULL(@IsActive, '') = '') Then ISNULL(B.IsActive, '') Else @IsActive End
		AND (
				(ISNULL(@UserID, 0) = 0) OR 
				(ISNULL(@UserID, 0) <> 0 AND B.OrganizationID IN (
									SELECT OrganizationID 
									FROM [urMapRegisteredUserFunction]
									WHERE ISNULL(UserID, 0) = ISNULL(@UserID, 0)
				))
			)
		order by OrganizationName asc
	END
END


GO
