USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[SPURMSTEmailVerification]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPURMSTEmailVerification]
(
	@EmailVerification			VARCHAR(550)
)
AS
BEGIN
	UPDATE urMstUserRegisteredEmail 
		SET IsEmailVerified = 'Y', 
			IsActive = 'Y', 
			EmailVerifiedOn = GETDATE()  
	  WHERE VerificationKey = @EmailVerification
END
GO
