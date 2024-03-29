USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spMSTServiceTypeGet]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[spMSTServiceTypeGet] (
	@ServiceTypeId		BIGINT, 
	@ServiceTypeName	VARCHAR(500)
)
AS 
BEGIN 

	SELECT B.ServiceTypeId, B.ServiceTypeName, B.DatauniqueID, B.ActivityType, B.IsActive, B.LastModifiedOn, B.LastModifiedBy
	FROM MSTServiceTypeMaster B 
		
	WHERE ServiceTypeId = CASE WHEN(ISNULL(@ServiceTypeId, -1) < 1) Then B.ServiceTypeId Else @ServiceTypeId End
	AND (
			(@ServiceTypeName = '') OR 
			(@ServiceTypeName <> '' AND ServiceTypeName LIKE '%' + @ServiceTypeName + '%')
		)
	
END


GO
