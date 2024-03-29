USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spUrMstUserChangeContact]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	[dbo].[spUrMstUserContactChange]
*/
CREATE PROCEDURE [dbo].[spUrMstUserChangeContact]
(
	@UserCode			VARCHAR(50),
	@DONEBY				VARCHAR(50),

	@Street1			VARCHAR(500),
	@Street2			VARCHAR(500),
	@City				VARCHAR(10),
	@State				VARCHAR(10),
	@Country			VARCHAR(10),
	@PIN				VARCHAR(10),

	@NewDatauniqueID 	BIGINT OUT,
	@ErrorMessage 		VARCHAR(max) OUT
)
AS 
BEGIN 
	DECLARE @UserID bigint
	
		SELECT @NewDatauniqueID = 1
		
		BEGIN TRANSACTION T1;
																																								BEGIN TRY
		SELECT @UserID = UserID FROM urMstUserRegisteredMaster WHERE UserCode = @UserCode
		
		INSERT INTO urMstUserRegisteredContactAudit
				(UserID, DatauniqueID, Street1, Street2,
				City, State, Country, IsApproved, IsActive,
				LastModifiedOn, LastModifiedBy, AuditOperationOn, 
				AuditOperationBy,ActivityType)
			SELECT UserID, DatauniqueID, Street1, Street2,
					City, State, Country, IsApproved, IsActive,
					LastModifiedOn, LastModifiedBy, GETDATE(),@DONEBY,
					ActivityType
	 		FROM urMstUserRegisteredContact
				WHERE UserID = @UserID

		SELECT @NewDatauniqueID = (MAX(DatauniqueID)+1)
	  		FROM urMstUserRegisteredMaster 
	 		WHERE UserID = @UserID

		UPDATE	urMstUserRegisteredContact
	  			SET	DatauniqueID = @NewDatauniqueID,
			 		Street1 = @Street1,
			 		Street2 = @Street2,
			 		City = @City,
			 		State = @State,
			 		Country = @Country,
					LastModifiedOn = GETDATE(),
			 		LastModifiedBy = @DONEBY,
					ActivityType = 'M'			 
	  		WHERE	UserID = @UserID
		COMMIT;
	END TRY
	BEGIN CATCH
		ROLLBACK;
		SELECT @ErrorMessage = ERROR_MESSAGE()
		SELECT @ErrorMessage
	END CATCH;
	RETURN
	
	SELECT @ErrorMessage
END
GO
