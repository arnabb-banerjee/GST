USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[GetCountryStateList]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCountryStateList]
(
	@CountryID bigint = -1
)
AS 
BEGIN
	IF (@CountryID < 1)
		SELECT ID, NAME FROM MSTCountries
	ELSE
		SELECT ID, NAME FROM MSTStates WHERE countryid = @CountryID
END


GO
