USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spPRMSTProductGet]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- select * from PRMSTProductMaster

/* 

select * FROM PRMSTProductMaster 
[spPRMSTProductGet] 0, '', '', 0, 'Y', 0 
*/
CREATE PROCEDURE [dbo].[spPRMSTProductGet] (
	@Mode							INT = 0, /*0 = MASTER, 1 =DROPDOWN*/
	@ProductId						BIGINT = -1, 
	@ProductName					VARCHAR(5000) = '',
	@OrganizationCode				VARCHAR(50)= '', 
	@CategoryId						BIGINT = -1,
	@IsActive  						VARCHAR(1) = '',
	@LanguageId						BIGINT = -1
)
AS 
BEGIN 
	DECLARE @OrganizationId BIGINT

	if(isnull(rtrim(ltrim(@OrganizationCode)), '') <> '')
	begin
		SELECT @OrganizationId = OrganizationId FROM [dbo].[URMSTOrganizationMaster] WHERE [OrganizationCode] = @OrganizationCode
	end

	IF @Mode = 0
	BEGIN 
		SELECT B.ProductId, B.CategoryId, C.ServiceOrGoods, B.CountryID,
			B.DatauniqueID, B.ActivityType, C.HSNCode, C.SACCode, B.IsActive, B.LastModifiedOn, B.LastModifiedBy,
			isnull((SELECT Value FROM LangMSTMasterDataMultiLanguage MLP WHERE MLP.LanguageId = ISNULL(@LanguageId, -1) AND MLP.MasterTablePrefix = 'P' AND MLP.MasterIDField = B.ProductId), B.ProductName) ProductName, 
			isnull((SELECT Value FROM LangMSTMasterDataMultiLanguage MLCA1 WHERE MLCA1.LanguageId = ISNULL(@LanguageId, -1) AND MLCA1.MasterTablePrefix = 'CA' AND MLCA1.MasterIDField = B.CategoryId), C.CategoryName) CategoryName,
			C1.name CountryName
		FROM PRMSTProductMaster B 
		left JOIN mstCountries C1 ON C1.id = B.CountryID 
		left JOIN PRMSTCategoryMaster C ON C.CategoryId = B.CategoryId

		WHERE ProductId = CASE WHEN(ISNULL(@ProductId, 0) = 0) Then B.ProductId Else @ProductId End
		--AND B.IsActive = CASE WHEN(ISNULL(@IsActive, '') = '') Then B.IsActive Else @IsActive End
		AND isnull(B.CategoryId, 0) = CASE WHEN(ISNULL(@CategoryId, 0) = 0) Then isnull(B.CategoryId, 0) Else @CategoryId End
	END
	ELSE IF @Mode = 1
	BEGIN
		if(ISNULL(@OrganizationId, 0) > 0) -- Business User
		begin
			SELECT a.OrganizationproductId ProductId, 
			ISNULL
			(NULLIF
				(ISNULL
					(NULLIF
						(
							(
								SELECT Value FROM LangMSTMasterDataMultiLanguage MLP 
								WHERE MLP.LanguageId = ISNULL(@LanguageId, -1) 
								AND MLP.MasterTablePrefix = 'P' 
								AND MLP.MasterIDField = B.ProductId
							), ''
						), B.ProductName
					), ''
				), a.Name
			) ProductName  
			FROM prMAPOrganizationproduct a 
			left join PRMSTProductMaster b on a.ProductId = b.ProductId
			left JOIN mstCountries C1 ON C1.id = B.CountryID 
			left JOIN PRMSTCategoryMaster C ON C.CategoryId = B.CategoryId

			WHERE A.OrganizationproductId = CASE WHEN(ISNULL(@ProductId, 0) = 0) Then A.OrganizationproductId Else @ProductId End
			--AND B.IsActive = CASE WHEN(ISNULL(@IsActive, '') = '') Then B.IsActive Else @IsActive End
		    AND isnull(B.CategoryId, 0) = CASE WHEN(ISNULL(@CategoryId, 0) = 0) Then isnull(B.CategoryId, 0) Else @CategoryId End
			AND isnull(a.OrganizationID, 0) = CASE WHEN(ISNULL(@OrganizationId, 0) = 0) Then isnull(a.OrganizationID, 0) Else @OrganizationId End
		END
		else if(ISNULL(@OrganizationId, 0) = 0)
		begin
			SELECT B.ProductId, 
				isnull((SELECT Value FROM LangMSTMasterDataMultiLanguage MLP WHERE MLP.LanguageId = ISNULL(@LanguageId, -1) AND MLP.MasterTablePrefix = 'P' AND MLP.MasterIDField = B.ProductId), B.ProductName) ProductName 
			FROM PRMSTProductMaster B 
			left JOIN mstCountries C1 ON C1.id = B.CountryID 
			left JOIN PRMSTCategoryMaster C ON C.CategoryId = B.CategoryId

			WHERE b.ProductId = CASE WHEN(ISNULL(@ProductId, 0) = 0) Then B.ProductId Else @ProductId End
			--AND B.IsActive = CASE WHEN(ISNULL(@IsActive, '') = '') Then B.IsActive Else @IsActive End
			AND isnull(B.CategoryId, 0) = CASE WHEN(ISNULL(@CategoryId, 0) = 0) Then isnull(B.CategoryId, 0) Else @CategoryId End
		END
	END
END







GO
