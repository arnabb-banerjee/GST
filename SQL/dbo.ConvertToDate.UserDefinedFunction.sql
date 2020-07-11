USE [GST_DEV_V2]
GO
/****** Object:  UserDefinedFunction [dbo].[ConvertToDate]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ConvertToDate] (
	@Date VARCHAR(50)
)
RETURNS DateTime
AS
BEGIN
	RETURN Convert(Date, @Date, 103) 
END


GO
