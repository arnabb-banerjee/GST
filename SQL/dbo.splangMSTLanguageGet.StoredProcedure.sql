USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[splangMSTLanguageGet]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[splangMSTLanguageGet] (
	@LanguageId		BIGINT, 
	@LanguageName	VARCHAR(500)
)
AS 
BEGIN 

	SELECT B.LanguageId, B.LanguageName, B.DatauniqueID, B.ActivityType, B.IsActive, B.LastModifiedOn, B.LastModifiedBy
	FROM langMSTLanguageMaster B 
		
	WHERE LanguageId = CASE WHEN(ISNULL(@LanguageId, -1) < 1) Then B.LanguageId Else @LanguageId End
	AND (
			(@LanguageName = '') OR 
			(@LanguageName <> '' AND LanguageName LIKE '%' + @LanguageName + '%')
		)
	
END


GO
