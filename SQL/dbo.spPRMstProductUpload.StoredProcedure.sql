USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spPRMstProductUpload]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spPRMstProductUpload] (
	@ProductData										[PRODUCTDATA] ReadOnly, 
	@UserCode											VARCHAR(50) = NULL,
	@isOvereWrite										VARCHAR(1) = NULL,
	@ErrorMessage 										VARCHAR(500) OUT
)
AS 
BEGIN 
	DECLARE @NewDatauniqueID BIGINT, @NewCusID BIGINT
	DECLARE @CategoryName VARCHAR(250), @CountrtyName VARCHAR(150), @ProductName VARCHAR(50)
	DECLARE @UserID BIGINT

	SELECT @UserID = (SELECT UserID from urMstUserRegisteredMaster where UserCode = @UserCode) 

	DECLARE vendorcursor CURSOR FOR
	SELECT CategoryName, CountrtyName, ProductName
	FROM @ProductData

	DECLARE @OldDatauniqueID BIGINT, @CategoryID BIGINT, @ProductID BIGINT, @CountryID BIGINT
	DECLARE @AllowSaveDeleteData VARCHAR(1)
	SELECT	@AllowSaveDeleteData = 'N'
	DECLARE @IsExists INT
	
	BEGIN TRANSACTION T1;
	BEGIN TRY
		OPEN vendorcursor;
		FETCH NEXT FROM vendorcursor 
		INTO @CategoryName, @CountrtyName, @ProductName

		WHILE @@FETCH_STATUS = 0  
		BEGIN
		
			IF (ISNULL(@isOvereWrite, '') <> 'Y' and (SELECT COUNT(ProductId) FROM prMstproductMaster WHERE ProductName = @ProductName) > 0)
				SELECT @ErrorMessage = @ProductName + ' is already been used for another record'
			ELSE 
				SELECT @AllowSaveDeleteData = 'Y'

			IF (@AllowSaveDeleteData = 'Y')
			BEGIN
				IF((SELECT count(*) FROM [dbo].[mstCountries] WHERE name = @CountrtyName) = 0)
				BEGIN
					select @ErrorMessage = @CountrtyName + ' is not valid conutry name'
				END
				ELSE IF((SELECT count(*) FROM [dbo].prMstCategoryMaster WHERE CategoryName = @CategoryName) = 0)
				BEGIN
					select @ErrorMessage = @CategoryName + ' is not a valid category name for parent category'
				END
				ELSE
				BEGIN
					SELECT @CategoryId = CategoryID from [prMstCategoryMaster] WHERE CategoryName = @CategoryName
					SELECT @CountryID = id from mstCountries where name = @CountrtyName

					IF (SELECT COUNT(ProductId) FROM prMstproductMaster WHERE ProductName = @ProductName AND CountryID = @CountryID) = 0
					BEGIN 
						SELECT @NewDatauniqueID = 1
		
						INSERT INTO prMstproductMaster(DatauniqueID, ActivityType, ProductName, CategoryId, CountryID, LastModifiedOn, LastModifiedBy)
						VALUES( @NewDatauniqueID, 'A', @ProductName, @CategoryId, @CountryID, GETDATE(), @UserID)
					END
					ELSE
					BEGIN 
						SELECT @ProductID = ProductId, @OldDatauniqueID = DatauniqueID FROM prMstproductMaster WHERE ProductName = @ProductName
						SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM prMstproductMaster WHERE ProductName = @ProductName

						IF (NOT EXISTS(
								SELECT DatauniqueID
								FROM   prMstproductMaster
								WHERE  CategoryId = @CategoryId
								AND ProductId = @ProductID
								AND CountryID = @CountryID
							)
						)
						BEGIN
							INSERT INTO prMstproductMasterAudit(
								ProductId, DatauniqueID, ActivityType, ProductName, CategoryId, CountryID, LastModifiedOn, LastModifiedBy, AuditOperationOn, AuditOperationBy)
							SELECT ProductId, DatauniqueID, ActivityType, ProductName, CategoryId, CountryID, LastModifiedOn, LastModifiedBy, GETDATE(), @UserID
							FROM prMstproductMaster
							WHERE ProductId = @ProductID

							UPDATE prMstproductMaster SET 
								DatauniqueID = @NewDatauniqueID,
								CategoryId = @CategoryID,
								CountryID = @CountryID,
				  				ProductName = @ProductName,		
								ActivityType = 'M', 
								IsActive = 'Y',
								LastModifiedOn = GETDATE(), 
								LastModifiedBy = @UserID
							WHERE ProductId = @ProductID
						END
					END
				END
			END

			FETCH NEXT FROM vendorcursor 
			INTO @CategoryName, @CountrtyName, @ProductName
		END

		IF @@TRANCOUNT > 0
			COMMIT;

	END TRY
	BEGIN CATCH
		SELECT @ErrorMessage = ERROR_MESSAGE()

		IF @@TRANCOUNT > 0
			ROLLBACK;
		throw
	END CATCH

END 


GO
