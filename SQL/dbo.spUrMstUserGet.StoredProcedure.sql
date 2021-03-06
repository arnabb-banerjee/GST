USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spUrMstUserGet]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [dbo].[spUrMstUserGet] 'M', '1'
--[dbo].[spUrMstUserGet] 'M', '<NEW>'
--[dbo].[spUrMstUserGet] 'E','','792000A3-4A44-40BD-9FFB-137F076AC301'

CREATE PROCEDURE [dbo].[spUrMstUserGet] (
	@UserType					VARCHAR(1),
	@UserCode					VARCHAR(50),
	@OrganizationCode			VARCHAR(50) = ''
)
AS 
BEGIN 
	DECLARE @UserID BIGINT, @OrganizationId BIGINT
	DECLARE @OrganizationName varchar(500)
	SELECT @OrganizationId = 0

	IF(ISNULL(RTRIM(LTRIM(@OrganizationCode)), '') <> '')
	BEGIN
		SELECT @OrganizationId = OrganizationId, @OrganizationName = OrganizationName FROM [URMSTOrganizationMaster] WHERE [OrganizationCode] = @OrganizationCode
	END

	SELECT B.UserCode, convert(datetime, b.dob, 103) dob, B.FirstName, b.activitytype, B.MiddleName, B.LastName, B.AccessAllowed, B.DatauniqueID, 
		B.Sex, B.Title, B.Safix, B.LastModifiedOn, B.LastModifiedBy, B.IsActive, B.IsApproved, B.DatauniqueID,
		E.EmailID, E.EmailConfirmationSentOn, E.EmailVerifiedOn, E.IsActive IsEmailActive, E.IsEmailConfirmationSend, E.IsEmailVerified,

		M.Mobile, M.MobileConfirmationSentOn, M.MobileVerifiedOn, M.IsActive IsMobileActive, M.IsMobileConfirmationSent, M.IsMobileVerified,
		c.Street1, c.Street2, c.City, c.[State], c.Country,
		B.UserType, 
		Y.OrganizationName,
		Y.OrganizationCode,
		CASE WHEN(ISNULL(RTRIM(LTRIM(B.UserType)),'') = 'M') THEN 'Moderate' 
			 WHEN(ISNULL(RTRIM(LTRIM(B.UserType)),'') = 'R') THEN 'Registered' 
			 WHEN(ISNULL(RTRIM(LTRIM(B.UserType)),'') = 'E') THEN 'Employee' 
			 WHEN(ISNULL(RTRIM(LTRIM(B.UserType)),'') = 'A') THEN 'Accountant' 
		END UserTypeName,   
		(select name from mstStates where id = c.State) StateName,
		(select name from mstCountries where id = c.Country) CountryName,
		
		CASE WHEN(ISNULL(B.Title, '')<>'') THEN B.Title + ' ' ELSE '' END + ISNULL(B.FirstName, '') + 
		CASE WHEN(ISNULL(B.MiddleName, '')<>'') THEN ' ' + B.MiddleName ELSE '' END + 
		CASE WHEN(ISNULL(B.LastName, '')<>'') THEN ' ' + B.LastName ELSE '' END DisplayName 

	
	FROM urMstUserRegisteredMaster B 
		LEFT JOIN urMapRegisteredUserFunction X ON X.UserID = B.UserID
		LEFT JOIN urMstOrganizationMaster Y ON Y.OrganizationID = X.OrganizationID
		LEFT JOIN urMstUserRegisteredEmail E ON E.UserID = b.UserID
		LEFT JOIN urMstUserRegisteredMobile M ON M.UserID = b.UserID
		LEFT JOIN urMstUserRegisteredContact C ON C.UserID = b.UserID
	WHERE ISNULL(B.UserCode, '') = CASE WHEN(ISNULL(@UserCode, '') = '') Then ISNULL(B.UserCode, '') Else @UserCode End
		AND ISNULL(B.UserType, '') = CASE WHEN(ISNULL(@UserType, '') = '') Then ISNULL(B.UserType, '') Else @UserType End
		AND (
				(ISNULL(RTRIM(LTRIM(@OrganizationCode)), '') = '') OR
				(
					B.UserID IN (SELECT FN.UserID FROM [urMapRegisteredUserFunction] FN WHERE FN.OrganizationId = @OrganizationId)
				)
		)

	IF(@UserCode != '')
	BEGIN
		SELECT @UserID = UserId from urMstUserRegisteredMaster where UserCode = @UserCode
			
		SELECT @OrganizationId = (SELECT TOP 1 OrganizationId FROM [urMapRegisteredUserFunction] WHERE UserID = @UserID)
			
		SELECT @OrganizationName = (SELECT TOP 1 OrganizationName FROM urMstOrganizationMaster WHERE OrganizationID = @OrganizationId)
			
		SELECT @OrganizationCode = (SELECT TOP 1 OrganizationCode FROM urMstOrganizationMaster WHERE OrganizationID = @OrganizationId)

		SELECT M.BranchID, M.BranchName, M.FunctionId, M.FunctionName, case when isnull(fn.FunctionId, '') <> '' then 'Y' else '' end isSelected,
			@OrganizationCode OrganizationCode, @OrganizationName OrganizationName
		FROM 
		(
			SELECT FunctionId, FunctionName, BranchID, BranchName 
			FROM (SELECT FunctionId, FunctionName FROM
				(
					SELECT 0 FunctionId, '' FunctionName where 0 = (select count(*) from urMstFunctions)
					UNION ALL
					SELECT FunctionId, FunctionName FROM urMstFunctions	
				)F
			)F, (SELECT BranchID, BranchName FROM
				(
					SELECT 0 BranchID, '' BranchName where 0 = (select count(*) from urMstOrganizationBranch where OrganizationId = @OrganizationId)

					UNION ALL
					SELECT BranchID, BranchName FROM urMstOrganizationBranch where OrganizationId = @OrganizationId
				)B
			)B
		) M
		LEFT JOIN [urMapRegisteredUserFunction] FN on FN.FunctionID = M.FunctionId 
				/*and FN.BranchId = M.BranchID */
				and FN.OrganizationId = @OrganizationId
				and FN.UserID = @UserID 
	END
END


GO
