USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[sp_defultvalues]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--sp_defultvalues

CREATE PROCEDURE [dbo].[sp_defultvalues]
AS
BEGIN
	BEGIN --Login Related
		EXEC sp_addmessage 50005, 10, 'Account validity has been end. Go to the forgot password page.',	null, null, replace	
		EXEC sp_addmessage 50006, 10, 'User not found',	null, null, replace	
	END
END
GO
