USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spCUSMSTCustomerGet]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*
[spCUSMSTCustomerGet] 0, 'C', '', '', 'C3D47953-292B-4662-8F67-2423660953F9', '', '', ''
*/
CREATE PROCEDURE [dbo].[spCUSMSTCustomerGet] (
	@Mode					INT = 0,/*0 = MASTER, 1 =DROPDOWN*/
	@UserType				VARCHAR(1),
	@CusID					VARCHAR(50),
	@BranchId 				VARCHAR(50),
	@OrganizationCode 		varchar(50),
	@IsActive  				VARCHAR(1),
	@UserID					VARCHAR(50),
	@LanguageId				VARCHAR(50)
)
AS 
BEGIN 
	
	DECLARE @OrganizationId BIGINT

	if(isnull(rtrim(ltrim(@OrganizationCode)), '') <> '')
	begin
		SELECT @OrganizationId = OrganizationId FROM [URMSTOrganizationMaster] WHERE [OrganizationCode] = @OrganizationCode
	end

	DECLARE @LanguageTable table (MasterIDField varchar(50), MasterTablePrefix varchar(3), Value varchar(5000))

	INSERT INTO LangMSTMasterDataMultiLanguage (L.MasterIDField, L.MasterTablePrefix, L.Value)
	SELECT L.MasterIDField, L.MasterTablePrefix, L.Value 
	FROM LangMSTMasterDataMultiLanguage L
	WHERE L.MasterTablePrefix IN ('C', 'S')
	AND L.LanguageId = @LanguageId

	IF @Mode = 0
	BEGIN 
		SELECT A.CusID, A.IsActive,
			A.Title, A.FirstName, A.MiddleName, A.LastName, A.Safix, A.DOB, A.Sex,
			A.IsSubCustomer, A.ParentCusID, (B2.FirstName + ' ' + B2.LastName) ParentCusName, A.BillingWith,
			O.OrganizationCode,O.OrganizationName,
			B.EmailID, B.Mobile, B.Street1, B.Street2, B.City, B.State, B.Country, B.Website,
			C.GSTRegistrationType, C.GSTIN, C.TaxRegNo, C.CSTRegNo, C.PANNo, C.Terms,
			D.PrefferedPaymentMethod, D.PrefferedDeliveryMethod,
			E.Notes,
			F.OpeningBalance, F.AsOfDate,
			BI.Street1 BillingStreet1, BI.Street2 BillingStreet2, BI.City BillingCity, BI.State BillingState, BI.Country BillingCountry,
			SH.Street1 ShippingStreet1, SH.Street2 ShippingStreet2, SH.City ShippingCity, SH.State ShippingState, SH.Country ShippingCountry,
			(SELECT ISNULL(Value, S.name) FROM LangMSTMasterDataMultiLanguage WHERE MasterTablePrefix = 'S' AND MasterIDField = B.State)StateName, 
			(SELECT ISNULL(Value, CO.name) FROM LangMSTMasterDataMultiLanguage WHERE MasterTablePrefix = 'CO' AND MasterIDField = B.Country) CountryName,
			(SELECT Value FROM MSTStaticValues WHERE [KEY] = 'GSTRegistrationType' AND id = c.GSTRegistrationType) GSTRegistrationTypeName,
			(SELECT name from MSTTerms where id = c.Terms) TermsName,
			(SELECT Value FROM MSTStaticValues WHERE [KEY] = 'PaymentMethod' AND id = d.PrefferedPaymentMethod) PrefferedPaymentMethodName,
			(SELECT Value FROM MSTStaticValues WHERE [KEY] = 'DeliveryMethod' AND id = D.PrefferedDeliveryMethod) PrefferedDeliveryMethodName,
			(SELECT ISNULL(Value, S.name) FROM LangMSTMasterDataMultiLanguage WHERE MasterTablePrefix = 'S' AND MasterIDField = BI.State)BillingStateName, 
			(SELECT ISNULL(Value, CO.name) FROM LangMSTMasterDataMultiLanguage WHERE MasterTablePrefix = 'CO' AND MasterIDField = BI.Country) BillingCountryName,
			(SELECT ISNULL(Value, S.name) FROM LangMSTMasterDataMultiLanguage WHERE MasterTablePrefix = 'S' AND MasterIDField = SH.State) ShippingStateName, 
			(SELECT ISNULL(Value, CO.name) FROM LangMSTMasterDataMultiLanguage WHERE MasterTablePrefix = 'CO' AND MasterIDField = SH.Country) ShippingCountryName
			,CASE WHEN(ISNULL(A.Title, '')<>'') THEN A.Title + ' ' ELSE '' END +
				ISNULL(A.FirstName, '') + 
				CASE WHEN(ISNULL(A.MiddleName, '')<>'') THEN ' ' + A.MiddleName ELSE '' END + 
				CASE WHEN(ISNULL(A.LastName, '')<>'') THEN ' ' + A.LastName ELSE '' END DisplayName 
		FROM CUSMSTCustomerMaster A
			INNER JOIN OrganizationCustSuppMapping OSCM ON ISNULL(OSCM.UserID, 0) = ISNULL(A.CusID, 0)
			LEFT JOIN CUSMSTCustomerContact B ON ISNULL(B.CusID, 0) = ISNULL(A.CusID, 0)
			LEFT JOIN CUSMSTCustomerTaxRelated C ON ISNULL(C.CusID, 0) = ISNULL(A.CusID, 0)
			LEFT JOIN CUSMSTCustomerPreferedMethods D ON ISNULL(D.CusID, 0) = ISNULL(A.CusID, 0)
			LEFT JOIN CUSMSTCustomerNotes E ON ISNULL(E.CusID, 0) = ISNULL(A.CusID, 0)
			LEFT JOIN CUSMSTCustomerOpeningBalance F ON ISNULL(F.CusID, 0) = ISNULL(A.CusID, 0)
			LEFT JOIN CUSMSTCustomerAddressBilling BI ON ISNULL(BI.CusID, 0) = ISNULL(A.CusID, 0)
			LEFT JOIN CUSMSTCustomerAddressShipping SH ON ISNULL(SH.CusID, 0) = ISNULL(A.CusID, 0)
			LEFT JOIN URMSTOrganizationMaster O ON ISNULL(O.OrganizationID, 0) = ISNULL(OSCM.OrganizationID, 0)
			LEFT JOIN MSTStates S ON ISNULL(S.id, 0) = ISNULL(B.State, 0)
			LEFT JOIN MSTCountries CO ON ISNULL(CO.id, 0) = ISNULL(B.Country, 0)
			LEFT JOIN CUSMSTCustomerMaster B2 ON ISNULL(A.ParentCusID, 0) = ISNULL(B2.CusID, 0)
		
		WHERE ISNULL(OSCM.UserType, '') = CASE WHEN(ISNULL(@UserType, '') = '') Then ISNULL(OSCM.UserType, '') Else ISNULL(@UserType, '') End
		AND ISNULL(A.CusID, 0) = CASE WHEN(ISNULL(NULLIF(@CusID, ''), 0) = 0) Then ISNULL(A.CusID, 0) Else ISNULL(NULLIF(@CusID, ''), 0) End
		AND ISNULL(OSCM.OrganizationID, 0) = CASE WHEN(ISNULL(NULLIF(@OrganizationID, 0), 0) = 0) Then ISNULL(OSCM.OrganizationID, 0) Else ISNULL(NULLIF(@OrganizationID, 0), 0) End
		AND ISNULL(A.IsActive, '') = CASE WHEN(ISNULL(@IsActive, '') = '') Then ISNULL(A.IsActive, '') Else ISNULL(@IsActive, '') End
	END
	ELSE IF @Mode = 1
	BEGIN
		SELECT A.CusID, 
				CASE WHEN(ISNULL(A.Title, '')<>'') THEN A.Title + ' ' ELSE '' END +
				ISNULL(A.FirstName, '') + 
				CASE WHEN(ISNULL(A.MiddleName, '')<>'') THEN ' ' + A.MiddleName ELSE '' END + 
				CASE WHEN(ISNULL(A.LastName, '')<>'') THEN ' ' + A.LastName ELSE '' END DisplayName 
		
		FROM CUSMSTCustomerMaster A
			INNER JOIN OrganizationCustSuppMapping OSCM ON ISNULL(OSCM.UserID, 0) = ISNULL(A.CusID, 0)
		
		WHERE OSCM.UserType = @UserType 
		AND ISNULL(A.CusID, 0) = CASE WHEN(ISNULL(NULLIF(@CusID, ''), 0) = 0) Then ISNULL(A.CusID, 0) Else ISNULL(NULLIF(@CusID, ''), 0) End
		AND ISNULL(A.OrganizationID, 0) = CASE WHEN(ISNULL(NULLIF(@OrganizationID, ''), 0) = 0) Then ISNULL(A.OrganizationID, 0) Else ISNULL(NULLIF(@OrganizationID, ''), 0) End
		AND ISNULL(A.IsActive, '') = CASE WHEN(ISNULL(@IsActive, '') = '') Then ISNULL(A.IsActive, '') Else ISNULL(@IsActive, '') End
	END
END



GO
