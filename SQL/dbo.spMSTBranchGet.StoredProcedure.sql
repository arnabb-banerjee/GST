USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spMSTBranchGet]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[spMSTBranchGet] '', ''
CREATE PROCEDURE [dbo].[spMSTBranchGet]
(
	@ID						varchar(50),
	@OrganizationCode 		varchar(50)
)
AS 
BEGIN
	DECLARE @OrganizationId BIGINT

	IF(ISNULL(RTRIM(LTRIM(@OrganizationCode)), '') <> '')
	BEGIN
		SELECT @OrganizationId = OrganizationId FROM [URMSTOrganizationMaster] WHERE [OrganizationCode] = @OrganizationCode
	END

	SELECT BranchID, M.DatauniqueID, M.[Name] BranchName, m.IsMainBranch, M.[IsActive], M.Street1, M.Street2, M.City, M.[State], M.Country, M.PIN, M.LastModifiedBy, M.LastModifiedOn,
	O.OrganizationCode, O.OrganizationName, S.name StateName, c.name CountryName, M.ActivityType
	FROM MSTBranch M
	INNER JOIN MSTCountries C ON M.Country = C.id 
	INNER JOIN MSTStates S ON M.State = S.id
	LEFT JOIN URMSTOrganizationMaster O ON O.OrganizationID = M.OrganizationID
	WHERE ISNULL(M.OrganizationID, 0) = CASE WHEN(ISNULL(NULLIF(@OrganizationID, ''), 0) = 0) Then ISNULL(M.OrganizationID, 0) Else ISNULL(NULLIF(@OrganizationID, ''), 0) End
	AND ISNULL(M.BranchID, 0) = CASE WHEN(ISNULL(NULLIF(@ID, ''), 0) = 0) Then ISNULL(M.BranchID, 0) Else ISNULL(NULLIF(@ID, ''), 0) End
END


GO
