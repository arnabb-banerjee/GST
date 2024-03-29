USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[splangValuGet]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*
	splangValuGet '', 'P,CA', '1,12', 'Y'
	splangValuGet '', 'CA', '12', 'Y'
*/
CREATE PROCEDURE [dbo].[splangValuGet]
(
	@MasterIDField			VARCHAR(50) = NULL,
	@MasterTablePrefixs		VARCHAR(50) = NULL,
	@LanguageIds			VARCHAR(50) = NULL,
	@IsActive				VARCHAR(1)  = NULL
)
AS 
BEGIN
	DECLARE @temp AS TABLE ([MasterIDField] VARCHAR(50), [MasterFieldValue] VARCHAR(5000), [MasterTablePrefix]  VARCHAR(2), [LanguageId]  BIGINT, [Value] VARCHAR(5000))
	DECLARE @item VARCHAR(2)

	DECLARE vendorcursor CURSOR FOR
	SELECT items FROM dbo.split(@MasterTablePrefixs, ',') 

	OPEN vendorcursor;
	FETCH NEXT FROM vendorcursor INTO @item 
	
	WHILE @@FETCH_STATUS = 0  
	BEGIN
		IF(@item = 'P')
		BEGIN
			INSERT INTO @temp (MasterIDField, MasterFieldValue, LanguageId, MasterTablePrefix, Value)
			SELECT ProductId, ProductName, items, 'P', (SELECT Value FROM [langMstMasterDataMultilanguage] C 
														WHERE c.MasterIDField = a.ProductId 
														AND c.MasterTablePrefix = 'P' 
														AND c.LanguageId = b.items) 
			FROM prMstproductMaster A, (SELECT items FROM dbo.Split(@LanguageIds, ',') ) B 
		END	
		IF(@item = 'CA')
		BEGIN
			INSERT INTO @temp (MasterIDField, MasterFieldValue, LanguageId, MasterTablePrefix, Value)
			SELECT CategoryId, CategoryName, items, 'CA', (SELECT Value FROM [langMstMasterDataMultilanguage] C 
														WHERE c.MasterIDField = a.CategoryId 
														AND c.MasterTablePrefix = 'CA' 
														AND c.LanguageId = b.items) 
			FROM prMstCategoryMaster A, (SELECT items FROM dbo.Split(@LanguageIds, ',') ) B 
		END	
		
		FETCH NEXT FROM vendorcursor INTO @item 
	END
	
	DEALLOCATE vendorcursor

	SELECT A.MasterIDField, MasterFieldValue, A.LanguageId, A.MasterTablePrefix, C.Value, b.LanguageName
	FROM @temp a
	LEFT JOIN langMstlanguageMaster B ON A.LanguageId = B.LanguageId
	LEFT JOIN [langMstMasterDataMultilanguage] C ON c.MasterIDField = a.MasterIDField 
				AND c.MasterTablePrefix = A.MasterTablePrefix 
				AND C.LanguageId = A.LanguageId
END


GO
