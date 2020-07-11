USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[GetStaticValueList]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetStaticValueList]
(
	@Key varchar(50) = ''
)
AS 
BEGIN
	SELECT ID, DatauniqueID, [Key], [Value], LastModifiedBy, LastModifiedOn 
	FROM MSTStaticValues 
	WHERE [Key] = 
	CASE WHEN(@Key = '') THEN [Key] ELSE @Key END 
END


GO
