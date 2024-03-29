USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spTaxMapCountrySave]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spTaxMapCountrySave] (
	@TaxDefinationID	BIGINT,
	@CountryId			BIGINT, 
	@ApplicableType		VARCHAR(10),
	@UserCode			VARCHAR(50),
	@isOnlyDelete		VARCHAR(1),
	@NewDatauniqueID 	BIGINT OUT,
	@ErrorMessage 		VARCHAR(50) OUT
)
AS 
BEGIN 
	DECLARE @OldDatauniqueID BIGINT
	DECLARE @IsExists INT
	DECLARE @UserID BIGINT

	SELECT @UserID = U.UserID FROM urMstUserRegisteredMaster U WHERE U.UserCode = @UserCode

	SELECT @OldDatauniqueID = DatauniqueID FROM taxMapTaxCountry
	WHERE CountryID = @CountryId
	AND TaxDefinationID = @TaxDefinationID

	INSERT INTO taxMapTaxCountryAudit ([TaxDefinationID], CountryID,  
		ApplicableType, DatauniqueID, LastModifiedOn, LastModifiedBy)
	SELECT [TaxDefinationID], CountryID, 
		ApplicableType, DatauniqueID, LastModifiedOn, LastModifiedBy
	FROM [dbo].[taxMapTaxCountry]
		WHERE CountryID = @CountryId
		AND TaxDefinationID = @TaxDefinationID
	
	DELETE FROM [dbo].[taxMapTaxCountry]
		WHERE CountryID = @CountryId
		AND TaxDefinationID = @TaxDefinationID
	
	IF (@isOnlyDelete <> 'Y')
	BEGIN 
		SELECT @NewDatauniqueID = CAST(ISNULL(Max(DatauniqueID), 0) AS BIGINT) + 1 FROM taxMapTaxCountry 
		WHERE CountryID = @CountryId
		AND TaxDefinationID = @TaxDefinationID
		
		INSERT INTO taxMapTaxCountry ([TaxDefinationID], CountryID,  
		ApplicableType, DatauniqueID, LastModifiedOn, LastModifiedBy)

		VALUES (@TaxDefinationID, @CountryId,  
			@ApplicableType, @NewDatauniqueID, GETDATE(), @UserID)	
	END 
END
GO
