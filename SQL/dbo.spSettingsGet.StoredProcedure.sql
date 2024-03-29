USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spSettingsGet]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spSettingsGet] (
	@OrganizationCode						VARCHAR(50) = '' 
)
AS 
BEGIN 
	DECLARE @OrganizationId BIGINT
	SELECT @OrganizationId = 0

	IF(ISNULL(RTRIM(LTRIM(@OrganizationCode)), '') <> '')
	BEGIN
		SELECT @OrganizationId = OrganizationId FROM [dbo].[URMSTOrganizationMaster] WHERE [OrganizationCode] = @OrganizationCode
	END

	SELECT O.[OrganizationCode]
		,OS.[DataUniqueId],[mc_isAllowedMutyCurrency]
		,OS.[mc_CurrencyList],[p_isAllowedOnlinePayment],[p_BankAccountNumber]
		,OS.[p_BankAccountHolder],[p_BankAccountIFSCCode],[p_BankAccountIMCRCode]
		,OS.[p_BankAccountIBranchName],[p_BankAccountIBankName],[p_PaypalAccountID]
		,OS.[c_isAllowedMultyLanguage],[an_isAllowedAlert_GSTDate],[an_isAllowedAlert_PaidMembership]
		,OS.[an_AlertText_GSTDate],[an_AlertText_PaidMembership]
		,OS.[LastModifiedOn],OS.[LastModifiedBy], OrganizationName
		,OS.CompanyName, OS.Email,OS.[Mobile], OS.[Address], OS.City, OS.[State] 
		,OS.Country, OS.Website, OS.CIN, OS.PAN, OS.DefaultEmail, OS.SMTP
	FROM [OrganizationSettings] OS
	INNER JOIN [urMstOrganizationMaster] O ON O.OrganizationID = OS.OrganizationId
	WHERE OS.OrganizationId = @OrganizationId		
END




GO
