USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spMSTBanksGet]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--[spMSTBanksGet] '', ''
CREATE PROCEDURE [dbo].[spMSTBanksGet]
(
	@ID						varchar(50),
	@OrganizationCode 		varchar(50)
)
AS 
BEGIN
	DECLARE @OrganizationId BIGINT

	if(isnull(rtrim(ltrim(@ID)), '') <> '')
	begin
		if(isnull(rtrim(ltrim(@OrganizationCode)), '') <> '')
		begin
			SELECT @OrganizationId = OrganizationId FROM [URMSTOrganizationMaster] WHERE [OrganizationCode] = @OrganizationCode
		end

		SELECT M.ID, M.[DatauniqueID],M.[ActivityType],M.[Name],M.[IsActive],O.[OrganizationCode], O.OrganizationName,
			M.[CorpID],M.[Address],M.[IFSCCode],M.[MCRCode],
			M.[LastModifiedOn],M.[LastModifiedBy]
		FROM MSTBanks M
		LEFT JOIN URMSTOrganizationMaster O ON O.OrganizationID = M.OrganizationID AND M.OrganizationID = @OrganizationId
		WHERE ISNULL(M.OrganizationID, 0) = CASE WHEN(ISNULL(NULLIF(@OrganizationID, ''), 0) = 0) Then ISNULL(M.OrganizationID, 0) Else ISNULL(NULLIF(@OrganizationID, ''), 0) End
	end
	else 
	begin
		SELECT M.ID, M.[DatauniqueID],M.[ActivityType],M.[Name],M.[IsActive],O.[OrganizationCode], O.OrganizationName,
			M.[CorpID],M.[Address],M.[IFSCCode],M.[MCRCode],
			M.[LastModifiedOn],M.[LastModifiedBy]
		FROM MSTBanks M
		LEFT JOIN URMSTOrganizationMaster O ON O.OrganizationID = M.OrganizationID AND M.OrganizationID = @OrganizationId
		WHERE ISNULL(M.OrganizationID, 0) = CASE WHEN(ISNULL(NULLIF(@OrganizationID, ''), 0) = 0) Then ISNULL(M.OrganizationID, 0) Else ISNULL(NULLIF(@OrganizationID, ''), 0) End
		AND ID = @ID
	end
END



GO
