USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spgetPercentageforaCategoryRangeTypeCountry]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spgetPercentageforaCategoryRangeTypeCountry] (
	@CategoryId				BIGINT, 
	@LocationRangeType	VARCHAR(10), 
	@CountryID				BIGINT
)
AS 
BEGIN 

	SELECT CategoryId, @LocationRangeType LocationRangeType, b.TaxDefinationID 
	FROM PRMAPCategoryTaxDefination A, PRMSTAllowedTaxDefinationCountryWise B
	where a.TaxDefinationID = b.TaxDefinationID
	AND a.CategoryId = CASE WHEN(ISNULL(@CategoryId, 0) = 0) then a.CategoryId else @CategoryId end
	AND b.LocationRangeType = CASE WHEN(ISNULL(@LocationRangeType, 0) = 0) then b.LocationRangeType else @LocationRangeType end
	AND b.CountryID = CASE WHEN(ISNULL(@CountryID, 0) = 0) then b.CountryID else @CountryID end
END


GO
