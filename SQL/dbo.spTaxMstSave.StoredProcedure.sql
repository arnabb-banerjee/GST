USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spTaxMstSave]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spTaxMstSave] (
	@TaxDefinationID	BIGINT = 0, 
	@TaxName			VARCHAR(10),
	@UserCode			VARCHAR(50),
	@isOnlyDelete		VARCHAR(1),
	@NewDatauniqueID 	BIGINT OUT,
	@ErrorMessage 		VARCHAR(50) OUT
)
AS 
BEGIN 
	DECLARE @OldDatauniqueID BIGINT
	DECLARE @NewTaxDefinationID BIGINT
	DECLARE @IsExists INT
	DECLARE @UserID BIGINT

	SELECT @UserID = U.UserID FROM urMstUserRegisteredMaster U WHERE U.UserCode = @UserCode

	SELECT @OldDatauniqueID = DatauniqueID FROM [dbo].[taxMstTaxMaster]
	WHERE TaxDefinationID = @TaxDefinationID

	INSERT INTO taxMstTaxMasterAudit ([TaxDefinationID],[TaxName],
		[DatauniqueID],[LastModifiedOn],[LastModifiedBy],[AuditOperationOn],[AuditOperationBy])
	SELECT [TaxDefinationID],[TaxName],
		[DatauniqueID],LastModifiedOn,LastModifiedBy,GETDATE(), @UserID
	FROM [dbo].[taxMstTaxMaster]
		WHERE TaxDefinationID = @TaxDefinationID
	
	DELETE FROM [dbo].[taxMstTaxMaster] WHERE TaxDefinationID = @TaxDefinationID
	
	IF (@isOnlyDelete <> 'Y')
	BEGIN 
		IF ISNULL(@TaxDefinationID, 0) < 1 BEGIN
			SELECT @TaxDefinationID = CAST(ISNULL(Max(TaxDefinationID), 0) AS BIGINT) + 1 FROM taxMstTaxMaster 

			SELECT @NewDatauniqueID = 1
		END
		ELSE BEGIN
			SELECT @NewDatauniqueID = CAST(ISNULL(Max(DatauniqueID), 0) AS BIGINT) + 1 FROM taxMstTaxMaster 
			WHERE TaxDefinationID = @TaxDefinationID
		END
		
		INSERT INTO taxMstTaxMaster ([TaxDefinationID],[TaxName],[DatauniqueID],[LastModifiedOn],[LastModifiedBy])
		VALUES (@TaxDefinationID, @TaxName,@NewDatauniqueID, GETDATE(), @UserID)	
	END 
END
GO
