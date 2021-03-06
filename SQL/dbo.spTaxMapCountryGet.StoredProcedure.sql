USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spTaxMapCountryGet]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spTaxMapCountryGet] (
	@Mode					INT = 0,
	@TaxDefinationID		BIGINT = 0,
	@CountryId				BIGINT = 0
)
AS
BEGIN 
	SELECT a.id CountryId, a.name CountryName, t.TaxDefinationID, T.TaxName, b.ApplicableType, 
			case when isnull(b.TaxDefinationID, 0) > 0 then 'Y' else 'N' end IsExist  
		FROM taxMapTaxCountry b 
		inner join mstCountries a on b.CountryID = a.id 
		inner join taxMstTaxMaster t on b.TaxDefinationID = t.TaxDefinationID 
	
	WHERE a.id = CASE WHEN ISNULL(@CountryId, 0) = 0 THEN a.id ELSE @CountryId END
	AND	t.TaxDefinationID = CASE WHEN ISNULL(@TaxDefinationID, 0) = 0 THEN t.TaxDefinationID ELSE @TaxDefinationID END
END
GO
