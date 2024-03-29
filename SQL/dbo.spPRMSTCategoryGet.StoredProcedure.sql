USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spPRMSTCategoryGet]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- select * from PRMSTCategoryMaster
--[spPRMSTCategoryGet] 0, '', 0, 0, '', 0
--[spPRMSTCategoryGet] 0, 0, '', 0, 0, '', 0
--[spPRMSTCategoryGet] 1, 0, 'N', 0, 0, '', 0

CREATE PROCEDURE [dbo].[spPRMSTCategoryGet] (
	@Mode				INT = 0, --0 = Master page, 1 = Dropdownlist 
	@CategoryId			BIGINT = 0, 
	@WillCarryContent	VARCHAR(1) = '',
	@CountryId			BIGINT = 0,
	@ProductId			BIGINT = 0,
	@IsActive			VARCHAR(1)='',
	@IsExpenseType  	VARCHAR(1) = '',
	@LanguageId			BIGINT = 0
)
AS 
BEGIN 
	IF(@ProductId <> 0)
	BEGIN
		SELECT @CategoryId = CategoryId FROM prMstproductMaster WHERE ProductId = @ProductId
	END

	IF @Mode = 0
	BEGIN
		SELECT B.CategoryId, B.ParentCategoryId, B.WillCarryContent, b.CountryID, c.name CountryName, 
			B.DatauniqueID, B.IsExpenseType, B.ActivityType, B.IsActive, b.WillCarryContent, b.ServiceOrGoods, B.LastModifiedOn, B.LastModifiedBy, b.HSNCode, b.SACCode,
			ISNULL((SELECT MLCA1.Value FROM LangMSTMasterDataMultiLanguage MLCA1 WHERE MLCA1.LanguageId = ISNULL(@LanguageId, -1) AND MLCA1.MasterTablePrefix = 'CA' AND MLCA1.MasterIDField = B.CategoryId), b.CategoryName) CategoryName, 
			ISNULL((SELECT MLCA1.Value FROM LangMSTMasterDataMultiLanguage MLCA1 WHERE MLCA1.LanguageId = ISNULL(@LanguageId, -1) AND MLCA1.MasterTablePrefix = 'CA' AND MLCA1.MasterIDField = B.ParentCategoryId), o.CategoryName) ParentCategoryName
		FROM PRMSTCategoryMaster B 
		left join mstCountries c on c.id = b.CountryID
		LEFT JOIN PRMSTCategoryMaster O ON ISNULL(B.ParentCategoryId, 0) = ISNULL(O.CategoryId, 0)
	
		WHERE B.CategoryId = CASE WHEN(ISNULL(@CategoryId, 0) = 0) Then B.CategoryId Else @CategoryId End
		AND B.CountryID = CASE WHEN(ISNULL(@CountryId, 0) = 0) Then B.CountryID Else @CountryId End
		AND ISNULL(B.IsExpenseType, '') = CASE WHEN(ISNULL(@IsExpenseType, '') = '') Then ISNULL(B.IsExpenseType, '') Else @IsExpenseType End
		
		AND 
		(
			(@IsActive = '') OR
			(@IsActive = 'Y' AND ISNULL(B.IsActive, '') IN ('', 'Y')) OR
			(@IsActive = 'N' AND ISNULL(B.IsActive, '') IN ('N'))
		)
		

		AND 
		(
			(@WillCarryContent = '') OR
			(@WillCarryContent = 'N' AND ISNULL(B.WillCarryContent, '') IN ('', 'N')) OR
			(@WillCarryContent = 'Y' AND ISNULL(B.WillCarryContent, '') IN ('Y'))
		)
	
	END
	ELSE IF @Mode = 1
	BEGIN
		SELECT B.CategoryId, b.CategoryName, B.ParentCategoryId
		FROM PRMSTCategoryMaster B 
		WHERE B.CategoryId = CASE WHEN(ISNULL(@CategoryId, 0) = 0) Then B.CategoryId Else @CategoryId End
		AND B.CountryID = CASE WHEN(ISNULL(@CountryId, 0) = 0) Then B.CountryID Else @CountryId End

		AND 
		(
			(@IsActive = '') OR
			(@IsActive = 'Y' AND ISNULL(B.IsActive, '') IN ('', 'Y')) OR
			(@IsActive = 'N' AND ISNULL(B.IsActive, '') IN ('N'))
		)
		

		AND 
		(
			(@WillCarryContent = '') OR
			(@WillCarryContent = 'N' AND ISNULL(B.WillCarryContent, '') IN ('', 'N')) OR
			(@WillCarryContent = 'Y' AND ISNULL(B.WillCarryContent, '') IN ('Y'))
		)
		
		AND ISNULL(B.IsExpenseType, '') = CASE WHEN(ISNULL(@IsExpenseType, '') = '') Then ISNULL(B.IsExpenseType, '') Else @IsExpenseType End
		order by b.CategoryName
	END
END




GO
