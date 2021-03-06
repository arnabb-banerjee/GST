USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spPRMAPCategoryTAXDefinationSave]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPRMAPCategoryTAXDefinationSave] (
	@TaxDefinationID	BIGINT,
	@CategoryId			BIGINT, 
	@Percentage			Numeric(2,2),
	@UserID				VARCHAR(50),
	@isOnlyDelete		VARCHAR(1),
	@NewDatauniqueID 	BIGINT OUT,
	@ErrorMessage 		VARCHAR(50) OUT
)
AS 
BEGIN 
	DECLARE @OldDatauniqueID BIGINT
	DECLARE @IsExists INT

	SELECT @OldDatauniqueID = DatauniqueID FROM PRMAPCategoryTaxDefination
	WHERE CategoryId = @CategoryId 
	AND TaxDefinationID = @TaxDefinationID

	INSERT INTO PRMAPCategoryTaxDefinationAudit ([TaxDefinationID], CategoryId, Percentage, 
		DatauniqueID, LastModifiedOn, LastModifiedBy)
	SELECT [TaxDefinationID], CategoryId, Percentage, 
		DatauniqueID, LastModifiedOn, LastModifiedBy
	FROM [dbo].[PRMAPCategoryTaxDefination]
		WHERE CategoryId = @CategoryId 
		AND TaxDefinationID = @TaxDefinationID
	
	DELETE FROM [dbo].[PRMAPCategoryTaxDefination]
		WHERE CategoryId = @CategoryId 
		AND TaxDefinationID = @TaxDefinationID
	
	IF (@isOnlyDelete <> 'Y')
	BEGIN 
		SELECT @NewDatauniqueID = MAX(DatauniqueID) FROM PRMAPCategoryTaxDefinationAudit 
		WHERE CategoryId = @CategoryId 
		AND TaxDefinationID = @TaxDefinationID
		
		SELECT @NewDatauniqueID = CAST(ISNULL(@NewDatauniqueID, 0) AS BIGINT) + 1

		INSERT INTO PRMAPCategoryTaxDefination ([TaxDefinationID], CategoryId, Percentage, 
		DatauniqueID, LastModifiedOn, LastModifiedBy)

		VALUES (@TaxDefinationID, @CategoryId, @Percentage, 
			@NewDatauniqueID, GETDATE(), @UserID)	
	END 
END



GO
