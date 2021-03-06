USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spLanguageCountryGet]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[spLanguageCountryGet](
	@Id bigint,
	@LanguageId bigint,
	@CountryId bigint,
	@Visibility varchar(1)
)
AS
BEGIN
	SELECT A.id, A.[LanguageId],A.[DatauniqueID],A.[Country] [CountryId],[Visibility],isnull([Priority], 0) [Priority],
		A.[LastModifiedOn],A.[LastModifiedBy],A.[CreatedOn],A.[CreatedBy],
		L.[LanguageName], C.name [CountryName]
	FROM [langMAPlanguageCountry] A
	INNER JOIN langMstlanguageMaster L on A.LanguageId = L.LanguageId
	INNER JOIN mstCountries C ON C.id = A.Country
	WHERE A.Id = CASE WHEN(ISNULL(@Id, 0) = 0) THEN A.Id ELSE @Id END 
	AND A.LanguageId = CASE WHEN(ISNULL(@LanguageId, 0) = 0) THEN A.LanguageId ELSE @LanguageId END 
	AND A.Country = CASE WHEN(ISNULL(@CountryId, 0) = 0) THEN A.Country ELSE @CountryId END
	AND ISNULL(A.Visibility, '') = CASE WHEN(ISNULL(@Visibility, '') = '') THEN ISNULL(a.Visibility, '') ELSE @Visibility END
END


GO
