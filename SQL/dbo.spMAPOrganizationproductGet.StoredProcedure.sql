USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spMAPOrganizationproductGet]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- select * from PRMSTCategoryMaster
--[spMAPOrganizationproductGet] 1, 0, '', 0, 1
--[spMAPOrganizationproductGet] 0, 0, '', 0, 1
CREATE PROCEDURE [dbo].[spMAPOrganizationproductGet] (
	@Mode					INT = 0, 
	@OrganizationproductId	BIGINT = 0,
	@ProductId				BIGINT = 0, 
	@OrganizationCode		VARCHAR(50),
	@CountryId				BIGINT = 0,
	@LanguageId				BIGINT = 0
)
AS 
BEGIN 
	DECLARE @OrganizationId BIGINT
	SELECT @OrganizationId = OrganizationID FROM urMstOrganizationMaster WHERE OrganizationCode = @OrganizationCode
	
	IF @Mode = 0
	BEGIN
		SELECT A.OrganizationproductId, B.ProductId, C.CategoryId, CASE WHEN(ISNULL(A.Name,'') <> '') THEN a.Name ELSE ISNULL(MLP.Value, B.ProductName) END ProductName, 
				ISNULL(MLCA.Value, C.CategoryName) CategoryName, C.ServiceOrGoods, 
				CASE WHEN (ISNULL(C.ServiceOrGoods, '') = 'G') THEN C.HSNCode ELSE C.SACCode END HSNSACCode,
			B.DatauniqueID, B.ActivityType, B.IsActive, B.LastModifiedOn, B.LastModifiedBy,
			A.[Name],A.[Description],A.[SKU],A.[Unit],A.[Class],A.[AbatementPercentage],A.[ServiceType], 
			A.[SalePrice],A.[isInclusiveTax],A.[AvailableQty],A.[IncomeAccount],A.[SupplierId],
			A.[PreferredSupplierId],A.[ReverseCharge],A.[PurchaseTax],A.[SaleTax], c1.name CountryName, 
			XX.[FileData], XX.[FileName], XX.FileType, XX.ImageId,
			(select OrganizationCode from urMstOrganizationMaster where OrganizationID = a.OrganizationID) OrganizationCode,
			-- Added by Partha on 14/08/2019
			(select OrganizationName from urMstOrganizationMaster where OrganizationID = a.OrganizationID) OrganizationName
			-- End 14/08/2019
		FROM prMAPOrganizationproduct A
		LEFT JOIN PRMSTProductMaster B ON A.ProductId = b.ProductId
		LEFT JOIN mstCountries C1 ON C1.id = A.CountryID 
		LEFT JOIN prMstCategoryMaster C ON ISNULL(C.IsExpenseType, '') = 'Y' AND C.CategoryId = (case when(A.CategoryId>0) THEN A.CategoryId ELSE B.CategoryId END)
		LEFT JOIN LangMSTMasterDataMultiLanguage MLP ON MLP.MasterTablePrefix = 'P' AND ISNULL(MLP.LanguageId, 0) = ISNULL(@LanguageId, 0) AND MLP.MasterIDField = B.ProductId AND ISNULL(MLP.Value, '') <> ''		
		LEFT JOIN LangMSTMasterDataMultiLanguage MLCA ON MLCA.MasterTablePrefix = 'CA' AND ISNULL(MLCA.LanguageId, 0) = ISNULL(@LanguageId, 0) AND MLCA.MasterIDField = B.CategoryId AND ISNULL(MLCA.Value, '') <> ''		
		LEFT JOIN (SELECT TOP 1 OrganizationproductId, ImageId, [Filename], [FileData], [Filetype] from prMAPOrganizationproductImage WHERE OrganizationproductId = @OrganizationproductId) XX ON XX.OrganizationproductId = A.OrganizationproductId
		--LEFT join prMAPCategoryTaxDefination D ON D.CategoryId = B.CategoryId AND D.CountryID = @CountryId
		WHERE ISNULL(B.CountryID, 0) = CASE WHEN(ISNULL(@CountryId, 0) = 0) Then ISNULL(B.CountryID, 0) Else @CountryId End
		AND A.isExpense IS NULL /* Added by Partha 30/08/2019 */
		AND ISNULL(A.OrganizationID, 0) = CASE WHEN(ISNULL(@OrganizationId, 0) = 0) Then ISNULL(A.OrganizationID, 0) Else ISNULL(@OrganizationId, 0) End
		AND ISNULL(A.OrganizationproductId, 0) = CASE WHEN(ISNULL(@OrganizationproductId, 0) = 0) Then ISNULL(A.OrganizationproductId, 0) Else ISNULL(@OrganizationproductId, 0) End
	END
	ELSE IF @Mode = 1
	BEGIN
		SELECT A.OrganizationproductId, P.ProductId, P.CategoryId, ISNULL(MLP.Value, P.ProductName) ProductName, ISNULL(MLCA.Value, C.CategoryName) CategoryName, C.ServiceOrGoods, 
				CASE WHEN (ISNULL(C.ServiceOrGoods, '') = 'G') THEN C.HSNCode ELSE C.SACCode END HSNSACCode,
			CASE WHEN(ISNULL(CAST(A.ProductId AS BIGINT), 0)) > 0 THEN 'Y' ELSE 'N' END isExist,
			P.DatauniqueID, P.ActivityType, P.IsActive, P.LastModifiedOn, P.LastModifiedBy,
			A.[Name],A.[Description],A.[SKU],A.[Unit],A.[Class],A.[AbatementPercentage],A.[ServiceType], 
			A.[SalePrice],A.[isInclusiveTax],A.[AvailableQty],A.[IncomeAccount],A.[SupplierId],
			A.[PreferredSupplierId],A.[ReverseCharge],A.[PurchaseTax],A.[SaleTax], c1.name CountryName,  
			XX.[FileData], XX.[FileName], XX.FileType, XX.ImageId,
			(select OrganizationCode from urMstOrganizationMaster where OrganizationID = A.OrganizationID) OrganizationCode,
			-- Added by Partha on 14/08/2019
			(select OrganizationName from urMstOrganizationMaster where OrganizationID = a.OrganizationID) OrganizationName
			-- End 14/08/2019
		FROM prMstproductMaster P
		LEFT JOIN prMstCategoryMaster C ON C.CategoryId = P.CategoryId AND ISNULL(C.IsExpenseType, '') = 'Y'
		LEFT JOIN mstCountries C1 ON C1.id = P.CountryID 
		LEFT JOIN prMAPOrganizationproduct A ON P.ProductId = A.ProductId
		-- Added by Partha on 14/08/2019
			AND ISNULL(A.OrganizationID, 0) = CASE WHEN(ISNULL(@OrganizationId, 0) = 0) Then ISNULL(A.OrganizationID, 0) Else ISNULL(@OrganizationId, 0) End
			AND ISNULL(A.OrganizationproductId, 0) = CASE WHEN(ISNULL(@OrganizationproductId, 0) = 0) Then ISNULL(A.OrganizationproductId, 0) Else ISNULL(@OrganizationproductId, 0) End
		-- End 14/08/2019

		LEFT JOIN LangMSTMasterDataMultiLanguage MLP ON MLP.MasterTablePrefix = 'P' AND ISNULL(MLP.LanguageId, 0) = ISNULL(@LanguageId, 0) AND MLP.MasterIDField = P.ProductId AND ISNULL(MLP.Value, '') <> ''		
		LEFT JOIN LangMSTMasterDataMultiLanguage MLCA ON MLCA.MasterTablePrefix = 'CA' AND ISNULL(MLCA.LanguageId, 0) = ISNULL(@LanguageId, 0) AND MLCA.MasterIDField = A.CategoryId AND ISNULL(MLCA.Value, '') <> ''
		-- Added by Partha on 14/08/2019
		LEFT JOIN (SELECT TOP 1 OrganizationproductId, ImageId, [Filename], [FileData], [Filetype] from prMAPOrganizationproductImage WHERE OrganizationproductId = @OrganizationproductId) 
		XX ON XX.OrganizationproductId = A.OrganizationproductId	
		-- End 03/08/2019
		--LEFT join prMAPCategoryTaxDefination D ON D.CategoryId = B.CategoryId AND D.CountryID = @CountryId
		WHERE ISNULL(P.CountryID, 0) = CASE WHEN(ISNULL(@CountryId, 0) = 0) Then ISNULL(P.CountryID, 0) Else @CountryId End
		AND A.isExpense IS NULL /* Added by Partha 30/08/2019 */
	END
END

-- exec spMAPOrganizationproductGet 1, 0, 0, '', 0, 0, ''




GO
