USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spPRMstCategoryUpload]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPRMstCategoryUpload] (
	@CategoryData										[CATEGORYDATA] ReadOnly, 
	@UserCode											VARCHAR(50)  = NULL,
	@isOvereWrite										VARCHAR(1) = NULL,
	@ErrorMessage 										VARCHAR(50) OUT
)
AS 
BEGIN 
	DECLARE @NewDatauniqueID BIGINT, @NewCusID BIGINT, @UserID BIGINT 
	DECLARE @ParentCategory VARCHAR(50), @CategoryName VARCHAR(250), @TaxName VARCHAR(50), @CountrtyName VARCHAR(150), 
			@ServiceGoods VARCHAR(20), @HSNSAC VARCHAR(20), @CGST VARCHAR(150), @IGST VARCHAR(500), @SGST VARCHAR(50), @VAT VARCHAR(50)
	DECLARE @AllowSaveDeleteData VARCHAR(1)
	SELECT	@AllowSaveDeleteData = ''
	
	SELECT @UserID = UserID FROM urMstUserRegisteredMaster WHERE UserCode = @UserCode

	DECLARE vendorcursor CURSOR FOR
	SELECT ParentCategory, CategoryName, CountrtyName, @ServiceGoods, @HSNSAC, CGST, IGST, SGST, VAT
	FROM @CategoryData

	BEGIN TRANSACTION T1;
	BEGIN TRY
		
		IF(ISNULL(@isOvereWrite, '') = 'N')
		BEGIN
			INSERT INTO prMstCategoryMasterAudit(
				CategoryID, DatauniqueID, ActivityType, CategoryName, ParentCategoryId, LastModifiedOn, LastModifiedBy, AuditOperationOn, AuditOperationBy)
			SELECT CategoryID, DatauniqueID, ActivityType, CategoryName, ParentCategoryId, LastModifiedOn, LastModifiedBy, GETDATE(), @UserID
			FROM prMstCategoryMaster

			INSERT prMAPCategoryTaxDefinationAudit (DatauniqueID,ParentDataUniqueID,CategoryId,CountryID,TaxDefinationID,Percentage,LastModifiedBy,LastModifiedOn)
			SELECT DatauniqueID,ParentDataUniqueID,CategoryId,CountryID,TaxDefinationID,Percentage,LastModifiedBy,LastModifiedOn
			FROM prMAPCategoryTaxDefination
			
			DELETE FROM prMAPCategoryTaxDefination
			DELETE FROM prMstCategoryMaster
			SELECT	@AllowSaveDeleteData = 'A'
		END	

		OPEN vendorcursor;
		FETCH NEXT FROM vendorcursor 
		INTO @ParentCategory, @CategoryName, @CountrtyName, @ServiceGoods, @HSNSAC, @CGST, @IGST, @SGST, @VAT

	
		WHILE @@FETCH_STATUS = 0  
		BEGIN  
			DECLARE @OldDatauniqueID BIGINT, @CategoryID BIGINT, @TaxDefinationID_CGST bigint, @TaxDefinationID_SGST bigint, 
		    @TaxDefinationID_IGST bigint, @TaxDefinationID_VAT bigint, @ParentCategoryId bigint, @CountryID bigint 
			DECLARE @IsExists INT

			SELECT @TaxDefinationID_SGST = TaxDefinationID FROM [prMstAllowedTaxDefinationCountryWise] WHERE TaxName = 'SGST'
			SELECT @TaxDefinationID_CGST = TaxDefinationID FROM [prMstAllowedTaxDefinationCountryWise] WHERE TaxName = 'CGST'
			SELECT @TaxDefinationID_IGST = TaxDefinationID FROM [prMstAllowedTaxDefinationCountryWise] WHERE TaxName = 'IGST'
			SELECT @TaxDefinationID_VAT = TaxDefinationID FROM [prMstAllowedTaxDefinationCountryWise] WHERE TaxName = 'VAT'

			IF(ISNULL(@isOvereWrite, '') <> 'N')
			BEGIN
				SELECT	@AllowSaveDeleteData = ''
	
				IF ((SELECT COUNT(CategoryId) FROM prMstCategoryMaster WHERE CategoryName = @CategoryName) > 0)
				BEGIN
					IF(ISNULL(@isOvereWrite, '') = 'O')
					BEGIN
						SELECT @AllowSaveDeleteData = 'M'
					END
				END
				ELSE 
					SELECT @AllowSaveDeleteData = 'A'
			END

			IF (@AllowSaveDeleteData <> '')
			BEGIN
				IF((SELECT COUNT(*) FROM [dbo].[mstCountries] WHERE name = @CountrtyName) = 0)
				BEGIN
					select @ErrorMessage = '[' + @CountrtyName + '] is not a valid conutry name'
				END
				IF((SELECT COUNT(*) FROM [dbo].[prMstCategoryMaster] WHERE HSNCode = @HSNSAC AND CategoryName <> @CategoryName) = 0)
				BEGIN
					select @ErrorMessage = '[' + @HSNSAC + '] is already used by anoter record'
				END
				IF((SELECT COUNT(*) FROM [dbo].[prMstCategoryMaster] WHERE SACCode = @HSNSAC AND CategoryName <> @CategoryName) = 0)
				BEGIN
					select @ErrorMessage = '[' + @HSNSAC + '] is already used by anoter record'
				END
				IF(@SGST > 100)
				BEGIN
					select @ErrorMessage = 'SGST = [' + @SGST + '] is not valid, it should be less than 100'
				END
				IF(@CGST > 100)
				BEGIN
					select @ErrorMessage = 'CGST = [' + @SGST + '] is not valid, it should be less than 100'
				END
				IF(@IGST > 100)
				BEGIN
					select @ErrorMessage = 'IGST = [' + @SGST + '] is not valid, it should be less than 100'
				END
				IF(@VAT > 100)
				BEGIN
					select @ErrorMessage = 'VAT = [' + @SGST + '] is not valid, it should be less than 100'
				END
				IF(UPPER(ISNULL(RTRIM(LTRIM(@ServiceGoods)), '')) <> 'SERVICE' and UPPER(ISNULL(RTRIM(LTRIM(@ServiceGoods)), '')) <> 'GOODS')
				BEGIN
					select @ErrorMessage = 'Service or Goods = [' + @ServiceGoods + '] is not valid'
				END

				IF ISNULL(RTRIM(LTRIM(@ErrorMessage)), '') <> ''
				BEGIN
					IF @@TRANCOUNT > 0
						ROLLBACK;
				END
				ELSE
				BEGIN
					IF(ISNULL(RTRIM(LTRIM(@ParentCategory)), '') <> '')
					BEGIN
						if((SELECT count(*) FROM [dbo].prMstCategoryMaster WHERE CategoryName = @ParentCategory) = 0)
						BEGIN
							INSERT INTO prMstCategoryMaster (DatauniqueID, ActivityType, CategoryName, ParentCategoryId, LastModifiedOn, LastModifiedBy)
							VALUES(1, 'A', NULLIF(@ParentCategory, ''), NULL, GETDATE(), @UserID)

							SELECT @ParentCategoryId = @@IDENTITY
						END
						ELSE
							SELECT @ParentCategoryId = CategoryID from [prMstCategoryMaster] WHERE CategoryName = @ParentCategory
					END

					SELECT @ServiceGoods = CASE WHEN UPPER(ISNULL(RTRIM(LTRIM(@ServiceGoods)), '')) = 'SERVICE' THEN 'S' ELSE 'G' END
					SELECT @CountryID = id from mstCountries where name = @CountrtyName

					IF @AllowSaveDeleteData = 'A'
					BEGIN 
						SELECT @NewDatauniqueID = 1

						INSERT INTO prMstCategoryMaster (DatauniqueID, ActivityType, CategoryName, ParentCategoryId, ServiceOrGoods, HSNCode, SACCode, LastModifiedOn, LastModifiedBy)
						VALUES(@NewDatauniqueID, 'A', NULLIF(@CategoryName, ''), NULLIF(@ParentCategoryId, ''), @ServiceGoods, CASE WHEN(@ServiceGoods = 'G') THEN @HSNSAC ELSE NULL END, 
									CASE WHEN(@ServiceGoods = 'S') THEN @HSNSAC ELSE NULL END, GETDATE(), @UserID)

						SELECT @NewCusID = @@Identity

						IF(ISNULL(RTRIM(LTRIM(@SGST)), '') <> '')
						BEGIN	
							INSERT INTO prMAPCategoryTaxDefination(CategoryId, DatauniqueID, TaxDefinationID, Percentage)
							VALUES(@NewCusID, @NewDatauniqueID, @TaxDefinationID_SGST, @SGST) 
						END
						IF(ISNULL(RTRIM(LTRIM(@CGST)), '') <> '')
						BEGIN	
							INSERT INTO prMAPCategoryTaxDefination(CategoryId, DatauniqueID, TaxDefinationID, Percentage)
							VALUES(@NewCusID, @NewDatauniqueID, @TaxDefinationID_CGST, @CGST)
						END
						IF(ISNULL(RTRIM(LTRIM(@IGST)), '') <> '')
						BEGIN	
							INSERT INTO prMAPCategoryTaxDefination(CategoryId, DatauniqueID, TaxDefinationID, Percentage)
							VALUES(@NewCusID, @NewDatauniqueID, @TaxDefinationID_IGST, @IGST)
						END
						IF(ISNULL(RTRIM(LTRIM(@VAT)), '') <> '' AND @TaxDefinationID_VAT IS NOT NULL)
						BEGIN	
							INSERT INTO prMAPCategoryTaxDefination(CategoryId, DatauniqueID, TaxDefinationID, Percentage)
							VALUES(@NewCusID, @NewDatauniqueID, @TaxDefinationID_VAT, @VAT)
						END
					END
					ELSE IF @AllowSaveDeleteData = 'M'
					BEGIN 
						SELECT @CategoryID = CategoryId, @OldDatauniqueID = DatauniqueID FROM prMstCategoryMaster WHERE CategoryName = @CategoryName
						SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM prMstCategoryMaster WHERE CategoryId = @CategoryID

						IF (NOT EXISTS(
								SELECT DatauniqueID
								FROM   prMstCategoryMaster
								WHERE  ISNULL(ParentCategoryId, '') = ISNULL(@ParentCategoryId, '')
								AND ISNULL(CategoryId, '') = ISNULL(@CategoryID, '')
							)
						)
						BEGIN
							INSERT INTO prMstCategoryMasterAudit(
								CategoryID, DatauniqueID, ActivityType, CategoryName, ParentCategoryId, LastModifiedOn, LastModifiedBy, AuditOperationOn, AuditOperationBy)
							SELECT CategoryID, DatauniqueID, ActivityType, CategoryName, ParentCategoryId, LastModifiedOn, LastModifiedBy, GETDATE(), @UserID
							FROM prMstCategoryMaster
							WHERE CategoryID = @CategoryID

							UPDATE prMstCategoryMaster SET 
								DatauniqueID = @NewDatauniqueID,
								ParentCategoryId = NULLIF(@ParentCategoryId, ''),
								ActivityType = 'M', 
								IsActive = 'Y',
								ServiceOrGoods = @ServiceGoods,
								HSNCode = CASE WHEN(@ServiceGoods = 'G') THEN @HSNSAC ELSE NULL END,
								SACCode = CASE WHEN(@ServiceGoods = 'S') THEN @HSNSAC ELSE NULL END,
								LastModifiedOn = GETDATE(), 
								LastModifiedBy = @UserID
								WHERE CategoryId = @CategoryID
						END

						IF (NOT EXISTS(
								SELECT DatauniqueID
								FROM   prMAPCategoryTaxDefination
								WHERE  TaxDefinationID = @TaxDefinationID_SGST
								AND CategoryId = @CategoryID
								AND Percentage = @SGST
							)
						)
						BEGIN
							INSERT prMAPCategoryTaxDefinationAudit (DatauniqueID,ParentDataUniqueID,CategoryId,CountryID,TaxDefinationID,Percentage,LastModifiedBy,LastModifiedOn)
							SELECT DatauniqueID,ParentDataUniqueID,CategoryId,CountryID,TaxDefinationID,Percentage,LastModifiedBy,LastModifiedOn
								FROM prMAPCategoryTaxDefination
								WHERE CategoryId = @CategoryID
								AND ISNULL(RTRIM(LTRIM(@SGST)), '') <> ''

							UPDATE prMAPCategoryTaxDefination 
								SET DatauniqueID = (SELECT (MAX(DatauniqueID) + 1) FROM prMAPCategoryTaxDefination WHERE CategoryId = @CategoryID AND TaxDefinationID = @TaxDefinationID_SGST), 
									TaxDefinationID = @TaxDefinationID_SGST, 
									Percentage = @SGST,
									ParentDataUniqueID = @NewDatauniqueID
							WHERE CategoryId = @CategoryID
							AND ISNULL(RTRIM(LTRIM(@SGST)), '') <> ''
						END

						IF (NOT EXISTS(
								SELECT DatauniqueID
								FROM   prMAPCategoryTaxDefination
								WHERE  TaxDefinationID = @TaxDefinationID_SGST
								AND CategoryId = @CategoryID
								AND Percentage = @CGST
							)
						)
						BEGIN
							INSERT prMAPCategoryTaxDefinationAudit (DatauniqueID,ParentDataUniqueID,CategoryId,CountryID,TaxDefinationID,Percentage,LastModifiedBy,LastModifiedOn)
							SELECT DatauniqueID,ParentDataUniqueID,CategoryId,CountryID,TaxDefinationID,Percentage,LastModifiedBy,LastModifiedOn
								FROM prMAPCategoryTaxDefination
								WHERE CategoryId = @CategoryID
								AND ISNULL(RTRIM(LTRIM(@CGST)), '') <> ''

							UPDATE prMAPCategoryTaxDefination 
								SET DatauniqueID = (SELECT (MAX(DatauniqueID) + 1) FROM prMAPCategoryTaxDefination WHERE CategoryId = @CategoryID AND TaxDefinationID = @TaxDefinationID_SGST), 
									TaxDefinationID = @TaxDefinationID_SGST, 
									Percentage = @CGST,
									ParentDataUniqueID = @NewDatauniqueID
							WHERE CategoryId = @CategoryID
							AND ISNULL(RTRIM(LTRIM(@CGST)), '') <> ''
						END

						IF (NOT EXISTS(
								SELECT DatauniqueID
								FROM   prMAPCategoryTaxDefination
								WHERE  TaxDefinationID = @TaxDefinationID_SGST
								AND CategoryId = @CategoryID
								AND Percentage = @IGST
							)
						)
						BEGIN
							INSERT prMAPCategoryTaxDefinationAudit (DatauniqueID,ParentDataUniqueID,CategoryId,CountryID,TaxDefinationID,Percentage,LastModifiedBy,LastModifiedOn)
							SELECT DatauniqueID,ParentDataUniqueID,CategoryId,CountryID,TaxDefinationID,Percentage,LastModifiedBy,LastModifiedOn
								FROM prMAPCategoryTaxDefination
								WHERE CategoryId = @CategoryID
								AND ISNULL(RTRIM(LTRIM(@IGST)), '') <> ''

							UPDATE prMAPCategoryTaxDefination 
								SET DatauniqueID = (SELECT (MAX(DatauniqueID) + 1) FROM prMAPCategoryTaxDefination WHERE CategoryId = @CategoryID AND TaxDefinationID = @TaxDefinationID_SGST), 
									TaxDefinationID = @TaxDefinationID_SGST, 
									Percentage = @IGST,
									ParentDataUniqueID = @NewDatauniqueID
							WHERE CategoryId = @CategoryID
							AND ISNULL(RTRIM(LTRIM(@IGST)), '') <> ''
						END
						
						IF (NOT EXISTS(
								SELECT DatauniqueID
								FROM   prMAPCategoryTaxDefination
								WHERE  TaxDefinationID = @TaxDefinationID_SGST
								AND CategoryId = @CategoryID
								AND Percentage = @VAT
							)
						)
						BEGIN
							INSERT prMAPCategoryTaxDefinationAudit (DatauniqueID,ParentDataUniqueID,CategoryId,CountryID,TaxDefinationID,Percentage,LastModifiedBy,LastModifiedOn)
							SELECT DatauniqueID,ParentDataUniqueID,CategoryId,CountryID,TaxDefinationID,Percentage,LastModifiedBy,LastModifiedOn
								FROM prMAPCategoryTaxDefination
								WHERE CategoryId = @CategoryID
								AND ISNULL(RTRIM(LTRIM(@VAT)), '') <> ''

							UPDATE prMAPCategoryTaxDefination 
								SET DatauniqueID = (SELECT (MAX(DatauniqueID) + 1) FROM prMAPCategoryTaxDefination WHERE CategoryId = @CategoryID AND TaxDefinationID = @TaxDefinationID_SGST), 
									TaxDefinationID = @TaxDefinationID_SGST, 
									Percentage = @VAT,
									ParentDataUniqueID = @NewDatauniqueID
								WHERE CategoryId = @CategoryID
								AND ISNULL(RTRIM(LTRIM(@VAT)), '') <> ''
						END

					END 
				END
			END
	
			FETCH NEXT FROM vendorcursor 
			INTO @ParentCategory, @CategoryName, @CountrtyName, @ServiceGoods, @HSNSAC, @CGST, @IGST, @SGST, @VAT

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

	CLOSE vendorcursor
	DEALLOCATE vendorcursor
END 

GO
