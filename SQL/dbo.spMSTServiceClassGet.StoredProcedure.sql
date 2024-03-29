USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spMSTServiceClassGet]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[spMSTServiceClassGet] (
	@ServiceClassId		BIGINT, 
	@ServiceClassName	VARCHAR(500)
)
AS 
BEGIN 

	SELECT B.ServiceClassId, B.ServiceClassName, B.DatauniqueID, B.ActivityType, B.IsActive, B.LastModifiedOn, B.LastModifiedBy
	FROM MSTServiceClassMaster B 
		
	WHERE ServiceClassId = CASE WHEN(ISNULL(@ServiceClassId, -1) < 1) Then B.ServiceClassId Else @ServiceClassId End
	AND (
			(@ServiceClassName = '') OR 
			(@ServiceClassName <> '' AND ServiceClassName LIKE '%' + @ServiceClassName + '%')
		)
	
END


GO
