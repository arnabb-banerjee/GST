USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spSUPPMSTSupplierGet]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spSUPPMSTSupplierGet] (
	@Mode					INT = 0,/*0 = MASTER, 1 =DROPDOWN*/
	@SuppID					VARCHAR(50),
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
		SELECT @OrganizationId = OrganizationId FROM [dbo].[URMSTOrganizationMaster] WHERE [OrganizationCode] = @OrganizationCode
	end

	DECLARE @LanguageTable table (MasterIDField varchar(50), MasterTablePrefix varchar(3), Value varchar(5000))

	INSERT INTO LangMSTMasterDataMultiLanguage (L.MasterIDField, L.MasterTablePrefix, L.Value)
	SELECT L.MasterIDField, L.MasterTablePrefix, L.Value 
	FROM LangMSTMasterDataMultiLanguage L
	WHERE L.MasterTablePrefix IN ('C', 'S')
	AND L.LanguageId = @LanguageId

	SELECT A.SuppID, A.IsActive,
		A.Title, A.FirstName, A.MiddleName, A.LastName, A.Safix, A.DOB, A.Sex,
		A.IsSubSupplier, A.ParentSuppID, A.BillingWith,
		O.OrganizationCode,O.OrganizationName,
		B.EmailID, B.Mobile, B.Street1, B.Street2, B.City, B.State, B.Country, B.Website,
		C.GSTRegistrationType, C.GSTIN, C.TaxRegNo, C.CSTRegNo, C.PANNo, C.Terms,
		D.PrefferedPaymentMethod, D.PrefferedDeliveryMethod,
		E.Notes,
		F.OpeningBalance, F.AsOfDate,
		(SELECT ISNULL(Value, S.name) FROM LangMSTMasterDataMultiLanguage WHERE MasterTablePrefix = 'S' AND MasterIDField = B.State), 
		(SELECT ISNULL(Value, CO.name) FROM LangMSTMasterDataMultiLanguage WHERE MasterTablePrefix = 'CO' AND MasterIDField = B.Country)
	FROM SuppMSTSupplierMaster A
		INNER JOIN SuppMSTSupplierContact B ON B.SuppID = A.SuppID
		INNER JOIN SuppMSTSupplierTaxRelated C ON C.SuppID = A.SuppID
		INNER JOIN SuppMSTSupplierPreferedMethods D ON D.SuppID = A.SuppID
		INNER JOIN SuppMSTSupplierNotes E ON E.SuppID = A.SuppID
		INNER JOIN SuppMSTSupplierOpeningBalance F ON F.SuppID = A.SuppID
		INNER JOIN URMSTOrganizationMaster O ON O.OrganizationID = A.OrganizationID
		LEFT JOIN MSTStates S ON S.id = B.State
		LEFT JOIN MSTCountries CO ON CO.id = B.Country
		LEFT JOIN SuppMSTSupplierMaster B2 ON A.ParentSuppID = B2.SuppID
		
	WHERE A.SuppID = @SuppID
	AND A.OrganizationID = CASE WHEN(ISNULL(@OrganizationID, '') = '') Then A.OrganizationID Else @OrganizationID End
	AND A.IsActive = CASE WHEN(ISNULL(@IsActive, '') = '') Then A.IsActive Else @IsActive End
	
END


GO
