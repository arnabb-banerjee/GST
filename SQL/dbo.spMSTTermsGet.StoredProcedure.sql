USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spMSTTermsGet]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[spMSTTermsGet] '', ''
CREATE PROCEDURE [dbo].[spMSTTermsGet]
(
	@ID						varchar(50),
	@OrganizationCode 		varchar(50)
)
AS 
BEGIN
	DECLARE @OrganizationId BIGINT

	if(isnull(rtrim(ltrim(@OrganizationCode)), '') <> '')
	begin
		SELECT @OrganizationId = OrganizationId FROM [URMSTOrganizationMaster] WHERE [OrganizationCode] = @OrganizationCode
	end

	SELECT ID, DatauniqueID, [Name], DueInFixedNumberDays, DueInCertainDayOfMonth, DueInNextMonth, 
	Discount, LastModifiedBy, LastModifiedOn 
	FROM MSTTerms
	WHERE ISNULL(OrganizationID, 0) = CASE WHEN(ISNULL(NULLIF(@OrganizationID, ''), 0) = 0) Then ISNULL(OrganizationID, 0) Else ISNULL(NULLIF(@OrganizationID, ''), 0) End
	AND ISNULL(ID, 0) = CASE WHEN(ISNULL(NULLIF(@ID, ''), 0) = 0) Then ISNULL(ID, 0) Else ISNULL(NULLIF(@ID, ''), 0) End
END


GO
