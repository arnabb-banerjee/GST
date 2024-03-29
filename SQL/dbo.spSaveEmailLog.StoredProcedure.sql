USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spSaveEmailLog]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spSaveEmailLog](
	@UserID				bigint
    ,@MailOption		varchar(50)
    ,@Subject			varchar(500)
    ,@Body				varchar(max)
    ,@From				varchar(200)
    ,@TO				varchar(max)
    ,@CC				varchar(max)
    ,@ErrorMessage		varchar(max)
    ,@InnerException	varchar(max)
    ,@StackStress		varchar(max)
)
AS
BEGIN
	INSERT INTO [dbo].[EmailLog]
			   ([UserID],[MailOption],[Subject],[Body],[FromEmail],[TO],[CC]
			   ,[ErrorMessage],[InnerException],[StackStress],[EmailSentOn])
		 VALUES
			   (@UserID,@MailOption,@Subject,@Body,@From,@TO,@CC
			   ,@ErrorMessage,@InnerException,@StackStress,GETDATE())

END


GO
