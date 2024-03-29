USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spSupMSTSupplierGet]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
[spSupMSTSupplierGet] '', '', '', '', '', ''
*/
CREATE PROCEDURE [dbo].[spSupMSTSupplierGet] (
	@SupID					VARCHAR(50),
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

	SELECT A.SupID, A.IsActive,
		A.Title, A.FirstName, A.MiddleName, A.LastName, A.Safix, A.DOB, A.Sex,
		A.IsSubSupplier, A.ParentSupID, (B2.FirstName + ' ' + B2.LastName) ParentSupName, A.BillingWith,
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
		
	FROM SupMSTSupplierMaster A
		LEFT JOIN SupMSTSupplierContact B ON ISNULL(B.SupID, 0) = ISNULL(A.SupID, 0)
		LEFT JOIN SupMSTSupplierTaxRelated C ON ISNULL(C.SupID, 0) = ISNULL(A.SupID, 0)
		LEFT JOIN SupMSTSupplierPreferedMethods D ON ISNULL(D.SupID, 0) = ISNULL(A.SupID, 0)
		LEFT JOIN SupMSTSupplierNotes E ON ISNULL(E.SupID, 0) = ISNULL(A.SupID, 0)
		LEFT JOIN SupMSTSupplierOpeningBalance F ON ISNULL(F.SupID, 0) = ISNULL(A.SupID, 0)
		LEFT JOIN SupMSTSupplierAddressBilling BI ON ISNULL(BI.SupID, 0) = ISNULL(A.SupID, 0)
		LEFT JOIN SupMSTSupplierAddressShipping SH ON ISNULL(SH.SupID, 0) = ISNULL(A.SupID, 0)
		LEFT JOIN URMSTOrganizationMaster O ON ISNULL(O.OrganizationID, 0) = ISNULL(A.OrganizationID, 0)
		LEFT JOIN MSTStates S ON ISNULL(S.id, 0) = ISNULL(B.State, 0)
		LEFT JOIN MSTCountries CO ON ISNULL(CO.id, 0) = ISNULL(B.Country, 0)
		LEFT JOIN SupMSTSupplierMaster B2 ON ISNULL(A.ParentSupID, 0) = ISNULL(B2.SupID, 0)
		
	WHERE ISNULL(A.SupID, 0) = CASE WHEN(ISNULL(NULLIF(@SupID, ''), 0) = 0) Then ISNULL(A.SupID, 0) Else ISNULL(NULLIF(@SupID, ''), 0) End
	AND ISNULL(A.OrganizationID, 0) = CASE WHEN(ISNULL(NULLIF(@OrganizationID, ''), 0) = 0) Then ISNULL(A.OrganizationID, 0) Else ISNULL(NULLIF(@OrganizationID, ''), 0) End
	AND ISNULL(A.IsActive, '') = CASE WHEN(ISNULL(@IsActive, '') = '') Then ISNULL(A.IsActive, '') Else ISNULL(@IsActive, '') End
	
END


GO
