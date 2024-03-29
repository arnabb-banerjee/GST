USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spTaxMapCountryCategoryGet]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spTaxMapCountryCategoryGet] (	
	@Mode					INT = 0,
	@TaxDefinationID		BIGINT = 0,
	@CategoryId				BIGINT = 0,
	@CountryId				BIGINT = 0
)
AS 
BEGIN 
	SELECT s.TaxDefinationID, s.TaxName, c.CategoryId, c.CategoryName, s.id CountryId, s.name CountryName,
		   ISNULL(M.ApplicableType, s.ApplicableType) ApplicableType, m.[Percentage],m.[DatauniqueID],m.[LastModifiedOn],m.[LastModifiedBy], 
			case when isnull(m.TaxDefinationID, 0) > 0 then 'Y' else 'N' end IsExist  
	FROM prMstCategoryMaster C 
	CROSS JOIN (SELECT a.id, a.name, b.TaxDefinationID, T.TaxName, b.ApplicableType 
				FROM mstCountries a, taxMapTaxCountry b, taxMstTaxMaster t
				WHERE a.id = b.CountryID
				AND B.TaxDefinationID = T.TaxDefinationID)S
	LEFT JOIN [taxMapTaxCountryCategory] m on m.CountryID = s.id
												and m.CategoryId = c.CategoryId
												and m.TaxDefinationID = s.TaxDefinationID
	WHERE C.CategoryId = CASE WHEN ISNULL(@CategoryId, 0) = 0 THEN C.CategoryId ELSE @CategoryId END
	AND	  C.CountryID = m.CountryID
	AND	  C.CountryID = CASE WHEN ISNULL(@CountryId, 0) = 0 THEN C.CountryID ELSE @CountryId END
	AND	  S.id = CASE WHEN ISNULL(@CountryId, 0) = 0 THEN S.id ELSE @CountryId END
	AND	  S.TaxDefinationID = CASE WHEN ISNULL(@TaxDefinationID, 0) = 0 THEN S.TaxDefinationID ELSE @TaxDefinationID END
	AND   ISNULL(C.IsExpenseType, '') <> 'Y'
END



GO
