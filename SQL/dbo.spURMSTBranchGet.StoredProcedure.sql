USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spURMSTBranchGet]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spURMSTBranchGet] (
    @BranchId				BIGINT = -1, 
    @BranchName				VARCHAR(5000),
    @OrganizationCode		varchar(50), 
    @IsMainBranch			VARCHAR(1), 
    @State					BIGINT, 
    @Country				BIGINT,
    @IsActive  				VARCHAR(1),
    @UserID					BIGINT,
    @LanguageId				BIGINT
)
AS 
BEGIN 
	DECLARE @OrganizationID BIGINT

	if(isnull(rtrim(ltrim(@OrganizationCode)), '') <> '')
	begin
		SELECT @OrganizationId = OrganizationId FROM [dbo].[URMSTOrganizationMaster] WHERE [OrganizationCode] = @OrganizationCode
	end


    SELECT B.BranchId, B.BranchName, 
	B.OrganizationID, 
	B.IsMainBranch, B.Street1, B.Street2,
	B.City, B.State, B.Country,
	B.DatauniqueID, B.ActivityType, B.IsActive, B.LastModifiedOn, B.LastModifiedBy,
	City, ISNULL(MLS.Value, S.name) StateName, ISNULL(MLCO.Value, CO.name) CountryName,
	O.OrganizationName
    FROM URMSTOrganizationBranch B 
	INNER JOIN URMSTOrganizationMaster O ON O.OrganizationID = B.OrganizationID
	INNER JOIN MSTStates S ON S.id = B.State
	INNER JOIN MSTCountries CO ON CO.id = B.Country
	LEFT JOIN LangMSTMasterDataMultiLanguage MLC ON MLC.LanguageId = ISNULL(@LanguageId, -1) AND MLC.MasterTablePrefix = 'C' AND MLC.MasterIDField = B.City
	LEFT JOIN LangMSTMasterDataMultiLanguage MLS ON MLS.LanguageId = ISNULL(@LanguageId, -1) AND MLS.MasterTablePrefix = 'S' AND MLS.MasterIDField = B.State
	LEFT JOIN LangMSTMasterDataMultiLanguage MLCO ON MLCO.LanguageId = ISNULL(@LanguageId, -1) AND MLCO.MasterTablePrefix = 'CO' AND MLCO.MasterIDField = B.Country

	WHERE BranchId = CASE WHEN(ISNULL(@BranchId, 0) < 1) Then B.BranchId Else @BranchId End
		AND B.OrganizationID = CASE WHEN(ISNULL(@OrganizationID, '') = '') Then B.OrganizationID Else @OrganizationID End
		AND IsMainBranch = CASE WHEN(ISNULL(@IsMainBranch, '') = '') Then B.IsMainBranch Else @IsMainBranch End
		AND State = CASE WHEN(ISNULL(@State, '') = '') Then B.State Else @State End
		AND Country = CASE WHEN(ISNULL(@Country, '') = '') Then B.Country Else @Country End
		AND B.IsActive = CASE WHEN(ISNULL(@IsActive, '') = '') Then B.IsActive Else @IsActive End
		AND (
			(@UserID = '') OR 
			(@UserID <> '' AND B.BranchId IN (
				SELECT BranchId 
				FROM URMAPOrganizationBranchUserRoleAudit
				WHERE UserID = @UserID
			    )
			)
		)
		AND (
			(@BranchName = '') OR 
			(@BranchName <> '' AND BranchName LIKE '%' + @BranchName + '%')
		)
	
END


GO
