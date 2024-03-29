USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spMSTServiceUnitGet]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[spMSTServiceUnitGet] (
	@ServiceUnitId		BIGINT, 
	@ServiceUnitName	VARCHAR(500)
)
AS 
BEGIN 

	SELECT B.ServiceUnitId, B.ServiceUnitName, B.DatauniqueID, B.ActivityType, B.IsActive, B.LastModifiedOn, B.LastModifiedBy
	FROM MSTServiceUnitMaster B 
		
	WHERE ServiceUnitId = CASE WHEN(ISNULL(@ServiceUnitId, -1) < 1) Then B.ServiceUnitId Else @ServiceUnitId End
	AND (
			(@ServiceUnitName = '') OR 
			(@ServiceUnitName <> '' AND ServiceUnitName LIKE '%' + @ServiceUnitName + '%')
		)
	
END


GO
