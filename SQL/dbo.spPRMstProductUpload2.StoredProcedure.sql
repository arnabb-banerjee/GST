USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spPRMstProductUpload2]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPRMstProductUpload2] (
	@isOvereWrite					VARCHAR(1) = 'N', 
	@ProductName					VARCHAR(5000) = '',
	@CategoryName					VARCHAR(50) = '',
	@CountryName					VARCHAR(50) = '',
	@IsActive  						VARCHAR(1) = '',
	@UserCode						VARCHAR(50) = '',
	@isOnlyDelete					VARCHAR(1) = '',
	@NewDatauniqueID 				BIGINT OUT,
	@ErrorMessage 					VARCHAR(500) OUT
)
AS 
BEGIN 

	DECLARE @ProductId INT, @OldDatauniqueID BIGINT, @CategoryId BIGINT, @CountryID BIGINT
	DECLARE @AllowSaveDeleteData VARCHAR(1)
	SELECT @AllowSaveDeleteData = 'Y'
	DECLARE @IsExists INT
	DECLARE @UserID BIGINT
	SELECT @UserID = 0

	IF(ISNULL(RTRIM(LTRIM(@UserCode)), '') <> '')
	BEGIN
		SELECT @UserID = UserId FROM [urMstUserRegisteredMaster] WHERE [UserCode] = @UserCode
	END

	IF(ISNULL(RTRIM(LTRIM(@CategoryName)), '') <> '')
	BEGIN
		SELECT @CategoryId = CategoryId FROM prMstCategoryMaster WHERE CategoryName = @CategoryName
	END
	ELSE
	BEGIN
		RAISERROR('Category name should not be blank', 16, 1)
	END

	IF ISNULL(RTRIM(LTRIM(@ProductName)), '') <> ''
	BEGIN
		SELECT @ProductId = ProductId FROM prMstproductMaster WHERE ProductName = @ProductName
	END
	ELSE
	BEGIN
		RAISERROR('Product name should not be blank', 16, 1)
	END

	IF(ISNULL(RTRIM(LTRIM(@isOvereWrite)), '') <> 'Y')
	BEGIN
		IF(ISNULL(RTRIM(LTRIM(@ProductId)), 0) > 0)
		BEGIN
			SELECT @ErrorMessage = @ProductName + ' is already avaiable as product in the system'
			RAISERROR(@ErrorMessage, 16, 1)
		END
	END

	IF(ISNULL(RTRIM(LTRIM(@CategoryName)), '') <> '')
	BEGIN
		SELECT @CategoryId = CategoryId FROM prMstCategoryMaster WHERE CategoryName = @CategoryName
	END
	ELSE
	BEGIN
		RAISERROR('Category name should not be blank', 16, 1)
	END
	
	IF(ISNULL(RTRIM(LTRIM(@CategoryId)), 0) < 1)
	BEGIN
		SELECT @ErrorMessage = @CategoryName + ' is not avaiable as a Category'
		RAISERROR(@ErrorMessage, 16, 1)
	END
	
	IF(ISNULL(RTRIM(LTRIM(@CountryName)), '') <> '')
	BEGIN
		SELECT @CountryID = id FROM mstCountries WHERE name = @CountryName
	END
	ELSE
	BEGIN
		RAISERROR('Country should not be blank', 16, 1)
	END

	IF(ISNULL(RTRIM(LTRIM(@CountryID)), 0) < 1)
	BEGIN
		SELECT @ErrorMessage = @CountryName + ' is not avaiable as a Country'
		RAISERROR(@ErrorMessage, 16, 1)
	END

	IF isnull(@ProductId, 0) < 1
	BEGIN 
		SELECT @NewDatauniqueID = 1
			
		INSERT INTO PRMSTProductMaster (ProductName, CategoryId, CountryID, 
			DatauniqueID, ActivityType, IsActive, LastModifiedOn, LastModifiedBy)
		VALUES(@ProductName, @CategoryId, @CountryID, 
			@NewDatauniqueID, 'A', @IsActive, GETDATE(), @UserID)
	END
	ELSE
	BEGIN 
		SELECT @OldDatauniqueID = DatauniqueID FROM PRMSTProductMaster WHERE ProductId = @ProductId
					
		INSERT INTO PRMSTProductMasterAudit (ProductId, ProductName, CategoryId, CountryID, 
				DatauniqueID, ActivityType, IsActive, LastModifiedOn, LastModifiedBy,
				AuditOperationOn, AuditOperationBy)
		SELECT ProductId, ProductName, CategoryId, CountryID,
				DatauniqueID, ActivityType, IsActive, LastModifiedOn, LastModifiedBy,
				GETDATE(), @UserID 
		FROM PRMSTProductMaster
		WHERE ProductId = @ProductId

		SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM PRMSTProductMaster WHERE ProductId = @ProductId
		
		UPDATE PRMSTProductMaster SET 
			ProductName = @ProductName, 
			DatauniqueID = @NewDatauniqueID, 
			CategoryId = @CategoryId,
			CountryID = @CountryID,
			ActivityType = 'M', 
			IsActive = @IsActive, 
			LastModifiedOn = GETDATE(), 
			LastModifiedBy = @UserID
		WHERE ProductId = @ProductId
	END 
END



GO
