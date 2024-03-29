USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spPRMstCategoryUpload2]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
[dbo].[spPRMstCategoryUpload2] 'N', 'Construction services', '', 'Service', '9954', 'CGST', 0, 
		'india', '', 'Yes', 'Y', 'Yes', '1C0E80ED-A137-49EC-9152-2D3FBAF0C42A',
		'N', 0, ''

		select * from taxMapTaxCountryCategory WHERE HSNCode = '9963'
		DELETE from prMstCategoryMaster WHERE CATEGORYnAME = 'Construction services'


			SELECT CategoryId FROM prMstCategoryMaster WHERE CountryID = 101 AND UPPER(RTRIM(LTRIM(CategoryName))) = UPPER(RTRIM(LTRIM('Construction services'))) /* AND ISNULL(WillCarryContent, '') <> 'Y'*/

*/

CREATE PROCEDURE [dbo].[spPRMstCategoryUpload2] (
	@isOvereWrite		[VARCHAR](1) = 'N',
	@CategoryName		VARCHAR(5000), 
	@ParentCategoryName	VARCHAR(5000), 
	@ServiceOrGoods		VARCHAR(1), 
	@HSNSACCode			VARCHAR(50),
	@TaxName			VARCHAR(150), 
	@Percentage			VARCHAR(150), 
	@CountrtyName		VARCHAR(150),
	@ApplicableType		VARCHAR(150),
	@WillCarryContent	VARCHAR(1),
	@IsActive  			VARCHAR(1),
	@IsExpenseType  	VARCHAR(1),
	@UserCode			VARCHAR(50),
	@isOnlyDelete		VARCHAR(1),
	@NewDatauniqueID 	BIGINT OUT,
	@ErrorMessage 		VARCHAR(500) OUT
)
AS 
BEGIN 
	DECLARE @OldDatauniqueID bigint,
	@IsExists int,
	@UserID bigint,
	@CountryId	bigint, 
	@CategoryId	bigint, 
	@Count_Category_map	bigint, 
	@ParentCategoryId bigint,
	@TaxDefinationID bigint
	
	SELECT @UserID = 0

	IF(ISNULL(RTRIM(LTRIM(@UserCode)), '') <> '')
	BEGIN
		SELECT @UserID = UserId FROM [urMstUserRegisteredMaster] WHERE [UserCode] = @UserCode
	END

	BEGIN /*First stage validation*/
		IF @UserID < 1
		BEGIN
			SELECT @ErrorMessage = 'Invalid user information'
		END
		ELSE IF(ISNULL(RTRIM(LTRIM(@CategoryName)), '') = '')
		BEGIN
			SELECT @ErrorMessage = 'Category column should not be blank'
		END
		ELSE IF(ISNULL(RTRIM(LTRIM(@TaxName)), '') = '')
		BEGIN
			SELECT @ErrorMessage = 'Tax name column should not be blank'
		END
		ELSE IF(ISNULL(RTRIM(LTRIM(@CountrtyName)), '') = '')
		BEGIN
			SELECT @ErrorMessage = 'Country column should not be blank'
		END
		ELSE IF(ISNULL(RTRIM(LTRIM(@Percentage)), '') = '')
		BEGIN
			SELECT @ErrorMessage = 'Percentage column should not be blank'
		END
		ELSE IF(ISNULL(RTRIM(LTRIM(@ServiceOrGoods)), '') = '')
		BEGIN
			select @ErrorMessage = 'Service or Goods should not be blank'
		END
		ELSE IF(SELECT count(TaxDefinationID) FROM taxMstTaxMaster WHERE isnull(upper(rtrim(ltrim(TaxName))), '') = isnull(upper(rtrim(ltrim(@TaxName))), '')) = 0
		BEGIN
			SELECT @ErrorMessage = '[' + @TaxName + '] is not a valid tax name'
		END
		ELSE IF((SELECT COUNT(id) FROM [dbo].[mstCountries] WHERE name = @CountrtyName) = 0)
		BEGIN
			select @ErrorMessage = '[' + @CountrtyName + '] is not a valid conutry name'
		END
		ELSE IF(UPPER(ISNULL(RTRIM(LTRIM(@ServiceOrGoods)), '')) <> 'SERVICE' and UPPER(ISNULL(RTRIM(LTRIM(@ServiceOrGoods)), '')) <> 'S' 
			and UPPER(ISNULL(RTRIM(LTRIM(@ServiceOrGoods)), '')) <> 'GOODS' AND UPPER(ISNULL(RTRIM(LTRIM(@ServiceOrGoods)), '')) <> 'G')
		BEGIN
			select @ErrorMessage = 'Service or Goods = [' + @ServiceOrGoods + '] is not valid'
		END

		IF(ISNULL(RTRIM(LTRIM(@ErrorMessage)), '') <> '')
		BEGIN
			RAISERROR(@ErrorMessage, 16, 1)
		END
	END

	SELECT @TaxDefinationID = TaxDefinationID FROM taxMstTaxMaster WHERE UPPER(RTRIM(LTRIM(TaxName))) = UPPER(RTRIM(LTRIM(@TaxName)))
	SELECT @CountryId = id FROM [dbo].[mstCountries] WHERE UPPER(RTRIM(LTRIM(name))) = UPPER(RTRIM(LTRIM(@CountrtyName)))
	SELECT @CategoryId = CategoryId FROM prMstCategoryMaster WHERE CountryID = @CountryId AND UPPER(RTRIM(LTRIM(CategoryName))) = UPPER(RTRIM(LTRIM(@CategoryName)))

	BEGIN /*2nd stage validation*/
		--IF((SELECT COUNT(*) FROM [dbo].[prMstCategoryMaster] WHERE CountryID = @CountryId AND RTRIM(LTRIM(HSNCode)) = RTRIM(LTRIM(@HSNSACCode)) AND CategoryName <> @CategoryName) > 0)
		--BEGIN
		--	select @ErrorMessage = 'HSN [' + @HSNSACCode + '] is already used by anoter record in this country:' + @CountrtyName
		--END
		--ELSE IF((SELECT COUNT(*) FROM [dbo].[prMstCategoryMaster] WHERE CountryID = @CountryId AND RTRIM(LTRIM(SACCode)) = RTRIM(LTRIM(@HSNSACCode)) AND CategoryName <> @CategoryName) > 0)
		--BEGIN
		--	select @ErrorMessage = 'SAC [' + @HSNSACCode + '] is already used by anoter record in this country:' + @CountrtyName
		--END
		IF(ISNULL(RTRIM(LTRIM(@Percentage)), '') <> '' AND CONVERT(numeric(18,7), @Percentage) > 100)
		BEGIN
			select @ErrorMessage = 'Percentage [' + @Percentage + '] is not valid, it should be less than 100'
		END
		ELSE IF(ISNULL(RTRIM(LTRIM(@ParentCategoryName)), '') <> '')
		BEGIN
			SELECT @ParentCategoryId = CategoryId FROM prMstCategoryMaster WHERE CountryID = @CountryId AND UPPER(RTRIM(LTRIM(CategoryName))) = UPPER(RTRIM(LTRIM(@ParentCategoryName))) /*AND ISNULL(WillCarryContent, '') <> 'Y'*/

			IF(ISNULL(RTRIM(LTRIM(@ParentCategoryId)), 0) < 1)
			BEGIN
				SELECT @ErrorMessage = 'Parent category [' + @ParentCategoryName + '] is not avaiable in the category module for this country:' + @CountrtyName
			END
		END
	
		IF(ISNULL(RTRIM(LTRIM(@ErrorMessage)), '') <> '')
		BEGIN
			RAISERROR(@ErrorMessage, 16, 1)
		END
	END

	/*BEGIN /*3rd stage validation*/
	--	IF(ISNULL(RTRIM(LTRIM(@isOvereWrite)), '') <> 'Y')
	--	BEGIN
	--		IF ISNULL(RTRIM(LTRIM(@CategoryId)), 0) > 0 AND
	--		(SELECT count(CategoryId) FROM taxMapTaxCountryCategory WHERE CountryID = @CountryId AND isnull(CategoryId, 0) = isnull(@CategoryId, 0)) > 0
	--		BEGIN
	--			SELECT @ErrorMessage = @CategoryName + ' is already avaiable as product in the system'
	--			RAISERROR(@ErrorMessage, 16, 1)
	--		END
	--	END
	--END*/
	
	SELECT @Count_Category_map = count(CategoryId) FROM taxMapTaxCountryCategory WHERE CountryID = @CountryId AND isnull(CategoryId, 0) = isnull(@CategoryId, 0) and TaxDefinationID = @TaxDefinationID

	if(@Count_Category_map < 1)
	BEGIN 
		SELECT @NewDatauniqueID = 1
		
		if (isnull(@CategoryId, 0) < 1) /*not available in master*/
		BEGIN 
			INSERT INTO prMstCategoryMaster (DatauniqueID, ActivityType, CategoryName, ParentCategoryId, ServiceOrGoods, 
						HSNCode, 
						SACCode, 
						WillCarryContent, 
						IsActive, 
						IsExpenseType,
						CountryID, CreatedOn, CreatedBy)
			VALUES(@NewDatauniqueID, 'A', NULLIF(@CategoryName, ''), NULLIF(@ParentCategoryId, ''), @ServiceOrGoods, 
				CASE WHEN(@ServiceOrGoods = 'G') THEN @HSNSACCode ELSE NULL END, 
				CASE WHEN(@ServiceOrGoods = 'S') THEN @HSNSACCode ELSE NULL END, 
				CASE WHEN(UPPER(ISNULL(RTRIM(LTRIM(@WillCarryContent)), '')) = 'YES') THEN 'Y' ELSE NULL END,
				@IsActive, 
				@IsExpenseType,
				@CountryId, GETDATE(), @UserID)

			SELECT @CategoryId = @@Identity
		END
		else if(ISNULL(RTRIM(LTRIM(@isOvereWrite)), '') <> 'Y')
		begin 
			UPDATE prMstCategoryMaster SET 
				DatauniqueID = (select (isnull(max(DatauniqueID), 0) + 1) FROM prMstCategoryMaster WHERE isnull(CategoryId, 0) = isnull(@CategoryId, 0) AND CountryID = @CountryID),
				ParentCategoryId = NULLIF(@ParentCategoryId, ''),
				ActivityType = 'M', 
				IsActive = @IsActive,
				CountryID = @CountryId,
				ServiceOrGoods = @ServiceOrGoods,
				HSNCode = CASE WHEN(@ServiceOrGoods = 'G') THEN @HSNSACCode ELSE NULL END,
				SACCode = CASE WHEN(@ServiceOrGoods = 'S') THEN @HSNSACCode ELSE NULL END,
				WillCarryContent = CASE WHEN(UPPER(ISNULL(RTRIM(LTRIM(@WillCarryContent)), '')) = 'YES') THEN NULL ELSE 'Y' END,
				IsExpenseType = @IsExpenseType,
				LastModifiedOn = GETDATE(), 
				LastModifiedBy = @UserID
				WHERE CategoryId = @CategoryID
		end

		INSERT INTO taxMapTaxCountryCategory(CategoryId, CountryID, TaxDefinationID, DatauniqueID, Percentage, ApplicableType, CreatedBy, CreatedOn)
		VALUES(@CategoryId, @CountryId, @TaxDefinationID, @NewDatauniqueID, @Percentage, @ApplicableType, @UserID, getdate()) 
	end
	ELSE IF(ISNULL(RTRIM(LTRIM(@isOvereWrite)), '') <> 'Y')
	begin
		INSERT INTO prMstCategoryMasterAudit(
			CategoryID, CountryID, DatauniqueID, ActivityType, CategoryName, ParentCategoryId, IsActive, IsExpenseType, CreatedOn, CreatedBy, LastModifiedOn, LastModifiedBy, AuditOperationOn, AuditOperationBy)
		SELECT CategoryID, CountryID, DatauniqueID, ActivityType, CategoryName, ParentCategoryId, IsActive, IsExpenseType, CreatedOn, CreatedBy, LastModifiedOn, LastModifiedBy, GETDATE(), @UserID
		FROM prMstCategoryMaster
		WHERE CategoryID = @CategoryID
		AND CountryID = @CountryId

		UPDATE prMstCategoryMaster SET 
			DatauniqueID = (SELECT (MAX(DatauniqueID) + 1) FROM prMstCategoryMaster WHERE isnull(CategoryId, 0) = isnull(@CategoryID, 0) AND CountryID = @CountryID),
			ParentCategoryId = NULLIF(@ParentCategoryId, ''),
			CountryID = @CountryId, 
			ActivityType = 'M', 
			IsActive = 'Y',
			ServiceOrGoods = @ServiceOrGoods,
			HSNCode = CASE WHEN(@ServiceOrGoods = 'G') THEN @HSNSACCode ELSE NULL END,
			SACCode = CASE WHEN(@ServiceOrGoods = 'S') THEN @HSNSACCode ELSE NULL END,
			WillCarryContent = CASE WHEN(UPPER(ISNULL(RTRIM(LTRIM(@WillCarryContent)), '')) = 'YES') THEN NULL ELSE 'Y' END,
			IsExpenseType = @IsExpenseType,
			LastModifiedOn = GETDATE(), 
			LastModifiedBy = @UserID
			WHERE CategoryID = @CategoryID
			AND CountryID = @CountryId

		INSERT INTO taxMapTaxCountryCategoryAudit(CategoryId, CountryID, DatauniqueID, TaxDefinationID, Percentage, ApplicableType, LastModifiedBy, LastModifiedOn, AuditOperationBy, AuditOperationOn)
		SELECT CategoryId, CountryID, DatauniqueID, TaxDefinationID, Percentage, ApplicableType, LastModifiedBy, LastModifiedOn, @UserID, GETDATE()
		FROM taxMapTaxCountryCategory
		WHERE CategoryID = @CategoryID
		AND CountryID = @CountryId
		AND TaxDefinationID = @TaxDefinationID

		UPDATE taxMapTaxCountryCategoryAudit SET
			DatauniqueID = (select (MAX(DatauniqueID) + 1) FROM prMstCategoryMaster WHERE CategoryId = @CategoryID AND CountryID = @CountryID AND TaxDefinationID = @TaxDefinationID), 
			Percentage = @Percentage, 
			ApplicableType = @ApplicableType, 
			LastModifiedOn = GETDATE(), 
			LastModifiedBy = @UserID
			WHERE CategoryID = @CategoryID
			AND CountryID = @CountryId
			AND TaxDefinationID = @TaxDefinationID
	end
END





GO
