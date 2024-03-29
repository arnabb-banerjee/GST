USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spTaxMapCountryCategoryExpenseSave]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[spTaxMapCountryCategoryExpenseSave] 
(
	@TaxDefinationID	BIGINT,
	@CategoryId	BIGINT, 
	@CountryId	BIGINT, 
	@Percentage	VARCHAR(50),
	@ApplicableType	VARCHAR(10),
	@UserCode	VARCHAR(50),
	@isOnlyDelete	VARCHAR(1),
	@NewDatauniqueID BIGINT OUT,
	@ErrorMessage VARCHAR(50) OUT
)
AS 
BEGIN 
	DECLARE @OldDatauniqueID BIGINT
	DECLARE @IsExists INT
	DECLARE @UserID BIGINT

	SELECT @UserID = U.UserID FROM urMstUserRegisteredMaster U WHERE U.UserCode = @UserCode

	SELECT @OldDatauniqueID = DatauniqueID FROM taxMapTaxCountryCategory
	WHERE CountryID = @CountryId
	AND CategoryId = @CategoryId 
	AND TaxDefinationID = @TaxDefinationID

	INSERT INTO taxMapTaxCountryCategoryAudit ([TaxDefinationID], CountryID, CategoryId, Percentage, 
	ApplicableType, DatauniqueID, LastModifiedOn, LastModifiedBy)

	SELECT [TaxDefinationID], CountryID, CategoryId, Percentage, 
	@ApplicableType, DatauniqueID, LastModifiedOn, LastModifiedBy
	FROM [dbo].[taxMapTaxCountryCategory]
	WHERE CountryID = @CountryId
	AND CategoryId = @CategoryId 
	AND TaxDefinationID = @TaxDefinationID

	DELETE FROM [dbo].[taxMapTaxCountryCategory]
	WHERE CountryID = @CountryId
	AND CategoryId = @CategoryId 
	AND TaxDefinationID = @TaxDefinationID

	IF (@isOnlyDelete <> 'Y')
	BEGIN 
		SELECT @NewDatauniqueID = CAST(ISNULL(Max(DatauniqueID), 0) AS BIGINT) + 1 FROM taxMapTaxCountryCategory 
		WHERE CountryID = @CountryId
		AND CategoryId = @CategoryId 
		AND TaxDefinationID = @TaxDefinationID

		INSERT INTO taxMapTaxCountryCategory ([TaxDefinationID], CountryID, CategoryId, Percentage, 
		ApplicableType, DatauniqueID, LastModifiedOn, LastModifiedBy)

		VALUES (@TaxDefinationID, @CountryId, @CategoryId, @Percentage, 
		@ApplicableType, @NewDatauniqueID, GETDATE(), @UserID)	
	END 
END


GO
